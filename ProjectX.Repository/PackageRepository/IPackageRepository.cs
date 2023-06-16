using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Package;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Repository.PackageRepository
{
    public interface IPackageRepository
    {
        public PackResp ModifyPackage(PackResp req);
        public List<TR_Package> GetPackage(PackReq req);
    }
}
