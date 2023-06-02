using Academy.Models;
using Academy.Models.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Academy.Data
{
    public class AcademyDbContext : IdentityDbContext
    {
        public AcademyDbContext(DbContextOptions<AcademyDbContext> options) : base(options)
        {}
        public DbSet<Course> courses { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Menu> menus { get; set; }  
        public DbSet<Slider> sliders { get; set; }  
        public DbSet<Trainer> trainers { get; set; }
        public DbSet<ContactUs>  contactUs { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<AboutUs> AboutUs { get; set; }
       
    }
}
