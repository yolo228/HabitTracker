using System.Collections.Generic;

namespace HabitTracker.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Habit> Habits { get; set; }
    }
}