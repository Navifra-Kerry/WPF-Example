# MessageBox

WPF는 애플리케이션에서 사용할 여러가지 대화상자를 제공하지만 가장 간단한 것은 MessageBox입니다. 이것의 유일한 목적은 사용자에게 메시지를 보여주고, 메시지에 사용자가 응답할 수 있는 하나 또는 여러가지 방법을 제공합니다.

MessageBox는 원하는 방식대로 보이고 작용할 수 있는 다양한 매개변수들을 취하는 static Show() 메소드 호출로 사용될 수 있습니다. 이번 장에서 각각의 변형을 MessageBox.Show() 라인과 결과 스크린샷으로 나타내어 모든 다양한 양식을 살펴보겠습니다. 이번 장의 마지막 부분에서 모든 변형을 테스트할 수 있는 완전한 예제를 찾을 수 있습니다.

가장 간단한 형태로 MessageBox는 표시할 메시지인 단일 매개 변수를 취합니다.

```C#
System.Windows.MessageBox.Show("Hello, world!");
```

![sample1](https://100.100.15.221/Kerry-cho/WPF-Example/blob/master/Dialog/WPFExample.MessageBox/sample1.png)

# MessageBox Title

위 예제는 너무 단순할 수 있습니다. - 메시지를 표시하는 창의 제목이 도움이 될 수 있습니다. 다행히 두번째 선택적 매개 변수를 사용하여 제목을 지정할 수 있습니다.

```C#
System.Windows.MessageBox.Show("Hello, world!", "My App");
```

![sample2](https://100.100.15.221/Kerry-cho/WPF-Example/blob/master/Dialog/WPFExample.MessageBox/sample2.png)

# MessageBox 추가 버튼

기본적으로 MessageBox에는 Ok버튼 하나만 있지만, 단지 정보만 표시하지 않고 사용자에게 질문을 하는 경우를 대비하여 변경할 수 있습니다. 또한 줄 바꿈 문자(\n)를 사용하여 메시지를 여러 줄로 나누는 방법을 확인하십시오.

```C#
System.Windows.MessageBox.Show("This MessageBox has extra options.\n\nHello, world?", "My App", MessageBoxButton.YesNoCancel);
```

MessageBoxButton 열거형의 값을 사용하여 표시할 버튼을 제어합니다. 이 경우 Yes, No 및 Cancel 버튼이 포함됩니다. 다음의 자명한 값을 사용할 수 있습니다.

* OK
* OKCancel
* YesNoCancel
* YesNo

이제 여러 선택 항목 중, 사용자가 선택한 것을 볼 수 있는 방법이 필요합니다. 다행히 MessageBoxResult 열거형 반환값을 MessageBox.Show() 메서드는 항상 반환합니다. 다음의 예제입니다

```C#
System.Windows.MessageBoxResult result = System.Windows.MessageBox.Show("Would you like to greet the world with a \"Hello, world\"?", "My App", MessageBoxButton.YesNoCancel);
switch (result)
{
    case System.Windows.MessageBoxResult.Yes:
        System.Windows.MessageBox.Show("Hello to you too!", "My App");
        break;
    case System.Windows.MessageBoxResult.No:
        System.Windows.MessageBox.Show("Oh well, too bad!", "My App");
        break;
    case System.Windows.MessageBoxResult.Cancel:
        System.Windows.MessageBox.Show("Nevermind then...", "My App");
        break;
}                
```

![sample3](https://100.100.15.221/Kerry-cho/WPF-Example/blob/master/Dialog/WPFExample.MessageBox/sample3.png)
![sample4](https://100.100.15.221/Kerry-cho/WPF-Example/blob/master/Dialog/WPFExample.MessageBox/sample4.png)
MessageBox.Show() 메서드의 결과 값을 확인하면, 스크린샷뿐만 아니라 코드 예제와 마찬가지로 사용자 선택에 반응할 수 있습니다.

# MessageBox 아이콘

MessageBox에는 네 번째 매개 변수를 사용하여 텍스트 메시지의 왼쪽에 미리 정의된 아이콘을 보여주는 기능이 있습니다.

```C#
System.Windows.MessageBox.Show("Hello, world!", "My App", MessageBoxButton.OK, MessageBoxImage.Information);
```

![sample5](https://100.100.15.221/Kerry-cho/WPF-Example/blob/master/Dialog/WPFExample.MessageBox/sample5.png)

MessageBoxImage 열거형을 사용하여 상황에 따른 아이콘을 선택할 수 있습니다. 전체 목록은 다음과 같습니다.

* Asterisk
* Error
* Exclamation
* Hand
* Information
* None
* Question
* Stop
* Warning

목록이 어떻게 보이는지에 대해 설명해야 하지만, 다양한 값을 시험하거나 각 값에 대해 설명하고 아이콘이 삽화된 MSDN 문서를 살펴보십시오
http://msdn.microsoft.com/en-us/library/system.windows.messageboximage.aspx

# MessageBox 기본 옵션

MessageBox는 기본 선정으로 버튼을 선택합니다. 대화 상자가 표시되어 사용자가 단지 Enter키를 누를 경우 선택된 버튼이 호출됩니다. 예를 들어 "Yes"와 "No"버튼이 있는 MessageBox를 표시하면 "Yes"가 기본 선택됩니다. MessageBox.Show() 메서드의 다섯 번째 매개 변수를 사용하여 이 행위를 변경할 수 있습니다.

```C#
System.Windows.MessageBox.Show("Hello, world?", "My App", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
```

![sample6](https://100.100.15.221/Kerry-cho/WPF-Example/blob/master/Dialog/WPFExample.MessageBox/sample6.png)

# 완성된 예제

약속한 대로 이 장에서 사용한 완전한 예제는 다음과 같습니다.

```XAML
<Window x:Class="WPFExample.MessageBox.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="MessageBoxSample" Height="250" Width="300">
    <StackPanel HorizontalAlignment="Center" VerticalAlignment="Center">
        <StackPanel.Resources>
            <Style TargetType="Button">
                <Setter Property="Margin" Value="0,0,0,10" />
            </Style>
        </StackPanel.Resources>
        <Button Name="btnSimpleMessageBox" Click="btnSimpleMessageBox_Click">Simple MessageBox</Button>
        <Button Name="btnMessageBoxWithTitle" Click="btnMessageBoxWithTitle_Click">MessageBox with title</Button>
        <Button Name="btnMessageBoxWithButtons" Click="btnMessageBoxWithButtons_Click">MessageBox with buttons</Button>
        <Button Name="btnMessageBoxWithResponse" Click="btnMessageBoxWithResponse_Click">MessageBox with response</Button>
        <Button Name="btnMessageBoxWithIcon" Click="btnMessageBoxWithIcon_Click">MessageBox with icon</Button>
        <Button Name="btnMessageBoxWithDefaultChoice" Click="btnMessageBoxWithDefaultChoice_Click">MessageBox with default choice</Button>
    </StackPanel>
</Window>
```

```C#
using System;
using System.Windows;

namespace WPFExample.MessageBox
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

        private void btnSimpleMessageBox_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Hello, world!");
        }

        private void btnMessageBoxWithTitle_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Hello, world!", "My App");
        }

        private void btnMessageBoxWithButtons_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("This MessageBox has extra options.\n\nHello, world?", "My App", System.Windows.MessageBoxButton.YesNoCancel);
        }

        private void btnMessageBoxWithResponse_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBoxResult result = System.Windows.MessageBox.Show("Would you like to greet the world with a \"Hello, world\"?", "My App", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case System.Windows.MessageBoxResult.Yes:
                    System.Windows.MessageBox.Show("Hello to you too!", "My App");
                    break;
                case System.Windows.MessageBoxResult.No:
                    System.Windows.MessageBox.Show("Oh well, too bad!", "My App");
                    break;
                case System.Windows.MessageBoxResult.Cancel:
                    System.Windows.MessageBox.Show("Nevermind then...", "My App");
                    break;
            }
        }

        private void btnMessageBoxWithIcon_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Hello, world!", "My App", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Information);
        }

        private void btnMessageBoxWithDefaultChoice_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Hello, world?", "My App", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question, System.Windows.MessageBoxResult.No);
        }
    }
}
```




