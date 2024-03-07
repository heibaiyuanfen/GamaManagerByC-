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

        IEnumerable<GameInfo> GetAll();
    }
}
