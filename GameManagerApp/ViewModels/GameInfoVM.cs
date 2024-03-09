﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GameManagerApp.Models;

namespace GameManagerApp.ViewModels
{
     class GameInfoVM :Utilites.ViewModelBase
    {
        private readonly PageModel _PageModel; // 声明一个私有只读字段_PageModel，类型为PageModel。
                                               // 这个字段用于在Customers视图模型中持有一个模型实例。


        private GameInfo CurrentGameInfo { get; set; }

        public GameInfoVM()
        {
            _PageModel = new PageModel();
        }

        public GameInfoVM(GameInfo game)
        {
            _PageModel = new PageModel();
            CurrentGameInfo = game;
            MessageBox.Show(game.Name);

        }


    }
}
