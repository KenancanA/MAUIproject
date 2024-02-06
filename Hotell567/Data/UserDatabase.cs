using Hotell567.Logic;
using Hotell567.Models;
using System.Data.SQLite;
using System.Diagnostics;

namespace Hotell567.Data
{
    public class UserDatabase
    {
        public UserDatabase()
        {
            Debug.WriteLine("UserDatabase connector created");
        }

        
        public List<User> ListUsers()
        {
            using (SQLiteConnection _connection = new SQLiteConnection(AppManager.connectionString))
            {
                _connection.Open();
                var command = new SQLiteCommand("SELECT * FROM Users", _connection);
                var reader = command.ExecuteReader();
                var users = new List<User>();

                while (reader.Read())
                {
                    users.Add(new User
                    {
                        user_id = reader["user_id"].GetHashCode(),
                        user_type = reader["user_type"].GetHashCode(),
                        first_name = reader["first_name"].ToString(),
                        last_name = reader["last_name"].ToString(),
                        username = reader["username"].ToString(),
                        password = reader["password"].ToString(),
                        email = reader["email"].ToString(),
                        phone_number = reader["phone_number"].GetHashCode(),
                        date_of_birth = reader["date_of_birth"].ToString(),
                        address_line_1 = reader["address_line_1"].ToString(),
                        address_line_2 = reader["address_line_2"].ToString(),
                        city = reader["city"].ToString(),
                        state = reader["state"].ToString(),
                        postal_code = reader["postal_code"].GetHashCode(),
                        country = reader["country"].ToString()
                    });
                }
                return users;
            }
        }

        public User GetUserById(int user_id)
        {
            using (SQLiteConnection _connection = new SQLiteConnection(AppManager.connectionString))
            {
                _connection.Open();
                User u = new User();

                var command = new SQLiteCommand("SELECT * FROM Users WHERE user_id = @user_id", _connection);
                command.Parameters.AddWithValue("@user_id", user_id);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    u.user_id = reader["user_id"].GetHashCode();
                    u.user_type = reader["user_type"].GetHashCode();
                    u.first_name = reader["first_name"].ToString();
                    u.last_name = reader["last_name"].ToString();
                    u.username = reader["username"].ToString();
                    u.password = reader["password"].ToString();
                    u.email = reader["email"].ToString();

                    if (reader["phone_number"] != DBNull.Value)
                    {
                        u.phone_number = reader["phone_number"].GetHashCode();
                    }
                    else
                    {
                        u.phone_number = 0;
                    }

                    u.date_of_birth = reader["date_of_birth"].ToString();
                    u.address_line_1 = reader["address_line_1"].ToString();
                    u.address_line_2 = reader["address_line_2"].ToString();
                    u.city = reader["city"].ToString();
                    u.state = reader["state"].ToString();

                    if (reader["postal_code"] != DBNull.Value)
                    {
                        u.postal_code = reader["postal_code"].GetHashCode();
                    }
                    else
                    {
                        u.postal_code = 0;
                    }

                    u.country = reader["country"].ToString();
                }
                return u;
            }
        }

        public User CheckDBForUser(string username)
        {
            using (SQLiteConnection _connection = new SQLiteConnection(AppManager.connectionString))
            {
                _connection.Open();
                User u = new User();

                var command = new SQLiteCommand("SELECT * FROM Users WHERE username = @username", _connection);
                command.Parameters.AddWithValue("@username", username);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    u.username = reader["username"].ToString();
                    u.password = reader["password"].ToString();
                    u.user_id = reader["user_id"].GetHashCode();
                }
                return u;
            }
        }

        public User CheckDBForUserType(string username)
        {
            using (SQLiteConnection _connection = new SQLiteConnection(AppManager.connectionString))
            {
                _connection.Open();
                User u = new User();

                var command = new SQLiteCommand("SELECT * FROM Users WHERE username = @username", _connection);
                command.Parameters.AddWithValue("@username", username);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    u.username = reader["username"].ToString();
                    u.user_type = reader["user_type"].GetHashCode();
                }
                return u;
            }
        }

        public void AddUser(User u)
        {
            using (SQLiteConnection _connection = new SQLiteConnection(AppManager.connectionString))
            {
                _connection.Open();
                var command = new SQLiteCommand("INSERT INTO Users (username, password, email) VALUES (@username, @password, @email)", _connection);
                command.Parameters.AddWithValue("@username", u.username);
                command.Parameters.AddWithValue("@password", u.password);
                command.Parameters.AddWithValue("@email", u.email);
                command.ExecuteNonQuery();
            }
        }

        
        public void EditUser(User u)
        {
            using (SQLiteConnection _connection = new SQLiteConnection(AppManager.connectionString))
            {
                _connection.Open();
                var command = new SQLiteCommand("UPDATE Users SET first_name = @first_name, last_name = @last_name, username = @username, password = @password, email = @email, phone_number = @phone_number, date_of_birth = @date_of_birth, address_line_1 = @address_line_1, address_line_2 = @address_line_2, city = @city, state = @state, postal_code = @postal_code, country = @country WHERE user_id = @user_id", _connection);
                command.Parameters.AddWithValue("@user_id", u.user_id);
                command.Parameters.AddWithValue("@first_name", u.first_name);
                command.Parameters.AddWithValue("@last_name", u.last_name);
                command.Parameters.AddWithValue("@username", u.username);
                command.Parameters.AddWithValue("@password", u.password);
                command.Parameters.AddWithValue("@email", u.email);
                command.Parameters.AddWithValue("@phone_number", u.phone_number);
                command.Parameters.AddWithValue("@date_of_birth", u.date_of_birth);
                command.Parameters.AddWithValue("@address_line_1", u.address_line_1);
                command.Parameters.AddWithValue("@address_line_2", u.address_line_2);
                command.Parameters.AddWithValue("@city", u.city);
                command.Parameters.AddWithValue("@state", u.state);
                command.Parameters.AddWithValue("@postal_code", u.postal_code);
                command.Parameters.AddWithValue("@country", u.country);
                command.ExecuteNonQuery();
            }
        }
    }
}
