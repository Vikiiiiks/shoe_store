using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using shoe_store.Models;

namespace shoe_store.Data
{
    public static class UserRepository
        {
            private const string AuthenticateSql = @"
SELECT u.user_id, u.last_name, u.first_name, u.patronymic, r.role_name
FROM app_user u
JOIN user_role r ON r.user_role_id = u.user_role_id
WHERE u.login = @login AND u.password = @password";
            // Возвращает пользователя или null, если логин или пароль неверны
            public static AppUser Authenticate(string login, string password)
            {
                using (NpgsqlConnection connection = Database.CreateConnection())
                {
                    connection.Open();
                    using (NpgsqlCommand command = new NpgsqlCommand(AuthenticateSql, connection))
                    {
                        command.Parameters.AddWithValue("login", login);
                        command.Parameters.AddWithValue("password", password);
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.Read())
                            {
                                return null;
                            }
                            AppUser user = new AppUser
                            {
                                UserId = reader.GetInt32(0),
                                LastName = reader.GetString(1),
                                FirstName = reader.GetString(2),
                                Patronymic = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                                RoleName = reader.GetString(4)
                            }; return user;
                    }
                }
            }
        }
    }
}