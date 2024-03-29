﻿using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks.Dataflow;

namespace WebChatServer
{
    public class Program
    {
        // адрес сервера в сети
        string? _ipAddress /*= "127.0.0.1"*/;
        // размер буфера
        const int _bufSize = 65536;
        // порт который слушает сервер
        const int _port = 8080;
        // удаленный порт на котором принимаются сообщения
        const int _remotePortMessage = 8081;

        readonly WebChatContext _context;

        static async Task Main(string[] args)
        {
            Program program = new Program();

            await program.StartServer();
        }

        public Program()
        {
            // создаем подключении к бд
            _context = new WebChatContext();

            // получить ip-адрес с файла конфигурации
            GetIPAddress();
        }

        // метод запускающий сервер
        async Task StartServer()
        {
            // создаем сокет сервера
            using Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            // создать локальную точку подключения
            EndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(_ipAddress!), _port);
            // связать создать локальную точку с сокетом
            server.Bind(localEndPoint);

            Console.WriteLine("UDP-server started...");

            // удаленная точка, с которой приходят сообщения
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);

            while (true)
            {
                try
                {
                    string message;
                    byte[] data = new byte[_bufSize];

                    // начинаем слушать входящий порт
                    var result = await server.ReceiveFromAsync(data, SocketFlags.None, remoteEndPoint);
                    // перекодируем данные
                    message = Encoding.UTF8.GetString(data, 0, result.ReceivedBytes);

                    DataPackage? package = new DataPackage();
                    package = JsonSerializer.Deserialize<DataPackage>(message);

                    if (package == null)
                    {
                        Console.WriteLine("Error deserialize!!!");
                        continue;
                    }

                    // преобразовать к классу наследника
                    remoteEndPoint = (IPEndPoint)result.RemoteEndPoint;

                    switch (package.Package)
                    {
                        case TypeData.Message:
                            await SendMessageToUser(server, package.StringSerialize);
                            break;
                            // регистрация пользователя
                        case TypeData.Registration:
                            await RegistrationAttempt(server, remoteEndPoint.Address, package.StringSerialize);
                            break;
                            // авторизация пользователя
                        case TypeData.Authorization:
                            await AuthorizationAttempt(server, remoteEndPoint.Address, package.StringSerialize);
                            break;
                            // верификация почты
                        case TypeData.Verification:
                            await VerificationEmailAttempt(server, remoteEndPoint.Address, package.StringSerialize);
                            break;
                        // поиск контакта в бд по почте
                        case TypeData.FindUser:
                            await FindUserAttempt(server, remoteEndPoint.Address, package.StringSerialize);
                            break;
                        // пользователь вышел из приложения
                        case TypeData.Exit:
                            await ExitApp(package.StringSerialize);
                            break;
                        default:
                            break;
                    }

                    Console.WriteLine(result.RemoteEndPoint);
                }
                catch (SocketException ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
        }

        // попытка найти контакт в бд для добавления в список контактов
        async Task FindUserAttempt(Socket socket, IPAddress iPAddress, string stringSerialize)
        {
            // десериализация данных пользователя
            GeneralUserData? addUser = new GeneralUserData();
            addUser = JsonSerializer.Deserialize<GeneralUserData>(stringSerialize);

            // если десериализация прошла не успешно выводим сообщение об ошибке
            // и выходим из регисртрации
            // TODO: реализовать отправку сообщения об ошибке клиенту
            if (addUser == null)
            {
                Console.WriteLine("Error deserialize!!!");
                return;
            }

            byte[] bytes = new byte[_bufSize];

            // создаем объект для работы с бд
            WorkWithDB workWithDB = new WorkWithDB(_context);
            // получаем данные пользователя
            var userData = workWithDB.GetDataUser(addUser.Email);

            if (userData != null)
            {
                bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(userData));
            }
            else
            {
                bytes = Encoding.UTF8.GetBytes("null");
            }

            // удаленная точка получателя
            IPEndPoint remoteEndPoint = new IPEndPoint(iPAddress, _remotePortMessage);
            // отправка данных о авторизации
            await socket.SendToAsync(bytes, SocketFlags.None, remoteEndPoint);
        }

