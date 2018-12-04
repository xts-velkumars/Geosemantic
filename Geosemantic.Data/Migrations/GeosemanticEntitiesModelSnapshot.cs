﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Geosemantic.Data.Migrations
{
    [DbContext(typeof(GeosemanticEntities))]
    partial class GeosemanticEntitiesModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Geosemantic.Domain.Entities.Page", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BadgeText");

                    b.Property<DateTime>("CreationTs");

                    b.Property<string>("CreationUserId")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("HasBadge");

                    b.Property<string>("Icon");

                    b.Property<string>("Label");

                    b.Property<DateTime?>("LastChangeTs");

                    b.Property<string>("LastChangeUserId")
                        .HasMaxLength(50);

                    b.Property<string>("Name");

                    b.Property<long>("ParentId");

                    b.Property<long>("Sequence");

                    b.Property<string>("ShortName");

                    b.Property<byte>("StatusType");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("Page");
                });

            modelBuilder.Entity("Geosemantic.Domain.Entities.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTs");

                    b.Property<string>("CreationUserId")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime?>("LastChangeTs");

                    b.Property<string>("LastChangeUserId")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<byte>("RoleSystemType");

                    b.Property<byte>("StatusType");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("Geosemantic.Domain.Entities.RolePageMapping", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTs");

                    b.Property<string>("CreationUserId")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime?>("LastChangeTs");

                    b.Property<string>("LastChangeUserId")
                        .HasMaxLength(50);

                    b.Property<long>("PageId");

                    b.Property<long>("RoleId");

                    b.Property<byte>("StatusType");

                    b.HasKey("Id");

                    b.HasIndex("PageId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePageMapping");
                });

            modelBuilder.Entity("Geosemantic.Domain.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationTs");

                    b.Property<string>("CreationUserId")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<byte?>("GenderType");

                    b.Property<DateTime?>("LastChangeTs");

                    b.Property<string>("LastChangeUserId")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasMaxLength(10);

                    b.Property<string>("Password");

                    b.Property<long>("RoleId");

                    b.Property<byte>("StatusType");

                    b.Property<byte>("UserStatusType");

                    b.Property<byte>("UserSystemType");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Xen.Entity.Entities.CommandAudit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Data");

                    b.Property<string>("ExceptionMessage");

                    b.Property<bool>("IsSuccess");

                    b.Property<long>("LoggedOnUserId");

                    b.Property<string>("MessageId");

                    b.Property<int>("Milliseconds");

                    b.Property<string>("Type");

                    b.Property<DateTime>("UtcTimeStamp");

                    b.HasKey("Id");

                    b.ToTable("CommandAudit");
                });

            modelBuilder.Entity("Geosemantic.Domain.Entities.RolePageMapping", b =>
                {
                    b.HasOne("Geosemantic.Domain.Entities.Page", "Page")
                        .WithMany("RolePageMapping")
                        .HasForeignKey("PageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Geosemantic.Domain.Entities.Role", "Role")
                        .WithMany("RolePageMapping")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Geosemantic.Domain.Entities.User", b =>
                {
                    b.HasOne("Geosemantic.Domain.Entities.Role", "Role")
                        .WithMany("User")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
