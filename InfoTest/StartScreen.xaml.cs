using System.Web;
using System.Windows;

namespace InfoTest;

public partial class StartScreen : Window
{
    public StartScreen()
    {
        InitializeComponent();
    }

    private void startKnopf_Click(object sender, RoutedEventArgs e)
    {
        Level1 levelEins = new Level1();
        MainWindow mainWindow = new MainWindow();
        //levelEins.Show();
        mainWindow.Show();
        Close();
    }

    private void loadKnopf_Click(object sender, RoutedEventArgs e)
    {

    }

    private void beendenKnopf_Click(object sender, RoutedEventArgs e)
    {
        Close();
    }
}