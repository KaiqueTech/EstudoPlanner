using EstudoPlanner.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EstudoPlanner.DAL.DataContext;

public class AppDbContext : DbContext
{
    public DbSet<UserModel> Users { get; set; }
    public DbSet<ScheduleModel> Schedules { get; set; }
    public DbSet<StudyPlanModel> StudyPlans { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public AppDbContext()
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<UserModel>(entity =>
        {
            entity.ToTable("tb_Users");//table name
            entity.HasKey(user => user.IdUser);//primary key for users table
            entity.HasMany(user => user.StudyPlans)
                .WithOne(studyPlans => studyPlans.User) // each studyPlans has a user
                .HasForeignKey(studyPlans => studyPlans.IdUser); // the foreingKey is in the studyplans
            entity.Property(user => user.IdUser).ValueGeneratedOnAdd();//the id is automatically generated
            entity.Property(user => user.Email).IsRequired().HasMaxLength(255);//the max characters for the email is 255
            entity.Property(user => user.PasswordHash).IsRequired();//the password is a required field
            entity.HasIndex(user => user.Email).IsUnique();//create an index for the email and makes it unique
            entity.Property(user => user.Name).IsRequired().HasMaxLength(255);//the max characters from name is 255 and required field
            
        });

        modelBuilder.Entity<StudyPlanModel>(entity =>
        {
            entity.ToTable("tb_StudyPlans");
            entity.HasKey(studyPlans => studyPlans.IdStudyPlan);
            entity.HasMany(studyPlans => studyPlans.Schedules)
                .WithOne(schedules => schedules.StudyPlan)
                .HasForeignKey(schedules => schedules.IdStudyPlan)
                .OnDelete(DeleteBehavior.Cascade);
            entity.Property(studyPlans => studyPlans.IdStudyPlan).ValueGeneratedOnAdd();
            entity.Property(studyPlans => studyPlans.Title).IsRequired().HasMaxLength(255);

        });

        modelBuilder.Entity<ScheduleModel>(entity =>
        {
            entity.ToTable("tb_Schedules");
            entity.HasKey(schedule => schedule.IdSchedule);
            entity.Property(schedule => schedule.IdSchedule).ValueGeneratedOnAdd();
            entity.Property(schedules => schedules.DayOfWeek).IsRequired();
            entity.Property(schedules => schedules.StartTime).IsRequired();
            entity.Property(schedules => schedules.EndTime).IsRequired();
            
            
            entity.HasOne(schedule => schedule.StudyPlan)
                .WithMany(plan => plan.Schedules)
                .HasForeignKey(schedule => schedule.IdStudyPlan)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}