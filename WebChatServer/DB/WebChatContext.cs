using Microsoft.EntityFrameworkCore;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Net;

namespace WebChatServer
{
    // подключение к бд
    public class WebChatContext : DbContext
    {
        // таблица пользователей
        public DbSet<User> Users { get; set; }
        public DbSet<WaitingMessage> WaitingMessages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // конфигурация подключения
            var config = JsonSerializer.Deserialize<JsonNode>(File.ReadAllText("appconfig.json"));

            // подключение к бд
            optionsBuilder.UseSqlServer(config?["database"]?["webchat"]?["connectionString"]?.ToString());
        }

        public WebChatContext()
        {
            Database.EnsureCreated();
        }
    }

    public class User
    {
        // id пользователя
        public string Id { get; set; } = null!;

        // Nickname пользователя
        public string Nickname { get; set; } = null!;

        // Email для регистрации
        public string Email { get; set; } = null!;

        // пароль аккаунта
        public string Password { get; set; } = null!;

        // true если Email верифицирован
        public bool IsVerifiedEmail { get; set; } = false;

        // код верификации
        public string VerificationCode { get; set; } = null!;

        // ip-адрес пользователя
        public string? IpAddress { get; set; } = null;

        // находится ли пользователь в сети
        public bool IsOnline { get; set;} = false;

        public List<WaitingMessage> WaitingMessages { get; set; }

        public User()
        {
            WaitingMessages = new List<WaitingMessage>();
        }
    }

    public class WaitingMessage
    {
        public int Id { get; set; }
        // внешний ключ с User
        public string UserId { get; set; }
        // навигационное поле
        public virtual User User { get; set; }
        // кто отправил сообщение
        public string Sender { get; set; }
        // сообщение
        public string Message { get; set; }
        // дата отправки
        public DateTimeOffset DepartureDate { get; set; }
    }

    public class WorkWithDB
    {
        // ссылка на контекст бд
        WebChatContext _db;

        public WorkWithDB(WebChatContext context)
        {
            _db = context;
        }

        // проверка наличия почты в бд
        public bool IsCheckEmailInDB(string email)
        {
            // если истина, то почта зарегистрированна в базе
            if (_db.Users.Where(o => o.Email == email).SingleOrDefault() != null)
            {
                return true;
            }
            else 
            {
                return false; 
            }
        }

        // добавить пользователя в базу
        public void AddUserToDB(UserRegistration userData, string code, IPAddress iPAddress)
        {
            // создать пользователя
            User user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Nickname = userData.Nickname,
                Email = userData.Email,
                Password = userData.Password,
                IsVerifiedEmail = false,
                VerificationCode = code,
                IpAddress = iPAddress.ToString(),
            };

            // отслеживать сущность
            _db.Users.Add(user);
            // сохранить в базу
            _db.SaveChanges();
        }

        // сверяем присланный код с бд
        public bool IsCheckVerifyCode(string email, string code)
        {
            // находим пользователя в бд
            var user = _db.Users.Where(o => o.Email == email).SingleOrDefault();

            // если истина
            if (user.VerificationCode.CompareTo(code) == 0)
            {
                // отмачаем в бд
                user.IsVerifiedEmail = true;
                _db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        // ищем пользователя в бд
        public bool IsCheckUserInDB(string email, string password)
        {
            // находим пользователя в бд
            var user = _db.Users.Where(o => o.Email == email).SingleOrDefault();

            // если истино, то авторизируем пользователя
            if (user != null && user.Email.CompareTo(email) == 0 && 
                user.Password.CompareTo(password) == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // получить данные пользователя для добавления в контакты
        public GeneralUserData GetDataUser(string email)
        {
            // находим пользователя в бд
            var user = _db.Users.Where(o => o.Email == email).SingleOrDefault();

            // истина если пользователь существует
            if (user != null)
            {
                return new()
                {
                    UserID = user.Id,
                    Name = user.Nickname,
                    Email = user.Email
                };
            }

            return null;
        }

        // обновление ip-адреса, если пользователь зашел с другого
        public void UpdateIPAddress(string email, IPAddress iPAddress)
        {
            // находим пользователя в бд
            var user = _db.Users.Where(o => o.Email == email).SingleOrDefault();

            // если ip-адреса не совпадают
            if (user != null && user.IpAddress?.CompareTo(iPAddress.ToString()) != 0)
            {
                // то обновляем ip-адрес
                user.IpAddress = iPAddress.ToString();
                _db.SaveChanges();
            }
        }

        // возращает ip-адрес
        public string GetIPAddress(string userId)
        {
            string? ipAddress = _db.Users.Where(o => o.Id == userId).Select(o => o.IpAddress).FirstOrDefault();

            return ipAddress != null ? ipAddress : string.Empty;
        }

        // узнать находится ли пользователь в сети
        public bool GetIsOnline(string userId)
        {
            bool online = _db.Users.Where(o => o.Id == userId).Select(o => o.IsOnline).FirstOrDefault();

            return online;
        }

        // снять/установить пользователь онлайн
        public void SetIsOnline(string userId, bool flag)
        {
            var user = _db.Users.Where(o => o.Id == userId).FirstOrDefault();

            if (user != null)
            {
                user.IsOnline = flag;
                _db.SaveChanges();
            }
        }

        // сохранение сообщения в дб
        public void SaveMessageToDB(OutgoingMessage outgoingMessage)
        {
            WaitingMessage waitingMessage = new WaitingMessage()
            {
                UserId = _db.Users.Where(o => o.Id == outgoingMessage.RecipientId).First().Id,
                Message = outgoingMessage.Message,
                Sender = outgoingMessage.UserId,
                DepartureDate = outgoingMessage.MessageSentTime
            };

            _db.WaitingMessages.Add(waitingMessage);
            _db.SaveChanges();
        }

        public List<IncomingMessage> FindUnsentMessages(string userId)
        {
            var result = _db.WaitingMessages.Where(o => o.UserId == userId).ToList();

            List<IncomingMessage> messages = new List<IncomingMessage>();

            foreach (var message in result)
            {
                messages.Add(new IncomingMessage
                {
                    UserId = message.Sender,
                    Message = message.Message,
                    MessageSentTime = message.DepartureDate
                });

                _db.WaitingMessages.Remove(message);
            }

            _db.SaveChanges();

            return messages;
        }
    }
}
