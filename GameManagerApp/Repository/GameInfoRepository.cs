using System;
using System.Data;
using System.Data.SqlClient;
using GameManagerApp.IRepository;
using System.Net;
using GameManagerApp.Models;

namespace GameManagerApp.Repository
{
    public class GameInfoRepository : RepositoryBase, IGameInfoRepository
    {
        private readonly string _connectionString;

        public GameInfoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task Add(GameInfo gameInfo)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                using (var command = new SqlCommand("INSERT INTO Games (Name, FilePath, Icon) VALUES (@Name, @FilePath, @Icon)", connection))
                {
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
                using (var connection = new SqlConnection(_connectionString))
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

        // ... Other methods from IGameInfoRepository
    }
}
