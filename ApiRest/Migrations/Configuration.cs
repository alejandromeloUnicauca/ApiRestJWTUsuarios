namespace ApiRest.Migrations
{
    using ApiRest.Models;
    using ApiRest.Util;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.SqlClient;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApiRest.Models.Context.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApiRest.Models.Context.DatabaseContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            //context.usuarios.AddOrUpdate(u => u.idUsuario,
            //   new Usuario() { nombre = "Pepito Perez", identificacion = "1234", username = "pepito01", password = Sha256.computeSha256("Passw0rd"), telefono = "1020304013" });
           
        }
    }
}
