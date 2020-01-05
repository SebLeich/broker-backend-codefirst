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
        protected override void Seed(BrokerContext context)
        {
            var uMgr = new UserManager<ApplicationUser, Guid>(new UserStore<ApplicationUser, ApplicationRole, Guid, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>(context));
            var rMgr = new RoleManager<ApplicationRole, Guid>(new RoleStore<ApplicationRole, Guid, ApplicationUserRole>(context));
            var adminRole = new ApplicationRole
            {
                Name = "Admin"
            };
            context.Roles.AddOrUpdate(x => x.Name, adminRole);
            var userRole = new ApplicationRole
            {
                Name = "User"
            };
            context.Roles.AddOrUpdate(x => x.Name, userRole);
            var adminUser = new ApplicationUser
            {
                UserName = "Admin",
                PasswordHash = uMgr.PasswordHasher.HashPassword("cloudBroker01")
            };
            context.Users.AddOrUpdate(x => x.UserName, adminUser);
            var defaultUser = new ApplicationUser
            {
                UserName = "User",
                PasswordHash = uMgr.PasswordHasher.HashPassword("12345678")
            };
            context.Users.AddOrUpdate(x => x.UserName, defaultUser);

            var createServicesRule = new Rule
            {
                RuleCode = "create-services",
                RuleTitle = "Services anlegen",
                RuleDesc = "Dürfen Nutzer der Rollengruppe neue Services in der Datenbank anlegen?"
            };
            context.Rule.AddOrUpdate(x => x.RuleCode, createServicesRule);
            var editServicesRule = new Rule
            {
                RuleCode = "edit-services",
                RuleTitle = "Services bearbeiten",
                RuleDesc = "Dürfen Nutzer der Rollengruppe Services in der Datenbank verändern?"
            };
            context.Rule.AddOrUpdate(x => x.RuleCode, editServicesRule);
            var deleteServicesRule = new Rule
            {
                RuleCode = "delete-services",
                RuleTitle = "Services löschen",
                RuleDesc = "Dürfen Nutzer der Rollengruppe Services aus der Datenbank löschen?"
            };
            context.Rule.AddOrUpdate(x => x.RuleCode, deleteServicesRule);
            var editSecurityGuidelines = new Rule
            {
                RuleCode = "edit-security-guidelines",
                RuleTitle = "Rollenberechtigungen bearbeiten",
                RuleDesc = "Dürfen Nutzer der Rollengruppe Rollenberechtigungen bearbeiten?"
            };
            context.Rule.AddOrUpdate(x => x.RuleCode, editSecurityGuidelines);
            var registerRoles = new Rule
            {
                RuleCode = "register-roles",
                RuleTitle = "Rollen anlegen",
                RuleDesc = "Dürfen Nutzer der Rollengruppe Rollen hinzufügen?"
            };
            context.Rule.AddOrUpdate(x => x.RuleCode, registerRoles);
            var deleteRoles = new Rule
            {
                RuleCode = "delete-roles",
                RuleTitle = "Rollen löschen",
                RuleDesc = "Dürfen Nutzer der Rollengruppe Rollen entfernen?"
            };
            context.Rule.AddOrUpdate(x => x.RuleCode, deleteRoles);

            context.SaveChanges();

            uMgr.AddToRole<ApplicationUser, Guid>(adminUser.Id, adminRole.Name);
            uMgr.AddToRole<ApplicationUser, Guid>(defaultUser.Id, userRole.Name);

            adminRole.Rules.Add(createServicesRule);
            adminRole.Rules.Add(editServicesRule);
            adminRole.Rules.Add(deleteServicesRule);
            adminRole.Rules.Add(editSecurityGuidelines);
            adminRole.Rules.Add(registerRoles);

            var pricingPeriodD = new PricingPeriod { PricingPeriodName = "Täglich", PricingPeriodeCode = "daily" };
            var pricingPeriodM = new PricingPeriod { PricingPeriodName = "Monatlich", PricingPeriodeCode = "monthly" };
            var pricingPeriodA = new PricingPeriod { PricingPeriodName = "Jährlich", PricingPeriodeCode = "annually" };

            context.PricingPeriod.Add(pricingPeriodD);
            context.PricingPeriod.Add(pricingPeriodM);
            context.PricingPeriod.Add(pricingPeriodA);

            var pricingModelUSR = new PricingModel { PricingModelName = "Nutzer-basiert" };
            var pricingModelUSA = new PricingModel { PricingModelName = "Nutzungs-basiert" };
            var pricingModelHYB = new PricingModel { PricingModelName = "Hybride Preiskalkulation" };

            context.PricingModel.Add(pricingModelUSR);
            context.PricingModel.Add(pricingModelUSA);
            context.PricingModel.Add(pricingModelHYB);

            var deploymentPUB = new DeploymentInfo { DeploymentName = "Public" };
            var deploymentPRV = new DeploymentInfo { DeploymentName = "Private" };

            context.DeploymentInfo.Add(deploymentPUB);
            context.DeploymentInfo.Add(deploymentPRV);

            var cert1 = new Certificate { CertificateName = "ISO 27001" };
            var cert2 = new Certificate { CertificateName = "CSA-STAR-Zertifizierung" };

            context.Certificate.Add(cert1);
            context.Certificate.Add(cert2);

            var locationTypeC = new DataLocationType { TypeName = "Kontinent" };
            var locationTypeR = new DataLocationType { TypeName = "Region" };

            context.DataLocationType.Add(locationTypeC);
            context.DataLocationType.Add(locationTypeR);

            context.SaveChanges();

            var dataLocationEU = new DataLocation { DataLocationName = "EU", DataLocationTypeId = locationTypeC.Id };
            var dataLocationGER = new DataLocation { DataLocationName = "Deutschsprachiger Raum", DataLocationTypeId = locationTypeR.Id };

            context.DataLocation.Add(dataLocationEU);
            context.DataLocation.Add(dataLocationGER);

            context.SaveChanges();
        }
    }
}
