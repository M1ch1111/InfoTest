using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace InfoTest
{
    /// <summary>
    /// Interaktionslogik für Level2.xaml
    /// </summary>
    public partial class Level2 : Window
    {
        private int zaehler = 0;

        private List<Button> Loesungen;
        private int rSchritt = 0;

        DispatcherTimer dp = new DispatcherTimer();

        public Level2()
        {
            InitializeComponent();


            
            dp.Interval = new TimeSpan(0, 0, 0, 1, 0);
            dp.Tick += rZaehler;
            dp.Start();

            Loesungen = new List<Button> { r1, r2, r4, r6, r5, r3 };
            
        }

        private void rZaehler(object? sender, EventArgs e)
        {
            zaehler++;
            if (zaehler == 1)
            {
                r1.Visibility = Visibility.Visible;
            }
            if (zaehler == 2)
            {
                r2.Visibility = Visibility.Visible;
            }
            if (zaehler == 3)
            {
                r4.Visibility = Visibility.Visible;
            }
            if (zaehler == 4)
            {
                r6.Visibility = Visibility.Visible;
            }
            if (zaehler == 5)
            {
                r5.Visibility = Visibility.Visible;
            }
            if (zaehler == 6)
            {
                r3.Visibility = Visibility.Visible;
                dp.Stop();
            }
        }
        private void r_Click(object sender, RoutedEventArgs e)
        {
            //if(sender == null) return; 
            Button gedrueckterKnopf = sender as Button;
            //if (gedrueckterKnopf == null) return;

            if (gedrueckterKnopf == Loesungen[rSchritt])
            {
                rSchritt++;
                //gedrueckterKnopf.Visibility = Visibility.Hidden;
                if (rSchritt >= Loesungen.Count)
                {
                    //LevelGeschafft();
                    MessageBox.Show("Level geschafft!");
                    this.Close();
                }
            }
            else
            {
                rSchritt = 0;
            }
        }
    }
}

