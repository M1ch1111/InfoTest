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
        private List<string> spieler = new List<string> { "Michi", "Ingrid", "David", "Quinn", "Kasimir", "Bob", "Greta", "Frederik", "Rolf", "Alex", "Susie", "Otto", "Jens", "Carlos", "Paul", "Luna", "Emil", "Hans", "Nick"};
        private List<string> trainer = new List<string> { "Trainer A", "Trainer B"};

        [ObservableProperty]
        private string aktuellerTrainer = "Leer";

        [ObservableProperty]
        private string spielerName;

        public ObservableCollection<string> ListeSpieler { get; } = new();
        public ObservableCollection<string> ListeTrainer { get; } = new();
        public ObservableCollection<string> ListeTeam { get; } = new();
        public ObservableCollection<string> FehlerListe { get; } = new();

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
                ListeTeam.Remove(name);
            }
            else
            {
                ListeTeam.Add(name);
            }
            PruefeRegeln();
        }

        [RelayCommand]
        private void ToggleTrainer(string neuerTrainer)
        {
            foreach (var t in trainer)
            {
                if (AktuellerTrainer == t)
                {
                    AktuellerTrainer = "kein Trainer";
                }
            }
            AktuellerTrainer = neuerTrainer;

            PruefeRegeln();
        }

        [RelayCommand]
        private void BestaetigenKnopf_Click()
        {
            PruefeGewonnen();
        }

        private void PruefeGewonnen()
        {
            if (FehlerListe.Count == 0 && AktuellerTrainer != "Leer" && ListeTeam.Count >= 5)
            {
                MessageBox.Show("Glückwunsch! Du hast das Level geschafft!");
            }
            else
            {
                MessageBox.Show("Du hast die Bedingungen noch nicht erfüllt. Schau dir die Fehlerliste an!");
            }
        }

        private void PruefeRegeln()
        {
            FehlerListe.Clear();
            if (AktuellerTrainer == "Leer" && ListeTeam.Count > 0)
            {
                FehlerListe.Add("Achtung: Du hast Spieler gewählt, aber noch keinen Trainer!");
                return;
            }

            if (ListeTeam.Count < 5)
            {
                FehlerListe.Add("Du brauchst mindestens 5 Spieler im Team!");
            }

            switch (AktuellerTrainer)
            {
                case "Trainer A":
                { 
                    foreach (var spieler in ListeTeam)
                    {
                        if (spieler.Length > 5)
                        {
                            FehlerListe.Add($"Trainer A: '{spieler}' ist zu lang. Den kann ich mir nicht merken");
                        }
                    }
                    for (int i = 1; i < ListeTeam.Count; i++)
                    {
                        string vorheriger = ListeTeam[i - 1];
                        string aktueller = ListeTeam[i];

                        if (string.Compare(vorheriger, aktueller) > 0)
                        {
                            FehlerListe.Add("Trainer A: Unordentlich! Sortieren!"); //Alphabetische Reihenfolge
                        }
                    }
                    break;
                }

                case "Trainer B":
                { 
                    foreach (var spieler in ListeTeam)
                    {
                        if (spieler.ToLower().Contains('e'))
                            FehlerListe.Add("Trainer B: Ein Buchstabe gefällt mir nicht!");
                    }

                    bool hatPalindrom = false;
                    foreach (var spieler in ListeTeam)
                    {
                        string nameKlein = spieler.ToLower();
                        char[] charArray = nameKlein.ToCharArray();
                        Array.Reverse(charArray);
                        string rueckwaerts = new string(charArray);

                        if (nameKlein == rueckwaerts) 
                        {
                            hatPalindrom = true;
                        }    
                    }
                    if (!hatPalindrom && ListeTeam.Count > 0)
                    {
                        FehlerListe.Add("Trainer B: Wir brauchen einen mit Gleichgewicht im Team."); //Palindrom
                    }

                    foreach (var spieler in ListeTeam)
                    {
                        if (spieler.ToLower().EndsWith("n"))
                        {
                            FehlerListe.Add($"Trainer B: '{spieler}' endet auf 'n'. Grrrr");
                        }
                    }
                    break;
                }
            }
        }
    }
}
