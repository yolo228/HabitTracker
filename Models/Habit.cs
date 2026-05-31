using System;
using System.Collections.Generic;

namespace HabitTracker.Models
{
    public class Habit
    {
        public int HabitId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public double? TargetValue { get; set; }
        public string FrequencyType { get; set; } // daily, weekly
        public string FrequencyDays { get; set; }
        public TimeSpan? ReminderTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<HabitRecord> Records { get; set; }
    }
}