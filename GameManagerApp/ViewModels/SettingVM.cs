using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameManagerApp.Models; // 引用Models命名空间，以便能够使用PageModel类。

namespace GameManagerApp.ViewModels
{
    // SettingVM类继承自ViewModelBase，使其具备了通知UI属性值更改的能力。
    class SettingVM : Utilites.ViewModelBase
    {
        private readonly PageModel _PageModel; // 私有只读字段_PageModel，持有模型层的实例。
                                               // 该模型实例用于存储和管理设置相关的数据。

        // 公共属性Settings，用于封装模型中的LocationStatus布尔属性。
        public bool Settings
        {
            get { return _PageModel.LocationStatus; } // 获取模型中LocationStatus属性的值。
            set
            {
                _PageModel.LocationStatus = value; // 设置模型中LocationStatus属性的值。
                OnPropertyChanged(); // 调用OnPropertyChanged方法来通知UI，
                                     // Settings属性已经更改。
            }
        }

        // SettingVM类的构造函数。
        public SettingVM()
        {
            _PageModel = new PageModel(); // 实例化PageModel，初始化_PageModel字段。
            Settings = true; // 设置Settings属性的初始值为true。
                             // 这将更新模型中的LocationStatus，并通过OnPropertyChanged
                             // 通知任何绑定到Settings属性的UI元素进行更新。
        }
    }
}
