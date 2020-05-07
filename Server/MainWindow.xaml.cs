using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Server.Model;

namespace Server
{
    public partial class MainWindow : Window
    {
        private bool _isLocked { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateCodes_OnClick(object sender, RoutedEventArgs e)
        {
            GenerateCodes.IsEnabled = false;
            var generateCodesTask = new Task(()=> CodeProcessing.GenerateDiscountCodes(1000));
            _isLocked = true;
            generateCodesTask.ContinueWith(x =>
            {
                _isLocked = false;
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    GenerateCodes.IsEnabled = true;
                }));
            });
            generateCodesTask.Start();
        }

        //prevent from closing before generating is over
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            while (_isLocked)
                Thread.Sleep(100);

            e.Cancel = false;
        }
    }
}
