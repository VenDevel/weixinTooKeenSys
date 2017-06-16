using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using WeixinTookeen.Client.Model.Initializer;

namespace WeixinTookeen.Client.Model
{
    public class RobotContext : DbContext
    {
        public RobotContext() 
            :base("RobotDb")
        {
            Configure();
        }

        private void Configure()
        {
            Configuration.ProxyCreationEnabled = true;
            Configuration.LazyLoadingEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.AddFromAssembly(typeof(RobotContext).Assembly);
            Database.SetInitializer(new DbInitalizerIfNotExists(modelBuilder));
        }

    }
}
