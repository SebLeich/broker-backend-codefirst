using backend.Core;
using backend.Models;
using System.Collections.Generic;
using System.Linq;

namespace backend.Repositories
{
    public class FeatureRepository
    {
        private BrokerContext _Ctx;

        public FeatureRepository()
        {
            _Ctx = new BrokerContext();
        }

        public List<Feature> GetFeatures()
        {
            return _Ctx.Feature.ToList();
        }

        public ResponseWrapper<Feature> GetFeatureById(int id)
        {
            var result = _Ctx.Feature.Find(id);
            if (result == null) return new ResponseWrapper<Feature>
            {
                state = System.Net.HttpStatusCode.NotFound,
                error = "no feature with the given id found"
            };
            return new ResponseWrapper<Feature>
            {
                state = System.Net.HttpStatusCode.OK,
                content = result
            };
        }

        public ResponseWrapper<Feature> PostFeature(Feature feature)
        {
            _Ctx.Feature.Add(feature);
            _Ctx.SaveChanges();
            return new ResponseWrapper<Feature>
            {
                state = System.Net.HttpStatusCode.Created,
                content = feature
            };
        }

        public ResponseWrapper<Feature> PutFeature(Feature feature)
        {
            validateNMRelations(feature);

            var result = _Ctx.Feature.Find(feature.Id);
            if (result == null) return new ResponseWrapper<Feature>
            {
                state = System.Net.HttpStatusCode.BadRequest,
                error = "no feature found with the given id for update"
            };

            result.Color = feature.Color;
            result.DescriptionDE = feature.DescriptionDE;
            result.DescriptionEN = feature.DescriptionEN;
            result.DescriptionES = feature.DescriptionES;
            result.Icon = feature.Icon;
            result.InternalDescription = feature.InternalDescription;

            _Ctx.SaveChanges();

            return new ResponseWrapper<Feature>
            {
                state = System.Net.HttpStatusCode.OK,
                content = result
            };
        }

        public ResponseWrapper<Feature> DeleteFeature(int id)
        {
            var result = _Ctx.Feature.Find(id);
            if (result == null) return new ResponseWrapper<Feature>
            {
                state = System.Net.HttpStatusCode.BadRequest,
                error = "no feature with the given id found while deleting"
            };

            _Ctx.Feature.Remove(result);
            _Ctx.SaveChanges();

            return new ResponseWrapper<Feature>
            {
                state = System.Net.HttpStatusCode.OK,
                content = result
            };
        }

        /// <summary>
        /// the method validates the n:m relations of the given entity
        /// all passed connections will be added, all leaved connections will be removed (in case they are stored before)
        /// </summary>
        protected Feature validateNMRelations(Feature feature)
        {
            List<Service> tempSer = new List<Service>();
            foreach (Service service in feature.Services)
            {
                tempSer.Add(_Ctx.Service.Find(service.Id));
            }
            Feature oldFeature = _Ctx.Feature.Find(feature.Id);
            if (oldFeature != null)
            {
                List<Service> addSer = tempSer.Except(oldFeature.Services.ToList()).ToList();
                List<Service> removeSer = oldFeature.Services.Except(tempSer.ToList()).ToList();
                oldFeature.Services.AddRange(addSer);
                oldFeature.Services.RemoveAll(x => removeSer.Contains(x));
                return oldFeature;
            }
            else
            {
                feature.Services = tempSer;
                return feature;
            }
        }
    }
}