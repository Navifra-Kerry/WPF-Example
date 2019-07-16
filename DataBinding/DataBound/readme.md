# Hello, bound world!

이 튜토리얼을 고전적인 "Hello, world!"로 시작한 것처럼, WPF에서 
"Hello, bound world!"로 데이터 바인딩을 사용하는 것이 얼마나 쉬운지 예를 들어 보여줍니다. 
바로 예제를 보여드리고 설명은 그 뒤에 하겠습니다.

```XAML
<Window x:Class="DataBound.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="HelloBoundWorldSample" Height="110" Width="280">
    <StackPanel Margin="10">
        <TextBox Name="txtValue" />
        <WrapPanel Margin="0,10">
            <TextBlock Text="Value: " FontWeight="Bold" />
            <TextBlock Text="{Binding Path=Text, ElementName=txtValue}" />
        </WrapPanel>
    </StackPanel>
</Window>
```

* Sample EXE  
  
![sample](https://100.100.15.221/Kerry-cho/WPF-Example/blob/master/DataBinding/DataBound/Sample.png)

이 간단한 예제는 TextBox의 Text 속성이 TextBlock의 Text 값과 일치하도록 바인딩하는 방법을 보여줍니다.
스크린 샷에서 볼 수 있듯이 TextBox에 텍스트를 입력하면 TextBlock이 자동으로 업데이트 됩니다. 
바인딩되지 않았다면 TextBox의 텍스트가 변경될 때마다 이벤트를 수신 한 다음 TextBlock을 업데이트 해야 하지만, 
데이터 바인딩을 사용하면 마크업을 사용하여 연결할 수 있습니다.

# 바인딩 구문

모든 마법은 XAML에서 마크업을 캡슐화한 중괄호 사이에서 발생합니다. 
데이터 바인딩의 경우 Binding 확장을 사용하여 Text 속성의 바인딩 관계를 설명할 수 있습니다. 
가장 간단한 형식에서 바인딩은 다음과 같이 보일 수 있습니다.

```XAML
{Binding}
```
이것은 단순히 현재 데이터 컨텍스트를 반환합니다 (나중에 자세히 설명합니다). 
이것은 유용 할 수 있지만 보통 일반적인 상황에서는 데이터 컨텍스트의 다른 속성에 어떤 속성을 바인딩해야 합니다. 
이와 같은 바인딩은 다음과 같습니다.

```XAML
{Binding Path=속성이름}
```

Path는 바인딩 할 속성을 나타내는데, Path가 바인딩의 기본 속성이기 때문에 다음과 같이 생략 가능합니다.

```XAML
{Binding 속성이름}
```

Path가 명시적으로 정의된 경우와 생략된 많은 다른 예제를 볼 수 있습니다만, 
이것은 당신의 선택 사항일 뿐입니다.

바인딩에는 많은 다른 속성이 있는데, 그 중 하나는 이 예제에서 사용한 ElementName입니다. 
이를 통해 우리는 다른 UI 요소에 직접 연결할 수 있습니다. 바인딩 내에서 각 속성은 쉼표로 구분합니다.

```XAML
{Binding Path=Text, ElementName=txtValue}
```

# Summary

우리는 WPF의 바인딩 가능성에 대해 간단히 훑어 보았습니다. 
