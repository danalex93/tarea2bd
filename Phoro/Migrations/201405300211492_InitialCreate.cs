namespace Phoro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BuzonEntradas",
                c => new
                    {
                        id_buzon = c.Int(nullable: false, identity: true),
                        id_usuario = c.Int(nullable: false),
                        mensajes = c.Int(nullable: false),
                        mensajes_sin_leer = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id_buzon)
                .ForeignKey("dbo.Usuarios", t => t.id_usuario)
                .Index(t => t.id_usuario);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        id_usuario = c.Int(nullable: false, identity: true),
                        id_grupo = c.Int(nullable: false),
                        nombre = c.String(),
                        contrasena = c.String(),
                        cantidad_comentarios = c.Int(nullable: false),
                        avatar_url = c.String(),
                        fecha_nacimiento = c.DateTime(nullable: false),
                        sexo = c.String(),
                        fecha_registro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id_usuario)
                .ForeignKey("dbo.GrupoUsuarios", t => t.id_grupo)
                .Index(t => t.id_grupo);
            
            CreateTable(
                "dbo.GrupoUsuarios",
                c => new
                    {
                        id_grupo = c.Int(nullable: false, identity: true),
                        nombre_grupo = c.String(),
                        creacion_categoria = c.Boolean(nullable: false),
                        eliminar_categoria = c.Boolean(nullable: false),
                        creacion_tema = c.Boolean(nullable: false),
                        eliminar_tema = c.Boolean(nullable: false),
                        publicar_comentario = c.Boolean(nullable: false),
                        eliminar_mensaje = c.Boolean(nullable: false),
                        editar_mensaje = c.Boolean(nullable: false),
                        editar_usuario = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id_grupo);
            
            CreateTable(
                "dbo.Categorias",
                c => new
                    {
                        id_categoria = c.Int(nullable: false, identity: true),
                        nombre = c.String(),
                        descripcion = c.String(),
                        publico = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id_categoria);
            
            CreateTable(
                "dbo.Comentarios",
                c => new
                    {
                        id_comentario = c.Int(nullable: false, identity: true),
                        id_tema = c.Int(nullable: false),
                        id_usuario = c.Int(nullable: false),
                        text = c.String(),
                    })
                .PrimaryKey(t => t.id_comentario)
                .ForeignKey("dbo.Temas", t => t.id_tema)
                .ForeignKey("dbo.Usuarios", t => t.id_usuario)
                .Index(t => t.id_tema)
                .Index(t => t.id_usuario);
            
            CreateTable(
                "dbo.Temas",
                c => new
                    {
                        id_tema = c.Int(nullable: false, identity: true),
                        id_categoria = c.Int(nullable: false),
                        id_usuario = c.Int(nullable: false),
                        nombre = c.String(),
                        mensaje = c.String(),
                        publico = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id_tema)
                .ForeignKey("dbo.Categorias", t => t.id_categoria)
                .ForeignKey("dbo.Usuarios", t => t.id_usuario)
                .Index(t => t.id_categoria)
                .Index(t => t.id_usuario);
            
            CreateTable(
                "dbo.MensajePrivadoes",
                c => new
                    {
                        id_mensaje = c.Int(nullable: false, identity: true),
                        id_remitente = c.Int(nullable: false),
                        id_buzon = c.Int(nullable: false),
                        leido = c.Boolean(nullable: false),
                        mensaje = c.String(),
                        fecha_de_envio = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id_mensaje)
                .ForeignKey("dbo.BuzonEntradas", t => t.id_buzon)
                .ForeignKey("dbo.Usuarios", t => t.id_remitente)
                .Index(t => t.id_remitente)
                .Index(t => t.id_buzon);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.MensajePrivadoes", "id_remitente", "dbo.Usuarios");
            DropForeignKey("dbo.MensajePrivadoes", "id_buzon", "dbo.BuzonEntradas");
            DropForeignKey("dbo.Comentarios", "id_usuario", "dbo.Usuarios");
            DropForeignKey("dbo.Comentarios", "id_tema", "dbo.Temas");
            DropForeignKey("dbo.Temas", "id_usuario", "dbo.Usuarios");
            DropForeignKey("dbo.Temas", "id_categoria", "dbo.Categorias");
            DropForeignKey("dbo.BuzonEntradas", "id_usuario", "dbo.Usuarios");
            DropForeignKey("dbo.Usuarios", "id_grupo", "dbo.GrupoUsuarios");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.MensajePrivadoes", new[] { "id_buzon" });
            DropIndex("dbo.MensajePrivadoes", new[] { "id_remitente" });
            DropIndex("dbo.Temas", new[] { "id_usuario" });
            DropIndex("dbo.Temas", new[] { "id_categoria" });
            DropIndex("dbo.Comentarios", new[] { "id_usuario" });
            DropIndex("dbo.Comentarios", new[] { "id_tema" });
            DropIndex("dbo.Usuarios", new[] { "id_grupo" });
            DropIndex("dbo.BuzonEntradas", new[] { "id_usuario" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.MensajePrivadoes");
            DropTable("dbo.Temas");
            DropTable("dbo.Comentarios");
            DropTable("dbo.Categorias");
            DropTable("dbo.GrupoUsuarios");
            DropTable("dbo.Usuarios");
            DropTable("dbo.BuzonEntradas");
        }
    }
}
