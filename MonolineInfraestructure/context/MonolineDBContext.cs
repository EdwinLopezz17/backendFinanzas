using Microsoft.EntityFrameworkCore;
using MonolineInfraestructure.models;

namespace MonolineInfraestructure.context;

public class MonolineDBContext : DbContext
{
    public MonolineDBContext(){}

    public MonolineDBContext(DbContextOptions<MonolineDBContext> options) : base(options)
    {
        
    }
    public DbSet<User>Users { get; set; }
    public DbSet<Client>Clients { get; set; }
    public DbSet<Flows>Flowss { get; set; }
    public DbSet<Credit>Credits { get; set; }
    public DbSet<Property>Properties { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 33));
            optionsBuilder.UseMySql("Server=127.0.0.1,3306;Uid=root;Pwd=UPC@intranet_17;Database=monolineDB", serverVersion);
        }
    }
    
    
    protected override void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);

        builder.Entity<User>().ToTable("Users");
        builder.Entity<User>().HasKey(p => p.Id);
        builder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        
        builder.Entity<Client>().ToTable("Clients");
        builder.Entity<Client>().HasKey(p => p.Dni);
        builder.Entity<Client>().Property(p => p.Dni).IsRequired().ValueGeneratedOnAdd();
        
        builder.Entity<Credit>().ToTable("Credits");
        builder.Entity<Credit>().HasKey(p => p.Id);
        builder.Entity<Credit>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Client>().Property(c => c.Dni).IsRequired();
        
        
        builder.Entity<Flows>().ToTable("Flows");
        builder.Entity<Flows>().HasKey(p => p.Id);
        builder.Entity<Flows>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        
        
        builder.Entity<Property>().ToTable("Properties");
        builder.Entity<Property>().HasKey(p => p.Id);
        builder.Entity<Property>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();

    }
}