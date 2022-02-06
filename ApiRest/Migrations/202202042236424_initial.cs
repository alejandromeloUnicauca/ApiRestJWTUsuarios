namespace ApiRest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.direccion",
                c => new
                    {
                        idDireccionUsuario = c.Int(nullable: false),
                        pais = c.String(nullable: false),
                        departamento = c.String(nullable: false),
                        ciudad = c.String(nullable: false),
                        direccion = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.idDireccionUsuario)
                .ForeignKey("dbo.usuario", t => t.idDireccionUsuario)
                .Index(t => t.idDireccionUsuario);
            
            CreateTable(
                "dbo.usuario",
                c => new
                    {
                        idUsuario = c.Int(nullable: false, identity: true),
                        identificacion = c.String(nullable: false, maxLength: 15),
                        nombre = c.String(nullable: false),
                        telefono = c.String(),
                        username = c.String(nullable: false, maxLength: 25),
                        password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.idUsuario)
                .Index(t => t.identificacion, unique: true)
                .Index(t => t.username, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.direccion", "idDireccionUsuario", "dbo.usuario");
            DropIndex("dbo.usuario", new[] { "username" });
            DropIndex("dbo.usuario", new[] { "identificacion" });
            DropIndex("dbo.direccion", new[] { "idDireccionUsuario" });
            DropTable("dbo.usuario");
            DropTable("dbo.direccion");
        }
    }
}
