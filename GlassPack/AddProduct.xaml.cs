using GlassPack.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GlassPack
{
    /// <summary>
    /// Логика взаимодействия для AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        GlassPackContext db = new GlassPackContext();

        List<Brand> brands;
        List<Provider> providers;
        public Product Product { get; set; }
        public AddProduct(Product product)
        {
            InitializeComponent();

            this.Loaded += Window_Loaded;

            //присваиваем комбобоксу записанное в бд значение для отображения
            brandComboBox.SelectedIndex = Product.Brand.Id - 1;
            providerComboBox.SelectedIndex = Product.Provider.Id - 1;


            //выключаем возможность взаимодействия с комбобокс
            // humansComboBox.IsEnabled = false;


            Product = product;
            //передача объекта в контекст
            DataContext = Product;
        }

        public AddProduct()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;

            brandComboBox.SelectedIndex = 1;
            providerComboBox.SelectedIndex = 1;
        }

        void Accept_Click(object sender, RoutedEventArgs e)
        {
            //если order не равен 0, то закрывается окно с результатом true
            if (Product != null) DialogResult = true;
            else
            {
                //получаем выбранные объекты
                Brand brand = brandComboBox.SelectedItem as Brand;
                Provider provider = providerComboBox.SelectedItem as Provider;

                //проверяем что все объекты получены
                //if (human == null) return;
                if (brand == null) return;
                if (provider == null) return;

                //создаем новый обьект и заполняем данными введенными в окне
                Product product = new Product
                {
                    Title = TitleBox.Text,
                    Description = DecriptionBox.Text,
                    ArticleNum = int.Parse(ArticleBox.Text),
                    Amount = int.Parse(AmountBox.Text),
                    Price = double.Parse(PriceBox.Text),
                    Brand = brand,
                    Provider = provider
                };

                //прикрепляем объекты к текущему контексту данных
                // db.Humans.Attach(human);
                db.Brands.Attach(brand);
                db.Providers.Attach(provider);

                //добавляем новый объект в бд
                db.Products.Add(product);
                //сохраняем изменения в бд
                db.SaveChanges();

                DialogResult = true;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //загружаем данные из бд
            db.Brands.Load();
            db.Products.Load();

            //humans = db.Humans.Where(x=>x.Id>1).ToList();
            brands = db.Brands.ToList();
            providers = db.Providers.ToList();

            brandComboBox.ItemsSource = brands;
            providerComboBox.ItemsSource = providers;
        }
    }
}
