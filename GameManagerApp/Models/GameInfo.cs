using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameManagerApp.Models
{
    public class GameInfo
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        // 对于图标，您可以选择存储图标的文件路径或将图标转换为字节数组存储
        public byte[] Icon { get; set; }

        public string runningtime { get; set; }
    }
}
