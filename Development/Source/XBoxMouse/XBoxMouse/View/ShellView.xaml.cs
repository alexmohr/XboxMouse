using System;
using System.Windows;
using XBoxMouse.ViewModel;

namespace XBoxMouse.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ShellViewModel viewModel = new ShellViewModel();

            DataContext = viewModel;
             viewModel.Init();
        }
    }
}
