using HabitTracker.Views;
using System.Windows;
using HabitTracker.Services;

namespace HabitTracker.Views
{
    public partial class LoginWindow : Window
    {
        private readonly IDataService _dataService;
        public LoginWindow()
        {
            InitializeComponent();
            _dataService = new DataService();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var user = _dataService.Authenticate(UsernameBox.Text, PasswordBox.Password);
            if (user != null)
            {
                var mainWindow = new MainWindow(user);
                mainWindow.Show();
                this.Close();  // закрываем окно входа
            }
            else
            {
                MessageText.Text = "Неверный логин или пароль";
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            var regWindow = new RegisterWindow(_dataService);
            regWindow.ShowDialog();
        }
    }
}