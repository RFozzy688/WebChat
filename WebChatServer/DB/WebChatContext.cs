using Microsoft.EntityFrameworkCore;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace WebChatServer
{
    // подключение к бд
    public class WebChatContext : DbContext
    {
        // таблица пользователей
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // конфигурация подключения
            var config = JsonSerializer.Deserialize<JsonNode>(File.ReadAllText(@"..\..\..\appconfig.json"));

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
            // если истина то зарегистрированной почты в базе нет
            if (_db.Users.Where(o => o.Email == email).SingleOrDefault() == null)
            {
                return true;
            }
            else 
            {
                return false; 
            }
        }

        // добавить пользователя в базу
        public void AddUser(UserRegistration userData, string code)
        {
            User user = new User()
            {
                Id = Guid.NewGuid().ToString(),
                Nickname = userData.Nickname,
                Email = userData.Email,
                Password = userData.Password,
                IsVerifiedEmail = false,
                VerificationCode = code
            };

            _db.Users.Add(user);
            _db.SaveChanges();
        }
    }
}
