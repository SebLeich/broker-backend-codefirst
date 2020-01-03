using backend.Core;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace backend.Repositories
{
    public class MatchingResponseRepository
    {
        private BrokerContext _Ctx;

        public MatchingResponseRepository()
        {
            _Ctx = new BrokerContext();
        }

        public ResponseWrapper<List<MatchingResponse>> GetMatchingResponses()
        {
            return new ResponseWrapper<List<MatchingResponse>>
            {
                state = HttpStatusCode.OK,
                content = _Ctx.MatchingResponse.ToList()
            };
        }

        public ResponseWrapper<MatchingResponse> GetMatchingResponseById(int id)
        {
            MatchingResponse OldResponse = _Ctx.MatchingResponse.Find(id);
            if (OldResponse == null) return new ResponseWrapper<MatchingResponse>
            {
                state = HttpStatusCode.NotFound,
                error = "Fehler: es wurde versucht eine existierende Matching Response zu löschen. ID konnte allerdings nicht gefunden werden."
            };
            return new ResponseWrapper<MatchingResponse>
            {
                state = HttpStatusCode.OK,
                content = OldResponse
            };
        }

        public ResponseWrapper<MatchingResponse> PostMatchingResponse(MatchingResponse matchingResponse)
        {
            _Ctx.MatchingResponse.Add(matchingResponse);
            _Ctx.SaveChanges();
            return new ResponseWrapper<MatchingResponse>
            {
                state = HttpStatusCode.OK,
                content = matchingResponse
            };
        }

        public ResponseWrapper<MatchingResponse> PutMatchingResponse(MatchingResponse matchingResponse)
        {
            MatchingResponse OldResponse = _Ctx.MatchingResponse.Find(matchingResponse.Id);
            if (OldResponse == null) return new ResponseWrapper<MatchingResponse>
            {
                state = HttpStatusCode.NotFound,
                error = "Fehler: es wurde versucht eine existierende Matching Response zu überschreiben. ID konnte allerdings nicht gefunden werden."
            };
            OldResponse.pointscategories = matchingResponse.pointscategories;
            OldResponse.pointscertificates = matchingResponse.pointscertificates;
            OldResponse.pointsdatalocations = matchingResponse.pointsdatalocations;
            OldResponse.pointsdeploymentinfos = matchingResponse.pointsdeploymentinfos;
            OldResponse.pointsHasAutomatedSynchronisation = matchingResponse.pointsHasAutomatedSynchronisation;
            OldResponse.pointsHasDBMS = matchingResponse.pointsHasDBMS;
            OldResponse.pointsHasFileCompression = matchingResponse.pointsHasFileCompression;
            OldResponse.pointsHasFileEncryption = matchingResponse.pointsHasFileEncryption;
            OldResponse.pointsHasFileLocking = matchingResponse.pointsHasFileLocking;
            OldResponse.pointsHasFilePermissions = matchingResponse.pointsHasFilePermissions;
            OldResponse.pointsHasFileVersioning = matchingResponse.pointsHasFileVersioning;
            OldResponse.pointsHasReplication = matchingResponse.pointsHasReplication;
            OldResponse.pointsmodels = matchingResponse.pointsmodels;
            OldResponse.pointsproviders = matchingResponse.pointsproviders;
            OldResponse.pointsstoragetype = matchingResponse.pointsstoragetype;
            _Ctx.SaveChanges();
            return new ResponseWrapper<MatchingResponse>
            {
                state = HttpStatusCode.OK,
                content = matchingResponse
            };
        }

        public ResponseWrapper<MatchingResponse> DeleteMatchingResponse(int id)
        {
            MatchingResponse OldResponse = _Ctx.MatchingResponse.Find(id);
            if (OldResponse == null) return new ResponseWrapper<MatchingResponse>
            {
                state = HttpStatusCode.NotFound,
                error = "Fehler: es wurde versucht eine existierende Matching Response zu löschen. ID konnte allerdings nicht gefunden werden."
            };
            _Ctx.MatchingResponse.Remove(OldResponse);
            _Ctx.SaveChanges();
            return new ResponseWrapper<MatchingResponse>
            {
                state = HttpStatusCode.OK,
                content = OldResponse
            };
        }
    }
}