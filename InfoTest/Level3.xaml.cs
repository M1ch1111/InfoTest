using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
    /// Interaktionslogik für Level3.xaml
    /// </summary>
    public partial class Level3 : Window, INotifyPropertyChanged
    {
        private string appText = "SpielFenster";
        private double Timer = 30;
        private double StartTimer = 3;
        private string buttonText = "Start";
        private string textText = "Vrsuche dich nicht vom Fenster fangen zu lassen!";
        private DispatcherTimer fangTimer = new DispatcherTimer();
        private DispatcherTimer spieltimer = new DispatcherTimer();
        private DispatcherTimer starttimer = new DispatcherTimer();


        public string AppText
        {
            get { return appText; }
            set
            {
                appText = value;
                OnPropertyChanged(nameof(AppText));
            }
        }

        public string TextText
        {
            get { return textText; }
            set
            {
                textText = value;
                OnPropertyChanged(nameof(TextText));
            }
        }

        public string ButtonText
        {
            get { return buttonText; }
            set
            {
                buttonText = value;
                OnPropertyChanged(nameof(ButtonText));
            }
        }

        public Level3()
        {
            InitializeComponent();
            this.DataContext = this;

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
                AppText = "Geschafft! Du hast überlebt.";
                OnPropertyChanged(nameof(AppText));
            }
            else
            {
                Timer--;
                buttonText = Convert.ToString(Timer);
                OnPropertyChanged(nameof(ButtonText));
            }
        }

        private void StartKnopf_Click(object sender, RoutedEventArgs e)
        {
            AppText = "Überlebe!";
            OnPropertyChanged(nameof(AppText));
            //Faenger.VerticalAlignment = 0;
            //Faenger.HorizontalAlignment = 0;
            
            starttimer.Start();
            StartKnopf.IsEnabled = false;
        }

        private void Fangen_Tick(object? sender, EventArgs e)
        {
            //Point mousePos = Mouse.GetPosition(Anwendung);
            double mausX = Mouse.GetPosition(Anwendung).X;
            double mausY = Mouse.GetPosition(Anwendung).Y;

            double fensterX = this.ActualWidth / 2;
            double fensterY = this.ActualHeight / 2;

            double vektorX = mausX - fensterX;
            double vektorY = mausY - fensterY;

            double FaengerGeschwindigkeit = 0.02;

            MoveTransform.X += (vektorX - MoveTransform.X) * FaengerGeschwindigkeit;
            MoveTransform.Y += (vektorY - MoveTransform.Y) * FaengerGeschwindigkeit;

            double faengerMitteX = fensterX + MoveTransform.X;
            double faengerMitteY = fensterY + MoveTransform.Y;

            double abstandX = Math.Abs(mausX - faengerMitteX);
            double abstandY = Math.Abs(mausY - faengerMitteY);

            if (abstandX < (Faenger.ActualWidth / 2 - 10) &&
        abstandY < (Faenger.ActualHeight / 2 - 10))
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
            OnPropertyChanged(nameof(AppText));
            OnPropertyChanged(nameof(TextText));
            OnPropertyChanged(nameof(ButtonText));

            StartKnopf.IsEnabled = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
