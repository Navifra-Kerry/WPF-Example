# Value conversion with IValueConverter


지금까지는 데이터의 속성이 항상 호환되는 몇 가지 간단한 데이터 바인딩을 사용했습니다. 그러나 한 유형의 바운드 값을 사용하고 약간 다른 방식으로 표현하려는 경우가 있습니다.

# When to use a value converter

ValueConverter는 데이터 바인딩과 함께 매우 자주 사용됩니다. 다음은 몇 가지 기본 예입니다.

* 숫자 값이 있지만 한 값으로 0 값을 표시하고 다른 값으로 양수 값을 표시하려고합니다.
* 값을 기준으로 CheckBox를 확인하려고하지만이 값은 부울 값 대신 "예"또는 "아니요"와 같은 문자열입니다.
* 파일 크기는 바이트이지만 파일 크기에 따라 바이트, 킬로바이트, 메가 바이트 또는 기가 바이트로 표시하려고합니다.

이것들은 단순한 경우이지만, 더 많은 것들이 있습니다. 예를 들어 부울 값을 기준으로 확인란을 선택하고 값을 반대로하고 값이 false이면 CheckBox를 선택하고 값이 true인지 확인하지 않을 수 있습니다. 심지어 True를 나타내는 녹색 기호 또는 False를 나타내는 빨간색 기호와 같은 값을 기반으로 ImageSource에 대한 이미지를 생성하기 위해 변환기를 사용할 수도 있습니다. 가능성은 거의 끝이 없습니다!

이런 경우 ValueConverter를 사용할 수 있습니다. IValueConverter 인터페이스를 구현하는이 작은 클래스는 중개자 처럼 행동하고 소스와 대상 사이의 값을 변환합니다. 따라서 대상에 도달하기 전에 값을 변환해야하거나 소스로 다시 변환해야하는 상황에서는 ValueConverter가 필요할 것입니다.

# Implementing a simple value converter

앞에서 언급했듯이 WPF ValueConverter는 IValueConverter 인터페이스 또는 IMultiValueConverter 인터페이스를 구현해야합니다 (자세한 내용은 나중에 설명 함). 두 인터페이스 모두 Convert () 및 ConvertBack ()의 ​​두 가지 메서드 만 구현하면됩니다. 이름에서 알 수 있듯이이 메서드는 값을 대상 형식으로 변환 한 다음 다시 반환하는 데 사용됩니다.

```XAML
<Window x:Class="WPFExample.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
		xmlns:local="clr-namespace:WPFExample"
        Title="ConverterSample" Height="140" Width="250">
    <Window.Resources>
        <local:YesNoToBooleanConverter x:Key="YesNoToBooleanConverter" />
    </Window.Resources>
    <StackPanel Margin="10">
        <TextBox Name="txtValue" />
        <WrapPanel Margin="0,10">
            <TextBlock Text="Current value is: " />
            <TextBlock Text="{Binding ElementName=txtValue, Path=Text, Converter={StaticResource YesNoToBooleanConverter}}"></TextBlock>
        </WrapPanel>
        <CheckBox IsChecked="{Binding ElementName=txtValue, Path=Text, Converter={StaticResource YesNoToBooleanConverter}}" Content="Yes" />
    </StackPanel>
</Window>
```

```C#
using System;
using System.Windows;
using System.Windows.Data;

namespace WPFExample
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
    }

    public class YesNoToBooleanConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            switch (value.ToString().ToLower())
            {
                case "yes":
                case "oui":
                    return true;
                case "no":
                case "non":
                    return false;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool)
            {
                if ((bool)value == true)
                    return "yes";
                else
                    return "no";
            }
            return "no";
        }
    }
}
```

![sample](https://100.100.15.221/Kerry-cho/WPF-Example/blob/master/DataBinding/IValueConverter/Sample.png)

# Code-behind


이제 뒤에서부터 시작해서 예제를 통해 작업 해 봅시다. 우리는 YesNoToBooleanConverter라는 코드 소스 파일에 Converter를 구현했습니다. 이야기 한대로 Convert () 및 ConvertBack ()이라는 두 가지 필수 메서드 만 구현합니다. Convert () 메서드는 문자열을 입력 (value 매개 변수)으로받은 다음 fallback 값이 false 인 Boolean true 또는 false 값으로 변환한다고 가정합니다. 재미있게, 나는 프랑스어 단어에서도이 변환을 할 수있는 가능성을 추가했습니다.


ConvertBack () 메서드는 부울 형식의 입력 값을 가정 한 다음 폴백 값이 "no"인 영어 단어 "yes"또는 "no"를 반환합니다.

이 두 메소드가 취하는 추가 매개 변수에 대해 궁금해 할 수도 있지만이 예제에서는 필요하지 않습니다. 우리는 다음 장들 중 하나에서 사용하여 설명 할 것입니다.

# XAML

프로그램의 XAML 부분에서는 변환기의 인스턴스를 창의 리소스로 선언하는 것으로 시작합니다. 그런 다음 TextBox, 두 개의 TextBlocks 및 CheckBox 컨트롤이 있고 재미있는 일이 발생합니다. TextBox의 값을 TextBlock 및 CheckBox 컨트롤에 바인딩하고 Converter 속성과 자체 변환기 참조를 사용하여 필요에 따라 문자열과 부울 값 사이에서 값을 앞뒤로 움직입니다.

이 예제를 실행하려고하면 두 위치에서 값을 변경할 수 있습니다. TextBox에 "yes"를 쓰거나 false를 원하면 다른 값을 쓰거나 CheckBox를 선택합니다. 사용자가하는 일과 관계없이 변경 내용은 TextBlock뿐만 아니라 다른 컨트롤에도 반영됩니다.

# Summary

이것은 단순한 가치관의 예이며, 설명을 위해 필요한 것보다 조금 더 길게 만들어졌습니다. 다음 장에서는 좀 더 고급 예제를 살펴 보겠다.하지만 자신 만의 변환기를 작성하기 전에 WPF에 이미 포함되어 있는지 확인하고 싶을 것이다. 글을 쓰는 시점에서 20 가지 이상의 내장 변환기가 있지만 활용할 수는 있지만 이름을 알아야합니다. 나는 당신에게 도움이 될만한 다음 목록을 찾았습니다.

http://stackoverflow.com/questions/505397/built-in-wpf-ivalueconverters

