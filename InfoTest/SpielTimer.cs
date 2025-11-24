using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace InfoTest
{
    internal class SpielTimer : INotifyPropertyChanged
    {
        private DispatcherTimer timer;

        public event EventHandler ZeitVorbei;

        private int _restzeit;
        public int Restzeit
        {
            get { return _restzeit; }
            private set 
            {
                _restzeit = value;
                OnPropertyChanged("Restzeit");
            }
        }

        public SpielTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
        }

        public void Starten(int sekunden)
        {
            Restzeit = sekunden;
            timer.Start();
        }

        public void Stoppen()
        {
            timer.Stop();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Restzeit--;

            if (Restzeit <= 0)
            {
                Stoppen();
                ZeitVorbei?.Invoke(this, EventArgs.Empty);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
