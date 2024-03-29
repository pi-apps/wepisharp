﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Volo.Abp.EntityFrameworkCore;
using WePi.EntityFrameworkCore;

#nullable disable

namespace WePi.Migrations
{
    [DbContext(typeof(WePiDbContext))]
    [Migration("20230226145612_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("_Abp_DatabaseProvider", EfCoreDatabaseProvider.PostgreSql)
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WePi.Domain.PiPayment", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<int>("a2u")
                        .HasColumnType("integer");

                    b.Property<double>("amount")
                        .HasColumnType("double precision");

                    b.Property<bool>("approved")
                        .HasColumnType("boolean");

                    b.Property<string>("asset")
                        .HasColumnType("text");

                    b.Property<bool>("cancelled")
                        .HasColumnType("boolean");

                    b.Property<string>("comment")
                        .HasColumnType("text");

                    b.Property<bool>("completed")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset>("created_at")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("direction")
                        .HasColumnType("text");

                    b.Property<string>("domain")
                        .HasColumnType("text");

                    b.Property<double>("fee")
                        .HasColumnType("double precision");

                    b.Property<bool>("finished")
                        .HasColumnType("boolean");

                    b.Property<string>("from_address")
                        .HasColumnType("text");

                    b.Property<string>("identifier")
                        .HasColumnType("text");

                    b.Property<Guid>("instance_id")
                        .HasColumnType("uuid");

                    b.Property<string>("memo")
                        .HasColumnType("text");

                    b.Property<string>("network")
                        .HasColumnType("text");

                    b.Property<string>("obj_cat")
                        .HasColumnType("text");

                    b.Property<Guid>("obj_id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("person_id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("pi_uid")
                        .HasColumnType("uuid");

                    b.Property<string>("pi_username")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("published")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ref_id")
                        .HasColumnType("uuid");

                    b.Property<string>("stat")
                        .HasColumnType("text");

                    b.Property<int>("step")
                        .HasColumnType("integer");

                    b.Property<bool>("testnet")
                        .HasColumnType("boolean");

                    b.Property<string>("to_address")
                        .HasColumnType("text");

                    b.Property<string>("tx_id")
                        .HasColumnType("text");

                    b.Property<string>("tx_link")
                        .HasColumnType("text");

                    b.Property<bool>("tx_verified")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset>("updated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("user_cancelled")
                        .HasColumnType("boolean");

                    b.Property<string>("user_uid")
                        .HasColumnType("text");

                    b.Property<bool>("verified")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("WePipipayment", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
