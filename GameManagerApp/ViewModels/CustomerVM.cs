using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameManagerApp.Models; // 引入Models命名空间，让PageModel类可用

namespace GameManagerApp.ViewModels
{
    // Customers类继承自ViewModelBase类。ViewModelBase类实现了INotifyPropertyChanged接口，
    // 允许Customers类能够通知视图（View）属性值的更改。
    class CustomerVM : Utilites.ViewModelBase
    {
        private readonly PageModel _PageModel; // 声明一个私有只读字段_PageModel，类型为PageModel。
                                               // 这个字段用于在Customers视图模型中持有一个模型实例。

        // CustmoerID属性，封装了_PageModel中的CustomerCount属性。
        // 提供了获取和设置_CustomerCount属性值的功能。
        public int CustmoerID
        {
            get { return _PageModel.CustomerCount; } // 获取_PageModel中的CustomerCount属性值。
            set
            {
                _PageModel.CustomerCount = value; // 设置_PageModel中的CustomerCount属性值。
                OnPropertyChanged(); // 调用基类ViewModelBase中的OnPropertyChanged方法，
                                     // 通知绑定到CustmoerID属性的UI元素，CustmoerID属性的值已更改。
            }
        }

        // Customers类的构造函数。
        public CustomerVM()
        {
            _PageModel = new PageModel(); // 在构造函数中实例化_PageModel。
            CustmoerID = 100528; // 设置CustmoerID的初始值为100528。
                                 // 这将通过CustmoerID属性的set访问器设置_PageModel中的CustomerCount属性，
                                 // 并通过调用OnPropertyChanged方法通知任何绑定的UI元素。
        }
    }
}
