using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;

namespace WebChatServer
{
    public class Program
    {
        // адрес сервера в сети
        string _ipAddress = "127.0.0.1";

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

            //User user1 = new User()
            //{
            //    Id = "qwerty",
            //    Nickname = "nick 1",
            //    Email = "email1@gmail.com",
            //    Password = "password1",
            //    IsVerifiedEmail = true,
            //    VerificationCode = "asdfg"
            //};
            //User user2 = new User()
            //{
            //    Id = "asdfgh",
            //    Nickname = "nick 1",
            //    Email = "email2@gmail.com",
            //    Password = "password1",
            //    IsVerifiedEmail = true,
            //    VerificationCode = "asdfg"
            //};

            //_context.Users.AddRange(new[] { user1, user2 });
            //_context.SaveChanges();
        }

        // метод запускающий сервер
        async Task StartServer()
        {
            // создаем сокет сервера
            using Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            // создать локальную точку подключения
            EndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(_ipAddress), _port);
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
                    byte[] data = new byte[256];

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

            byte[] bytes = new byte[256];

            // создаем объект для работы с бд
            WorkWithDB workWithDB = new WorkWithDB(_context);

            if (workWithDB.IsCheckVerifyCode(verificationEmail.Email, verificationEmail.Code))
            {
                bytes = Encoding.UTF8.GetBytes("true");
            }
            else
            {
                bytes = Encoding.UTF8.GetBytes("Неверный код верификации");
            }

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

            byte[] bytes = new byte[256];

            // создаем объект для работы с бд
            WorkWithDB workWithDB = new WorkWithDB(_context);

            // проверяем почту в бд для регистрации
            if (workWithDB.IsCheckEmailInDB(dataRegistration.Email))
            {
                // генирация кода верификации
                //string code = GenerationVerificationCode();

                // добавить пользователя в бд
                //workWithDB.AddUser(dataRegistration, code);

                // отправка письма на почту для верификации
                //VerificationEmail verification = new VerificationEmail();
                //verification.SendVerificationCode(dataRegistration.Email, code);

                bytes = Encoding.UTF8.GetBytes("true");
            }
            else
            {
                bytes = Encoding.UTF8.GetBytes("Пользователь с такой почтой уже зарегистрирован");
            }

            await Task.Delay(3000);
            // удаленная точка получателя
            IPEndPoint remoteEndPoint = new IPEndPoint(iPAddress, _remotePortMessage);
            // отправка данных о авторизации
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

            byte[] bytes = new byte[256];

            // временная проверка. Реальные данные будут сверяться с данными БД
            if (dataAuthorization.Email.CompareTo(UserData.Email) == 0 && 
                dataAuthorization.Password.CompareTo(UserData.Password) == 0)
            {
                bytes = Encoding.UTF8.GetBytes("true");
            }
            else
            {
                bytes = Encoding.UTF8.GetBytes("false");
            }

            await Task.Delay(3000);
            // удаленная точка получателя
            IPEndPoint remoteEndPoint = new IPEndPoint(iPAddress, _remotePortMessage);
            // отправка данных о авторизации
            await socket.SendToAsync(bytes, SocketFlags.None, remoteEndPoint);
        }

        // сгенирировать ключ верификации
        string GenerationVerificationCode()
        {
            return Guid.NewGuid().ToString().Remove(8);
        }
    }

}