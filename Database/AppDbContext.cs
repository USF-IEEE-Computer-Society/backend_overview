using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Workshop_Basics.Database.Entities;

namespace Workshop_Basics.Database;

public class AppDbContext: DbContext
{
    #region Constructor
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    #endregion
   
    #region DbSet Properties for Entities
    public DbSet<User> Users { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Post> Posts { get; set; }
    
    #endregion
    
    #region Configuration Methods
    
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
        
        // builder
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
        // User does not have a navigation property back to Admin
        // .HasForeignKey(a => a.UserId); // Assuming a relationship from Admin to User
        // builder
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
            .HasMaxLength(100); // Example max length for Header

        builder.HasOne(p => p.User) // Navigation property to 'User'
            .WithMany(u => u.Posts) // User has many posts
            .HasForeignKey(p => p.UserId); // Foreign key to User
    }
    #endregion
   
    #region Calling Entity Configuration Methods
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Admin>(AdminConfigure);
        modelBuilder.Entity<Post>(PostConfigure);
        modelBuilder.Entity<User>(UserConfigure);
    }
    #endregion
}