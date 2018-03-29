using ICoinManager.Domain.Busiess.Entities;
using ICoinManager.Domain.Entities.Business;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ICoinManager.Infra.EntityData.Context
{
    public class ICoinManagerContext : DbContext
    {
        public ICoinManagerContext()
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;

            var createIfNotExists = new CreateDatabaseIfNotExists<ICoinManagerContext>();
            createIfNotExists.InitializeDatabase(this);
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Exchange> Exchanges { get; set; }
        public DbSet<Coin> Coins { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Remoção de convenções default do Entity Framework
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            //Configurações padrão
            modelBuilder.Properties()
                .Where(p => p.Name == "ID" || p.Name == "Id")
                .Configure(p => p.IsKey());

            modelBuilder.Properties()
                .Where(p => p.Name == "ID" || p.Name == "Id")
                .Configure(p => p.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity));

            modelBuilder.Properties()
                .Where(p => Regex.IsMatch(p.Name, @"^\w*?Id$"))
                .Configure(p => p.HasColumnName(p.ClrPropertyInfo.Name.Replace("Id", "ID")));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Properties<string>()
                .Configure(p => p.HasMaxLength(64));

            base.OnModelCreating(modelBuilder);
        }
        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries().Where(e => e.Entity.GetType().GetProperty("CreatedOn") != null))
            {
                if (entry.State == EntityState.Added)
                    entry.Property("CreatedOn").CurrentValue = DateTime.Now;

                if (entry.State == EntityState.Modified)
                    entry.Property("CreatedOn").IsModified = false;
            }

            return base.SaveChanges();
        }
    }
}
