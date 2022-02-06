using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ApiRest.Models.Context
{
    public class DatabaseContext:DbContext
    {
        public DatabaseContext() : base("conex") 
        {
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }

        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<Direccion> direcciones { get; set; }

    }
}