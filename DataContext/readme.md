# DataContext 사용하기

DataContext 속성은 이전 챕터에서 살펴 본 ElementName 속성처럼 다른 소스를 명시적으로 선언하는게 불필요한 바인딩의 기본 소스입니다. DataContext는 WPF Window를 비롯한 대부분 UI 컨트롤이 상속하는 FrameworkElement 클래스에 정의되어 있습니다. 다시 말해서, 바인딩의 기초를 지정할 수 있습니다.

DataContext 속성에 대한 기본 설정은 없습니다 (처음부터 null 값을 가짐). 그러나 DataContext는 컨트롤 계층을 통해 상속되므로 Window 자체에 DataContext를 설정한 다음 모든 자식 컨트롤에 사용할 수 있습니다. 간단한 예를 들어 설명해 보겠습니다.

```XAML
<Window x:Class="DataContext.MainWindow"
       xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="DataContextSample" Height="130" Width="280">
    <StackPanel Margin="15">
        <WrapPanel>
            <TextBlock Text="Window title:  " />
            <TextBox Text="{Binding Title, UpdateSourceTrigger=PropertyChanged}" Width="150" />
        </WrapPanel>
        <WrapPanel Margin="0,10,0,0">
            <TextBlock Text="Window dimensions: " />
            <TextBox Text="{Binding Width}" Width="50" />
            <TextBlock Text=" x " />
            <TextBox Text="{Binding Height}" Width="50" />
        </WrapPanel>
    </StackPanel>
</Window>
```

```C#
using System;
using System.Windows;

namespace DataContext
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
    }
}
```
![sampleImage](https://100.100.15.221/Kerry-cho/WPF-Example/blob/master/DataContext/Sample.png) 

이 예제에서는 코드에 흥미로운 딱 한 줄의 코드만 추가합니다. 표준 InitalizeComponent() 호출 후에 DataContext에 "this" 참조하도록 합니다. 이 참조는 기본적으로 데이터 컨텍스트가 Window 자체가 되기를 요구합니다.

XAML에서 이를 이용하여 Title, Width 및 Height 등의 여러 Window 속성에 바인딩합니다. 윈도우에는 자식 컨트롤에 전달되는 DataContext가 있으므로 각 바인딩에 공급자를 정의 할 필요가 없고, 전역으로 사용할 수있는 값들만 사용합니다.

예제를 실행하고 창 크기를 변경해 보십시오. 윈도우 사이즈 변경 사항이 textbox에 즉시 반영됩니다. 하지만 첫 번째 textbox에 다른 title을 쓰는 것을 시도해도, 이 사항은 즉시 반영되지 않는다는 점에 놀랄 것입니다. 대신 변경 사항을 적용하기 전에 다른 컨트롤로 포커스를 이동해야합니다. 왜 그럴까요? 이것은 다음 챕터의 주제입니다.

# Summary
DataContext 속성을 사용한다는 것은 컨트롤의 계층 구조를 통해 모든 바인딩의 기초를 설정하는 것과 같습니다. 이렇게 하므로써 각 바인딩에 대한 소스를 수동으로 정의하는 번거로움이 줄어들고, 일단 데이터 바인딩을 사용하기 시작하면, 확실히 시간과 타이핑을 절약할 수 있다는 점을 인정하게 될 것입니다.

그러나 이것은 Window 내의 모든 컨트롤에 대해 동일한 DataContext를 사용해야 한다는 것은 아닙니다. 각 컨트롤에는 고유한 DataContext 속성이 있으므로 쉽게 상속을 끊고 DataContext를 override 해서 새 값을 할당할 수 있습니다. 이렇게 하면 윈도우에 전역 DataContext가 있고, 로컬 영역, 예를 들면 라인 같은걸로 분리된 별도의 패널에는 DataContext가 따로 정의되어 있는 것처럼 작업할 수 있습니다.
