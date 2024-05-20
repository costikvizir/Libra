namespace Libra.Dal.Migrations
{
    using Libra.Dal.Context;
    using System.Data.Entity.Migrations;

    public sealed class Configuration : DbMigrationsConfiguration<Libra.Dal.Context.LibraContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LibraContext context)
        {
        }
    }
}