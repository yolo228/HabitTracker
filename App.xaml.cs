using System.Windows;
using HabitTracker.Views;  // для LoginWindow

namespace HabitTracker
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Показываем окно входа
            var loginWindow = new LoginWindow();
            loginWindow.Show();
        }
    }
}