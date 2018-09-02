﻿// <auto-generated />
using System;
using DreamsComeTrueAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
                .HasAnnotation("ProductVersion", "2.1.2-rtm-30932");

            modelBuilder.Entity("DreamsComeTrueAPI.Models.TodoItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AuthorId");

                    b.Property<int>("Cost");

                    b.Property<string>("Objective");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("TodoItems");
                });

            modelBuilder.Entity("DreamsComeTrueAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Login");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DreamsComeTrueAPI.Models.TodoItem", b =>
                {
                    b.HasOne("DreamsComeTrueAPI.Models.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId");
                });
#pragma warning restore 612, 618
        }
    }
}
