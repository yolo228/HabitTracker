using System.Collections.Generic;
using HabitTracker.Models;

namespace HabitTracker.Services
{
    public interface IDataService
    {
        User Authenticate(string username, string password);
        bool Register(string username, string password, string email);
        void AddDefaultCategories(int userId);
        List<Category> GetCategories(int userId);
        List<Habit> GetHabits(int userId);
        void AddCategory(int userId, string name);
        void DeleteCategory(int categoryId);
        void DeleteHabit(int habitId);
        bool DeleteUser(int userId);
    }
}