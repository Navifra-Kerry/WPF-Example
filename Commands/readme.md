# Using WPF commands

이전 예제에서 우리는 명령이 무엇이며 어떻게 작동하는지에 대한 많은 이론을 논의했습니다. 이 장에서는 명령을 사용자 인터페이스 요소에 할당하고 명령을 모두 연결하는 명령 바인딩을 작성하여 실제로 명령을 사용하는 방법을 살펴 보겠습니다.


우리는 아주 간단한 예제로 시작할 것입니다.

```XAML
<Window x:Class="WPFExample.UsingCommandsSample.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="UsingCommandsSample" Height="100" Width="200">
    <Window.CommandBindings>
        <CommandBinding Command="ApplicationCommands.New" Executed="NewCommand_Executed" CanExecute="NewCommand_CanExecute" />
    </Window.CommandBindings>

    <StackPanel HorizontalAlignment="Center" VerticalAlignment="Center">
        <Button Command="ApplicationCommands.New">New</Button>
    </StackPanel>
</Window>

```

```C#
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace WPFExample.UsingCommandsSample
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("The New command was invoked");
        }
    }
}
```

![sample](https://100.100.15.221/Kerry-cho/WPF-Example/blob/master/Commands/sample.png)


Window에 CommandBindings 추가하여 명령 바인딩을 정의합니다. 우리는 사용하고자하는 Command (ApplicationCommand의 New 명령)와 두 개의 이벤트 핸들러를 지정합니다. 비주얼 인터페이스는 하나의 버튼으로 구성되며 Command property을 사용하여 명령을 첨부합니다.

Code-behind에서는 두 가지 이벤트를 처리합니다. 특정 명령이 현재 사용 가능한지 알기 위해 응용 프로그램이 유휴 상태 일 때 WPF가 호출하는 CanExecute 이 처리기는이 예제에서 항상 사용 가능하도록 하여 이 예제를 매우 간단하게 만듭니다. 이벤트 인수의 CanExecute 속성을 true로 설정하면됩니다.

Executed 처리기는 명령이 호출 될 때 메시지 상자를 표시합니다. 샘플을 실행하고 버튼을 누르면이 메시지가 표시됩니다. 알아 두어야 할 점은이 명령에는 정의 된 기본 키보드 단축키가 정의되어있어 추가 보너스로 사용할 수 있습니다. 버튼을 클릭하는 대신 키보드의 Ctrl + N 키를 눌러도 동일하게 나타납니다.
