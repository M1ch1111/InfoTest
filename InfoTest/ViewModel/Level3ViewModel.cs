using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Xml.Linq;

namespace InfoTest
{
    public partial class Level3ViewModel : ObservableObject
    {
        private double Timer = 30;
        private double StartTimer = 3;

        private double geschwindigkeitX = 0;
        private double geschwindigkeitY = 0;

        private bool levelGeschafft = false;

        private DispatcherTimer fangTimer = new DispatcherTimer();
        private DispatcherTimer spieltimer = new DispatcherTimer();
        private DispatcherTimer starttimer = new DispatcherTimer();
        private FrameworkElement anwendung;

        [ObservableProperty]
        private string appText = "SpielFenster";

        [ObservableProperty]
        private string textText = "Versuche dich nicht vom Fenster fangen zu lassen!"; 

        [ObservableProperty]
        private string buttonText = "Start";

        [ObservableProperty]
        private bool startKnopfEnabled = true;

        [ObservableProperty]
        private double bewegeX = 0;

        [ObservableProperty]
        private double bewegeY = 0;

        [ObservableProperty]
        private double faengerWidth = 400;

        [ObservableProperty]
        private double faengerHeight = 250;

        [ObservableProperty]
        private string spielerName;

        public Level3ViewModel(string name)
        {
            SpielerName = name;
            Speichern.SpeichereLevel(SpielerName, 3);

            fangTimer.Interval = new TimeSpan(0, 0, 0, 0, 15);
            fangTimer.Tick += Fangen_Tick;

            spieltimer.Interval = new TimeSpan(0, 0, 1);
            spieltimer.Tick += timer_Tick;

            starttimer.Interval = new TimeSpan(0, 0, 1);
            starttimer.Tick += starttimer_Tick;
        }

        private void starttimer_Tick(object? sender, EventArgs e)
        {
            StartTimer--;
            TextText = "Los geht's in " + Convert.ToString(StartTimer) + " Sekunden";
            if (StartTimer == 0)
            {
                starttimer.Stop();
                TextText = "Los geht's!";
                OnPropertyChanged(nameof(TextText));
                spieltimer.Start();
                fangTimer.Start();
            }
        }

        private void timer_Tick(object? sender, EventArgs e)
        {
            if (Timer == 0)
            {
                fangTimer.Stop();
                spieltimer.Stop();
                AppText = "Super! Du kannst das Fenster schließen";
                levelGeschafft = true;
            }
            else
            {
                Timer--;
                ButtonText = Convert.ToString(Timer);
            }
        }

        [RelayCommand]
        private void StartKnopf_Click(FrameworkElement view)
        {
            anwendung = view;
            AppText = "Überlebe!";

            starttimer.Start();
            StartKnopfEnabled = false;
        }

        [RelayCommand]
        private void Beenden_Click(FrameworkElement element)
        {
            Window w = Window.GetWindow(element);

            if (w == null) return;
            if (levelGeschafft)
            {
                Level4 l4 = new Level4(SpielerName);
                l4.Show();
                w.Close();
            }
            else
            {
                BewegeX = 0;
                BewegeY = 0;
            }
        }

        [RelayCommand]
        private void Groß_Click() 
        {
            if(FaengerWidth < 500 && FaengerHeight < 350)
            {
                FaengerWidth += 10;
                FaengerHeight += 10;
            }
        }

        [RelayCommand]
        private void Klein_Click()
        {
            if(FaengerWidth > 300 && FaengerHeight > 150)
            {
                FaengerWidth -= 10;
                FaengerHeight -= 10;
            }
        }

        private void Fangen_Tick(object? sender, EventArgs e)
        {
            double mausX = Mouse.GetPosition(anwendung).X;
            double mausY = Mouse.GetPosition(anwendung).Y;

            double fensterMitteX = anwendung.ActualWidth / 2;
            double fensterMitteY = anwendung.ActualHeight / 2;

            double faengerAktuellX = fensterMitteX + BewegeX;
            double faengerAktuellY = fensterMitteY + BewegeY;

            double beschleunigung = 0.5;
            double reibung = 0.96;       // Werte von 90 - 99

            double richtungX = mausX - faengerAktuellX;
            double richtungY = mausY - faengerAktuellY;

            double abstand = Math.Sqrt(richtungX * richtungX + richtungY * richtungY);

            if (abstand > 0)
            {
                richtungX /= abstand;
                richtungY /= abstand;

                geschwindigkeitX += richtungX * beschleunigung;
                geschwindigkeitY += richtungY * beschleunigung;
            }
            geschwindigkeitX *= reibung;
            geschwindigkeitY *= reibung;
            BewegeX += geschwindigkeitX;
            BewegeY += geschwindigkeitY;

            double faengerNeuX = fensterMitteX + BewegeX;
            double faengerNeuY = fensterMitteY + BewegeY;

            double abstandX = Math.Abs(mausX - faengerNeuX);
            double abstandY = Math.Abs(mausY - faengerNeuY);

            if (abstandX < (FaengerWidth / 2 - 10) && abstandY < (FaengerHeight / 2 - 10))
            {
                GameOver();
            }
        }

        public void GameOver()
        {
            fangTimer.Stop();
            spieltimer.Stop();
            Timer = 30;
            StartTimer = 3;
            AppText = "Verloren! Das Fenster hat dich gefangen.";
            TextText = "Verloren! Das Fenster hat dich gefangen.";
            ButtonText = "Neustart";
            StartKnopfEnabled = true;
        }
    }
}
