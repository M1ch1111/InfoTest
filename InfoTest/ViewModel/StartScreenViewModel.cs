using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace InfoTest
{
    public partial class StartScreenViewModel : ObservableObject
    {
        public StartScreenViewModel()
        {

        }

        [RelayCommand]
        private void StartKnopf_Click(Window w)
        {
            Level1 l1 = new Level1();
            l1.Show();
            w.Close();
        }

        [RelayCommand]
        private void LadeKnopf_Click()
        {

        }

        [RelayCommand]
        private void VerlassenKnopf_Click(Window w)
        {
            w.Close();
        }
        [RelayCommand]
        private void CreditsKnopf_Click()
        {
            MessageBox.Show("Programmiert von Michael Wiebe" + Environment.NewLine + "Design von Michael Wiebe" + Environment.NewLine + "Viel Spaß beim Spielen!" +Environment.NewLine + "Logo generiert von Gemini");
        }
    }
}
