using lab1.utils;
using lab1.viewmodel;
using System.IO;
using System.Windows;

namespace lab1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            bool showStartupMessage = SettingsManager.LoadShowStartupMessageSetting();

            if (showStartupMessage)
            {
                Loaded += MainWindow_Loaded;
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Показываем всплывающее окно
            MainViewModel.ShowStartupInfo();
        }
    }
}