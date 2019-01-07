﻿// <auto-generated />

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using StoreApplication.Repository;

namespace StoreApplication.WebApi.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20190105135546_TestProgram.Models.ApplicationContext")]
    partial class TestProgramModelsApplicationContext
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TestProgram.Models.Categorie", b =>
                {
                    b.Property<Guid>("CategoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CategoryDescription");

                    b.Property<string>("CategoryName");

                    b.Property<int>("CategorySortOrder");

                    b.Property<DateTime?>("CreationDate");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("TestProgram.Models.Product", b =>
                {
                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("CreationDate");

                    b.Property<Guid?>("ProductCategoryCategoryId");

                    b.Property<string>("ProductDescription");

                    b.Property<string>("ProductImage");

                    b.Property<string>("ProductName");

                    b.Property<int>("ProductSortOrder");

                    b.HasKey("ProductId");

                    b.HasIndex("ProductCategoryCategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("TestProgram.Models.Product", b =>
                {
                    b.HasOne("TestProgram.Models.Categorie", "ProductCategory")
                        .WithMany()
                        .HasForeignKey("ProductCategoryCategoryId");
                });
#pragma warning restore 612, 618
        }
    }
}
