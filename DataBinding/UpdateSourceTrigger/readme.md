# UpdateSourceTrigger

이전 챕터에서 TextBox의 변경 내용이 window로 즉시 반영되지 않는 것을 보았습니다. 대신 TextBox에서 포커스가 손실된 후에 변경 내용이 업데이트되었습니다. 이 동작은 UpdateSourceTrigger라는 바인딩의 속성에 의해 제어됩니다. 기본값은 기본적으로 "Default"이며, 이는 기본적으로 바인딩하는 속성을 기반으로 소스가 업데이트된다는 것을 의미합니다. 작성 시점에서 Text 속성을 제외한 모든 속성은 속성을 변경(PropertyChanged)하는 즉시 업데이트되고 Text 속성은 대상 요소의 포커스를 잃을 때 업데이트됩니다 (LostFocus).

"Default"는 분명히 UpdateSourceTrigger의 기본값입니다. 다른 옵션으로는 PropertyChanged, LostFocus 및 Explicit입니다. 처음 두 가지는 위에서 설명한 대로 동작하고, 마지막 Explicit는 Binding에서 UpdateSource를 직접 호출하는 식으로 업데이트를 수동으로 지시해야 한다는 것을 의미합니다.

```XAML
<Window x:Class="UpdateSourceTrigger.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="DataContextSample" Height="130" Width="310">
    <StackPanel Margin="15">
        <WrapPanel>
            <TextBlock Text="Window title:  " />
            <TextBox Name="txtWindowTitle" Text="{Binding Title, UpdateSourceTrigger=Explicit}" Width="150" />
            <Button Name="btnUpdateSource" Click="btnUpdateSource_Click" Margin="5,0" Padding="5,0">*</Button>
        </WrapPanel>
        <WrapPanel Margin="0,10,0,0">
            <TextBlock Text="Window dimensions: " />
            <TextBox Text="{Binding Width, UpdateSourceTrigger=LostFocus}" Width="50" />
            <TextBlock Text=" x " />
            <TextBox Text="{Binding Height, UpdateSourceTrigger=PropertyChanged}" Width="50" />
        </WrapPanel>
    </StackPanel>
</Window>
```

```C#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UpdateSourceTrigger
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void btnUpdateSource_Click(object sender, RoutedEventArgs e)
        {
            BindingExpression binding = txtWindowTitle.GetBindingExpression(TextBox.TextProperty);
            binding.UpdateSource();
        }
    }
}

```

![sample](https://100.100.15.221/Kerry-cho/WPF-Example/blob/master/DataBinding/UpdateSourceTrigger/Sample.png)

보시다시피, 세 개의 TextBox는 각각 다른 UpdateSourceTrigger를 사용합니다.

첫 번째 것은 Explicit로 설정했습니다. 기본적으로 소스에서 직접 업데이트를 수행하지 않으면 내용이 변경되지 않습니다. 그렇기 때문에, TextBox 옆에 버튼을 추가하여 요청했을 때만 원본 값을 업데이트합니다. 코드에서 Click 핸들러를 찾아서 대상 컨트롤에서 바인딩을 가져온 다음 UpdateSource() 메서드를 호출하는 두 줄의 코드를 사용합니다.

두 번째 TextBox는 LostFocus 값을 사용합니다. 이 값은 실제로 Text 바인딩의 기본값입니다. 대상 컨트롤이 포커스를 잃을 때마다 값이 업데이트 됩니다.

마지막 TextBox는 PropertyChanged 값을 사용합니다. 즉, 바운드 속성이 변경될 때마다 원본 값이 업데이트 합니다. 이 경우 텍스트가 변경되는 즉시 값이 변경됩니다.

예제를 실행하고 세 가지 TextBox가 완전히 다른 방식으로 동작하는 것을 확인하십시오. 첫 번째 값은 버튼을 클릭하기 전까지는 업데이트 되지 않고, 두 번째 TextBox는 포커스가 이동할 때까지 업데이트 되지 않고, 세 번째 값은 키입력, 텍스트 변경 등이 자동으로 업데이트됩니다

# Summary

바인딩의 UpdateSourceTrigger 속성은 변경된 값을 소스로 다시 보내는 방법과 타이밍을 제어합니다. 그러나 WPF는 대부분 컨트롤에서 최선의 조합을 설정해 두어 기본값으로도 상당히 편리하게 사용할 수 있지만, 직접 제어하더라도 우수한 성능을 낼 것입니다.

프로세스에 대한 더 섬세한 제어가 필요한 상황에서는 이 속성들이 확실히 도움이 될 것입니다. 그렇지만 실제로 필요한 것보다 더 자주 업데이트하지 않도록 주의하십시오. 모든 권한을 원한다면 Explicit 값을 사용하고 수동으로 업데이트를 수행 할 수 있지만, 데이터 바인딩 작업의 묘미는 조금밖에 느낄 수 없을 것입니다.
