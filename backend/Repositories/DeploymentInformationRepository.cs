using backend.Core;
using backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace backend.Repositories
{
    public class DeploymentInformationRepository
    {
        private BrokerContext _Ctx;

        public DeploymentInformationRepository()
        {
            _Ctx = new BrokerContext();
        }

        public List<DeploymentInfo> GetDeploymentInfo()
        {
            return _Ctx.DeploymentInfo.ToList();
        }

        public DeploymentInfo PostDeploymentInfo(DeploymentInfo DeploymentInfo)
        {
            _Ctx.DeploymentInfo.Add(DeploymentInfo);
            _Ctx.SaveChanges();
            return DeploymentInfo;
        }
    }
}