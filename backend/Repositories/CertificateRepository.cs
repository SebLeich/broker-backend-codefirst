using backend.Core;
using backend.Models;
using System.Collections.Generic;
using System.Linq;

namespace backend.Repositories
{
    public class CertificateRepository
    {
        private BrokerContext _Ctx;

        public CertificateRepository()
        {
            _Ctx = new BrokerContext();
        }

        public List<Certificate> GetCertificates()
        {
            return _Ctx.Certificate.ToList();
        }

        public Certificate PostCertificate(Certificate Certificate)
        {
            _Ctx.Certificate.Add(Certificate);
            _Ctx.SaveChanges();
            return Certificate;
        }
    }
}