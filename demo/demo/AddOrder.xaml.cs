using demo.Properties;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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

namespace demo
{
    /// <summary>
    /// Логика взаимодействия для AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Window
    {
        public AddOrder()
        {
            InitializeComponent();
            demoEntities demoEntities = new demoEntities();
            List<string> types = new List<string> {"Сильное", "Среднее", "Слабое"};
            typeBox.ItemsSource = types;
            var objects = demoEntities.Object.ToList();
            List<String> nameObject = new List<String>();
            foreach (var item in objects)
            {
                nameObject.Add(item.name_object);  
            }
            objectBox.ItemsSource = nameObject;
            var users = demoEntities.Users.ToList().Where(u => u.role_id == 2).ToList();
            List<int> ints = new List<int>();
            users.ForEach(u => ints.Add(u.id_user));
            ispBox.ItemsSource = ints;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            demoEntities demoEntities = new demoEntities();
            int idObject = demoEntities.Object.FirstOrDefault(u=> u.name_object == objectBox.SelectedValue.ToString()).id_object;
            Random rand = new Random();
            int number = rand.Next(0, 1000);
            Order order = new Order(0,number , DateTime.Now, "Начал", LoginWindow.userID, null, problemBox.Text, typeBox.SelectedValue.ToString(), idObject, null);
            demoEntities.Order.Add(order);
            demoEntities.SaveChanges();
            int orderID = demoEntities.Order.FirstOrDefault(u => u.number == number).id_order;
            demoEntities.order_ispolnitel.Add(new order_ispolnitel(0, int.Parse(ispBox.SelectedValue.ToString()), orderID));
            demoEntities.SaveChanges();
            MessageBox.Show("Создан заказ", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Uri fileUri = new Uri(openFileDialog.FileName);
                image.Source = new BitmapImage(fileUri);

            }
        }
    }
}
