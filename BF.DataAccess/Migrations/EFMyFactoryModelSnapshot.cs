﻿// <auto-generated />
using System;
using BF.DataAccess.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BF.DataAccess.Migrations
{
    [DbContext(typeof(EFMyFactory))]
    partial class EFMyFactoryModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("BF.Entity.Concrete.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasColumnName("password");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("BF.Entity.Concrete.Operation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasColumnName("name");

                    b.Property<string>("ProductType")
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasColumnName("product_type");

                    b.HasKey("Id");

                    b.ToTable("Operations");
                });

            modelBuilder.Entity("BF.Entity.Concrete.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("deadline");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("order_date");

                    b.Property<int>("State")
                        .HasColumnType("int")
                        .HasColumnName("state");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("BF.Entity.Concrete.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount")
                        .HasColumnType("int")
                        .HasColumnName("amount");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("BF.Entity.Concrete.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount")
                        .HasColumnType("int")
                        .HasColumnName("amount");

                    b.Property<bool>("IsSalable")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("is_salable");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasColumnName("name");

                    b.Property<string>("Type")
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("BF.Entity.Concrete.Schedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("end");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("start");

                    b.Property<int>("WorkcenterId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("WorkcenterId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("BF.Entity.Concrete.SubProduct", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Amount")
                        .HasColumnType("int")
                        .HasColumnName("amount");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("SubProducts");
                });

            modelBuilder.Entity("BF.Entity.Concrete.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4")
                        .HasColumnName("name");

                    b.Property<string>("Password")
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasColumnName("password");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BF.Entity.Concrete.WorkCenter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Active")
                        .HasColumnType("tinyint(1)")
                        .HasColumnName("active");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("WorkCenters");
                });

            modelBuilder.Entity("BF.Entity.Concrete.WorkCenterOperation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OperationId")
                        .HasColumnType("int");

                    b.Property<int>("Speed")
                        .HasColumnType("int")
                        .HasColumnName("speed");

                    b.Property<int>("WorkCenterId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OperationId");

                    b.HasIndex("WorkCenterId");

                    b.ToTable("WorkCenterOperations");
                });

            modelBuilder.Entity("BF.Entity.Concrete.Order", b =>
                {
                    b.HasOne("BF.Entity.Concrete.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("BF.Entity.Concrete.OrderItem", b =>
                {
                    b.HasOne("BF.Entity.Concrete.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BF.Entity.Concrete.Product", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("BF.Entity.Concrete.Schedule", b =>
                {
                    b.HasOne("BF.Entity.Concrete.Product", "Product")
                        .WithMany("Schedules")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BF.Entity.Concrete.WorkCenter", "WorkCenter")
                        .WithMany("Schedules")
                        .HasForeignKey("WorkcenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("WorkCenter");
                });

            modelBuilder.Entity("BF.Entity.Concrete.SubProduct", b =>
                {
                    b.HasOne("BF.Entity.Concrete.Product", "Product")
                        .WithMany("SubProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("BF.Entity.Concrete.WorkCenterOperation", b =>
                {
                    b.HasOne("BF.Entity.Concrete.Operation", "Operation")
                        .WithMany("WorkCenterOperations")
                        .HasForeignKey("OperationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BF.Entity.Concrete.WorkCenter", "WorkCenter")
                        .WithMany("WorkCenterOperations")
                        .HasForeignKey("WorkCenterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Operation");

                    b.Navigation("WorkCenter");
                });

            modelBuilder.Entity("BF.Entity.Concrete.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("BF.Entity.Concrete.Operation", b =>
                {
                    b.Navigation("WorkCenterOperations");
                });

            modelBuilder.Entity("BF.Entity.Concrete.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("BF.Entity.Concrete.Product", b =>
                {
                    b.Navigation("OrderItems");

                    b.Navigation("Schedules");

                    b.Navigation("SubProducts");
                });

            modelBuilder.Entity("BF.Entity.Concrete.WorkCenter", b =>
                {
                    b.Navigation("Schedules");

                    b.Navigation("WorkCenterOperations");
                });
#pragma warning restore 612, 618
        }
    }
}
