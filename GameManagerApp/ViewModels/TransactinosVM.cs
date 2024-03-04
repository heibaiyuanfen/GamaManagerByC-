// 引入所需的命名空间。
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameManagerApp.Models; // 引用Models命名空间，以便能够使用PageModel类。
using System.Windows.Input;

namespace GameManagerApp.ViewModels
{
    // Transactinos类继承自ViewModelBase，使其能够通知UI属性值的更改。
    // 注意：类名"Transactinos"可能是"Transactions"的拼写错误。
    class Transactinos : Utilites.ViewModelBase
    {
        // 私有只读字段_PageModel，持有模型层的实例。
        // 该模型实例用于存储和管理交易相关的数据。
        private readonly PageModel _PageModel;

        // 公共属性TransactionAmount，封装了模型中的TransactionValue属性。
        public decimal TransactionAmount
        {
            get { return _PageModel.TransactionValue; } // 获取模型中TransactionValue属性的值。
            set
            {
                _PageModel.TransactionValue = value; // 设置模型中TransactionValue属性的值。
                OnPropertyChanged(); // 调用OnPropertyChanged方法来通知UI，
                                     // TransactionAmount属性已经更改。
            }
        }

        // Transactinos类的构造函数。
        public Transactinos()
        {
            _PageModel = new PageModel(); // 实例化PageModel，初始化_PageModel字段。
            TransactionAmount = 5638; // 设置TransactionAmount属性的初始值为5638。
                                      // 这将更新模型中的TransactionValue，并通过OnPropertyChanged
                                      // 通知任何绑定到TransactionAmount属性的UI元素进行更新。
        }
    }
}
