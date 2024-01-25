using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace WebChatServer
{
    public class Program
    {
        // адрес сервера в сети
        string _ipAddress = "127.0.0.1";

        // порт который слушает сервер
        int _port = 8080;

        static async Task Main(string[] args)
        {
            Program program = new Program();

            await program.StartServer();
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
            //IPEndPoint remoteSendEndPoint = new IPEndPoint(IPAddress.Any, 8081);

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

                    UserAuthorization userAuthorization = new UserAuthorization();

                    if (userAuthorization.IsAuthorization(message))
                    {
                        message = message.Remove(0);
                        message = "good";
                    }
                    else
                    {
                        message = message.Remove(0);
                        message = "bad";
                    }

                    await Task.Delay(3000);

                    remoteEndPoint = (IPEndPoint)result.RemoteEndPoint;
                    remoteEndPoint.Port = 8081;

                    data = Encoding.UTF8.GetBytes(message);
                    await server.SendToAsync(data, SocketFlags.None, remoteEndPoint);

                    Console.WriteLine(result.RemoteEndPoint + ": " + message);
                }
                catch (SocketException ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }
        }
    }

    public class UserAuthorization
    {
        string _email = "rf";
        string _password = "q";
        User _user;

        public UserAuthorization()
        {
            _user = new User();
        }

        public bool IsAuthorization(string message)
        {
            _user = JsonSerializer.Deserialize<User>(message);

            if (_email.CompareTo(_user.Email) == 0 && _password.CompareTo(_user.Password) == 0)
            {
                return true;
            }
            else 
            { 
                return false;
            }
        }
    }

    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}