using Microsoft.EntityFrameworkCore;

namespace cinema_ticket_seller_pdi.Models
{
    public class TicketSellerContext : DbContext
    {
        public TicketSellerContext(DbContextOptions<TicketSellerContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<MovieTheater> MovieTheaters { get; set; } = null!;
        public DbSet<Movie> Movies { get; set; } = null!;
        public DbSet<MovieSession> MovieSessions { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Ticket> Tickets { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .Property(c => c.Active)
                .HasDefaultValue(true);
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Document)
                .IsUnique();
            modelBuilder.Entity<Employee>()
                .Property(c => c.Active)
                .HasDefaultValue(true);
            modelBuilder.Entity<Movie>()
                .Property(m => m.ParentalRating)
                .HasConversion<string>();
            modelBuilder.Entity<Movie>()
                .HasIndex(m => m.Name)
                .IsUnique();
            modelBuilder.Entity<MovieSession>()
                .Property(m => m.Active)
                .HasDefaultValue(true);
            modelBuilder.Entity<Order>()
                .Property(o => o.Status)
                .HasConversion<string>();
            modelBuilder.Entity<Order>()
                .Property(o => o.CreatedAt)
                .HasDefaultValueSql("NOW()");

            base.OnModelCreating(modelBuilder);
        }
    }
}
