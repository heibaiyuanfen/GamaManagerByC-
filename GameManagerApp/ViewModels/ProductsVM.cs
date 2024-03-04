using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameManagerApp.Models; // 引用Models命名空间，以便能够使用PageModel类。

namespace GameManagerApp.ViewModels
{
    // Products类继承自ViewModelBase类，使其具备了通知UI属性值更改的能力。
    class Products : Utilites.ViewModelBase
    {
        private readonly PageModel _PageModel; // 私有只读字段_PageModel，持有模型层的实例。
                                               // 该模型实例用于存储和管理产品相关的数据。

        // 公共属性ProductAvailability，用于封装模型中的ProductStatus属性。
        public string ProductAvailability
        {
            get { return _PageModel.ProductStatus; } // 获取模型中ProductStatus属性的值。
            set
            {
                _PageModel.ProductStatus = value; // 设置模型中ProductStatus属性的值。
                OnPropertyChanged(); // 调用OnPropertyChanged方法来通知UI，
                                     // ProductAvailability属性已经更改。
            }
        }

        // Products类的构造函数。
        public Products()
        {
            _PageModel = new PageModel(); // 实例化PageModel，初始化_PageModel字段。
            ProductAvailability = "out of stock "; // 设置ProductAvailability属性的初始值为"out of stock"。
                                                   // 这将更新模型中的ProductStatus，并通过OnPropertyChanged
                                                   // 通知任何绑定到ProductAvailability属性的UI元素进行更新。
        }
    }
}
