# GamaManagerByC#


### 1. `RelayCommand`类的实现

- **构造函数**：接收两个参数，第一个是一个`Action<object>`类型的`execute`委托，指定了命令被执行时需要调用的方法；第二个是一个`Func<object, bool>`类型的`canExecute`委托（可选），用于确定命令是否可执行。如果`canExecute`未提供，则命令总是可执行。

- **`CanExecute`方法**：调用`canExecute`委托（如果提供）来决定命令是否可以执行。如果没有提供`canExecute`委托，命令总是可执行。

- **`Execute`方法**：调用`execute`委托，执行命令的实际逻辑。

- **`CanExecuteChanged`事件**：通过`CommandManager.RequerySuggested`事件自动监听命令的可执行状态变化。当命令的可执行状态可能改变时，这个事件被触发，通知界面更新命令的状态（如按钮的启用/禁用）。

### 2. `NavigationVM`视图模型中对`RelayCommand`的使用

- **命令属性**：`NavigationVM`定义了多个命令属性（如`HomeCommand`, `CustomersCommand`, 等），每个命令对应应用程序中的一个导航动作。

- **构造函数中的命令初始化**：在`NavigationVM`的构造函数中，每个命令属性都被初始化为一个`RelayCommand`实例。为每个`RelayCommand`提供了对应的执行方法（如`Home`, `Custmoer`, 等），这些方法更新`CurrentView`属性，从而改变当前显示的视图模型。

- **命令执行逻辑**：当用户界面上的元素（如按钮）被激活（点击）时，绑定到这些元素的命令将被执行。例如，如果一个按钮绑定到了`HomeCommand`，那么点击这个按钮会调用`Home`方法，`CurrentView`属性被设置为一个新的`HomeVM`实例，触发`OnPropertyChanged`通知，从而更新UI中当前显示的视图。

- **动态视图更新**：通过修改`CurrentView`属性来动态更换界面中展示的视图模型。这种方式使得在不同视图模型之间进行切换变得简单，同时保持了视图和视图模型的清晰分离。

综上所述，这部分代码实现了一个基于命令的导航系统，允许应用程序在不同视图之间进行无缝切换，而不需要在视图层中编写复杂的逻辑。通过使用`RelayCommand`来封装导航逻辑，`NavigationVM`能够以声明式的方式处理用户界面的导航行为，这是MVVM设计模式的核心优势之一。