        // попытка верифицировать почту пользователя
        async Task VerificationEmailAttempt(Socket socket, IPAddress iPAddress, string stringSerialize)
        {
            // десериализация данных пользователя
            UserVerificationEmail? verificationEmail = new UserVerificationEmail();
            verificationEmail = JsonSerializer.Deserialize<UserVerificationEmail>(stringSerialize);

            // если десериализация прошла не успешно выводим сообщение об ошибке
            // и выходим из верификации почты
            // TODO: реализовать отправку сообщения об ошибке клиенту
            if (verificationEmail == null)
            {
                Console.WriteLine("Error deserialize!!!");
                return;
            }

            byte[] bytes = new byte[_bufSize];
            // массив строк в который записывается ответ о верификации
            string[] strings = new string[2];
            strings[0] = "verification";

            // создаем объект для работы с бд
            WorkWithDB workWithDB = new WorkWithDB(_context);

            if (workWithDB.IsCheckVerifyCode(verificationEmail.Email, verificationEmail.Code))
            {
                strings[1] = "true";
            }
            else
            {
                strings[1] = "Неверный код верификации";
            }

            bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(strings));

            // удаленная точка получателя
            IPEndPoint remoteEndPoint = new IPEndPoint(iPAddress, _remotePortMessage);
            // отправка данных о авторизации
            await socket.SendToAsync(bytes, SocketFlags.None, remoteEndPoint);
        }

        // попытка регистрации пользователя на сервере
        async Task RegistrationAttempt(Socket socket, IPAddress iPAddress, string stringSerialize)
        {
            // десериализация данных пользователя
            UserRegistration? dataRegistration = new UserRegistration();
            dataRegistration = JsonSerializer.Deserialize<UserRegistration>(stringSerialize);

            // если десериализация прошла не успешно выводим сообщение об ошибке
            // и выходим из регисртрации
            // TODO: реализовать отправку сообщения об ошибке клиенту
            if (dataRegistration == null)
            {
                Console.WriteLine("Error deserialize!!!");
                return;
            }

            byte[] bytes = new byte[_bufSize];

            // создаем объект для работы с бд
            WorkWithDB workWithDB = new WorkWithDB(_context);

            // проверяем почту в бд для регистрации, если истина, то почты в базе нет
            if (!workWithDB.IsCheckEmailInDB(dataRegistration.Email))
            {
                // генирация кода верификации
                string code = GenerationVerificationCode();

                // добавить пользователя в бд
                workWithDB.AddUserToDB(dataRegistration, code, iPAddress);

                // отправка письма на почту для верификации
                VerificationEmail verification = new VerificationEmail();
                verification.SendVerificationCode(dataRegistration.Email, code);

                // получаем данные пользователя
                var userData = workWithDB.GetDataUser(dataRegistration.Email);
                bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(userData));

                workWithDB.SetIsOnline(userData.UserID, true);
            }
            else
            {
                bytes = Encoding.UTF8.GetBytes("Пользователь с такой почтой уже зарегистрирован");
            }

            await Task.Delay(1000);
            // удаленная точка получателя
            IPEndPoint remoteEndPoint = new IPEndPoint(iPAddress, _remotePortMessage);
            // отправка данных о регистрации
            await socket.SendToAsync(bytes, SocketFlags.None, remoteEndPoint);
        }

        // попытка авторизации пользователя на сервере
        async Task AuthorizationAttempt(Socket socket, IPAddress iPAddress, string stringDeserialize)
        {
            // десериализация данных пользователя
            UserAuthorization? dataAuthorization = new UserAuthorization();
            dataAuthorization = JsonSerializer.Deserialize<UserAuthorization>(stringDeserialize);

            // если десериализация прошла не успешно выводим сообщение об ошибке
            // и выходим из авторизации
            // TODO: реализовать отправку сообщения об ошибке клиенту
            if (dataAuthorization == null)
            {
                Console.WriteLine("Error deserialize!!!");
                return;
            }

            byte[] bytes = new byte[_bufSize];
            bool flag = false;
            string userId = string.Empty;

            // создаем объект для работы с бд
            WorkWithDB workWithDB = new WorkWithDB(_context);

            // проверяем пользователя в БД
            if (workWithDB.IsCheckUserInDB(dataAuthorization.Email, dataAuthorization.Password))
            {
                // получаем данные пользователя
                var userData = workWithDB.GetDataUser(dataAuthorization.Email);
                bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(userData));

                // сверяем ip-адрес с которого прошла авторизация и ip-адрес в бд
                // если не совпадают, то обновляем ip-адрес
                workWithDB.UpdateIPAddress(dataAuthorization.Email, iPAddress);

                workWithDB.SetIsOnline(userData.UserID, true);

                flag = true;
                userId = userData.UserID;
            }
            else
            {
                bytes = Encoding.UTF8.GetBytes("null");
            }

            await Task.Delay(1000);
            // удаленная точка получателя
            IPEndPoint remoteEndPoint = new IPEndPoint(iPAddress, _remotePortMessage);
            // отправка данных о авторизации
            await socket.SendToAsync(bytes, SocketFlags.None, remoteEndPoint);

            if (flag)
            {
                await Task.Delay(5000);
                await SendWaitingMessage(socket, iPAddress, workWithDB.FindUnsentMessages(userId));
            }
        }

        // сгенирировать ключ верификации
        string GenerationVerificationCode()
        {
            return Guid.NewGuid().ToString().Remove(8);
        }

        // получить ip-адрес с файла конфигурации
        void GetIPAddress()
        {
            // получить данные с файла конфигурации
            var config = JsonSerializer.Deserialize<JsonNode>(File.ReadAllText("appconfig.json"));

            // ip-адрес сервера
            _ipAddress = config?["server"]?["ipaddress"]?.ToString();
        }

        // отправить сообщение пользователю
        async Task SendMessageToUser(Socket socket, string stringDeserialize)
        {
            // десериализация входящего сообщения
            OutgoingMessage? messageOut = new();
            messageOut = JsonSerializer.Deserialize<OutgoingMessage>(stringDeserialize);

            WorkWithDB workWithDB = new WorkWithDB(_context);

            if (messageOut != null)
            {
                if (workWithDB.GetIsOnline(messageOut.RecipientId))
                {
                    byte[] bytes = new byte[_bufSize];

                    // находим ip-адрес получателя в бд
                    string ipAddress = workWithDB.GetIPAddress(messageOut.RecipientId);

                    // создать сообщение для получателя
                    IncomingMessage messageIn = new();
                    messageIn.UserId = messageOut.UserId;
                    messageIn.Message = messageOut.Message;
                    messageIn.MessageSentTime = messageOut.MessageSentTime;

                    bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(messageIn));

                    // удаленная точка получателя
                    IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress), _remotePortMessage);
                    // отправка сообщения получателю
                    await socket.SendToAsync(bytes, SocketFlags.None, remoteEndPoint);
                }
                else
                {
                    // если получатель не находится в сети, то временно сохраняем сообщение в бд
                    workWithDB.SaveMessageToDB(messageOut);
                }
            }
        }

        // пользователь выходит из приложения
        async Task ExitApp(string stringDeserialize)
        {
            await Task.Delay(1);

            // создаем объект для работы с бд
            WorkWithDB workWithDB = new WorkWithDB(_context);

            workWithDB.SetIsOnline(stringDeserialize, false);
        }

        async Task SendWaitingMessage(Socket socket, IPAddress iPAddress, List<IncomingMessage> messages)
        {
            foreach (IncomingMessage message in messages)
            {
                byte[] bytes = new byte[_bufSize];

                bytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

                // удаленная точка получателя
                IPEndPoint remoteEndPoint = new IPEndPoint(iPAddress, _remotePortMessage);
                // отправка сообщения получателю
                await socket.SendToAsync(bytes, SocketFlags.None, remoteEndPoint);
            }
        }
    }

}