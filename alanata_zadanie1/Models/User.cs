using System.Collections.Generic;
using System.Linq;
using System.Resources;
using alanata_zadanie1.Resources;

namespace alanata_zadanie1.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using System.Data.Entity;
    public class User
    {
        [HiddenInput]
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "SurnameRequired",ErrorMessageResourceType = typeof(TextResources))]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        
        [EmailAddress(ErrorMessageResourceName = "EmailInvalid", ErrorMessageResourceType = typeof(TextResources))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "UsernameRequired",ErrorMessageResourceType = typeof(TextResources))]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessageResourceName = "PasswordRequired",ErrorMessageResourceType = typeof(TextResources))]
        public string Password { get; set; }

        public static List<string> GetVisibleFields()
        {
            return typeof(User).GetProperties().Select(x => x.Name).Where(x=>x!="Password" && x!="Id").ToList();
        }
    }

    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> User { get; set; }

        public ApplicationDbContext() : base("DefaultConnection")
        {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            base.OnModelCreating(modelBuilder);
        }
    }
}