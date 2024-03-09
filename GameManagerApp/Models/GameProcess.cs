using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerApp.Models
{
    public class GameProcess
    {
        public Process Process { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        // 计算运行时间
        public TimeSpan RunningTime { get; set; }
    }

}
