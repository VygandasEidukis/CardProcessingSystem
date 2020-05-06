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
            var generateCodesTask = new Task(()=> CodeProcessing.GenerateDiscountCodes(1000));
            generateCodesTask.Start();
        }
    }
}
