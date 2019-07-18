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

# Using the CanExecute method

첫 번째 예제에서는 버튼을 항상 사용할 수 있도록 true를 반환하는 CanExecute 이벤트를 구현했습니다. 그러나 이것은 물론 모든 버튼에 해당하는 것은 아닙니다. 대부분의 경우 애플리케이션의 상태에 따라 버튼을 활성화 또는 비활성화해야합니다.

가장 일반적인 예는 텍스트가 선택되어있을 때만 잘라 내기 및 복사 단추를 사용하고 텍스트가 클립 보드에 있는 경우에만 붙여 넣기 단추를 사용하려는  단추를 전환하는 것입니다. 이 예제에서 해당 내용에 대해 설명 합니다.

```XAML
<Window x:Class="WPFExample.CanExecute.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="CommandCanExecuteSample" Height="200" Width="250">
    <Window.CommandBindings>
        <CommandBinding Command="ApplicationCommands.Cut" CanExecute="CutCommand_CanExecute" Executed="CutCommand_Executed" />
        <CommandBinding Command="ApplicationCommands.Paste" CanExecute="PasteCommand_CanExecute" Executed="PasteCommand_Executed" />
    </Window.CommandBindings>
    <DockPanel>
        <WrapPanel DockPanel.Dock="Top" Margin="3">
            <Button Command="ApplicationCommands.Cut" Width="60">_Cut</Button>
            <Button Command="ApplicationCommands.Paste" Width="60" Margin="3,0">_Paste</Button>
        </WrapPanel>
        <TextBox AcceptsReturn="True" Name="txtEditor" />
    </DockPanel>
</Window>
```

```C#
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace WPFExample.CanExecute
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

        private void CutCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (txtEditor != null) && (txtEditor.SelectionLength > 0);
        }

        private void CutCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            txtEditor.Cut();
        }

        private void PasteCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Clipboard.ContainsText();
        }

        private void PasteCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            txtEditor.Paste();
        }
    }
}

```

![sample1](https://100.100.15.221/Kerry-cho/WPF-Example/blob/master/Commands/Sample2.png)

이 예제는 두 개의 버튼과 TextBox 컨트롤을 가진 매우 간단한 인터페이스를 가집니다. 첫 번째 버튼은 클립 보드로 자르고 두 번째 버튼은 클립 보드에서 붙여 넣습니다.

Code-behind를 살펴 보면, 우리는 각 버튼에 대해 두 가지 이벤트, 즉 _Executed로 끝나는 실제 작업을 수행 한 다음 CanExecute 이벤트를 수행합니다. 각각의 경우 액션이 실행될 수 있는지 여부를 결정하는 논리를 적용한 다음 EventArgs의 반환 값 CanExecute에 할당합니다.

이것이 멋진 점은 단추를 업데이트 할 때 이러한 메서드를 호출 할 필요가 없다는 것입니다. WPF는 응용 프로그램이 유휴 상태 일 때 자동으로 수행하여 인터페이스가 항상 업데이트 된 채로 있는지 확인합니다.

# Default command behavior and CommandTarget

앞의 예에서 보았 듯이 일련의 명령을 처리하면 메소드 선언이 많고 표준 로직이 많아 코드가 상당히 복잡해질 수 있습니다. 아마 WPF 팀이 당신을 위해 그것을 처리하기로 결정한 이유 일 것입니다. WPF TextBox는 잘라 내기, 복사, 붙여 넣기, 실행 취소 및 다시 실행과 같은 일반적인 명령을 자동으로 처리 할 수 ​​있기 때문에 실제로 이번 예제에서 코드를 작성할 필요가 없습니다.


WPF는 TextBox와 같은 텍스트 입력 컨트롤에 포커스가있을 때 Executed 및 CanExecute 이벤트를 처리하여이 작업을 수행합니다. 기본적으로 앞의 예제에서 수행 한 이벤트를 재정의 할 수 있지만 기본 동작 만 원할 경우 WPF에서 명령과 TextBox 컨트롤을 연결하고 작업을 수행 할 수 있습니다. 이 예제가 얼마나 단순한 지보십시오.

```XAML
<Window x:Class="WPFExample.CommandsWithCommandTargetSample.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="CommandsWithCommandTargetSample" Height="200" Width="250">
    <DockPanel>
        <WrapPanel DockPanel.Dock="Top" Margin="3">
            <Button Command="ApplicationCommands.Cut" CommandTarget="{Binding ElementName=txtEditor}" Width="60">_Cut</Button>
            <Button Command="ApplicationCommands.Paste" CommandTarget="{Binding ElementName=txtEditor}" Width="60" Margin="3,0">_Paste</Button>
        </WrapPanel>
        <TextBox AcceptsReturn="True" Name="txtEditor" />
    </DockPanel>
</Window>
```

![sample2](https://100.100.15.221/Kerry-cho/WPF-Example/blob/master/Commands/Sample3.png)

이 예제에 필요한 코드 숨김 코드가 없습니다. WPF는이 모든 특정 명령을이 특정 컨트롤에 사용하기 때문에 모든 코드를 처리합니다. TextBox는 우리를 위해 일합니다.

단추의 CommandTarget 속성을 사용하여 명령을 TextBox 컨트롤에 바인딩하는 방법에 유의하십시오. 이것은 WrapPanel이 동일한 방식으로 포커스를 처리하지 않기 때문에이 특정 예에서 필요합니다. 툴바 또는 메뉴를 사용할 수 있지만 명령을 대상으로 지정하는 것이 좋습니다.

# Summary

커맨드 다루기는 꽤나 간단하지만 약간의 마크 업과 코드가 필요합니다. 효과는 여러 코드에서 동일한 액션을 호출해야 할 때나 마지막 예제에서 보았 듯이 WPF가 완벽하게 처리 할 수있는 내장 명령을 사용할 때 특히 분명합니다.
