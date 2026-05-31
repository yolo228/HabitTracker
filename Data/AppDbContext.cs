using System.Data.Entity;
using HabitTracker.Models;

namespace HabitTracker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=HabitTrackerDB") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Habit> Habits { get; set; }
        public DbSet<HabitRecord> HabitRecords { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Уникальность имени категории для пользователя
            modelBuilder.Entity<Category>()
                .HasIndex(c => new { c.UserId, c.Name })
                .IsUnique();

            // Уникальность записи за день для привычки
            modelBuilder.Entity<HabitRecord>()
                .HasIndex(r => new { r.HabitId, r.RecordDate })
                .IsUnique();

            // Дополнительные настройки
            modelBuilder.Entity<Habit>()
                .HasRequired(h => h.User)
                .WithMany(u => u.Habits)
                .HasForeignKey(h => h.UserId);

            modelBuilder.Entity<Habit>()
                .HasOptional(h => h.Category)
                .WithMany(c => c.Habits)
                .HasForeignKey(h => h.CategoryId);
        }
    }
}