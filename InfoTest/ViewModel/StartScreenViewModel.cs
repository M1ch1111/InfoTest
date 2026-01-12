using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InfoTest
{
    public partial class StartScreenViewModel : ObservableValidator
    {
        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Der Name darf nicht leer sein.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Keine Sonderzeichen oder Leerzeichen erlaubt!")] //Entnommen aus StackOverflow
        [NotifyCanExecuteChangedFor(nameof(StartKnopf_ClickCommand))]
        private string neuerSpielerName = ""; 

        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "Der Name darf nicht leer sein.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Keine Sonderzeichen oder Leerzeichen erlaubt!")] //Entnommen aus StackOverflow
        [NotifyCanExecuteChangedFor(nameof(LadeKnopf_ClickCommand))]
        private string savegameName = "";
        private int logoKlickZaehler = 0;

        public StartScreenViewModel()
        {
            ValidateAllProperties();
        }

        [RelayCommand(CanExecute = nameof(KannStarten))]
        private void StartKnopf_Click(Window w)
        {
            Speichern.SpeichereLevel(NeuerSpielerName, 1);

            Level1 l1 = new Level1(NeuerSpielerName);
            l1.Show();
            w.Close();
        }

        private bool KannStarten()
        {
            return !GetErrors(nameof(NeuerSpielerName)).Any() && !string.IsNullOrEmpty(NeuerSpielerName);
        }

        [RelayCommand(CanExecute = nameof(Ladbar))]
        private void LadeKnopf_Click(Window w)
        {
            int level = Speichern.LadeLevel(SavegameName);

            if (level == 1)
            {
                Level1 l1 = new Level1(SavegameName);
                l1.Show();
                w.Close();
            }
            else if (level == 2)
            {
                Level2 l2 = new Level2(SavegameName);
                l2.Show();
                w.Close();
            }
            else if (level == 3)
            {
                Level3 l3 = new Level3(SavegameName);
                l3.Show();
                w.Close();
            }
            else if (level == 4)
            {
                Level4 l4 = new Level4(SavegameName);
                l4.Show();
                w.Close();
            }
        }

        private bool Ladbar()
        {
            if (GetErrors(nameof(SavegameName)).Any() || string.IsNullOrEmpty(SavegameName))
            {
                return false;
            }

            int level = Speichern.LadeLevel(SavegameName);

            return level != 0;
        }

        [RelayCommand]
        private void VerlassenKnopf_Click(Window w)
        {
            w.Close();
        }
        [RelayCommand]
        private void CreditsKnopf_Click()
        {
            MessageBox.Show("Programmiert von Michael Wiebe" + Environment.NewLine + "Design von Michael Wiebe" + Environment.NewLine + "Logo generiert von Gemini" + Environment.NewLine + "Viel Spaß beim Spielen!" );
        }

        [RelayCommand]
        private void SecretClick()
        {
            logoKlickZaehler++;
            if (logoKlickZaehler >= 5)
            {
                MessageBox.Show("GEHEIMNIS FREIGESCHALTET!" + Environment.NewLine + "Probiere mal den Spielstand ´Secret´ :)");
                logoKlickZaehler = 0;
            }
        }
    }
}
