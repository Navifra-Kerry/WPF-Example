# Implementing a custom WPF Command

이전 장에서는 WPF에서 이미 정의 된 명령을 사용하는 다양한 방법을 살펴 보았지만 물론 직접 명령을 구현할 수도 있습니다. 그것은 매우 간단합니다. 일단 작업을 완료하면 WPF에 정의 된 것과 같은 명령을 사용할 수 있습니다.

자신의 명령을 구현하는 가장 쉬운 방법은이를 포함 할 정적 클래스를 만드는 것입니다. 각 명령은이 클래스에 정적 필드로 추가되어 응용 프로그램에서 사용할 수 있습니다. 몇 가지 이유로 인해 WPF가 Exit / Quit 명령을 구현하지 않기 때문에 사용자 지정 명령 예제 용으로 구현하기로 결정했습니다. 다음과 같습니다.

```XAML
<Window x:Class="WPFExample.CustomCommands.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:self="clr-namespace:WPFExample.CustomCommands"
        Title="CustomCommandSample" Height="150" Width="200">
    <Window.CommandBindings>
        <CommandBinding Command="self:CustomCommands.Exit" CanExecute="ExitCommand_CanExecute" Executed="ExitCommand_Executed" />
    </Window.CommandBindings>
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="Auto" />
            <RowDefinition Height="*" />
        </Grid.RowDefinitions>
        <Menu>
            <MenuItem Header="File">
                <MenuItem Command="self:CustomCommands.Exit" />
            </MenuItem>
        </Menu>
        <StackPanel Grid.Row="1" HorizontalAlignment="Center" VerticalAlignment="Center">
            <Button Command="self:CustomCommands.Exit">Exit</Button>
        </StackPanel>
    </Grid>
</Window>
```
```C#
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace WPFExample.CustomCommands
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

        private void ExitCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void ExitCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }

    public static class CustomCommands
    {
        public static readonly RoutedUICommand Exit = new RoutedUICommand
            (
                "Exit",
                "Exit",
                typeof(CustomCommands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.F4, ModifierKeys.Alt)
                }
            );

        //Define more commands here, just like the one above
    }
}
```

![sample](https://100.100.15.221/Kerry-cho/WPF-Example/blob/master/Commands/WPFExample.CustomCommands/sample.png)

XAML에서 메뉴와 버튼으로 된 아주 간단한 인터페이스를 정의했습니다. 둘 다 새 사용자 정의 Exit 명령을 사용 합니다. 이 명령은 코드 비하인드에서 자체 CustomCommands 클래스로 정의 된 다음 실행 가능 / 실행에 사용할 이벤트를 할당하는 창의 CommandBindings 컬렉션에서 참조됩니다.

이 모든 것은 이전 장의 예제와 같습니다. 단, 내장 명령 대신 자체 코드에서 명령을 참조하고 있습니다 (맨 위에 정의 된 "자체"네임 스페이스 사용).

코드 숨김에서는 명령에 대한 두 가지 이벤트에 응답합니다. 하나의 이벤트는 명령을 항상 실행하도록 허용합니다. 일반적으로 exit / quit 명령에 해당하므로 다른 명령은 Shutdown 메서드를 호출하여 명령을 종료합니다. 신청. 모두 아주 간단합니다.


이미 설명했듯이 Exit 명령은 정적 CustomCommands 클래스의 필드로 구현합니다. 커맨드에 속성을 정의하고 지정하는 몇 가지 방법이 있지만 좀 더 컴팩트 한 방법을 택했습니다 (동일한 행에 배치하는 경우 훨씬 더 작아 지지만 읽기 쉽도록 줄 바꿈을 추가했습니다). 그것 모두는 생성자를 통해. 매개 변수는 명령의 텍스트 / 레이블, 명령의 이름, 소유자 유형 및 다음 InputGestureCollection입니다. 명령의 기본 단축키 (Alt + F4)를 정의 할 수 있습니다.

# Summary

사용자 지정 WPF 명령을 구현하는 작업은 기본 제공 명령을 사용하는 것만 큼 쉽지만 응용 프로그램의 모든 용도로 명령을 사용할 수 있습니다. 따라서이 장의 예와 같이 여러 위치에서 작업을 다시 사용하는 것이 매우 쉽습니다.

