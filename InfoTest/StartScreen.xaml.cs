using System.Web;
using System.Windows;

namespace InfoTest;

public partial class StartScreen : Window
{
    public StartScreen()
    {
        InitializeComponent();
        //[cite_start]// Verbinden von View und ViewModel [cite: 153]
        DataContext = this.DataContext;
    }

    //[cite_start]// Keine Click-Methoden mehr hier! [cite: 159]
}