using System;
using System.Threading.Tasks;
using System.Windows;
using Server.Model;

namespace Server
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateCodes_OnClick(object sender, RoutedEventArgs e)
        {
            GenerateCodes.IsEnabled = false;
            var generateCodesTask = new Task(()=> CodeProcessing.GenerateDiscountCodes(1000));
            generateCodesTask.ContinueWith(x =>
            {
                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    GenerateCodes.IsEnabled = true;
                }));
            });
            generateCodesTask.Start();
        }
    }
}
