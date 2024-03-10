using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameManagerApp.Models;

namespace GameManagerApp.IRepository
{
    public   interface IGameInfoRepository
    {
        Task Add(GameInfo gameInfo);
        void remove(GameInfo gameInfo);
        void edit(int Id);

        GameInfo GetById(int Id);

        Task<GameInfo> GetByNameAsync(string filename);

        IEnumerable<GameInfo> GetAll();

        Task<IEnumerable<GameInfo>> GetAllAsync();
        Task UpdateRunningTimeAsync(string gameFilePath, string runningTimeString);

        Task<GameInfo> GetByFilePathAsync(string filePath);
        Task<GameInfo> GetGameInfoAsync(string gameId);
    }
}
