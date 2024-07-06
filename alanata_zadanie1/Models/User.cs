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

        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [EmailAddress] public string Email { get; set; }

        [Required]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
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