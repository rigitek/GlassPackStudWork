using GlassPack.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GlassPack
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        GlassPackContext db = new GlassPackContext();

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db.Products.Load();
            db.Brands.Load();
            db.Providers.Load();
            DataContext = db.Products.Local.ToObservableCollection();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddProduct AddProduct = new AddProduct();

            //если окно закрыто с тру
            if (AddProduct.ShowDialog() == true)
            {
                //передача объекта из окна 
                //Product Product = AddProduct.Product;
                ////добавление нового объекта в бд
                //db.Products.Add(Product);
                ////сохранение изменений
                //db.SaveChanges();
                db.Products.Load();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //передача выбранного элемента
            Product? product = productsGrid.SelectedItem as Product;
            // если элемент не выбран, то ничего не происходит
            if (product is null) return;

            //создание объекта окна и отправка выбранных данных в конструкторе
            AddProduct AddProduct = new AddProduct(new Product
            {
                Id = product.Id,
                Title = product.Title,
                Description=product.Description,
                ArticleNum=product.ArticleNum,
                Amount = product.Amount,
                Price = product.Price,
                Brand=product.Brand,
                Provider=product.Provider
            });


            //если при закрытии нажато добавить, то тру и
            if (AddProduct.ShowDialog() == true)
            {
                // получаем измененный объект и ищем его в бд
                product = db.Products.Find(AddProduct.Product.Id);
                //если объект найдет, то сохраняем изменения
                if (product != null)
                {
                    product.Title = AddProduct.Product.Title;
                    product.Description = AddProduct.Product.Description;
                    product.ArticleNum = AddProduct.Product.ArticleNum;
                    product.Amount = AddProduct.Product.Amount;
                    product.Price = AddProduct.Product.Price;
                    product.Brand= AddProduct.Product.Brand;
                    product.Provider = AddProduct.Product.Provider;
                    //сохранение данных в бд
                    db.SaveChanges();
                    //обновление списка
                    db.Products.Load();
                    productsGrid.Items.Refresh();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Product? product = productsGrid.SelectedItem as Product;
            // если ни одного объекта не выделено, выходим
            if (product is null) return;
            //удаляем объект
            db.Products.Remove(product);
            //сохранение данных в бд
            db.SaveChanges();
        }

        private void Brand_Click(object sender, RoutedEventArgs e)
        {
            BrandWin brandWin = new BrandWin();
            this.Close();
            brandWin.Show();
        }

        private void Provider_Click(object sender, RoutedEventArgs e)
        {
            ProviderWin providerWin = new ProviderWin();
            this.Close();
            providerWin.Show();
        }
    }
}