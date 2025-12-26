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
    public partial class Level1 : Window
    {
        double screenWidth = SystemParameters.PrimaryScreenWidth;
        double screenHeight = SystemParameters.PrimaryScreenHeight;

        double x;
        double y;

        bool bewegtSich = false;
        bool richtung = true;
        bool hoehe = true;

        Random rnd = new Random();

        public Level1()
        {
            InitializeComponent();
        }

        private void neinKnopf_Click(object sender, RoutedEventArgs e)
        {
            if (!bewegtSich)
            {
                MessageBox.Show("Du hast die richtige Wahl getroffen!" + Environment.NewLine + "Es geht weiter!");
                this.Hide(); 
                Level2 levelZwei = new Level2();
                levelZwei.Show();
                this.Close();
            }
            if (bewegtSich)
            {
                bewegtSich = false;
                tB.Text = "Ich gebe dir noch eine Chance!";
            }
            
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
                //RandomWerte();
                tanzen();
                this.Left = x;
                this.Top = y;
                //await Task.Delay(700);
                await Task.Delay(1000/60);
            }
        }

        private void RandomWerte()
        {
            x = rnd.NextDouble() * (screenWidth - this.Width);
            y = rnd.NextDouble() * (screenHeight - this.Height);
        }

        private void tanzen()
        {
            if (x == (screenWidth - this.Width))
            {
                richtung = false;
            }
            if (x == 0)
            {
                richtung = true;
            }
            if (x < (screenWidth - this.Width) && richtung)
            {
                x += 10;
            }
            else if (x > 0 && !richtung)
            {
                x -= 10;
            }

            if (y == (screenHeight - this.Height))
            {
                hoehe = false;
            }
            if (y == 0)
            {
                hoehe = true;
            }
            if (y < (screenHeight - this.Height) && hoehe)
            {
                y += 10;
            }
            else if (y > 0 && !hoehe)
            {
                y -= 10;
            }
        }
    }
}