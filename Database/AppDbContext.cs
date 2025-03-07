using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshop_Basics.Database.Entities;

namespace Workshop_Basics.Database;

public class AppDbContext: DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Admin> Admins { get; set; } 
    public DbSet<Post> Posts { get; set; }
    
    public void UserConfigure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.UserId);
        
        builder.Property(u => u.UserId)
            .ValueGeneratedOnAdd(); // Ensures auto-generation for UserId
        
        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(u => u.Nickname)
            .IsRequired()
            .HasMaxLength(20);
        
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.Property(u => u.Password)
            .HasMaxLength(50)
            .IsRequired();
        
        //Minlength is not supported by ef 
    }
    
    public void AdminConfigure(EntityTypeBuilder<Admin> builder)
    {
        builder.ToTable("Admins");

        builder.HasKey(a => a.AdminId);
        builder.Property(u => u.AdminId)
            .ValueGeneratedOnAdd();

        builder.HasOne(a => a.User)
            .WithOne(u => u.Admin)
            .HasForeignKey<Admin>(a => a.UserId); 
    }
    
    public void PostConfigure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("Posts");

        builder.HasKey(p => p.PostId);
        
        builder.Property(u => u.PostId)
            .ValueGeneratedOnAdd(); // Ensures auto-generation for UserId

        builder.Property(p => p.Content)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(p => p.Header)
            .HasMaxLength(100); 

        builder.HasOne(p => p.User) // Navigation property to 'User'
            .WithMany(u => u.Posts) // User has many posts
            .HasForeignKey(p => p.UserId); // Foreign key to User
    }

    //Calls all the configuration methods 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Admin>(AdminConfigure);
        modelBuilder.Entity<Post>(PostConfigure);
        modelBuilder.Entity<User>(UserConfigure);
    }

}