using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InfoTest
{
    public class Level4ViewModel
    {
        List<string> spieler = new List<string> { "Alex", "Bob", "Carlos", "David", "Emil", "Frederik", "Greta", "Hans", "Ingrid", "Jens", "Kasimir" };
        List<string> trainer = new List<string> { "Trainer A", "Trainer B", "Trainer C", "Trainer D" };

        public ObservableCollection<string> ListeSpieler { get; } = new();
        public ObservableCollection<string> ListeTrainer { get; } = new();

        public Level4ViewModel()
        {
            SpielerHinzufuegen();
            TrainerHinzufuegen();
        }

        private void SpielerHinzufuegen()
        {
            foreach (var name in spieler)
            {
                ListeSpieler.Add(name);
            }
        }

        private void TrainerHinzufuegen()
        {
            foreach (var name in trainer)
            {
                ListeTrainer.Add(name);
            }
        }
    }
}
