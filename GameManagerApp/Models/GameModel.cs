using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace GameManagerApp.Models
{
    public class GameModel
    {
        public string Name { get; set; }
        public string FilePath { get; set; }
        public string ImagePath { get; set; } // 保留ImagePath以备不时之需
        public Icon GameIcon { get; set; } // 添加Icon属性
    }

}
