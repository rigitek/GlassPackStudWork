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

namespace GlassPack
{
    /// <summary>
    /// Логика взаимодействия для BrandWin.xaml
    /// </summary>
    public partial class BrandWin : Window
    {
        GlassPackContext db = new GlassPackContext();
        public BrandWin()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db.Brands.Load();
            // устанавливаем данные в качестве контекста
            DataContext = db.Brands.Local.ToObservableCollection();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
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
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //получаем выделенный объект
            Brand? brand = brandList.SelectedItem as Brand;
            if (brand is null) return;

            //передача данных выбранного обьекта в окно
            AddBrand AddBrand = new AddBrand(new Brand
            {
                Id = brand.Id,
                Title = brand.Title,
            });


            if (AddBrand.ShowDialog() == true)
            {
                // получаем измененный объект
                brand = db.Brands.Find(AddBrand.Brand.Id);
                //если объект найдет
                if (brand != null)
                {
                    brand.Title = AddBrand.Brand.Title;

                    //сохраняем изменения в бд
                    db.SaveChanges();
                    //обновляем список 
                    brandList.Items.Refresh();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Brand? brand = brandList.SelectedItem as Brand;
            // если ни одного объекта не выделено, выходим
            if (brand is null) return;
            //удаляем выделенный обьект из бд
            db.Brands.Remove(brand);
            // сохраняем изменения в бд
            db.SaveChanges();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            //для открытия окна создаем его объект
            MainWindow MainWindow = new MainWindow();
            //закрывает уже открытое окно
            this.Close();
            //открываем новое окно
            MainWindow.Show();
        }
    }
}
