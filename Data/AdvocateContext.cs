using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Advocate.Areas.Identity.Data;
using Advocate.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Advocate.Data
{
    public class AdvocateContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<NavigationEntity> MenuItems { get; set; }
        public DbSet<SubjectEntity> Subjects { get; set; }
        public DbSet<ActTypeEntity> ActTyes { get; set; }
        public DbSet<BookEntity> BookMaster { get; set; }
        public DbSet<GazetteTypeEntity> gazetteTypeEntities { get; set; }
        public DbSet<PartEntity> PartEntities { get; set; }
        public DbSet<ActEntity> ActEntities { get; set; }
        public DbSet<AmendedActEntity> AmendedActEntities { get; set; }
        public DbSet<RepealedActEntity> RepealedActEntities{ get; set; }
        public DbSet<ActBookEntity> ActBookEntities{ get; set; }
        public DbSet<RuleEntity> RuleEntities { get; set; }
        public DbSet<RuleAmendedEntity> RuleAmendedEntities { get; set; }
        public DbSet<RuleRepealedEntity> RuleRepealedEntities { get; set; }
        public DbSet<RuleBookEntity> RuleBookEntities { get; set; }
        public DbSet<RuleActExtraEntity> RuleActExtraEntities { get; set; }
        public DbSet<NotificationTypeEntity> NotificationTypes { get; set; }
        public DbSet<NotificationEntity> NotificationEntities { get; set; }
        public DbSet<NotificationAmendedEntity> NotificationAmendedEntities { get; set; }
        public DbSet<NotificationRepealedEntity> notificationRepealedEntities { get; set; }
        public DbSet<NotificationBookEntity> NotificationBookEntities { get; set; }
        public DbSet<NotificationExtActEntity> NotificationExtActEntities { get; set; }
        public DbSet<BookEntryDetailEntity> BookEntryDetailEntities { get; set; }

        public AdvocateContext(DbContextOptions<AdvocateContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
           
            builder.Entity<ApplicationUser>(entity => {
                entity.ToTable(name:"Mst_Users");
            });

            builder.Entity<IdentityRole>(entity => {
                entity.ToTable(name:"Mst_Roles");
            });

            builder.Entity<IdentityUserLogin<string>>(entity => {
                entity.ToTable(name: "Mst_UserLogins");
            });
            builder.Entity<IdentityUserRole<string>>(entity => {
                entity.ToTable(name: "Mst_UserRoles");
            });            
           
            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("Mst_RoleClaims");
            });
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("Mst_UserTokens");
            });

            builder.Entity<NavigationEntity>().HasIndex(code=>code.MenuCode).IsUnique();

            builder.Entity<SubjectEntity>().HasIndex(subject=>subject.Name).IsUnique();
            builder.Entity<SubjectEntity>().Property(p => p.Id).ValueGeneratedNever();

            builder.Entity<ActTypeEntity>().HasIndex(subject => subject.ActType).IsUnique();            

            builder.Entity<GazetteTypeEntity>().HasIndex(subject => subject.GazetteName).IsUnique();

            builder.Entity<PartEntity>().HasIndex(p=>new { p.PartName,p.GazettId}).IsUnique();


            builder.Entity<ActEntity>().HasIndex(p => new { p.ActTypeId, p.ActNumber,p.ActYear }).IsUnique();

            builder.Entity<ActEntity>().Property(p => p.Id).ValueGeneratedNever();
            builder.Entity<RuleEntity>().Property(p => p.Id).ValueGeneratedNever();

            builder.Entity<NotificationEntity>().Property(p => p.Id).ValueGeneratedNever();
            builder.Entity<NotificationTypeEntity>().HasIndex(noti => noti.Name).IsUnique();
            //builder.Entity<NotificationTypeEntity>().Property(p => p.Id).ValueGeneratedNever();

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
