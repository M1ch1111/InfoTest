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

        private List<string> LoesungenNR = new List<string> { "1", "2", "4", "6", "5", "3" };

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

            dp.Interval = new TimeSpan(0, 0, 0, 1, 0);
            dp.Tick += rZaehler;
            dp.Start();
        }

        private void rZaehler(object? sender, EventArgs e)
        {
            zaehler++;
            if (zaehler == 1)
            {
                R1 = Visibility.Visible;
            }
            if (zaehler == 2)
            {
                R2 = Visibility.Visible;
            }
            if (zaehler == 3)
            {
                R4 = Visibility.Visible;
            }
            if (zaehler == 4)
            {
                R6 = Visibility.Visible;
            }
            if (zaehler == 5)
            {
                R5 = Visibility.Visible;
            }
            if (zaehler == 6)
            {
                R3 = Visibility.Visible;
                dp.Stop();
            }
        }

        [RelayCommand]
        private void rClick(string buttonNR)
        {
            string erwarteteNR = LoesungenNR[rSchritt];

            if (buttonNR == erwarteteNR)
            {
                rSchritt++;
                if (rSchritt >= LoesungenNR.Count)
                {
                    Window w = Application.Current.Windows.OfType<Window>().FirstOrDefault();
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
