using System.Windows;
using HabitTracker.Models;
using HabitTracker.Services;
using HabitTracker.Views;  

namespace HabitTracker
{
    public partial class MainWindow : Window
    {
        private User _currentUser;
        private IDataService _dataService;

        public MainWindow(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            _dataService = new DataService();
            UserGreeting.Text = $"Привет, {_currentUser.Username}!";
            LoadHabits();
            LoadCategories();
        }

        private void LoadHabits()
        {
            var habits = _dataService.GetHabits(_currentUser.UserId);
            HabitsList.ItemsSource = habits;
        }

        private void LoadCategories()
        {
            var categories = _dataService.GetCategories(_currentUser.UserId);
            CategoriesList.ItemsSource = categories;
        }

        private void AddHabitButton_Click(object sender, RoutedEventArgs e)
        {
            // Пока просто заглушка
            MessageBox.Show("Откроется окно добавления привычки");
        }

        private void DeleteHabitButton_Click(object sender, RoutedEventArgs e)
        {
            if (HabitsList.SelectedItem is Habit habit)
            {
                _dataService.DeleteHabit(habit.HabitId);
                LoadHabits();
            }
        }

        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NewCategoryBox.Text.Trim();
            if (!string.IsNullOrEmpty(name))
            {
                _dataService.AddCategory(_currentUser.UserId, name);
                LoadCategories();
                NewCategoryBox.Clear();
            }
        }

        private void DeleteCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (CategoriesList.SelectedItem is Category category)
            {
                _dataService.DeleteCategory(category.CategoryId);
                LoadCategories();
            }
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Экспорт в CSV пока не реализован");
        }

        private void DeleteAccountButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить аккаунт навсегда?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _dataService.DeleteUser(_currentUser.UserId);
                MessageBox.Show("Аккаунт удалён. Приложение закроется.");
                Application.Current.Shutdown();
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            var login = new Views.LoginWindow();
            login.Show();
            Close();
        }
    }
}