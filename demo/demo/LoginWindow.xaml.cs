using demo.Properties;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace demo
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public static int userID = 0;
        demoEntities demoEntities;
        public LoginWindow()
        {
            InitializeComponent();
            demoEntities = new demoEntities();
            AddOrder addOrder = new AddOrder();
            addOrder.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Users user = demoEntities.Users.FirstOrDefault(u => u.login == loginBox.Text);
            if (user == null)
            {
                MessageBox.Show("Пользователя нет в базе!", "Ошибка при авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (passwordBox.Password != user.password)
                {
                    MessageBox.Show("Пароль неверный!", "Ошибка при авторизации", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                else
                {
                    userID = user.id_user;
                    if (user.role_id == 1)
                    {
                        MessageBox.Show("Клиент!", "Ошибка при авторизации", MessageBoxButton.OK, MessageBoxImage.Error);
                        AddOrder addOrder = new AddOrder();
                        addOrder.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Исполнитель!", "Ошибка при авторизации", MessageBoxButton.OK, MessageBoxImage.Error);

                    }
                }
            }
        }
    }
}
