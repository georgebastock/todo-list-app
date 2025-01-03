﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoListApi.Data;

#nullable disable

namespace TodoListApi.Migrations
{
    [DbContext(typeof(TodoContext))]
    [Migration("20241219152323_UpdateSeedData")]
    partial class UpdateSeedData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("TodoListApi.Models.TaskItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Tasks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Incomplete task 1",
                            DueDate = new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsCompleted = false,
                            Title = "Initial Task 1"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Complete task 2",
                            DueDate = new DateTime(2024, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsCompleted = true,
                            Title = "Initial Task 2"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
