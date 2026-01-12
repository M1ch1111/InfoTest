using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using System.Xml.Linq;

namespace InfoTest
{
    public partial class Level2ViewModel : ObservableObject
    {
        private int zaehler = 0;
        private int rSchritt = 0;

        private List<string> LoesungenNR = new List<string>();

        private DispatcherTimer dp = new DispatcherTimer();
        private string spielerName;

        [ObservableProperty] 
        private Visibility r1 = Visibility.Hidden;

        [ObservableProperty] 
        private Visibility r2 = Visibility.Hidden;

        [ObservableProperty] 
        private Visibility r3 = Visibility.Hidden;

        [ObservableProperty] 
        private Visibility r4 = Visibility.Hidden;

        [ObservableProperty] 
        private Visibility r5 = Visibility.Hidden;

        [ObservableProperty] 
        private Visibility r6 = Visibility.Hidden;

        public Level2ViewModel(string name)
        {
            spielerName = name;
            Speichern.SpeichereLevel(spielerName, 2);
            GeneriereZufallsLoesung();

            dp.Interval = new TimeSpan(0, 0, 0, 1, 0);
            dp.Tick += rZaehler;
            dp.Start();
        }

        private void GeneriereZufallsLoesung()
        {
            LoesungenNR.Clear();
            List<string> zahlen = new List<string> { "1", "2", "3", "4", "5", "6" };

            Random rnd = new Random();
            while (zahlen.Count > 0)
            {
                int zufallsIndex = rnd.Next(0, zahlen.Count);
                string gezogeneZahl = zahlen[zufallsIndex];
                LoesungenNR.Add(gezogeneZahl);
                zahlen.RemoveAt(zufallsIndex);
            }
            LoesungenNR.ForEach(z => MessageBox.Show(z));
        }

        private void rZaehler(object? sender, EventArgs e)
        {
            if (Convert.ToInt32(LoesungenNR[zaehler]) == 1)
            {
                R1 = Visibility.Visible;
            }
            if (Convert.ToInt32(LoesungenNR[zaehler]) == 2)
            {
                R2 = Visibility.Visible;
            }
            if (Convert.ToInt32(LoesungenNR[zaehler]) == 3)
            {
                R3 = Visibility.Visible;
            }
            if (Convert.ToInt32(LoesungenNR[zaehler]) == 4)
            {
                R4 = Visibility.Visible;
            }
            if (Convert.ToInt32(LoesungenNR[zaehler]) == 5)
            {
                R5 = Visibility.Visible;
            }
            if (Convert.ToInt32(LoesungenNR[zaehler]) == 6)
            {
                R6 = Visibility.Visible;
            }
            zaehler++;
            if(zaehler == 6)
            {
                dp.Stop();
            }
        }

        [RelayCommand]
        private void rClick(string buttonNR)
        {
            string erwarteteNR = LoesungenNR[rSchritt];
            Window w = Application.Current.Windows.OfType<Window>().FirstOrDefault();

            if (buttonNR == erwarteteNR)
            {
                rSchritt++;
                if (rSchritt >= LoesungenNR.Count)
                {
                    MessageBox.Show("Level geschafft!");
                    Level3 levelDrei = new Level3(spielerName);
                    levelDrei.Show();
                    w.Close();
                }
            }
            else
            {
                rSchritt = 0;
            }
        }
    }
}
