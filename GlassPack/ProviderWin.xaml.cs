using GlassPack.Models;
using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для ProviderWin.xaml
    /// </summary>
    public partial class ProviderWin : Window
    {
        GlassPackContext db = new GlassPackContext();
        public ProviderWin()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db.Providers.Where(x=>x.Id>1).Load();
            // устанавливаем данные в качестве контекста
            DataContext = db.Providers.Local.ToObservableCollection();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
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
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //получаем выделенный объект
            Provider? provider = providerList.SelectedItem as Provider;
            if (provider is null) return;

            //передача данных выбранного обьекта в окно
            AddProvider AddProvider = new AddProvider(new Provider
            {
                Id = provider.Id,
                Title = provider.Title,
                Address = provider.Address
            });


            if (AddProvider.ShowDialog() == true)
            {
                // получаем измененный объект
                provider = db.Providers.Find(AddProvider.Provider.Id);
                //если объект найдет
                if (provider != null)
                {
                    provider.Title = AddProvider.Provider.Title;

                    //сохраняем изменения в бд
                    db.SaveChanges();
                    //обновляем список 
                    providerList.Items.Refresh();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Provider? provider = providerList.SelectedItem as Provider;
            // если ни одного объекта не выделено, выходим
            if (provider is null) return;
            //удаляем выделенный обьект из бд
            db.Providers.Remove(provider);
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
