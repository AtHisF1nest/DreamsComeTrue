﻿// <auto-generated />
using System;
using DreamsComeTrueAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DreamsComeTrueAPI.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DreamsComeTrueAPI.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AuthorId");

                    b.Property<string>("BackgroundColor");

                    b.Property<int>("CategoryType");

                    b.Property<string>("Color");

                    b.Property<string>("Name");

                    b.Property<int>("UsersPairId");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("UsersPairId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("DreamsComeTrueAPI.Models.CategoryTodoItemBinding", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoryId");

                    b.Property<int?>("TodoItemId");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("TodoItemId");

                    b.ToTable("CategoryTodoItemBindings");
                });

            modelBuilder.Entity("DreamsComeTrueAPI.Models.History", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Done");

                    b.Property<int?>("TodoItemId");

                    b.HasKey("Id");

                    b.HasIndex("TodoItemId");

                    b.ToTable("Histories");
                });

            modelBuilder.Entity("DreamsComeTrueAPI.Models.ManagementType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ButtonText");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("PhotoUrl");

                    b.Property<string>("Theme");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("ManagementTypes");
                });

            modelBuilder.Entity("DreamsComeTrueAPI.Models.Photo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Url");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("DreamsComeTrueAPI.Models.TodoItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AuthorId");

                    b.Property<int>("Cost");

                    b.Property<DateTime>("Created");

                    b.Property<DateTime?>("LastDone");

                    b.Property<string>("Objective");

                    b.Property<int>("UsersPairId");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("UsersPairId");

                    b.ToTable("TodoItems");
                });

            modelBuilder.Entity("DreamsComeTrueAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Login");

                    b.Property<string>("Name");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DreamsComeTrueAPI.Models.UsersPair", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RelationshipType");

                    b.Property<int?>("User2Id");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("User2Id");

                    b.HasIndex("UserId");

                    b.ToTable("UsersPairs");
                });

            modelBuilder.Entity("DreamsComeTrueAPI.Models.Category", b =>
                {
                    b.HasOne("DreamsComeTrueAPI.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.HasOne("DreamsComeTrueAPI.Models.UsersPair", "UsersPair")
                        .WithMany()
                        .HasForeignKey("UsersPairId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DreamsComeTrueAPI.Models.CategoryTodoItemBinding", b =>
                {
                    b.HasOne("DreamsComeTrueAPI.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId");

                    b.HasOne("DreamsComeTrueAPI.Models.TodoItem", "TodoItem")
                        .WithMany()
                        .HasForeignKey("TodoItemId");
                });

            modelBuilder.Entity("DreamsComeTrueAPI.Models.History", b =>
                {
                    b.HasOne("DreamsComeTrueAPI.Models.TodoItem", "TodoItem")
                        .WithMany()
                        .HasForeignKey("TodoItemId");
                });

            modelBuilder.Entity("DreamsComeTrueAPI.Models.Photo", b =>
                {
                    b.HasOne("DreamsComeTrueAPI.Models.User", "User")
                        .WithOne("Photo")
                        .HasForeignKey("DreamsComeTrueAPI.Models.Photo", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DreamsComeTrueAPI.Models.TodoItem", b =>
                {
                    b.HasOne("DreamsComeTrueAPI.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");

                    b.HasOne("DreamsComeTrueAPI.Models.UsersPair", "UsersPair")
                        .WithMany()
                        .HasForeignKey("UsersPairId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("DreamsComeTrueAPI.Models.UsersPair", b =>
                {
                    b.HasOne("DreamsComeTrueAPI.Models.User", "User2")
                        .WithMany()
                        .HasForeignKey("User2Id");

                    b.HasOne("DreamsComeTrueAPI.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
