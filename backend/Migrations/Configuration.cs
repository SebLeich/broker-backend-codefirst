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
            context.ServiceClass.SeedEnumValues<ServiceClass, ServiceClassEnum>(@enum => @enum);
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
            var manageUseCases = new Rule
            {
                RuleCode = "manage-use-cases",
                RuleTitle = "Use-Cases verwalten",
                RuleDesc = "Dürfen Nutzer der Rollengruppe Use-Cases erstellen, ändern und löschen?"
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
            adminRole.Rules.Add(deleteRoles);
            adminRole.Rules.Add(manageUseCases);

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
            var cert3 = new Certificate { CertificateName = "SOC 1" };
            var cert4 = new Certificate { CertificateName = "SOC 2" };
            var cert5 = new Certificate { CertificateName = "SOC 3" };
            var cert6 = new Certificate { CertificateName = "PCI DSS" };
            var cert7 = new Certificate { CertificateName = "CSA STAR" };
            var cert8 = new Certificate { CertificateName = "GDPR" };
            var cert9 = new Certificate { CertificateName = "ISO 27017" };
            var cert10 = new Certificate { CertificateName = "ISO 27018" };

            context.Certificate.Add(cert1);
            context.Certificate.Add(cert2);
            context.Certificate.Add(cert3);
            context.Certificate.Add(cert4);
            context.Certificate.Add(cert5);
            context.Certificate.Add(cert6);
            context.Certificate.Add(cert7);
            context.Certificate.Add(cert8);
            context.Certificate.Add(cert9);
            context.Certificate.Add(cert10);

            var locationTypeC = new DataLocationType { TypeName = "Kontinent" };
            var locationTypeR = new DataLocationType { TypeName = "Region" };

            context.DataLocationType.Add(locationTypeC);
            context.DataLocationType.Add(locationTypeR);

            context.SaveChanges();

            var dataLocationEU = new DataLocation { DataLocationName = "EU", DataLocationTypeId = locationTypeC.Id };
            var dataLocationGER = new DataLocation { DataLocationName = "Deutschsprachiger Raum", DataLocationTypeId = locationTypeR.Id };

            context.DataLocation.Add(dataLocationEU);
            context.DataLocation.Add(dataLocationGER);

            CloudServiceModel saaSModel = new CloudServiceModel
            {
                CloudServiceModelName = "Saas"
            };
            CloudServiceModel iaaSModel = new CloudServiceModel
            {
                CloudServiceModelName = "IaaS"
            };
            CloudServiceModel paaSModel = new CloudServiceModel
            {
                CloudServiceModelName = "PaaS"
            };

            context.CloudServiceModel.AddOrUpdate(saaSModel);
            context.CloudServiceModel.AddOrUpdate(iaaSModel);
            context.CloudServiceModel.AddOrUpdate(paaSModel);

            Provider dropboxProvider = new Provider
            {
                ProviderName = "Dropbox",
                URL = "https://www.dropbox.com/"
            };
            Provider stratoProvider = new Provider
            {
                ProviderName = "Strato",
                URL = "https://www.strato.de/cloud-speicher/"
            };
            Provider googleProvider = new Provider
            {
                ProviderName = "Google Cloud",
                URL = "https://cloud.google.com/"
            };
            Provider awsProvider = new Provider
            {
                ProviderName = "Amazon Web Services (AWS)",
                URL = "https://aws.amazon.com/de/"
            };
            Provider azureProvider = new Provider
            {
                ProviderName = "Microsoft Azure",
                URL = "https://azure.microsoft.com/de-de/"
            };
            Provider digitalOceanProvider = new Provider
            {
                ProviderName = "DigitalOcean",
                URL = "https://www.digitalocean.com/"
            };
            Provider hetznerProvider = new Provider
            {
                ProviderName = "Hetzner",
                URL = "https://www.hetzner.de"
            };
            Provider rackspaceProvider = new Provider
            {
                ProviderName = "Rackspace",
                URL = "https://www.rackspace.com/"
            };
            Provider ibmProvider = new Provider
            {
                ProviderName = "IBM",
                URL = "https://www.ibm.com/cloud/"
            };
            Provider ionosProvider = new Provider
            {
                ProviderName = "IONOS",
                URL = "https://www.ionos.de/enterprise-cloud/object-storage"
            };
            Provider gridscaleProvider = new Provider
            {
                ProviderName = "Gridscale",
                URL = "https://gridscale.io/"
            };

            context.Provider.AddOrUpdate(dropboxProvider);
            context.Provider.AddOrUpdate(stratoProvider);
            context.Provider.AddOrUpdate(googleProvider);
            context.Provider.AddOrUpdate(awsProvider);
            context.Provider.AddOrUpdate(azureProvider);
            context.Provider.AddOrUpdate(digitalOceanProvider);
            context.Provider.AddOrUpdate(hetznerProvider);
            context.Provider.AddOrUpdate(rackspaceProvider);
            context.Provider.AddOrUpdate(ibmProvider);
            context.Provider.AddOrUpdate(ionosProvider);
            context.Provider.AddOrUpdate(gridscaleProvider);

            context.SaveChanges();

            context.OnlineDriveStorageService.AddOrUpdate(new OnlineDriveStorageService
            {
                ServiceName = "Dropbox Basic",
                ServiceDescription = "Dropbox bietet Speicherplatz für Dateien auf all Ihren verknüpften Geräte",
                ServiceTitle = "Kostenlose Version von Dropbox",
                CloudServiceModelId = saaSModel.Id,
                CloudServiceModel = saaSModel,
                ProviderId = dropboxProvider.Id,
                Provider = dropboxProvider,
                HasFileEncryption = false,
                HasFileVersioning = false,
                HasFilePermissions = false,
                HasAutomatedSynchronisation = false
            });
            context.OnlineDriveStorageService.AddOrUpdate(new OnlineDriveStorageService
            {
                ServiceName = "OneDrive",
                ServiceDescription = "Speichern Sie Ihre Dateien und Fotos auf OneDrive, um sie von jedem Gerät und überall aus abrufen zu können",
                CloudServiceModelId = saaSModel.Id,
                CloudServiceModel = saaSModel,
                HasFileEncryption = false,
                HasFileVersioning = false,
                HasFilePermissions = false,
                HasAutomatedSynchronisation = false
            });
            context.OnlineDriveStorageService.AddOrUpdate(new OnlineDriveStorageService
            {
                ServiceName = "HiDrive",
                ServiceDescription = "Der Cloud-Speicher für Ihre Fotos, Videos & Dateien",
                ServiceTitle = "HiDrive",
                ServiceAvailability = "99,99",
                CloudServiceModelId = saaSModel.Id,
                CloudServiceModel = saaSModel,
                ProviderId = stratoProvider.Id,
                Provider = stratoProvider,
                HasFileEncryption = false,
                HasFileVersioning = false,
                HasFilePermissions = false,
                HasAutomatedSynchronisation = false
            });
            context.ObjectStorageService.AddOrUpdate(new ObjectStorageService
            {
                ServiceName = "Amazon S3",
                ServiceDescription = "Filehosting-Dienst, der beliebig große Datenmengen speichern kann und nach Verbrauch abgerechnet wird",
                ServiceTitle = "Amazon Simple Storage Service",
                ServiceAvailability = "99,99",
                CloudServiceModelId = iaaSModel.Id,
                CloudServiceModel = iaaSModel,
                HasFileEncryption = true,
                HasFileVersioning = true,
                HasFilePermissions = true,
                HasReplication = true,
                HasFileLocking = true
            });
            context.ObjectStorageService.AddOrUpdate(new ObjectStorageService
            {
                ServiceName = "Google Cloud Storage",
                ServiceDescription = "Einheitlicher Objektspeicher für Entwickler und Unternehmen",
                ServiceTitle = "Google Cloud Storage",
                ServiceAvailability = "99,99",
                CloudServiceModelId = iaaSModel.Id,
                CloudServiceModel = iaaSModel,
                ProviderId = googleProvider.Id,
                Provider = googleProvider,
                HasFileEncryption = true,
                HasFileVersioning = true,
                HasFilePermissions = true,
                HasReplication = false,
                HasFileLocking = false
            });
            context.BlockStorageService.AddOrUpdate(new BlockStorageService
            {
                ServiceName = "Azure Disk Storage",
                ServiceDescription = "Persistente und leistungsstarke Datenträger für virtuelle Azure-Computer",
                ServiceAvailability = "99,95",
                CloudServiceModelId = iaaSModel.Id,
                CloudServiceModel = iaaSModel,
                HasFileEncryption = false,
                HasReplication = false
            });
            context.ObjectStorageService.AddOrUpdate(new ObjectStorageService
            {
                ServiceName = "S3 Object Storage",
                ServiceAvailability = "99,95",
                CloudServiceModelId = iaaSModel.Id,
                CloudServiceModel = iaaSModel,
                HasFileEncryption = true,
                HasFileVersioning = false,
                HasFilePermissions = true,
                HasReplication = false,
                HasFileLocking = false
            });
            context.BlockStorageService.AddOrUpdate(new BlockStorageService
            {
                ServiceName = "Amazon Elastic Block Store",
                ServiceDescription = "Amazon Elastic Block Store (Amazon EBS) bietet Volumes für die Speicherung auf Blockebene, die in Verbindung mit EC2-Instances verwendet werden. EBS-Volumes verhalten sich wie unformatierte Blockgeräte",
                ServiceTitle = "Amazon EBS",
                ServiceAvailability = "99,99",
                CloudServiceModelId = iaaSModel.Id,
                CloudServiceModel = iaaSModel,
                ProviderId = awsProvider.Id,
                Provider = awsProvider,
                HasFileEncryption = true,
                HasReplication = true
            });
            context.BlockStorageService.AddOrUpdate(new BlockStorageService
            {
                ServiceName = "Blockspeicher für VM-Instanzen",
                ServiceDescription = "Zuverlässiger, leistungsstarker Blockspeicher für VM-Instanzen",
                ServiceTitle = "Persistent Disk",
                ServiceAvailability = "99,99",
                CloudServiceModelId = iaaSModel.Id,
                CloudServiceModel = iaaSModel,
                ProviderId = googleProvider.Id,
                Provider = googleProvider,
                HasFileEncryption = true,
                HasReplication = true
            });
            context.BlockStorageService.AddOrUpdate(new BlockStorageService
            {
                ServiceName = "Azure Disk Storage",
                ServiceDescription = "Persistente und leistungsstarke Datenträger für virtuelle Azure-Compute",
                ServiceTitle = "Azure Disk Storage",
                ServiceAvailability = "99,99",
                CloudServiceModelId = iaaSModel.Id,
                CloudServiceModel = iaaSModel,
                ProviderId = azureProvider.Id,
                Provider = azureProvider,
                HasFileEncryption = true,
                HasReplication = true
            });
            context.ObjectStorageService.AddOrUpdate(new ObjectStorageService
            {
                ServiceName = "Blob Storage",
                ServiceDescription = "Hochgradig skalierbarer Objektspeicher für unstrukturierte Daten",
                ServiceTitle = "Blob Storage",
                ServiceAvailability = "99,99",
                CloudServiceModelId = iaaSModel.Id,
                CloudServiceModel = iaaSModel,
                HasFileEncryption = false,
                HasFileVersioning = false,
                HasFilePermissions = true,
                HasReplication = true,
                HasFileLocking = false
            });
            context.ObjectStorageService.AddOrUpdate(new ObjectStorageService
            {
                ServiceName = "Spaces",
                ServiceDescription = "Spaces ergänzt den lokalen und Netzwerkspeicher, um Ihrem Unternehmen die Skalierung zu erleichtern",
                ServiceTitle = "Spaces",
                ServiceAvailability = "99,95",
                CloudServiceModelId = iaaSModel.Id,
                CloudServiceModel = iaaSModel,
                ProviderId = digitalOceanProvider.Id,
                Provider = digitalOceanProvider,
                HasFileEncryption = false,
                HasFileVersioning = false,
                HasFilePermissions = true,
                HasReplication = true,
                HasFileLocking = false
            });
            context.BlockStorageService.AddOrUpdate(new BlockStorageService
            {
                ServiceName = "Volumes Block Storage",
                ServiceDescription = "Hochverfügbaren und skalierbaren SSD-basierten Blockspeicher",
                ServiceTitle = "Volumes Block Storage",
                ServiceAvailability = "99,95",
                CloudServiceModelId = iaaSModel.Id,
                CloudServiceModel = iaaSModel,
                ProviderId = digitalOceanProvider.Id,
                Provider = digitalOceanProvider,
                HasFileEncryption = false,
                HasReplication = true
            });
            context.BlockStorageService.AddOrUpdate(new BlockStorageService
            {
                ServiceName = "Volumes",
                ServiceDescription = "Volumes bieten hochverfügbaren und zuverlässigen SSD-Speicherplatz für Ihre Cloud Server",
                ServiceTitle = "Volumes",
                CloudServiceModelId = iaaSModel.Id,
                CloudServiceModel = iaaSModel,
                ProviderId = hetznerProvider.Id,
                Provider = hetznerProvider,
                HasFileEncryption = false,
                HasReplication = true
            });
            context.OnlineDriveStorageService.AddOrUpdate(new OnlineDriveStorageService
            {
                ServiceName = "Storage Share",
                ServiceDescription = "Daten speichern und teilen",
                ServiceTitle = "Storage Share",
                CloudServiceModelId = saaSModel.Id,
                CloudServiceModel = saaSModel,
                ProviderId = hetznerProvider.Id,
                Provider = hetznerProvider,
                HasFileEncryption = false,
                HasFileVersioning = false,
                HasFilePermissions = false,
                HasAutomatedSynchronisation = false
            });
            context.ObjectStorageService.AddOrUpdate(new ObjectStorageService
            {
                ServiceName = "Cloud Files",
                ServiceDescription = "Cloud Files bietet Online-Objektspeicher für Dateien und Medien und stellt sie weltweit mit rasender Geschwindigkeit über ein weltweites Content-Delivery-Netzwerk (CDN) bereit",
                ServiceAvailability = "99,99",
                ServiceTitle = "Cloud Files",
                CloudServiceModelId = iaaSModel.Id,
                CloudServiceModel = iaaSModel,
                HasFileEncryption = false,
                HasFileVersioning = false,
                HasFilePermissions = true,
                HasReplication = true,
                HasFileLocking = false
            });
            context.BlockStorageService.AddOrUpdate(new BlockStorageService
            {
                ServiceName = "Cloud Block Storage",
                ServiceDescription = "Cloud Block Storage bietet zuverlässigen, leistungsstarken On-Demand-Speicher für Anwendungen, die auf Cloud-Servern gehostet werden",
                ServiceTitle = "Cloud Block Storage",
                CloudServiceModelId = iaaSModel.Id,
                CloudServiceModel = iaaSModel,
                ProviderId = rackspaceProvider.Id,
                Provider = rackspaceProvider,
                HasFileEncryption = true,
                HasReplication = true
            });
            context.ObjectStorageService.AddOrUpdate(new ObjectStorageService
            {
                ServiceName = "Cloud Object Storage",
                ServiceDescription = "Stelle deine Daten von überall aus wieder her oder mache Backups einfach über unseren redundanten Objekt Storage mit S3-kompatibler Schnittstelle",
                ServiceTitle = "Cloud Object Storage",
                ServiceAvailability = "99,95",
                CloudServiceModelId = iaaSModel.Id,
                CloudServiceModel = iaaSModel,
                HasFileEncryption = false,
                HasFileVersioning = false,
                HasFilePermissions = true,
                HasReplication = false,
                HasFileLocking = false
            });
            context.ObjectStorageService.AddOrUpdate(new ObjectStorageService
            {
                ServiceName = "IBM Cloud Object Storage",
                ServiceDescription = "IBM Cloud Object Storage wurde entwickelt, um ein exponentielles Datenwachstum und Cloud-native Arbeitslasten zu unterstützen",
                ServiceTitle = "IBM Cloud Object Storage",
                ServiceAvailability = "99,99",
                CloudServiceModelId = iaaSModel.Id,
                CloudServiceModel = iaaSModel,
                ProviderId = ibmProvider.Id,
                Provider = ibmProvider,
                HasFileEncryption = false,
                HasFileVersioning = false,
                HasFilePermissions = true,
                HasReplication = true,
                HasFileLocking = false
            });
            context.BlockStorageService.AddOrUpdate(new BlockStorageService
            {
                ServiceName = "IBM Block Storage",
                ServiceDescription = "Flash-basierter, leistungsstarker lokaler Plattenspeicher mit SAN-Persistenz und -Beständigkeit, anpassbaren E/A-Operationen pro Sekunde und kalkulierbaren Kosten",
                ServiceTitle = "IBM Block Storage",
                ServiceAvailability = "99,99",
                CloudServiceModelId = iaaSModel.Id,
                CloudServiceModel = iaaSModel,
                ProviderId = ibmProvider.Id,
                Provider = ibmProvider,
                HasFileEncryption = true,
                HasReplication = true
            });

            context.SaveChanges();
        }
    }
}
