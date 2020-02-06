namespace backend.Migrations
{
    using backend.Core;
    using backend.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

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

            context.UseCase.Add(new UseCase
            {
                Creation = DateTime.Now,
                TitleDE = "Ich möchte mit meinen Kollegen gemeinsam Dokumente erstellen und bearbeiten",
                TitleEN = null,
                TitleES = null,
                InternalDescription = null,
                ServiceClassMapping = context.ServiceClass.Where(x => new List<int> { 4 }.Contains(x.Id)).ToList()
            });
            context.UseCase.Add(new UseCase
            {
                Creation = DateTime.Now,
                TitleDE = "Ich möchte meinen Arbeitsplatz mit mehreren Endgeräten synchronisieren",
                TitleEN = null,
                TitleES = null,
                InternalDescription = null,
                ServiceClassMapping = context.ServiceClass.Where(x => new List<int> { 4 }.Contains(x.Id)).ToList()
            });
            context.UseCase.Add(new UseCase
            {
                Creation = DateTime.Now,
                TitleDE = "Ich möchte Medien (Fotos, Videos etc,) über das Internet streamen",
                TitleEN = null,
                TitleES = null,
                InternalDescription = null,
                ServiceClassMapping = context.ServiceClass.Where(x => new List<int> { 3 }.Contains(x.Id)).ToList()
            });
            context.UseCase.Add(new UseCase
            {
                Creation = DateTime.Now,
                TitleDE = "Ich möchte Dateien und Ordner mit anderen teilen",
                TitleEN = null,
                TitleES = null,
                InternalDescription = null,
                ServiceClassMapping = context.ServiceClass.Where(x => new List<int> { 4 }.Contains(x.Id)).ToList()
            });
            context.UseCase.Add(new UseCase
            {
                Creation = DateTime.Now,
                TitleDE = "Ich möchte regelmäßige Backups von meinen Dateien erstellen",
                TitleEN = null,
                TitleES = null,
                InternalDescription = null,
                ServiceClassMapping = context.ServiceClass.Where(x => new List<int> { 4 }.Contains(x.Id)).ToList()
            });
            context.UseCase.Add(new UseCase
            {
                Creation = DateTime.Now,
                TitleDE = "Ich möchte Dateien für eine Applikation bereitstellen",
                TitleEN = null,
                TitleES = null,
                InternalDescription = null,
                ServiceClassMapping = context.ServiceClass.Where(x => new List<int> { 0 }.Contains(x.Id)).ToList()
            });
            context.UseCase.Add(new UseCase
            {
                Creation = DateTime.Now,
                TitleDE = "Ich benötige (peristenten) Speicher für einen Container (z.B. Docker)",
                TitleEN = null,
                TitleES = null,
                InternalDescription = null,
                ServiceClassMapping = context.ServiceClass.Where(x => new List<int> { 0 }.Contains(x.Id)).ToList()
            });
            context.UseCase.Add(new UseCase
            {
                Creation = DateTime.Now,
                TitleDE = "Ich benötige Speicher für den Betrieb eigener Applikationen (z.B. ERP-System)",
                TitleEN = null,
                TitleES = null,
                InternalDescription = null,
                ServiceClassMapping = context.ServiceClass.Where(x => new List<int> { 0 }.Contains(x.Id)).ToList()
            });
            context.UseCase.Add(new UseCase
            {
                Creation = DateTime.Now,
                TitleDE = "Ich benötige Laufwerke für virtuelle Maschinen",
                TitleEN = null,
                TitleES = null,
                InternalDescription = null,
                ServiceClassMapping = context.ServiceClass.Where(x => new List<int> { 0 }.Contains(x.Id)).ToList()
            });
            context.UseCase.Add(new UseCase
            {
                Creation = DateTime.Now,
                TitleDE = "Ich benötige lokalen Speicher für Datenbanken",
                TitleEN = null,
                TitleES = null,
                InternalDescription = null,
                ServiceClassMapping = context.ServiceClass.Where(x => new List<int> { 0 }.Contains(x.Id)).ToList()
            });
            context.UseCase.Add(new UseCase
            {
                Creation = DateTime.Now,
                TitleDE = "Ich benötige Speicher für die Bereitstellung von Bildern und Websiteinhalten",
                TitleEN = null,
                TitleES = null,
                InternalDescription = null,
                ServiceClassMapping = context.ServiceClass.Where(x => new List<int> { 3 }.Contains(x.Id)).ToList()
            });

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
            var cert3 = new Certificate { CertificateName = "SOC 1" };
            var cert4 = new Certificate { CertificateName = "SOC 2" };
            var cert5 = new Certificate { CertificateName = "SOC 3" };
            var cert6 = new Certificate { CertificateName = "PCI DSS" };
            var cert7 = new Certificate { CertificateName = "CSA STAR" };
            var cert8 = new Certificate { CertificateName = "GDPR" };
            var cert9 = new Certificate { CertificateName = "ISO 27017" };
            var cert10 = new Certificate { CertificateName = "ISO 27018" };

            context.Certificate.Add(cert1);
            context.Certificate.Add(cert3);
            context.Certificate.Add(cert4);
            context.Certificate.Add(cert5);
            context.Certificate.Add(cert6);
            context.Certificate.Add(cert7);
            context.Certificate.Add(cert8);
            context.Certificate.Add(cert9);
            context.Certificate.Add(cert10);

            var locationTypeC = new DataLocationType {
                TypeNameDE = "Kontinent",
                TypeNameEN = "Continent",
                TypeNameES = "Continente"
            };
            var locationTypeR = new DataLocationType {
                TypeNameDE = "Region",
                TypeNameEN = "Region",
                TypeNameES = "Región"
            };
            var locationTypeL = new DataLocationType {
                TypeNameDE = "Land",
                TypeNameEN = "Country",
                TypeNameES = "Pais"
            };

            context.DataLocationType.Add(locationTypeC);
            context.DataLocationType.Add(locationTypeR);
            context.DataLocationType.Add(locationTypeL);

            context.SaveChanges();

            var dataLocationEU = new DataLocation {
                DataLocationNameDE = "EU",
                DataLocationNameEN = "EU",
                DataLocationNameES = "EU",
                DataLocationTypeId = locationTypeC.Id
            };
            var dataLocationGER = new DataLocation {
                DataLocationNameDE = "Deutschsprachiger Raum",
                DataLocationNameEN = "German speaking area",
                DataLocationNameES = "Área de habla alemana",
                DataLocationTypeId = locationTypeR.Id
            };

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

            Feature hasFileEncryption = new Feature
            {
                Color = "#1a78b8",
                DescriptionDE = "Dateiverschlüsselung",
                DescriptionEN = "File encryption",
                DescriptionES = "Cifrado de archivos",
                Icon = "vpn_key"
            };
            Feature hasReplication = new Feature
            {
                Color = "#1c17bd",
                DescriptionDE = "Redundante Datenspeicherung",
                DescriptionEN = "File replication",
                DescriptionES = "Replicación de archivos",
                Icon = "file_copy"
            };
            Feature hasVersioning = new Feature
            {
                Color = "#a74ddb",
                DescriptionDE = "Dateiversionierung",
                DescriptionEN = "File versioning",
                DescriptionES = "Versionado de archivos",
                Icon = "restore"
            };
            Feature hasPermissions = new Feature
            {
                Color = "#f00e7b",
                DescriptionDE = "Nutzerberechtigungssystem",
                DescriptionEN = "File permissions",
                DescriptionES = "Permisos de archivos",
                Icon = "security"
            };
            Feature hasAutomatedSynchronization = new Feature
            {
                Color = "#3c9e49",
                DescriptionDE = "Automatische Synchronisation",
                DescriptionEN = "Automated synchronization",
                DescriptionES = "sincronización automatizada",
                Icon = "cached"
            };
            Feature hasLocking = new Feature
            {
                Color = "#e81046",
                DescriptionDE = "Datei Sperren",
                DescriptionEN = "File locking",
                DescriptionES = "bloqueo de archivos",
                Icon = "block"
            };

            context.Feature.Add(hasFileEncryption);
            context.Feature.Add(hasReplication);
            context.Feature.Add(hasVersioning);
            context.Feature.Add(hasPermissions);
            context.Feature.Add(hasAutomatedSynchronization);
            context.Feature.Add(hasLocking);
            context.SaveChanges();

            context.OnlineDriveStorageService.AddOrUpdate(
                x => x.ServiceName,
                new OnlineDriveStorageService
            {
                ServiceName = "Dropbox Basic",
                ServiceDescriptionDE = "Dropbox bietet Speicherplatz für Dateien auf all Ihren verknüpften Geräten",
                ServiceTitleDE = "Kostenlose Version von Dropbox",
                CloudServiceModelId = saaSModel.Id,
                CloudServiceModel = saaSModel,
                ProviderId = dropboxProvider.Id,
                Provider = dropboxProvider,
                Creation = DateTime.Now,
                LastModified = DateTime.Now,
                Features = new List<Feature> {  }
            }
            );
            context.OnlineDriveStorageService.AddOrUpdate(
                x => x.ServiceName,
                new OnlineDriveStorageService
                {
                    ServiceName = "OneDrive",
                    ServiceDescriptionDE = "Speichern Sie Ihre Dateien und Fotos auf OneDrive, um sie von jedem Gerät und überall aus abrufen zu können",
                    CloudServiceModelId = saaSModel.Id,
                    CloudServiceModel = saaSModel,
                    Creation = DateTime.Now,
                    LastModified = DateTime.Now,
                    Features = new List<Feature> { }
                }
            );
            context.OnlineDriveStorageService.AddOrUpdate(
                x => x.ServiceName,
                new OnlineDriveStorageService {
                    ServiceName = "HiDrive",
                    ServiceDescriptionDE = "Der Cloud-Speicher für Ihre Fotos, Videos & Dateien",
                    ServiceTitleDE = "HiDrive",
                    ServiceAvailability = "99,99",
                    CloudServiceModelId = saaSModel.Id,
                    CloudServiceModel = saaSModel,
                    ProviderId = stratoProvider.Id,
                    Provider = stratoProvider,
                    Creation = DateTime.Now,
                    LastModified = DateTime.Now,
                    Features = new List<Feature> { }
                }
            );
            context.ObjectStorageService.AddOrUpdate(
                x => x.ServiceName,
                new ObjectStorageService {
                    ServiceName = "Amazon S3",
                    ServiceDescriptionDE = "Filehosting-Dienst, der beliebig große Datenmengen speichern kann und nach Verbrauch abgerechnet wird",
                    ServiceTitleDE = "Amazon Simple Storage Service",
                    ServiceAvailability = "99,99",
                    CloudServiceModelId = iaaSModel.Id,
                    CloudServiceModel = iaaSModel,
                    Creation = DateTime.Now,
                    LastModified = DateTime.Now,
                    Features = new List<Feature> { hasFileEncryption, hasVersioning, hasPermissions, hasReplication, hasLocking }
                }
            );
            context.ObjectStorageService.AddOrUpdate(
                x => x.ServiceName,
                new ObjectStorageService {
                    ServiceName = "Google Cloud Storage",
                    ServiceDescriptionDE = "Einheitlicher Objektspeicher für Entwickler und Unternehmen",
                    ServiceTitleDE = "Google Cloud Storage",
                    ServiceAvailability = "99,99",
                    CloudServiceModelId = iaaSModel.Id,
                    CloudServiceModel = iaaSModel,
                    ProviderId = googleProvider.Id,
                    Provider = googleProvider,
                    Creation = DateTime.Now,
                    LastModified = DateTime.Now,
                    Features = new List<Feature> { hasFileEncryption, hasVersioning, hasPermissions }
                }
            );
            context.BlockStorageService.AddOrUpdate(
                x => x.ServiceName,
                new BlockStorageService {
                    ServiceName = "Azure Disk Storage",
                    ServiceDescriptionDE = "Persistente und leistungsstarke Datenträger für virtuelle Azure-Computer",
                    ServiceAvailability = "99,95",
                    CloudServiceModelId = iaaSModel.Id,
                    CloudServiceModel = iaaSModel,
                    Creation = DateTime.Now,
                    LastModified = DateTime.Now,
                    Features = new List<Feature> { }
                }
            );
            context.ObjectStorageService.AddOrUpdate(
                x => x.ServiceName,
                new ObjectStorageService {
                    ServiceName = "S3 Object Storage",
                    ServiceAvailability = "99,95",
                    CloudServiceModelId = iaaSModel.Id,
                    CloudServiceModel = iaaSModel,
                    Creation = DateTime.Now,
                    LastModified = DateTime.Now,
                    Features = new List<Feature> { hasFileEncryption, hasPermissions }
                }
            );
            context.BlockStorageService.AddOrUpdate(
                x => x.ServiceName,
                new BlockStorageService {
                    ServiceName = "Amazon Elastic Block Store",
                    ServiceDescriptionDE = "Amazon Elastic Block Store (Amazon EBS) bietet Volumes für die Speicherung auf Blockebene, die in Verbindung mit EC2-Instances verwendet werden. EBS-Volumes verhalten sich wie unformatierte Blockgeräte",
                    ServiceTitleDE = "Amazon EBS",
                    ServiceAvailability = "99,99",
                    CloudServiceModelId = iaaSModel.Id,
                    CloudServiceModel = iaaSModel,
                    ProviderId = awsProvider.Id,
                    Provider = awsProvider,
                    Creation = DateTime.Now,
                    LastModified = DateTime.Now,
                    Features = new List<Feature> { hasFileEncryption, hasReplication }
                }
            );
            context.BlockStorageService.AddOrUpdate(
                x => x.ServiceName,
                new BlockStorageService {
                    ServiceName = "Blockspeicher für VM-Instanzen",
                    ServiceDescriptionDE = "Zuverlässiger, leistungsstarker Blockspeicher für VM-Instanzen",
                    ServiceTitleDE = "Persistent Disk",
                    ServiceAvailability = "99,99",
                    CloudServiceModelId = iaaSModel.Id,
                    CloudServiceModel = iaaSModel,
                    ProviderId = googleProvider.Id,
                    Provider = googleProvider,
                    Creation = DateTime.Now,
                    LastModified = DateTime.Now,
                    Features = new List<Feature> { hasFileEncryption, hasReplication }
                }
            );
            context.BlockStorageService.AddOrUpdate(
                x => x.ServiceName,
                new BlockStorageService {
                    ServiceName = "Azure Disk Storage",
                    ServiceDescriptionDE = "Persistente und leistungsstarke Datenträger für virtuelle Azure-Compute",
                    ServiceTitleDE = "Azure Disk Storage",
                    ServiceAvailability = "99,99",
                    CloudServiceModelId = iaaSModel.Id,
                    CloudServiceModel = iaaSModel,
                    ProviderId = azureProvider.Id,
                    Provider = azureProvider,
                    Creation = DateTime.Now,
                    LastModified = DateTime.Now,
                    Features = new List<Feature> { hasFileEncryption, hasReplication }
                }
            );
            context.ObjectStorageService.AddOrUpdate(
                x => x.ServiceName,
                new ObjectStorageService {
                    ServiceName = "Blob Storage",
                    ServiceDescriptionDE = "Hochgradig skalierbarer Objektspeicher für unstrukturierte Daten",
                    ServiceTitleDE = "Blob Storage",
                    ServiceAvailability = "99,99",
                    CloudServiceModelId = iaaSModel.Id,
                    CloudServiceModel = iaaSModel,
                    Creation = DateTime.Now,
                    LastModified = DateTime.Now,
                    Features = new List<Feature> { hasPermissions, hasReplication }
                }
            );
            context.ObjectStorageService.AddOrUpdate(
                x => x.ServiceName,
                new ObjectStorageService {
                    ServiceName = "Spaces",
                    ServiceDescriptionDE = "Spaces ergänzt den lokalen und Netzwerkspeicher, um Ihrem Unternehmen die Skalierung zu erleichtern",
                    ServiceTitleDE = "Spaces",
                    ServiceAvailability = "99,95",
                    CloudServiceModelId = iaaSModel.Id,
                    CloudServiceModel = iaaSModel,
                    ProviderId = digitalOceanProvider.Id,
                    Provider = digitalOceanProvider,
                    Creation = DateTime.Now,
                    LastModified = DateTime.Now,
                    Features = new List<Feature> { hasPermissions, hasReplication }
                }
            );
            context.BlockStorageService.AddOrUpdate(
                x => x.ServiceName,
                new BlockStorageService {
                    ServiceName = "Volumes Block Storage",
                    ServiceDescriptionDE = "Hochverfügbaren und skalierbaren SSD-basierten Blockspeicher",
                    ServiceTitleDE = "Volumes Block Storage",
                    ServiceAvailability = "99,95",
                    CloudServiceModelId = iaaSModel.Id,
                    CloudServiceModel = iaaSModel,
                    ProviderId = digitalOceanProvider.Id,
                    Provider = digitalOceanProvider,
                    Creation = DateTime.Now,
                    LastModified = DateTime.Now,
                    Features = new List<Feature> { hasReplication }
                }
            );
            context.BlockStorageService.AddOrUpdate(
                x => x.ServiceName,
                new BlockStorageService {
                    ServiceName = "Volumes",
                    ServiceDescriptionDE = "Volumes bieten hochverfügbaren und zuverlässigen SSD-Speicherplatz für Ihre Cloud Server",
                    ServiceTitleDE = "Volumes",
                    CloudServiceModelId = iaaSModel.Id,
                    CloudServiceModel = iaaSModel,
                    ProviderId = hetznerProvider.Id,
                    Provider = hetznerProvider,
                    Creation = DateTime.Now,
                    LastModified = DateTime.Now,
                    Features = new List<Feature> { hasReplication }
                }
            );
            context.OnlineDriveStorageService.AddOrUpdate(
                x => x.ServiceName,
                new OnlineDriveStorageService {
                    ServiceName = "Storage Share",
                    ServiceDescriptionDE = "Daten speichern und teilen",
                    ServiceTitleDE = "Storage Share",
                    ServiceTitleEN = "Storage Share",
                    ServiceTitleES = "Storage Share",
                    CloudServiceModelId = saaSModel.Id,
                    CloudServiceModel = saaSModel,
                    ProviderId = hetznerProvider.Id,
                    Provider = hetznerProvider,
                    Creation = DateTime.Now,
                    LastModified = DateTime.Now,
                    Features = new List<Feature> { }
                }
            );
            context.ObjectStorageService.AddOrUpdate(
                x => x.ServiceName,
                new ObjectStorageService {
                    ServiceName = "Cloud Files",
                    ServiceDescriptionDE = "Cloud Files bietet Online-Objektspeicher für Dateien und Medien und stellt sie weltweit mit rasender Geschwindigkeit über ein weltweites Content-Delivery-Netzwerk (CDN) bereit",
                    ServiceAvailability = "99,99",
                    ServiceTitleDE = "Cloud Files",
                    ServiceTitleEN = "Cloud Files",
                    ServiceTitleES = "Cloud Files",
                    CloudServiceModelId = iaaSModel.Id,
                    CloudServiceModel = iaaSModel,
                    Creation = DateTime.Now,
                    LastModified = DateTime.Now,
                    Features = new List<Feature> { hasPermissions, hasReplication }
                }
            );
            context.BlockStorageService.AddOrUpdate(
                x => x.ServiceName,
                new BlockStorageService {
                    ServiceName = "Cloud Block Storage",
                    ServiceDescriptionDE = "Cloud Block Storage bietet zuverlässigen, leistungsstarken On-Demand-Speicher für Anwendungen, die auf Cloud-Servern gehostet werden",
                    ServiceTitleDE = "Cloud Block Storage",
                    CloudServiceModelId = iaaSModel.Id,
                    CloudServiceModel = iaaSModel,
                    ProviderId = rackspaceProvider.Id,
                    Provider = rackspaceProvider,
                    Creation = DateTime.Now,
                    LastModified = DateTime.Now,
                    Features = new List<Feature> { hasFileEncryption, hasReplication }
                }
            );
            context.ObjectStorageService.AddOrUpdate(
                x => x.ServiceName,
                new ObjectStorageService {
                    ServiceName = "Cloud Object Storage",
                    ServiceDescriptionDE = "Stelle deine Daten von überall aus wieder her oder mache Backups einfach über unseren redundanten Objekt Storage mit S3-kompatibler Schnittstelle",
                    ServiceTitleDE = "Cloud Object Storage",
                    ServiceAvailability = "99,95",
                    CloudServiceModelId = iaaSModel.Id,
                    CloudServiceModel = iaaSModel,
                    Creation = DateTime.Now,
                    LastModified = DateTime.Now,
                    Features = new List<Feature> { hasPermissions }
                }
            );
            context.ObjectStorageService.AddOrUpdate(
                x => x.ServiceName,
                new ObjectStorageService {
                    ServiceName = "IBM Cloud Object Storage",
                    ServiceDescriptionDE = "IBM Cloud Object Storage wurde entwickelt, um ein exponentielles Datenwachstum und Cloud-native Arbeitslasten zu unterstützen",
                    ServiceTitleDE = "IBM Cloud Object Storage",
                    ServiceAvailability = "99,99",
                    CloudServiceModelId = iaaSModel.Id,
                    CloudServiceModel = iaaSModel,
                    ProviderId = ibmProvider.Id,
                    Provider = ibmProvider,
                    Creation = DateTime.Now,
                    LastModified = DateTime.Now,
                    Features = new List<Feature> { hasPermissions, hasReplication }
                }
            );
            context.BlockStorageService.AddOrUpdate(
                x => x.ServiceName,
                new BlockStorageService {
                    ServiceName = "IBM Block Storage",
                    ServiceDescriptionDE = "Flash-basierter, leistungsstarker lokaler Plattenspeicher mit SAN-Persistenz und -Beständigkeit, anpassbaren E/A-Operationen pro Sekunde und kalkulierbaren Kosten",
                    ServiceTitleDE = "IBM Block Storage",
                    ServiceAvailability = "99,99",
                    CloudServiceModelId = iaaSModel.Id,
                    CloudServiceModel = iaaSModel,
                    ProviderId = ibmProvider.Id,
                    Provider = ibmProvider,
                    Creation = DateTime.Now,
                    LastModified = DateTime.Now,
                    Features = new List<Feature> { hasFileEncryption, hasReplication }
                }
            );

            context.SaveChanges();
        }
    }
}
