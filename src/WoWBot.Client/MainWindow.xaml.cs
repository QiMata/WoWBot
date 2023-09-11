using System;
using System.Windows;

namespace WoWBot.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Dispose()
        {
            this.Close();
        }
    }
}
