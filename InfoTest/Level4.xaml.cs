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
    /// Interaktionslogik für Level4.xaml
    /// </summary>
    public partial class Level4 : Window
    {
        public Level4()
        {
            InitializeComponent();
            this.DataContext = new Level4ViewModel();
        }
    }
}
