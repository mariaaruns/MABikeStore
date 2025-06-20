﻿using System;
using System.Collections.Generic;
using BikeStore.Domain.Models;

using BikeStore.Persistence.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BikeStore.Persistence.Data;

public partial class BikeStoresContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
{

    public BikeStoresContext(DbContextOptions<BikeStoresContext> options) : base(options)
    {

    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Staff> Staffs { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<Store> Stores { get; set; }
    
    public virtual DbSet<Payment> Payments { get; set; }
    public virtual DbSet<Lookup> Lookup { get; set; }

    public virtual DbSet<RepairService> RepairService { get; set; }
    public virtual DbSet<RepairIssues> RepairIssues { get; set; }
    public virtual DbSet<Invoice> Invoice { get; set; }
    public virtual DbSet<InvoiceItems> InvoiceItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var hasher = new PasswordHasher<ApplicationUser>();

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUser>(entity => entity.ToTable("Users"));
        modelBuilder.Entity<ApplicationRole>(entity => entity.ToTable("Roles"));
        modelBuilder.Entity<IdentityUserRole<int>>(entity => entity.ToTable("UserRoles"));
        modelBuilder.Entity<IdentityUserClaim<int>>(entity => entity.ToTable("UserClaims"));
        modelBuilder.Entity<IdentityUserLogin<int>>(entity => entity.ToTable("UserLogins"));
        modelBuilder.Entity<IdentityRoleClaim<int>>(entity => entity.ToTable("RoleClaims"));
        modelBuilder.Entity<IdentityUserToken<int>>(entity => entity.ToTable("UserTokens"));

        modelBuilder.Entity<RepairService>(entity =>
        {
            entity.HasKey(e => e.ServiceId).HasName("PK__RepairService__5E5A8E27729211BE");

            entity.ToTable("RepairService", "Service");

            entity.HasOne<ApplicationUser>()
           .WithMany(u => u.RepairService)
           .HasForeignKey(s => s.AssignTo)
           .OnDelete(DeleteBehavior.NoAction);

            entity.HasOne(rs => rs.Store)
           .WithMany(s => s.RepairServices)
           .HasForeignKey(rs => rs.StoreId)
           .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<RepairIssues>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RepairIssues__5E5A8E27729211BE");

            entity.ToTable("RepairIssues", "Service");

            entity.HasOne(x => x.RepairServices)
           .WithMany(u => u.RepairIssues)
           .HasForeignKey(s => s.RepairServiceId)
           .OnDelete(DeleteBehavior.NoAction);

        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Invoice__5E5A8E27729211BE");
            entity.ToTable("Invoice", "Service");

            entity.HasOne(x=>x.RepairServices)
           .WithOne(u => u.Invoice)
           .HasForeignKey<Invoice>("ServiceId")
           .OnDelete(DeleteBehavior.NoAction);
        });

        modelBuilder.Entity<InvoiceItems>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__InvoiceItems__5E5A8E27729211BE");
            entity.ToTable("InvoiceItems", "Service");

            entity.HasOne(x => x.Invoices)
           .WithMany(u => u.InvoiceItems)
           .HasForeignKey(x=>x.InvoiceId)
           .OnDelete(DeleteBehavior.NoAction);
        });

       /* modelBuilder.Entity<Lookup>().HasData(
        new Lookup { LookupId = 1, LookupName = "Order Status", LookupValue = "Order Placed", CreatedDate = DateTime.Now, IsActive = true },
        new Lookup { LookupId = 2, LookupName = "Order Status", LookupValue = "In Progress", CreatedDate = DateTime.Now, IsActive = true },
        new Lookup { LookupId = 3, LookupName = "Order Status", LookupValue = "Ready for Pickup/Delivery", CreatedDate = DateTime.Now, IsActive = true },
        new Lookup { LookupId = 4, LookupName = "Order Status", LookupValue = "Completed", CreatedDate = DateTime.Now, IsActive = true }

        );*/

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PK__brands__5E5A8E27729211BE");

            entity.ToTable("brands", "production");

            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.BrandName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("brand_name");
            entity.Property(e => e.Logo).IsUnicode(false);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__categori__D54EE9B4A37881F0");

            entity.ToTable("categories", "production");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("category_name");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__customer__CD65CB85E8D589C1");

