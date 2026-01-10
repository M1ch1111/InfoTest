using System.Web;
using System.Windows;

namespace InfoTest;

public partial class StartScreen : Window
{
    public StartScreen()
    {
        InitializeComponent();
        DataContext = new StartScreenViewModel();
    }
}