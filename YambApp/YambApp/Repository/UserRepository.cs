using BCrypt.Net;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YambApp.Model;

namespace YambApp.Repository
{
    public class UserRepository:IUserRepository
    {



        private readonly string _connectionString;

        public UserRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["PostgresConnection"].ConnectionString;
        }

        public void Create(User user)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "INSERT INTO Users (Username, Email, Password) VALUES (@username, @email,@password)";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("username", user.Username);
                    command.Parameters.AddWithValue("email", user.Email);
                    command.Parameters.AddWithValue("password", user.Password);
                    command.ExecuteNonQuery();
                }
            }
        }

        public List<User> GetAll()
        {
            var users = new List<User>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT  Username, Email FROM Users";

                using (var command = new NpgsqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        users.Add(new User
                        {

                            Username = reader.GetString(reader.GetOrdinal("username")),
                            Email = reader.GetString(reader.GetOrdinal("email")),
                         

                        });
                    }
                }
            }
            return users;
        }

        public bool UsernameExist(string username)
        {
            var users = new List<User>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT  Username,Email FROM Users WHERE Username=@username";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("username", username);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new User
                            {

                                Username = reader.GetString(reader.GetOrdinal("username")),
                                Email = reader.GetString(reader.GetOrdinal("email")),
                               

                            });
                        }
                    }
                }
            }
            return users.Any();
        }

        public bool EmailExist(string email)
        {
            var users = new List<User>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT  Username,Email FROM Users WHERE Email=@email";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("email", email);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new User
                            {

                                Username = reader.GetString(reader.GetOrdinal("username")),
                                Email = reader.GetString(reader.GetOrdinal("email")),
                              

                            });
                        }
                    }
                }
            }
            return users.Any();
        }

        public bool Authenticate(string usernameEmail, string password) 
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT Password FROM Users WHERE Email=@usernameEmail OR Username=@usernameEmail; ";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("usernameEmail", usernameEmail);

                    var result = command.ExecuteScalar();

                    if (result != null)
                    {
                       return VerifyPassword(result.ToString(), password);

                    }
                  


                }
            }
            return false;
        }

        public bool VerifyPassword(string hashedPassword,string plainPassword) 
        {
            return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
        }
    }
        
}
