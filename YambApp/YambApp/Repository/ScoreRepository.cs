using Npgsql;
using NUnit.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using YambApp.FileHandler;
using YambApp.Model;
using YambApp.Service;

namespace YambApp.Repository
{
    public class ScoreRepository:IScoreRepository
    {
       // IPlayerFile playerFile;

       //public  PlayerRepository()
       // {
       //     playerFile = new PlayerFile();
       // }


       // public List<Player> GetAll()
       // {
       //     return playerFile.Read();
       // }

       // public void Create(Player player)
       // {
       //     List<Player> players = GetAll();
       //     if (players == null)
       //     {
       //         players = new List<Player>();
       //     }
       //     players.Add(player);
       //     playerFile.Save(players);
       // }


       // {
        private readonly string _connectionString;

        public ScoreRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["PostgresConnection"].ConnectionString;
        }

        public void Create(ScoreTable score)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "INSERT INTO Scores (UserName, Score, GameType) VALUES (@username, @score, @gameType)";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("username", score.Username);
                    command.Parameters.AddWithValue("score", score.Scores);
                    command.Parameters.AddWithValue("gameType", score.GameType.ToString());
                    command.ExecuteNonQuery();
                }
            }
        }

         public List<ScoreTable> GetAllByType(int gameType)
         {
            var scores = new List<ScoreTable>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT  UserName, Score FROM Scores WHERE gameType=@gameType ORDER BY Score DESC";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("gameType",gameType.ToString());
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            scores.Add(new ScoreTable
                            {

                                Username = reader.GetString(reader.GetOrdinal("username")),
                                Scores = reader.GetInt32(reader.GetOrdinal("score")),


                            });
                        }
                    }
                }
            }
            return scores;
         }
        public List<ScoreTable> GetAllByUser(string username,int gameType)
        {
            var scores = new List<ScoreTable>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT  UserName, Score FROM Scores WHERE gameType=@gameType AND username=@username ORDER BY Score DESC";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("username", username);
                    command.Parameters.AddWithValue("gameType", gameType.ToString());
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            scores.Add(new ScoreTable
                            {

                                Username = reader.GetString(reader.GetOrdinal("username")),
                                Scores = reader.GetInt32(reader.GetOrdinal("score")),


                            });
                        }
                    }
                }
            }
            return scores;
        }


        public int GetAverageScoreByUser(string username,int gameType)
        {
            int score = 0;
            int counter = 0;
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT  UserName, Score FROM Scores WHERE UserName=@username AND gameType=@gameType";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("username", username);
                    command.Parameters.AddWithValue("gameType", gameType.ToString());
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {


                            score += reader.GetInt32(reader.GetOrdinal("score"));
                            ++counter;


                        }
                    }

                }
                if (counter != 0)
                {
                    return score / counter;
                }
                else
                    return 0;
               
            }
        }

        public int GetBestScoreByUser(string username, int gameType)
        {
            int score = 0;    
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT  MAX(Score) as maxScore FROM Scores WHERE UserName=@username AND gameType=@gameType";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("username", username);
                    command.Parameters.AddWithValue("gameType", gameType.ToString());
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {


                            if (!reader.IsDBNull(reader.GetOrdinal("maxScore")))
                            {
                                score = reader.GetInt32(reader.GetOrdinal("maxScore"));
                            }




                        }
                    }

                }
                return score;
            }
        }

        public int GetAverageScore(int gameType)
        {
            int score = 0;
            int counter = 0;
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT  UserName, Score FROM Scores WHERE gameType=@gameType";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("gameType", gameType.ToString());
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {


                            score += reader.GetInt32(reader.GetOrdinal("score"));
                            ++counter;


                        }
                    }

                }
                if (counter != 0)
                {
                    return score / counter;
                }
                else
                    return 0;
            }
        }

        public int GetBestScore(int gameType)
        {
            int score = 0;
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT  MAX(Score) as maxScore FROM Scores WHERE gameType=@gameType";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("gameType", gameType.ToString());
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {


                            if (!reader.IsDBNull(reader.GetOrdinal("maxScore")))
                            {
                                score = reader.GetInt32(reader.GetOrdinal("maxScore"));
                            }


                        }
                    }

                }
                return score;
            }
        }
    }










    
}
