using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using FileStorageMVC.Models;

namespace FileStorageMVC.Migrations
{
    [DbContext(typeof(FileContext))]
    [Migration("20160831102125_FileStorageMigration")]
    partial class FileStorageMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("FileStorageMVC.File", b =>
                {
                    b.Property<int>("Key")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Deleted");

                    b.Property<DateTime>("MarkedDeleted");

                    b.Property<string>("Name");

                    b.Property<byte[]>("Payload");

                    b.HasKey("Key");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("FileStorageMVC.Tag", b =>
                {
                    b.Property<int>("Key")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("FileKey");

                    b.Property<string>("Value");

                    b.HasKey("Key");

                    b.HasIndex("FileKey");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("FileStorageMVC.Tag", b =>
                {
                    b.HasOne("FileStorageMVC.File")
                        .WithMany("Tags")
                        .HasForeignKey("FileKey");
                });
        }
    }
}
