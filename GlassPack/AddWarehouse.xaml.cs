using GlassPack.Models;
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

namespace GlassPack
{
    /// <summary>
    /// Логика взаимодействия для AddWarehouse.xaml
    /// </summary>
    public partial class AddWarehouse : Window
    {
        public Warehouse Warehouse{ get; set; }
        public AddWarehouse(Warehouse warehouse)
        {
            InitializeComponent();

            Warehouse = warehouse;

            DataContext = Warehouse;
        }

        void Accept_Click(object sender, RoutedEventArgs e)
        {

            DialogResult = true;
        }
    }
}
