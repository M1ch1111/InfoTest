using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InfoTest
{
    public partial class Level4ViewModel : ObservableObject
    {
        private List<string> spieler = new List<string> { "Alex", "Bob", "Carlos", "David", "Emil", "Frederik", "Greta", "Hans", "Ingrid", "Jens", "Kasimir", "Luna", "Michi", "Nick", };
        private List<string> trainer = new List<string> { "Trainer A", "Trainer B", "Trainer C", "Trainer D", "Trainer E", "Trainer F", "Trainer G", "Trainer H", "Trainer I", "Trainer J" };

        [ObservableProperty]
        private string aktuellerTrainer = "Leer";

        [ObservableProperty]
        private string spielerName;

        public ObservableCollection<string> ListeSpieler { get; } = new();
        public ObservableCollection<string> ListeTrainer { get; } = new();
        public ObservableCollection<string> ListeTeam { get; } = new();

        public Level4ViewModel(string name)
        {
            SpielerName = name;
            Speichern.SpeichereLevel(SpielerName, 4);

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

        [RelayCommand]
        private void ToggleSpieler(string name)
        {
            if (ListeTeam.Contains(name))
            {
                ListeTeam.Remove(name); // War schon drin -> Raus
            }
            else
            {
                ListeTeam.Add(name);    // War noch nicht drin -> Rein
            }
        }

        [RelayCommand]
        private void ToggleTrainer(string neuerTrainer)
        {
            foreach (var t in trainer)
            {
                if (AktuellerTrainer == t)
                {
                    AktuellerTrainer = "kein Trainer"; // Rauswerfen!
                }
            }

            // 2. Den neuen Trainer hinzufügen
            AktuellerTrainer = neuerTrainer;
        }
    }
}
