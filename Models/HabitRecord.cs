using System;
using System.ComponentModel.DataAnnotations;

namespace HabitTracker.Models
{
    public class HabitRecord
    {
        [Key]  // явно указываем, что это первичный ключ
        public int RecordId { get; set; }

        public int HabitId { get; set; }
        public virtual Habit Habit { get; set; }

        public DateTime RecordDate { get; set; }

        public bool IsCompleted { get; set; }

        public double? ActualValue { get; set; }
    }
}