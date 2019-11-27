namespace backend.Migrations
{
    using backend.Core;
    using backend.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<backend.Core.BrokerContext>
    {
        /// <summary>
        /// the constructor is creating a new instance of the configuration
        /// </summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        /// <summary>
        /// the method can be called by execute "update-database" with the nuget-console
        /// </summary>
        /// <param name="context">the database context</param>
        protected override void Seed(backend.Core.BrokerContext context)
        {
            var uMgr = new UserManager<ApplicationUser, Guid>(new UserStore<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(context));
            var rMgr = new RoleManager<ApplicationRole, Guid>(new RoleStore<ApplicationRole, Guid, ApplicationUserRole>(context));
            var adminRole = new ApplicationRole()
            {
                Name = "Admin"
            };
            context.Roles.AddOrUpdate(x => x.Name, adminRole);
            var userRole = new ApplicationRole()
            {
                Name = "User"
            };
            context.Roles.AddOrUpdate(x => x.Name, userRole);
            var adminUser = new ApplicationUser()
            {
                UserName = "Admin",
                PasswordHash = uMgr.PasswordHasher.HashPassword("cloudBroker01")
            };
            context.Users.AddOrUpdate(x => x.UserName, adminUser);
            var defaultUser = new ApplicationUser()
            {
                UserName = "User",
                PasswordHash = uMgr.PasswordHasher.HashPassword("12345678")
            };
            context.Users.AddOrUpdate(x => x.UserName, defaultUser);

            var createServicesRule = new Rule()
            {
                RuleCode = "create-services",
                RuleTitle = "Services anlegen",
                RuleDesc = "D�rfen Nutzer der Rollengruppe neue Services in der Datenbank anlegen?"
            };
            context.Rule.AddOrUpdate(x => x.RuleCode, createServicesRule);
            var editServicesRule = new Rule()
            {
                RuleCode = "edit-services",
                RuleTitle = "Services bearbeiten",
                RuleDesc = "D�rfen Nutzer der Rollengruppe Services in der Datenbank ver�ndern?"
            };
            context.Rule.AddOrUpdate(x => x.RuleCode, editServicesRule);
            var deleteServicesRule = new Rule()
            {
                RuleCode = "delete-services",
                RuleTitle = "Services l�schen",
                RuleDesc = "D�rfen Nutzer der Rollengruppe Services aus der Datenbank l�schen?"
            };
            context.Rule.AddOrUpdate(x => x.RuleCode, deleteServicesRule);
            var editSecurityGuidelines = new Rule()
            {
                RuleCode = "edit-security-guidelines",
                RuleTitle = "Rollenberechtigungen bearbeiten",
                RuleDesc = "D�rfen Nutzer der Rollengruppe Rollenberechtigungen bearbeiten?"
            };
            context.Rule.AddOrUpdate(x => x.RuleCode, editSecurityGuidelines);
            var registerRoles = new Rule()
            {
                RuleCode = "register-roles",
                RuleTitle = "Rollen anlegen",
                RuleDesc = "D�rfen Nutzer der Rollengruppe Rollen hinzuf�gen?"
            };
            context.Rule.AddOrUpdate(x => x.RuleCode, registerRoles);

            context.SaveChanges();

            uMgr.AddToRole<ApplicationUser, Guid>(adminUser.Id, adminRole.Name);
            uMgr.AddToRole<ApplicationUser, Guid>(defaultUser.Id, userRole.Name);

            adminRole.Rules.Add(createServicesRule);
            adminRole.Rules.Add(editServicesRule);
            adminRole.Rules.Add(deleteServicesRule);
            adminRole.Rules.Add(editSecurityGuidelines);
            adminRole.Rules.Add(registerRoles);

            context.SaveChanges();
        }
    }
}
