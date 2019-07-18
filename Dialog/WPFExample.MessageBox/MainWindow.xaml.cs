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
