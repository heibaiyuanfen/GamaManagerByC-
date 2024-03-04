// 引入所需的命名空间
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameManagerApp.Models; // 引用Models命名空间，以便能够使用PageModel类。

namespace GameManagerApp.ViewModels
{
    // Shipments类继承自ViewModelBase，提供属性更改通知的能力。
    class Shipments : Utilites.ViewModelBase
    {
        // 私有只读字段_PageModel，持有模型层的一个实例。
        // 此模型实例用于存储和管理发货相关的数据。
        private readonly PageModel _PageModel;

        // 公共属性ShipmentTracking，封装了模型中的ShipmentDelivery属性。
        // 此属性代表发货跟踪信息的时间。
        public TimeOnly ShipmentTracking
        {
            get { return _PageModel.ShipmentDelivery; } // 获取模型中ShipmentDelivery属性的值。
            set
            {
                _PageModel.ShipmentDelivery = value; // 设置模型中ShipmentDelivery属性的值。
                OnPropertyChanged(); // 调用OnPropertyChanged方法来通知UI，
                                     // ShipmentTracking属性已经更改。
            }
        }

        // Shipments类的构造函数。
        public Shipments()
        {
            _PageModel = new PageModel(); // 实例化PageModel，初始化_PageModel字段。
            TimeOnly time = TimeOnly.FromDateTime(DateTime.Now); // 创建一个TimeOnly实例，代表当前时间。
            ShipmentTracking = time; // 设置ShipmentTracking属性的初始值为当前时间。
                                     // 这将更新模型中的ShipmentDelivery，并通过OnPropertyChanged
                                     // 通知任何绑定到ShipmentTracking属性的UI元素进行更新。
        }
    }
}
