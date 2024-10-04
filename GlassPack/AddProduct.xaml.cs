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
        List<Warehouse> warehouses;
        public Product Product { get; set; }
        public AddProduct(Product product)
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;

            Product = product;

            //присваиваем комбобоксу записанное в бд значение для отображения
            brandComboBox.SelectedIndex = Product.Brand.Id - 2;
            providerComboBox.SelectedIndex = Product.Provider.Id - 2;
            warehouseComboBox.SelectedIndex = Product.Warehouse.Id - 2;


            //выключаем возможность взаимодействия с комбобокс
            brandComboBox.IsEnabled = false;
            providerComboBox.IsEnabled = false;
            warehouseComboBox.IsEnabled = false;

            //передача объекта в контекст
            DataContext = Product;
        }

        public AddProduct()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;

            brandComboBox.SelectedIndex = 0;
            providerComboBox.SelectedIndex = 0;
            warehouseComboBox.SelectedIndex = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //загружаем данные из бд
            db.Brands.Load();
            db.Providers.Load();
            db.Warehouses.Load();

            //humans = db.Humans.Where(x=>x.Id>1).ToList();
            brands = db.Brands.ToList();
            providers = db.Providers.ToList();
            warehouses = db.Warehouses.ToList();

            brandComboBox.ItemsSource = brands;
            providerComboBox.ItemsSource = providers;
            warehouseComboBox.ItemsSource = warehouses;
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
                Warehouse warehouse = warehouseComboBox.SelectedItem as Warehouse;

                //проверяем что все объекты получены
                //if (human == null) return;
                if (brand == null) return;
                if (provider == null) return;
                if (warehouse == null) return;

                //создаем новый обьект и заполняем данными введенными в окне
                Product product = new Product
                {
                    Title = TitleBox.Text,
                    Description = DecriptionBox.Text,
                    ArticleNum = int.Parse(ArticleBox.Text),
                    Shelf = ShelfBox.Text,
                    Unit = UnitBox.Text,
                    Amount = int.Parse(AmountBox.Text),
                    Price = double.Parse(PriceBox.Text),
                    Brand = brand,
                    Provider = provider,
                    Warehouse = warehouse
                };

                //прикрепляем объекты к текущему контексту данных
                // db.Humans.Attach(human);
                db.Brands.Attach(brand);
                db.Providers.Attach(provider);
                db.Warehouses.Attach(warehouse);

                //добавляем новый объект в бд
                db.Products.Add(product);
                //сохраняем изменения в бд
                db.SaveChanges();

                DialogResult = true;
            }
        }

        private void Add_Provider(object sender, RoutedEventArgs e)
        {
            //создаем обьект нового окна с созданием нового обьекта для записи в бд
            AddProvider AddProvider = new AddProvider(new Provider());

            
            //если открытое окно завершилось с true
            if (AddProvider.ShowDialog() == true)
            {
                Provider Provider = AddProvider.Provider;
                db.Providers.Add(Provider);
                db.SaveChanges();
            }

            db.Providers.Load();
            providers = db.Providers.ToList();
            providerComboBox.ItemsSource = providers;
        }

        private void Add_Brand(object sender, RoutedEventArgs e)
        {
            //создаем обьект нового окна с созданием нового обьекта для записи в бд
            AddBrand AddBrand = new AddBrand(new Brand());

            //если открытое окно завершилось с true
            if (AddBrand.ShowDialog() == true)
            {
                Brand Brand = AddBrand.Brand;
                db.Brands.Add(Brand);
                db.SaveChanges();
            }

            //загружаем данные из бд
            db.Brands.Load();
            brands = db.Brands.ToList();
            brandComboBox.ItemsSource = brands;
        }

        private void Add_Warehouse(object sender, RoutedEventArgs e)
        {
            //создаем обьект нового окна с созданием нового обьекта для записи в бд
            AddWarehouse AddWarehouse = new AddWarehouse(new Warehouse());

            //если открытое окно завершилось с true
            if (AddWarehouse.ShowDialog() == true)
            {
                Warehouse Warehouse = AddWarehouse.Warehouse;
                db.Warehouses.Add(Warehouse);
                db.SaveChanges();
            }
            db.Warehouses.Load();
            warehouses = db.Warehouses.ToList();
            warehouseComboBox.ItemsSource = warehouses;
        }
    }
}