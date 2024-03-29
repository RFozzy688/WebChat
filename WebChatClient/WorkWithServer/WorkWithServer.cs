﻿using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace WebChatClient
{
    // отправка/получение сообщений с сервера
    public static class WorkWithServer
    {
        // делегат для события
        public delegate void ResponceDelegat(string str);

        // событие ответа сервера
        public static event ResponceDelegat ResponceEvent = null;

        // сокет отправки сообщения
        static Socket _socketSendMessage;

        // сокет получения сообщения
        static Socket _socketReceiveMessage;

        // удаленная точка сервера
        static EndPoint _remoteEndPoint;

        // локальная точка клиента
        static EndPoint _localEndPoint;

        // IP адрес сервера
        static string? _ipRemoteAddress/* = "127.0.0.1"*/;
        static string _ipLocalAddress = "127.0.0.1";

        // порт на который отправляются сообщения
        static int _remotePort = 8080;

        // порт на котором принимаются сообщения
        static int _localPort = 8081;

        // размер буфера
        const int _bufSize = 65536;

        static WorkWithServer()
        {
            // получить данные с файла конфигурации
            var config = JsonSerializer.Deserialize<JsonNode>(File.ReadAllText("appconfig.json"));

            // ip-адрес сервера
            _ipRemoteAddress = config?["server"]?["ipaddress"]?.ToString();
        }

        // создать сокеты
        public static void ConnectTo()
        {
            try
            {
                // инициализировать сокет отправки сообщения
                _socketSendMessage = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                // инициализировать сокет получения сообщения
                _socketReceiveMessage = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                // удаленная точка сервера
                _remoteEndPoint = new IPEndPoint(IPAddress.Parse(_ipRemoteAddress!), _remotePort);
                // локальная точка клиента
                _localEndPoint = new IPEndPoint(IPAddress.Any, _localPort);

                // связать сокет получения сообщения с локальной точко клиента
                _socketReceiveMessage.Bind(_localEndPoint);
            }
            catch (SocketException ex)
            {

                throw;
            }
        }

        // постоянно слушать входной порт
        public static async Task ReceiveMessageAsync()
        {
            while (true)
            {
                byte[] data = new byte[_bufSize];
                // получить сообщение
                var result = await _socketReceiveMessage.ReceiveFromAsync(data, SocketFlags.None, _localEndPoint);
                // перевести в строку
                string message = Encoding.UTF8.GetString(data, 0, result.ReceivedBytes);

                // создать событие о получении сообщения
                Responce(message);
            }
        }

        // отправить данные на сервер
        public static async Task SendMessageAsync(string str)
        {
            string message = str;
            byte[] data = new byte[_bufSize];
            // перевести в байты
            data = Encoding.UTF8.GetBytes(message);
            // отправить
            await _socketSendMessage.SendToAsync(data, SocketFlags.None, _remoteEndPoint);
        }

        // отправить данные на сервер
        public static void SendMessage(string str)
        {
            string message = str;
            byte[] data = new byte[_bufSize];
            // перевести в байты
            data = Encoding.UTF8.GetBytes(message);
            // отправить
            _socketSendMessage.SendTo(data, data.Length, SocketFlags.None, _remoteEndPoint);
        }

        // оповестить всех подписчиков о событии
        static void Responce(string str)
        {
            if (ResponceEvent != null)
            {
                ResponceEvent(str);
            }
        }
    }
}
