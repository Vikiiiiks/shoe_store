using Npgsql;
using shoe_store.Data;
using shoe_store.Models;
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

namespace shoe_store
{
    /// <summary>
    /// Логика взаимодействия для ProductListWindow.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        public ProductListWindow()
        {
            InitializeComponent();
            ShowUserInfo();
            ApplyRolePermissions();
            LoadProducts();
        }
        private void ShowUserInfo()
        {
            if (Session.CurrentUser == null)
            {
                UserNameTextBlock.Text = "Гость";
                RoleTextBlock.Text = "Просмотр товаров";
                return;
            }
            UserNameTextBlock.Text = Session.CurrentUser.FullName;
            RoleTextBlock.Text = Session.CurrentUser.RoleName;
        }
        // Показываем только те кнопки, которые разрешены роли
        private void ApplyRolePermissions()
        {
            UserRole role = Session.CurrentRole;
            bool isAdministrator = role == UserRole.Administrator;
            bool isManager = role == UserRole.Manager;
            AddProductButton.Visibility = isAdministrator ? Visibility.Visible : Visibility.Collapsed;
            OrdersButton.Visibility = (isAdministrator || isManager) ? Visibility.Visible : Visibility.Collapsed;
        }
        private void LoadProducts()
        {
            try
            {
                ProductsListBox.ItemsSource = ProductRepository.GetAll();
            }
            catch (NpgsqlException)
            {
                MessageBox.Show(
                "Не удалось загрузить список товаров из базы данных. Проверьте подключение к серверу PostgreSQL повторите вход.",
                "Ошибка загрузки данных",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                "Непредвиденная ошибка при загрузке товаров: " + ex.Message,
                "Ошибка",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
            }
        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            Session.CurrentUser = null;
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            Close();
        }
        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
            "Форма добавления товара будет реализована в Модуле 3.",
            "Информация",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
        }
        private void OrdersButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
            "Просмотр заказов будет реализован на следующем этапе.",
            "Информация",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
        }
    }
}