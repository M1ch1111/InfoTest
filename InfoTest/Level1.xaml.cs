using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
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
    /// Interaktionslogik für Level1.xaml
    /// </summary>
    public partial class Level1 : Window , INotifyPropertyChanged
    {
        private string _TextContent;
        private string _ButtonContent;
        private SpielTimer timer = new SpielTimer();
        private DispatcherTimer bewegung = new DispatcherTimer();
        
        public string TextContent 
        {
            get { return _TextContent; } 
            set 
            { 
                _TextContent = value; 
                OnPropertyChanged(nameof(TextContent));
            }
        }
        public string ButtonContent
        {
            get { return _ButtonContent; }
            set
            {
                _ButtonContent = value;
                OnPropertyChanged(nameof(ButtonContent));
            }
        }

        public Level1()
        {
            InitializeComponent();
            TextContent = "Du musst überleben."+ Environment.NewLine + "Lasse das Fenster nicht deine Maus berühren!";
            ButtonContent = "Start";

            this.DataContext = this;

            timer.PropertyChanged += SpielTimer_PropertyChanged;

            bewegung.Interval = TimeSpan.FromMilliseconds(20);
            bewegung.Tick += (s, e) => verfolgeMaus();
        }

        private void SpielTimer_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Restzeit")
            {
                ButtonContent = timer.Restzeit.ToString();

                if (timer.Restzeit <= 0)
                {
                    bewegung.Stop(); 
                    this.MouseEnter -= Level1_MouseEnter; 
                    TextContent = "GEWONNEN! Du hast überlebt.";
                    this.Background = System.Windows.Media.Brushes.LightGreen;
                    timerButton.IsEnabled = true; 
                }
            }
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            TextContent = "Überlebe";
            this.Background = System.Windows.Media.Brushes.White;
            Level1Spiel();
        }

        public void Level1Spiel()
        {
            timerButton.IsEnabled = false;
            timer.Starten(30);
            OnPropertyChanged(nameof(ButtonContent));
            ButtonContent = Convert.ToString(timer.Restzeit);

            this.MouseEnter += Level1_MouseEnter;

            bewegung.Start();
        }

        private void Level1_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            // Hier sofort Game Over auslösen
            TextContent = "Verloren! Du hast das Fenster berührt.";
            timer.Stoppen();
            bewegung.Stop();

            // Optional: Fenster rot färben als Feedback
            this.Background = System.Windows.Media.Brushes.Red;
        }

        private void verfolgeMaus()
        {
            // Hole die globale Position direkt von Windows
            Point globalMouse = MausHelfer.GetPosition();

            // Wo ist mein Fenster?
            double fensterMitteX = this.Left + (this.ActualWidth / 2);
            double fensterMitteY = this.Top + (this.ActualHeight / 2);

            // Vergleich
            if (globalMouse.X > fensterMitteX) this.Left += 5;
            if (globalMouse.X < fensterMitteX) this.Left -= 5;

            if (globalMouse.Y > fensterMitteY) this.Top += 5;
            if (globalMouse.Y < fensterMitteY) this.Top -= 5;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public static class MausHelfer
        {
            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            private static extern bool GetCursorPos(ref Win32Point pt);

            [StructLayout(LayoutKind.Sequential)]
            private struct Win32Point { public int X; public int Y; }

            // Diese Methode rufst du auf!
            public static Point GetPosition()
            {
                Win32Point w32Mouse = new Win32Point();
                GetCursorPos(ref w32Mouse);
                return new Point(w32Mouse.X, w32Mouse.Y);
            }
        }
    }
}
