using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Threading;
using System.Threading.Tasks;

namespace InfoTest
{
    public partial class MainWindow : Window
    {
        double screenWidth = SystemParameters.PrimaryScreenWidth;
        double screenHeight = SystemParameters.PrimaryScreenHeight;

        double x;
        double y;

        bool bewegtSich = false;

        Random rand = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void neinKnopf_Click(object sender, RoutedEventArgs e)
        {
            if (bewegtSich)
            {
                bewegtSich = false;
                
            }
            MessageBox.Show("Du hast die richtige Wahl getroffen!" + Environment.NewLine + "Es geht weiter!");
            this.Close();
        }

        private void jaKnopf_Click(object sender, RoutedEventArgs e)
        {
            if (!bewegtSich)
            {
                bewegtSich = true;
                MoveWindow();
            }
        }   

        private async void MoveWindow()
        {
            while (bewegtSich)
            {               
                x = rand.NextDouble() * (screenWidth - this.Width);
                y = rand.NextDouble() * (screenHeight - this.Height);
                this.Left = x;
                this.Top = y;
               await Task.Delay(700);
            }
            
        }
    }
}