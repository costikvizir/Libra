namespace Libra.Dal.Migrations
{
    using Libra.Dal.Context;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<Libra.Dal.Context.LibraContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Libra.Dal.Context.LibraContext context)
        {
            
        }
    }
}
