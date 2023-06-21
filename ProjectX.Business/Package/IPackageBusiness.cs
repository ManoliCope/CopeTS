using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Package;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.Package
{
    public interface IPackageBusiness
    {
        public PackResp ModifyPackage(PackReq req, string act, int userid);
        public List<TR_Package> GetPackageList(PackSearchReq req);
        public PackResp GetPackage(int IdPackage);
    }
}
