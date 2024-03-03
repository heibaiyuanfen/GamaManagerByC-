using System; // 导入基础系统命名空间
using System.Collections.Generic; // 导入集合命名空间
using System.Linq; // 导入LINQ命名空间
using System.Text; // 导入文本操作命名空间
using System.Threading.Tasks; // 导入异步任务命名空间
using System.Runtime.CompilerServices; // 导入运行时编译器服务命名空间，用于CallerMemberName属性
using System.ComponentModel; // 导入组件模型命名空间，用于INotifyPropertyChanged接口

// 定义一个名为GameManagerApp的命名空间下的Utilities子命名空间
namespace GameManagerApp.Utilites
{
    // 定义一个ViewModelBase类，用作所有视图模型的基类
    class ViewModelBase : INotifyPropertyChanged
    {
        // INotifyPropertyChanged接口事件声明，用于属性变更通知
        public event PropertyChangedEventHandler? PropertyChanged;

        // OnPropertyChanged方法，当属性值改变时调用此方法来发起通知
        // 使用了CallerMemberName特性来获取调用此方法的属性名称
        public void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            // ?. 是空合并运算符，只有当PropertyChanged不为空时，才会调用其Invoke方法
            // 从而避免了在属性未绑定时引发空引用异常的风险
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
