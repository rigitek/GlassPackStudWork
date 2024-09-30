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
    /// Логика взаимодействия для WarehouseWin.xaml
    /// </summary>
    public partial class WarehouseWin : Window
    {
        GlassPackContext db = new GlassPackContext();
        public WarehouseWin()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db.Warehouses.Load();
            // устанавливаем данные в качестве контекста
            DataContext = db.Warehouses.Local.ToObservableCollection();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
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
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //получаем выделенный объект
            Warehouse? warehouse = warehouseList.SelectedItem as Warehouse;
            if (warehouse is null) return;

            //передача данных выбранного обьекта в окно
            AddWarehouse AddWarehouse = new AddWarehouse(new Warehouse
            {
                Id = warehouse.Id,
                Title = warehouse.Title,
                Address = warehouse.Address
            });


            if (AddWarehouse.ShowDialog() == true)
            {
                // получаем измененный объект
                warehouse = db.Warehouses.Find(AddWarehouse.Warehouse.Id);
                //если объект найдет
                if (warehouse != null)
                {
                    warehouse.Title = AddWarehouse.Warehouse.Title;
                    warehouse.Address = AddWarehouse.Warehouse.Address;

                    //сохраняем изменения в бд
                    db.SaveChanges();
                    //обновляем список 
                    warehouseList.Items.Refresh();
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            // получаем выделенный объект
            Warehouse? warehouse = warehouseList.SelectedItem as Warehouse;
            // если ни одного объекта не выделено, выходим
            if (warehouse is null) return;
            //удаляем выделенный обьект из бд
            db.Warehouses.Remove(warehouse);
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