            entity.ToTable("customers", "sales");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.Image).IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.State)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("state");
            entity.Property(e => e.Street)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("street");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("zip_code");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__orders__4659622953058B8B");

            entity.ToTable("orders", "sales");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.OrderDate).HasColumnName("order_date");
            entity.Property(e => e.OrderStatus).HasColumnName("order_status");
            entity.Property(e => e.RequiredDate).HasColumnName("required_date");
            entity.Property(e => e.ShippedDate).HasColumnName("shipped_date");
            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.StoreId).HasColumnName("store_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__orders__customer__47DBAE45");

            entity.HasOne(d => d.Staff).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StaffId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__orders__staff_id__49C3F6B7");

            entity.HasOne(d => d.Store).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK__orders__store_id__48CFD27E");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ItemId }).HasName("PK__order_it__837942D49394313B");

            entity.ToTable("order_items", "sales");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.Discount)
                .HasColumnType("decimal(4, 2)")
                .HasColumnName("discount");
            entity.Property(e => e.ListPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("list_price");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__order_ite__order__4D94879B");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__order_ite__produ__4E88ABD4");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__products__47027DF55A34BA08");

            entity.ToTable("products", "production");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Image).IsUnicode(false);
            entity.Property(e => e.ListPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("list_price");
            entity.Property(e => e.ModelYear).HasColumnName("model_year");
            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("product_name");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK__products__brand___3C69FB99");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__products__catego__3B75D760");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasKey(e => e.StaffId).HasName("PK__staffs__1963DD9C5F828E70");
            // entity.Property(e => e.StaffId).ValueGeneratedNever();
            entity.Property(e => e.StaffId)
            .HasColumnName("staff_id");
            entity.ToTable("staffs", "sales");

            entity.HasIndex(e => e.Email, "UQ__staffs__AB6E6164F5B4BF55").IsUnique();
            entity.Property(e => e.StaffId).HasColumnName("staff_id");
            entity.Property(e => e.Active).HasColumnName("active");
            entity.Property(e => e.Avatar).IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.ManagerId).HasColumnName("manager_id");
            entity.Property(e => e.Phone)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.StoreId).HasColumnName("store_id");

            entity.HasOne(d => d.Manager).WithMany(p => p.InverseManager)
                .HasForeignKey(d => d.ManagerId)
                .HasConstraintName("FK__staffs__manager___44FF419A");

            entity.HasOne(d => d.Store).WithMany(p => p.Staff)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK__staffs__store_id__440B1D61");

            entity.HasOne<ApplicationUser>()
            .WithOne(u => u.Staff)
            .HasForeignKey<Staff>(s => s.StaffId)
            .OnDelete(DeleteBehavior.Cascade);


        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => new { e.StoreId, e.ProductId }).HasName("PK__stocks__E68284D3B971BC12");

            entity.ToTable("stocks", "production");

            entity.Property(e => e.StoreId).HasColumnName("store_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Product).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__stocks__product___52593CB8");

            entity.HasOne(d => d.Store).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK__stocks__store_id__5165187F");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PK__stores__A2F2A30C25AEB2E7");

            entity.ToTable("stores", "sales");

            entity.Property(e => e.StoreId).HasColumnName("store_id");
            entity.Property(e => e.City)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("city");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Phone)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.State)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("state");
            entity.Property(e => e.StoreName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("store_name");
            entity.Property(e => e.Street)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("street");
            entity.Property(e => e.ZipCode)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("zip_code");
        });

        modelBuilder.Entity<Payment>(entity =>
        {

            entity.ToTable("Payment", "sales");
            entity.HasKey(e => e.PaymentId);

            entity.Property(e => e.PaymentMethod)
              .HasMaxLength(255)
              .IsUnicode(false)
              .HasColumnName("PaymentMethod");

            entity.Property(e => e.PaymentStatus)
              .HasMaxLength(255)
              .IsUnicode(false)
              .HasColumnName("PaymentStatus");

            entity.Property(e => e.PaymentDate)
            .HasColumnName("PaymentDate");

            entity.Property(e => e.UpiPayerVpa)
            .HasMaxLength(255)
            .IsUnicode(false)
            .HasColumnName("UpiPayerVpa");

            entity.Property(e => e.UpiTransactionId)
           .HasMaxLength(255)
           .IsUnicode(false)
           .HasColumnName("UpiTransactionId");


            entity.Property(e => e.PaymentStatus)
           .HasMaxLength(255)
           .IsUnicode(false)
           .HasColumnName("PaymentStatus");

            entity.Property(e => e.OrderId)
           .HasColumnName("OrderId");

            entity.HasOne(x => x.Order)
            .WithOne(x => x.Payment)
            .HasForeignKey<Payment>(p => p.OrderId)
            .OnDelete(DeleteBehavior.NoAction);


        });

        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
