using System.Windows;
using HabitTracker.Services;

namespace HabitTracker.Views
{
    public partial class RegisterWindow : Window
    {
        private readonly IDataService _dataService;
        public RegisterWindow(IDataService dataService)
        {
            InitializeComponent();
            _dataService = dataService;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text.Trim();
            string password = PasswordBox.Password;
            string confirm = ConfirmBox.Password;
            string email = EmailBox.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageText.Text = "Логин и пароль обязательны";
                return;
            }
            if (password != confirm)
            {
                MessageText.Text = "Пароли не совпадают";
                return;
            }

            bool success = _dataService.Register(username, password, email);
            if (success)
            {
                MessageBox.Show("Регистрация успешна! Теперь войдите.", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            else
            {
                MessageText.Text = "Пользователь с таким именем уже существует";
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}