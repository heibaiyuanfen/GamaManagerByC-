using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameManagerApp.Models; // 引用Models命名空间，使PageModel类可用。

namespace GameManagerApp.ViewModels
{
    // OrdersVM类继承自ViewModelBase，使其能够通知UI属性的更改。
    class OrdersVM : Utilites.ViewModelBase
    {
        private readonly PageModel _PageModel; // 私有只读字段_PageModel，用于存储模型实例。
                                               // 这使视图模型可以访问和修改模型数据。

        // 公共属性DisplayOrderDate，封装了_PageModel中的OrderDate属性。
        public DateOnly DisplayOrderDate
        {
            get { return _PageModel.OrderDate; } // 获取模型中的OrderDate值。
            set
            {
                _PageModel.OrderDate = value; // 设置模型中的OrderDate值。
                OnPropertyChanged(); // 调用OnPropertyChanged方法，通知UI DisplayOrderDate属性已更改。
            }
        }

        // OrdersVM的构造函数。
        public OrdersVM()
        {
            _PageModel = new PageModel(); // 实例化PageModel，初始化_PageModel字段。
            DisplayOrderDate = DateOnly.FromDateTime(DateTime.Now); // 设置DisplayOrderDate属性的初始值为当前日期。
                                                                    // 这将通过属性的set访问器更新_PageModel的OrderDate，
                                                                    // 并通过OnPropertyChanged通知绑定的UI元素更新。
        }
    }
}
