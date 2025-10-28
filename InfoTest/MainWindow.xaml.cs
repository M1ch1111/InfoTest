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

namespace InfoTest
{
    public partial class MainWindow : Window
    {
        private double x = 3.5;
        private double y = 3;
        private bool _isMoving = false;
        private Thread? _movementThread;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void neinKnopf_Click(object sender, RoutedEventArgs e)
        {
            if (_isMoving)
            {
                _isMoving = false;
                _movementThread?.Join();
                neinKnopf.Content = "Nein";
            }
            else
            {
                MessageBox.Show("Bravo du hast die Aufgabe gelöst!" + Environment.NewLine + "Auf ins nächste Rätsel");
            }
        }

        private void jaKnopf_Click(object sender, RoutedEventArgs e)
        {
            if (!_isMoving)
            {
                _isMoving = true;
                _movementThread = new Thread(MoveWindow);
                _movementThread.IsBackground = true;
                _movementThread.Start();
                neinKnopf.Content = "NEIN! Stop";
            }
        }

        private void MoveWindow()
        {
            while (_isMoving)
            {
                Dispatcher.Invoke(() =>
                {
                    Left += x;
                    Top += y;

                    var screen = SystemParameters.WorkArea;

                    if (Left <= screen.Left || Left + Width >= screen.Right)
                        x = -x;

                    if (Top <= screen.Top || Top + Height >= screen.Bottom)
                        y = -y;
                });

                Thread.Sleep(10);
            }
        }
    }
}