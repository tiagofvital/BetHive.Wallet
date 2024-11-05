﻿// <auto-generated />
using System;
using BetHive.Wallet.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BetHive.Wallet.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240229001522_WalletInitialMigration")]
    partial class WalletInitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BetHive.Wallet.Domain.BatchMovements.BatchMovement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("BatchMovements");
                });

            modelBuilder.Entity("BetHive.Wallet.Domain.Wallets.Wallet", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<float>("Balance")
                        .HasColumnType("real");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ModifiedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2");

                    b.Property<int>("TenantId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Token")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("BetHive.Wallet.Domain.BatchMovements.BatchMovement", b =>
                {
                    b.OwnsMany("BetHive.Wallet.Domain.BatchMovements.MovementRequest", "MovementRequests", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("uniqueidentifier");

                            b1.Property<float>("Amount")
                                .HasColumnType("real");

                            b1.Property<Guid>("BatchId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("ErrorDescription")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<int>("OperationType")
                                .HasColumnType("int");

                            b1.Property<int>("Status")
                                .HasColumnType("int");

                            b1.Property<Guid>("UserId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("Id");

                            b1.HasIndex("BatchId");

                            b1.ToTable("MovementRequest");

                            b1.WithOwner()
                                .HasForeignKey("BatchId");
                        });

                    b.Navigation("MovementRequests");
                });
#pragma warning restore 612, 618
        }
    }
}