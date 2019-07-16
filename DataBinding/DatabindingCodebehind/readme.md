# Data binding via Code-behind


이전 데이터 바인딩 예제에서 보았듯이 XAML을 사용하여 바인딩을 정의하는 것은 매우 쉽지만 특정 경우에는 코드 숨김 대신 코드에서 수행 할 수 있습니다. 이것은 매우 쉽고 XAML을 사용할 때와 동일한 가능성을 제공합니다. "Hello, bound world"예제를 사용해 보겠습니다.하지만 이번에는 Code-behind에서 필요한 바인딩을 만듭니다.

```XAML
<Window x:Class="DatabindingCodebehind.MainWindow"
    xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
    xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
    Title="CodeBehindBindingsSample" Height="110" Width="280">
    <StackPanel Margin="10">
        <TextBox Name="txtValue" />
        <WrapPanel Margin="0,10">
            <TextBlock Text="Value: " FontWeight="Bold" />
            <TextBlock Name="lblValue" />
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

namespace DatabindingCodebehind
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Binding binding = new Binding("Text");
            binding.Source = txtValue;
            lblValue.SetBinding(TextBlock.TextProperty, binding);
        }
    }
}

```

![sample](https://100.100.15.221/Kerry-cho/WPF-Example/blob/master/DataBinding/DatabindingCodebehind/Sample.png)

그것은 Binding 인스턴스를 생성하여 작동합니다. 우리는 Text 속성에 바인딩하기를 원하기 때문에 생성자에서 직접 원하는 경로를 지정합니다.이 경우 "Text"입니다. 이 예제에서는 TextBox 컨트롤이어야하는 Source를 지정합니다. 이제 WPF는 TextBox를 소스 컨트롤로 사용해야한다는 것을 알고 있으며 특히 Text 속성에 포함 된 값을 찾고 있습니다.

# Summary


보시다시피 C # 코드로 바인딩을 만드는 것은 쉽고 XAML에서 인라인으로 작성하는 데 사용되는 구문과 비교할 때 데이터 바인딩을 처음 접하는 사람들을 쉽게 파악할 수 있습니다. 어떤 방법을 사용 하느냐에 달렸지 만 둘 다 잘 작동합니다.

