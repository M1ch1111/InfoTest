using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace InfoTest
{
    public partial class Level3ViewModel : ObservableObject
    {
        //private string appText = "SpielFenster";
        private double Timer = 30;
        private double StartTimer = 3;
        //private string buttonText = "Start";
        //private string textText = "Vrsuche dich nicht vom Fenster fangen zu lassen!";
        private DispatcherTimer fangTimer = new DispatcherTimer();
        private DispatcherTimer spieltimer = new DispatcherTimer();
        private DispatcherTimer starttimer = new DispatcherTimer();
        private FrameworkElement anwendung;


        [ObservableProperty]
        private string appText = "SpielFenster";

        [ObservableProperty]
        private string textText = "Versuche dich nicht vom Fenster fangen zu lassen!"; // Tippfehler korrigiert ;)

        [ObservableProperty]
        private string buttonText = "Start";

        [ObservableProperty]
        private bool startKnopfEnabled = true;

        [ObservableProperty]
        private double moveX = 0;

        [ObservableProperty]
        private double moveY = 0;

        [ObservableProperty]
        private double faengerWidth = 400;

        [ObservableProperty]
        private double faengerHeight = 250;

        public Level3ViewModel()
        {
            //InitializeComponent();
            //this.DataContext = this;

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
            if (StartTimer == 0)
            {
                starttimer.Stop();
                TextText = "Los geht's!";
                OnPropertyChanged(nameof(textText));
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
                appText = "Geschafft! Du hast überlebt.";
            }
            else
            {
                Timer--;
                buttonText = Convert.ToString(Timer);
            }
        }

        [RelayCommand]
        private void StartKnopf_Click(FrameworkElement view)
        {
            anwendung = view;
            AppText = "Überlebe!";
            //Faenger.VerticalAlignment = 0;
            //Faenger.HorizontalAlignment = 0;

            starttimer.Start();
            startKnopfEnabled = false;
        }

        private void Fangen_Tick(object? sender, EventArgs e)
        {
            //Point mousePos = Mouse.GetPosition(Anwendung);
            double mausX = Mouse.GetPosition(anwendung).X;
            double mausY = Mouse.GetPosition(anwendung).Y;

            double fensterX = anwendung.ActualWidth / 2;
            double fensterY = anwendung.ActualHeight / 2;

            double vektorX = mausX - fensterX;
            double vektorY = mausY - fensterY;

            double FaengerGeschwindigkeit = 0.02;

            MoveX += (vektorX - MoveX) * FaengerGeschwindigkeit;
            MoveY += (vektorY - MoveY) * FaengerGeschwindigkeit;

            double faengerMitteX = fensterX + MoveX;
            double faengerMitteY = fensterY + MoveY;

            double abstandX = Math.Abs(mausX - faengerMitteX);
            double abstandY = Math.Abs(mausY - faengerMitteY);

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



            startKnopfEnabled = true;
        }
    }
}
