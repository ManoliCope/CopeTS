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
        public PackResp ModifyPackage(PackReq req, string act, int userid);
        public List<TR_Package> GetPackageList(PackSearchReq req);
        public TR_Package GetPackage(int IdPackage);
    }
}
