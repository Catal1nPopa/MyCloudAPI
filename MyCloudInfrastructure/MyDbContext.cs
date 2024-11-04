﻿using Microsoft.EntityFrameworkCore;
using MyCloudDomain.Auth;
using MyCloudDomain.Files;

public class MyDbContext : DbContext
{
    public DbSet<CreateUserLoginEntitiy> usersLogins { get; set; }
    public DbSet<FileRecordEntity> filesAdded { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=MyCloud;Username=postgres;Password=admin;IncludeErrorDetail=true");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}