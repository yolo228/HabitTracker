using System.Collections.Generic;

namespace HabitTracker.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string Name { get; set; }
        public bool IsDefault { get; set; }
        public virtual ICollection<Habit> Habits { get; set; }
    }
}