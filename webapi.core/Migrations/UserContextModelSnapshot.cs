﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using webapi.core.Data;

namespace webapi.core.Migrations
{
    [DbContext(typeof(UserContext))]
    partial class UserContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("webapi.core.Entities.UserEntities", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd();                   
                    b.Property<DateTime>("created");
                    b.Property<string>("createdby")
                        .HasMaxLength(150);
                    b.Property<DateTime>("modified");
                    b.Property<string>("modifiedby")
                        .HasMaxLength(150);
                    b.Property<string>("password")
                        .IsRequired()
                        .HasMaxLength(150);
                    b.Property<string>("userName")
                        .IsRequired()
                        .HasMaxLength(150);
                    b.HasKey("id");
                    b.ToTable("tblUser");
                });
#pragma warning restore 612, 618
        }
    }
}