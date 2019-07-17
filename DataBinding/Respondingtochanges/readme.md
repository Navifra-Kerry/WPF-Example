# Responding to changes

지금까지이 튜토리얼에서는 UI 요소와 기존 클래스 간의 바인딩을 주로 만들었지 만 실제 애플리케이션에서는 분명히 자신의 데이터 객체에 바인딩됩니다. 이것도 마찬가지로 쉽지만 일을 시작하면 실망스러운 것을 발견 할 수 있습니다. 변경 사항은 이전 예와 같이 자동으로 반영되지 않습니다. 이 예제에서 배우게 될 것이지만, WPF를 사용하면이 작업을 약간의 추가 작업으로 수행 할 수 있습니다. 그러나 다행히도 WPF를 사용하면이 작업을 매우 쉽게 처리 할 수 ​​있습니다.


데이터 원본 변경 사항을 처리 할 때 처리해야 할 수도 있고 그렇지 않을 수도있는 두 가지 시나리오가 있습니다. 항목 목록 변경 및 각 데이터 개체의 바운드 속성 변경. 어떻게 처리하는지는 당신이하고있는 일과 수행하고자하는 바에 따라 다를 수 있지만, WPF는 두 가지 매우 쉬운 솔루션 인 ObservableCollection과 INotifyPropertyChanged 인터페이스를 제공합니다.

# 다음 예제는 우리가 왜 이 두가지가 필요 한지 보여 줍니다.

```XAML
<Window x:Class="Respondingtochanges.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="ChangeNotificationSample" Height="150" Width="300">
	<DockPanel Margin="10">
		<StackPanel DockPanel.Dock="Right" Margin="10,0,0,0">
			<Button Name="btnAddUser" Click="btnAddUser_Click">Add user</Button>
			<Button Name="btnChangeUser" Click="btnChangeUser_Click" Margin="0,5">Change user</Button>
			<Button Name="btnDeleteUser" Click="btnDeleteUser_Click">Delete user</Button>
		</StackPanel>
		<ListBox Name="lbUsers" DisplayMemberPath="Name"></ListBox>
	</DockPanel>
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
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Respondingtochanges
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<User> users = new List<User>();

        public MainWindow()
        {
            InitializeComponent();

            users.Add(new User() { Name = "John Doe" });
            users.Add(new User() { Name = "Jane Doe" });

            lbUsers.ItemsSource = users;
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            users.Add(new User() { Name = "New user" });
        }

        private void btnChangeUser_Click(object sender, RoutedEventArgs e)
        {
            if (lbUsers.SelectedItem != null)
                (lbUsers.SelectedItem as User).Name = "Random Name";
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (lbUsers.SelectedItem != null)
                users.Remove(lbUsers.SelectedItem as User);
        }
    }

	public class User
	{
		public string Name { get; set; }
	}

}
```

![sample](https://100.100.15.221/Kerry-cho/WPF-Example/blob/master/DataBinding/Respondingtochanges/Sample.png)

직접 실행 해보고 목록에 항목을 추가하거나 사용자 중 하나의 이름을 변경하더라도 UI의 아무 것도 업데이트되지 않습니다. 이 예제는 사용자 이름을 유지하는 User 클래스, 목록 상자를 표시하는 ListBox 및 목록과 내용을 조작하는 일부 버튼을 포함하여 매우 간단합니다. 목록의 ItemsSource는 창 생성자에서 생성 한 몇 명의 사용자의 빠른 목록에 할당됩니다. 문제는 아무 버튼도 작동하지 않는 것입니다. 두 가지 쉬운 단계로 해결해 보겠습니다.

# Reflecting changes in the list data source

첫 번째 단계는 사용자를 추가하거나 삭제할 때처럼 목록 소스 (ItemsSource)의 변경 사항에 UI가 응답하도록하는 것입니다. 우리가 필요로하는 것은 콘텐츠의 변경 대상을 알려주는 목록이며, 다행스럽게도 WPF는이를 수행 할 수있는 유형의 목록을 제공합니다. 그것은 ObservableCollection이라고 불리며, 약간의 차이점을 제외하고는 일반적인 List <T>와 매우 비슷하게 사용합니다.

아래에서 찾을 수있는 마지막 예제에서는 List <User>를 ObservableCollection <User>로 간단하게 대체했습니다. 이렇게하면 추가 및 삭제 버튼이 작동하지만 "변경 이름"버튼에는 아무런 변화가 없습니다. 변경 사항은 소스 목록이 아닌 바운드 데이터 객체 자체에서 발생하므로 두 번째 단계는 해당 시나리오를 처리합니다 .

# Reflecting changes in the data objects

두 번째 단계는 사용자 정의 사용자 클래스가 INotifyPropertyChanged 인터페이스를 구현하도록하는 것입니다. 이를 통해 User 객체는 속성에 대한 변경 사항을 UI 레이어에 경고 할 수 있습니다. 위와 같이 목록 유형을 변경하는 것보다 약간 성가 시지만이 자동 업데이트를 수행하는 가장 간단한 방법 중 하나입니다.

# The final and working example

위에서 설명한 두 가지 변경 사항을 통해 이제 데이터 소스의 변경 사항을 반영하는 예가 나타납니다. 다음과 같이 보입니다.

```XAML
<Window x:Class="Respondingtochanges.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="ChangeNotificationSample" Height="135" Width="300">
    <DockPanel Margin="10">
        <StackPanel DockPanel.Dock="Right" Margin="10,0,0,0">
            <Button Name="btnAddUser" Click="btnAddUser_Click">Add user</Button>
            <Button Name="btnChangeUser" Click="btnChangeUser_Click" Margin="0,5">Change user</Button>
            <Button Name="btnDeleteUser" Click="btnDeleteUser_Click">Delete user</Button>
        </StackPanel>
        <ListBox Name="lbUsers" DisplayMemberPath="Name"></ListBox>
    </DockPanel>
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
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Respondingtochanges
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<User> users = new ObservableCollection<User>();

        public MainWindow()
        {
            InitializeComponent();

            users.Add(new User() { Name = "John Doe" });
            users.Add(new User() { Name = "Jane Doe" });

            lbUsers.ItemsSource = users;
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            users.Add(new User() { Name = "New user" });
        }

        private void btnChangeUser_Click(object sender, RoutedEventArgs e)
        {
            if (lbUsers.SelectedItem != null)
                (lbUsers.SelectedItem as User).Name = "Random Name";
        }

        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (lbUsers.SelectedItem != null)
                users.Remove(lbUsers.SelectedItem as User);
        }
    }

    public class User : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get { return this.name; }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    this.NotifyPropertyChanged("Name");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
    }
}
```

![sample2](https://100.100.15.221/Kerry-cho/WPF-Example/blob/master/DataBinding/Respondingtochanges/Sample.png)

# Summary

보시다시피 INotifyPropertyChanged를 구현하는 것은 매우 쉽지만 클래스에 약간의 추가 코드를 작성하고 속성에 약간의 추가 로직을 추가합니다. 이것은 자신의 클래스에 바인딩하고 변경 사항이 UI에 즉시 반영되도록하려는 경우 지불해야하는 가격입니다. 분명히 바인드 한 속성의 setter에서 NotifyPropertyChanged를 호출하면됩니다. 나머지는 그대로 유지할 수 있습니다.

ObservableCollection은 처리하기가 매우 쉽습니다. 바인딩 대상에 반영된 소스 목록을 변경하려는 상황에서이 특정 목록 유형을 사용하기 만하면됩니다.
