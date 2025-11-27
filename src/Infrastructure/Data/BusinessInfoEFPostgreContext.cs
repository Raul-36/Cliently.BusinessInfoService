using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Businesses.Entities;
using Core.DynamicItems.Entities;
using Core.InfoLists.Entities;
using Core.InfoTexts.Entities;
using Core.Users.Entities; 
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.VisualBasic;


namespace Infrastructure.Data;
public class BusinessInfoEFPostgreContext : DbContext
{
    public DbSet<Business> Businesses { get; set; } 
    public DbSet<InfoText> InfoTexts { get; set; } 
    public DbSet<InfoList> InfoLists { get; set; } 
    public DbSet<DynamicItem> DynamicItems { get; set; } 
    public DbSet<User> Users { get; set; }  

    public BusinessInfoEFPostgreContext(DbContextOptions<BusinessInfoEFPostgreContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User Entity Configuration
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.HasOne<Business>()
                .WithOne()
                .HasForeignKey<Business>(b => b.Id)
                .OnDelete(DeleteBehavior.Cascade);
        });

        //DynamicItem 
        {
            var serializerOptions = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = true
            };
            var jsonConverter = new ValueConverter<Dictionary<string, object>, string>(
                v => JsonSerializer.Serialize(v, serializerOptions),
                v => JsonSerializer.Deserialize<Dictionary<string, object>>(v, serializerOptions)!//! 
            );

            var jsonComparer = new ValueComparer<Dictionary<string, object>>(
                (c1, c2) => JsonSerializer.Serialize(c1, serializerOptions) == JsonSerializer.Serialize(c2, serializerOptions),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => JsonSerializer.Deserialize<Dictionary<string, object>>(JsonSerializer.Serialize(c, serializerOptions), serializerOptions)!//!
            );
            if(jsonConverter is null)
                throw new NullReferenceException(nameof(jsonConverter));
            if(jsonComparer is null)
                throw new NullReferenceException(nameof(jsonComparer));


            modelBuilder.Entity<DynamicItem>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasDefaultValueSql("gen_random_uuid()");


                entity.Property(e => e.Properties)
                    .HasConversion(jsonConverter!) //
                    .HasColumnType("jsonb")
                    .Metadata.SetValueComparer(jsonComparer);

                entity.Property(e => e.ListId)
                    .IsRequired();
            });
        }

        modelBuilder.Entity<InfoList>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("gen_random_uuid()");

            entity.HasMany(e => e.Items)
                .WithOne()
                .HasForeignKey(i => i.ListId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Property(e => e.BusinessId)
                .IsRequired();
        });

        modelBuilder.Entity<InfoText>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("gen_random_uuid()");


            entity.Property(e => e.BusinessId)
                .IsRequired();
        });

        modelBuilder.Entity<Business>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("gen_random_uuid()");

            entity.HasMany(e => e.Lists)
                .WithOne()
                .HasForeignKey(l => l.BusinessId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(e => e.Texts)
                .WithOne()
                .HasForeignKey(l => l.BusinessId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
