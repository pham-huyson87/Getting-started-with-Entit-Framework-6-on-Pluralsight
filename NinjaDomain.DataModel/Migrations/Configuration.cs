namespace NinjaDomain.DataModel.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<NinjaDomain.DataModel.NinjaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(NinjaDomain.DataModel.NinjaContext context)
        {
            context.Clans.AddOrUpdate(x => x.Id,
                new Classes.Clan() {
                Id = 1,
                ClanName = "Vermont Ninjas"
            });
        }
    }
}
