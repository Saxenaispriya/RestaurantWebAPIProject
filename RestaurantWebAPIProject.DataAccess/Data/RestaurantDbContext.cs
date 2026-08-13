using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantWebAPIProject.Common.Models.Entities;

namespace RestaurantWebAPIProject.DataAccess.Data
{
    public class RestaurantDbContext:DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options):base(options)
        {

        }

        public DbSet<RestaurantTable> RestaurantTables { get; set; }

        public DbSet<FoodItem> FoodItems { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RestaurantTable>()
              .HasIndex(table => table.TableNumber)
              .IsUnique();

            // RestaurantTable 1 → Many Orders
            modelBuilder.Entity<Order>()
                .HasOne(order => order.RestaurantTable)
                .WithMany(table => table.Orders)
                .HasForeignKey(order => order.RestaurantTableId);

            // Order 1 → Many OrderItems
            modelBuilder.Entity<OrderItem>()
                .HasOne(orderItem => orderItem.Order)
                .WithMany(order => order.OrderItems)
                .HasForeignKey(orderItem => orderItem.OrderId);

            // FoodItem 1 → Many OrderItems
            modelBuilder.Entity<OrderItem>()
                .HasOne(orderItem => orderItem.FoodItem)
                .WithMany(food => food.OrderItems)
                .HasForeignKey(orderItem => orderItem.FoodItemId);
        }

    }
}
