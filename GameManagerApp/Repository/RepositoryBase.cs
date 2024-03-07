using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace GameManagerApp.Repository
{
    public abstract class RepositoryBase
    {
        private readonly string _connectionString;


        public RepositoryBase()
        {
            _connectionString = "Server=localhost\\GAMEINFO;Database=GameInfo;User Id=sa;Password=123456";

        }


        protected SqlConnection GetSqlConnection()
        {
            return new SqlConnection(_connectionString);
        }
       



    }
}
