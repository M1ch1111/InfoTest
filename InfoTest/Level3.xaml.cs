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

namespace InfoTest
{
    /// <summary>
    /// Interaktionslogik für Level3.xaml
    /// </summary>
    public partial class Level3 : Window, INotifyPropertyChanged
    {
        private string appText = "SpielFenster";
        public string AppText
        {
            get { return appText; }
            set
            {
                appText = value;
                OnPropertyChanged(nameof(AppText));
            }
        }
        public Level3()
        {
            InitializeComponent();
            this.DataContext = this;


        }

        private void StartKnopf_Click(object sender, RoutedEventArgs e)
        {
            AppText = "Überlebe!";
            OnPropertyChanged(nameof(AppText));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
