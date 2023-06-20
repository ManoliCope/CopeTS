using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Package;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.Package
{
    public interface IPackageBusiness
    {
        public PackResp ModifyPackage(PackResp req);
        public List<TR_Package> GetPackageList(PackReq req);
        public TR_Package GetPackage(int IdPackage);
    }
}
