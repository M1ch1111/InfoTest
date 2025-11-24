using System;
using System.Collections.Generic;
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
    /// Interaktionslogik für Level2.xaml
    /// </summary>
    public partial class Level2 : Window
    {
        public Level2()
        {
            InitializeComponent();

            Thread.Sleep(1000);
            r1.Visibility = Visibility.Visible;
            Thread.Sleep(2000);
            r2.Visibility = Visibility.Visible;
            Thread.Sleep(2000);
            r3.Visibility = Visibility.Visible;
            Thread.Sleep(2000);
            r4.Visibility = Visibility.Visible;
        }
    }
}
