using System;
using System.Data;
using System.Data.SqlClient;
using GameManagerApp.IRepository;
using System.Net;
using GameManagerApp.Models;
using Microsoft.EntityFrameworkCore;

namespace GameManagerApp.Repository
{
    public class GameInfoRepository : RepositoryBase, IGameInfoRepository
    {


        public async Task UpdateRunningTimeAsync(string gameFilePath, string newRunningTimeString)
        {
            using (var connection = GetSqlConnection())
            {
                // 先获取当前的累计运行时间
                var currentGame = await GetByFilePathAsync(gameFilePath);
                var totalRunningTime = TimeSpan.Parse(currentGame.runningtime ?? "00:00:00").Add(TimeSpan.Parse(newRunningTimeString)).ToString();

                // 更新数据库中的运行时间
                var command = new SqlCommand("UPDATE Games SET runningtime = @RunningTime WHERE FilePath = @FilePath", connection);
                command.Parameters.AddWithValue("@RunningTime", totalRunningTime);
                command.Parameters.AddWithValue("@FilePath", gameFilePath);

                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }


        public async Task<GameInfo> GetByFilePathAsync(string filePath)
        {
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand("SELECT * FROM Games WHERE FilePath = @FilePath", connection);
                command.Parameters.AddWithValue("@FilePath", filePath);

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new GameInfo
                        {
                            Id = reader["Id"].ToString(),
                            Name = reader["Name"].ToString(),
                            FilePath = reader["FilePath"].ToString(),
                            runningtime = reader.IsDBNull(reader.GetOrdinal("RunningTime")) ? null : reader["RunningTime"].ToString()
                        };
                    }
                }
            }
            return null; // 没有找到指定FilePath的游戏
        }


        public async Task<IEnumerable<GameInfo>> GetAllAsync()
        {
            using (var connection = GetSqlConnection())
            {
                var command = new SqlCommand("SELECT * FROM Games", connection);
                var gamesList = new List<GameInfo>();

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        var game = new GameInfo
                        {
                            Id = reader["Id"].ToString(),
                            Name = reader["Name"].ToString(),
                            FilePath = reader["FilePath"].ToString(),
                            // 根据实际情况添加其他字段
                        };
                        gamesList.Add(game);
                    }
                }

                return gamesList;
            }
        }


        public async Task<GameInfo> GetByNameAsync(string name)
    {
        try
        {
            using (var connection = GetSqlConnection())
            using (var command = new SqlCommand("SELECT * FROM Games WHERE Name = @Name", connection))
            {
                command.Parameters.AddWithValue("@Name", name);

                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var gameInfo = new GameInfo
                        {
                            Id = reader["Id"].ToString(),
                            Name = reader["Name"].ToString(),
                            FilePath = reader["FilePath"].ToString(),
                            // 根据情况添加其他字段
                        };
                        return gameInfo;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log exception details
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }


        public async Task Add(GameInfo gameInfo)
        {
            gameInfo.Id = Guid.NewGuid().ToString();
            //byte[] data = gameInfo.Icon;

            //string binaryString = BitConverter.ToString(data).Replace("-", "");

            //bool[] bits = binaryString.Select(c => c == '1').ToArray();
            try
            {
                using (var connection = GetSqlConnection())



                using (var command = new SqlCommand("INSERT INTO Games (Id,Name, FilePath) VALUES (@Id,@Name, @FilePath)", connection))
                {
                    command.Parameters.AddWithValue("id", gameInfo.Id);
                    command.Parameters.AddWithValue("@Name", gameInfo.Name);
                    command.Parameters.AddWithValue("@FilePath", gameInfo.FilePath);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                // Log exception details
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }

        //public async Task<bool> AuthenticateGameAsync(string name, string filePath)
        //{
        //    try
        //    {
        //        using (var connection = new SqlConnection(_connectionString))
        //        using (var command = new SqlCommand("SELECT COUNT(1) FROM Games WHERE Name = @Name AND FilePath = @FilePath", connection))
        //        {
        //            command.Parameters.AddWithValue("@Name", name);
        //            command.Parameters.AddWithValue("@FilePath", filePath);

        //            await connection.OpenAsync();
        //            var result = await command.ExecuteScalarAsync();
        //            return Convert.ToInt32(result) > 0;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log exception details
        //        Console.WriteLine($"An error occurred: {ex.Message}");
        //        throw;
        //    }
        //}

        public async Task EditAsync(GameInfo gameInfo)
        {
            try
            {
                using (var connection = GetSqlConnection())
                using (var command = new SqlCommand("UPDATE Games SET Name = @Name, FilePath = @FilePath, Icon = @Icon WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", gameInfo.Id);
                    command.Parameters.AddWithValue("@Name", gameInfo.Name);
                    command.Parameters.AddWithValue("@FilePath", gameInfo.FilePath);
                    command.Parameters.AddWithValue("@Icon", gameInfo.Icon);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception ex)
            {
                // Log exception details
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }





        public void edit(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GameInfo> GetAll()
        {
            throw new NotImplementedException();
        }

        public GameInfo GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public void remove(GameInfo gameInfo)
        {
            throw new NotImplementedException();
        }



        //用于gameinfovm中
        public async Task<GameInfo> GetGameInfoAsync(string Filepath)
        {
            using var connection = GetSqlConnection();
            var command = new SqlCommand("SELECT * FROM Games WHERE FilePath = @FilePath", connection);
            command.Parameters.AddWithValue("@FilePath", Filepath);

            await connection.OpenAsync();
            using (var reader = await command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    return new GameInfo
                    {
                        Id = reader["Id"].ToString(),
                        Name = reader["Name"].ToString(),
                        FilePath = reader["FilePath"].ToString(),
                        runningtime = reader.IsDBNull(reader.GetOrdinal("runningtime")) ? null : reader["runningtime"].ToString()
                        // 根据实际情况添加其他字段
                    };
                }
            }
            return null; // 如果没有找到对应ID的游戏信息
        }


        // ... Other methods from IGameInfoRepository
    }
}
