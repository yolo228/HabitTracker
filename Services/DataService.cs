using System;
using System.Collections.Generic;
using System.Linq;
using HabitTracker.Data;
using HabitTracker.Models;
using HabitTracker.Helpers;

namespace HabitTracker.Services
{
    public class DataService : IDataService
    {
        public User Authenticate(string username, string password)
        {
            using (var db = new AppDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == username);
                if (user == null) return null;
                return PasswordHasher.VerifyPassword(password, user.PasswordHash) ? user : null;
            }
        }

        public bool Register(string username, string password, string email)
        {
            using (var db = new AppDbContext())
            {
                if (db.Users.Any(u => u.Username == username)) return false;
                var user = new User
                {
                    Username = username,
                    PasswordHash = PasswordHasher.HashPassword(password),
                    Email = email
                };
                db.Users.Add(user);
                db.SaveChanges();
                AddDefaultCategories(user.UserId);
                return true;
            }
        }

        public void AddDefaultCategories(int userId)
        {
            using (var db = new AppDbContext())
            {
                var defaultNames = new[] { "Спорт", "Учёба", "Здоровье", "Творчество", "Работа", "Дом" };
                foreach (var name in defaultNames)
                {
                    if (!db.Categories.Any(c => c.UserId == userId && c.Name == name))
                        db.Categories.Add(new Category { UserId = userId, Name = name, IsDefault = true });
                }
                db.SaveChanges();
            }
        }

        public List<Category> GetCategories(int userId)
        {
            using (var db = new AppDbContext())
            {
                return db.Categories.Where(c => c.UserId == userId).ToList();
            }
        }

        public List<Habit> GetHabits(int userId)
        {
            using (var db = new AppDbContext())
            {
                return db.Habits.Where(h => h.UserId == userId).ToList();
            }
        }

        public void AddCategory(int userId, string name)
        {
            using (var db = new AppDbContext())
            {
                db.Categories.Add(new Category { UserId = userId, Name = name });
                db.SaveChanges();
            }
        }

        public void DeleteCategory(int categoryId)
        {
            using (var db = new AppDbContext())
            {
                var category = db.Categories.Find(categoryId);
                if (category != null)
                {
                    var habits = db.Habits.Where(h => h.CategoryId == categoryId).ToList();
                    foreach (var habit in habits) habit.CategoryId = null;
                    db.Categories.Remove(category);
                    db.SaveChanges();
                }
            }
        }

        public void DeleteHabit(int habitId)
        {
            using (var db = new AppDbContext())
            {
                var habit = db.Habits.Find(habitId);
                if (habit != null)
                {
                    db.Habits.Remove(habit);
                    db.SaveChanges();
                }
            }
        }

        public bool DeleteUser(int userId)
        {
            using (var db = new AppDbContext())
            {
                var user = db.Users.Find(userId);
                if (user == null) return false;
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
        }
    }
}