using backend.Core;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace backend.Repositories
{
    public class UseCaseRepository
    {
        /// <summary>
        /// the attribute provides database access
        /// </summary>
        private BrokerContext _Ctx;

        /// <summary>
        /// the constructor creates a new instance of the repository
        /// </summary>
        public UseCaseRepository()
        {
            _Ctx = new BrokerContext();
        }

        /// <summary>
        /// the method returns all use cases from the database
        /// </summary>
        /// <returns></returns>
        public List<UseCase> GetUseCases()
        {
            return _Ctx.UseCase.ToList();
        }

        /// <summary>
        /// the method returns an use case with the given id
        /// </summary>
        /// <param name="id">usecase's id</param>
        /// <returns>usecase</returns>
        public ResponseWrapper<UseCase> GetUseCaseById(int id)
        {
            UseCase useCase = _Ctx.UseCase.Find(id);
            if (useCase == null) return new ResponseWrapper<UseCase>
            {
                state = System.Net.HttpStatusCode.NotFound,
                error = $"Fehler beim Abrufen: Usecase mit ID {id} konnte nicht gefunden werden"
            };
            return new ResponseWrapper<UseCase>
            {
                state = System.Net.HttpStatusCode.OK,
                content = useCase
            };
        }

        /// <summary>
        /// the method enables user's to add new use cases to the database
        /// </summary>
        /// <param name="useCase">new use case</param>
        /// <returns>created use case</returns>
        public ResponseWrapper<UseCase> PostUseCase(UseCase useCase)
        {
            List<ServiceClass> temp = new List<ServiceClass>();
            foreach (ServiceClass serviceClass in useCase.ServiceClassMapping)
            {
                temp.Add(_Ctx.ServiceClass.Find(serviceClass.Id));
            }
            useCase.ServiceClassMapping = temp;
            useCase.Creation = DateTime.Now;
            _Ctx.UseCase.Add(useCase);
            _Ctx.SaveChanges();
            return new ResponseWrapper<UseCase>
            {
                state = System.Net.HttpStatusCode.OK,
                content = useCase
            };
        }

        /// <summary>
        /// the method enables user's to update existing use cases
        /// </summary>
        /// <param name="useCase">use case for update</param>
        /// <returns>updated use case</returns>
        public ResponseWrapper<UseCase> PutUseCase(UseCase useCase)
        {
            UseCase dbUseCase = _Ctx.UseCase.Find(useCase.Id);
            if (dbUseCase == null) return new ResponseWrapper<UseCase>
            {
                error = $"Fehler beim Bearbeiten des UseCases: UseCase mit ID {useCase.Id} konnte nicht gefunden werden",
                state = System.Net.HttpStatusCode.NotFound
            };
            List<ServiceClass> temp = new List<ServiceClass>();
            foreach (ServiceClass serviceClass in useCase.ServiceClassMapping)
            {
                temp.Add(_Ctx.ServiceClass.Find(serviceClass.Id));
            }
            if (dbUseCase != null)
            {
                List<ServiceClass> add = temp.Except(dbUseCase.ServiceClassMapping.ToList()).ToList();
                List<ServiceClass> remove = dbUseCase.ServiceClassMapping.Except(temp.ToList()).ToList();
                dbUseCase.ServiceClassMapping.AddRange(add);
                dbUseCase.ServiceClassMapping.RemoveAll(x => remove.Contains(x));
            }
            else
            {
                dbUseCase.ServiceClassMapping = temp;
            }
            dbUseCase.Title = useCase.Title;
            dbUseCase.Description = useCase.Description;
            _Ctx.SaveChanges();
            return new ResponseWrapper<UseCase>
            {
                content = dbUseCase,
                state = System.Net.HttpStatusCode.OK
            };
        }

        /// <summary>
        /// the method deletes an use case with the given id
        /// </summary>
        /// <param name="id">id of the use case</param>
        /// <returns>deleted use case</returns>
        public ResponseWrapper<UseCase> DeleteUseCase(int id)
        {
            UseCase useCase = _Ctx.UseCase.Find(id);
            if (useCase == null) return new ResponseWrapper<UseCase>
            {
                state = System.Net.HttpStatusCode.NotFound,
                error = $"Fehler beim Löschen: UseCase mit der ID {id} konnte nicht gefunden werden"
            };
            _Ctx.UseCase.Remove(useCase);
            _Ctx.SaveChanges();
            return new ResponseWrapper<UseCase>
            {
                state = System.Net.HttpStatusCode.OK,
                content = useCase
            };
        }
    }
}