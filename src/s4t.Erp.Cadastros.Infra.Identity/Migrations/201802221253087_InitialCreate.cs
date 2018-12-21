namespace s4t.Erp.Cadastros.Infra.Identity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "s4tIdentity.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "s4tIdentity.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("s4tIdentity.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("s4tIdentity.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "s4tIdentity.AspNetUsers",
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
                "s4tIdentity.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("s4tIdentity.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "s4tIdentity.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("s4tIdentity.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("s4tIdentity.AspNetUserRoles", "UserId", "s4tIdentity.AspNetUsers");
            DropForeignKey("s4tIdentity.AspNetUserLogins", "UserId", "s4tIdentity.AspNetUsers");
            DropForeignKey("s4tIdentity.AspNetUserClaims", "UserId", "s4tIdentity.AspNetUsers");
            DropForeignKey("s4tIdentity.AspNetUserRoles", "RoleId", "s4tIdentity.AspNetRoles");
            DropIndex("s4tIdentity.AspNetUserLogins", new[] { "UserId" });
            DropIndex("s4tIdentity.AspNetUserClaims", new[] { "UserId" });
            DropIndex("s4tIdentity.AspNetUsers", "UserNameIndex");
            DropIndex("s4tIdentity.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("s4tIdentity.AspNetUserRoles", new[] { "UserId" });
            DropIndex("s4tIdentity.AspNetRoles", "RoleNameIndex");
            DropTable("s4tIdentity.AspNetUserLogins");
            DropTable("s4tIdentity.AspNetUserClaims");
            DropTable("s4tIdentity.AspNetUsers");
            DropTable("s4tIdentity.AspNetUserRoles");
            DropTable("s4tIdentity.AspNetRoles");
        }
    }
}
