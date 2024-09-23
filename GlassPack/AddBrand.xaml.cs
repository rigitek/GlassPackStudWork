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
    /// Логика взаимодействия для AddBrand.xaml
    /// </summary>
    public partial class AddBrand : Window
    {
        public Brand Brand { get; set; }
        public AddBrand(Brand brand)
        {
            InitializeComponent();

            Brand=brand;

            DataContext = Brand;
        }

        void Accept_Click(object sender, RoutedEventArgs e)
        {

            DialogResult = true;
        }
    }
}
