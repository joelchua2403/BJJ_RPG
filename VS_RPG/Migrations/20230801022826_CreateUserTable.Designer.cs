﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using VS_RPG.Data;

#nullable disable

namespace VS_RPG.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230801022826_CreateUserTable")]
    partial class CreateUserTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("VS_RPG.Models.Character", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Agility")
                        .HasColumnType("integer");

                    b.Property<int>("Class")
                        .HasColumnType("integer");

                    b.Property<int>("HitPoints")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Skill")
                        .HasColumnType("integer");

                    b.Property<int>("Strength")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("VS_RPG.Models.CharacterMove", b =>
                {
                    b.Property<int>("CharacterId")
                        .HasColumnType("integer");

                    b.Property<int>("MoveId")
                        .HasColumnType("integer");

                    b.HasKey("CharacterId", "MoveId");

                    b.HasIndex("MoveId");

                    b.ToTable("CharacterMoves");
                });

            modelBuilder.Entity("VS_RPG.Models.Move", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Points")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Moves");
                });

            modelBuilder.Entity("VS_RPG.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("VS_RPG.Models.CharacterMove", b =>
                {
                    b.HasOne("VS_RPG.Models.Character", "Character")
                        .WithMany("CharacterMoves")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("VS_RPG.Models.Move", "Move")
                        .WithMany("CharacterMoves")
                        .HasForeignKey("MoveId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");

                    b.Navigation("Move");
                });

            modelBuilder.Entity("VS_RPG.Models.Character", b =>
                {
                    b.Navigation("CharacterMoves");
                });

            modelBuilder.Entity("VS_RPG.Models.Move", b =>
                {
                    b.Navigation("CharacterMoves");
                });
#pragma warning restore 612, 618
        }
    }
}
