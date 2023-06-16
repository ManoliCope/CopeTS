using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Package;
using ProjectX.Repository.PackageRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.Package
{
    public class PackageBusiness : IPackageBusiness
    {
        IPackageRepository _packageRepository;

        public PackageBusiness(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }
        public PackResp ModifyPackage(PackResp req)
        {
            return _packageRepository.ModifyPackage(req);
        }
        public List<TR_Package> GetPackage(PackReq req)
        {
            return _packageRepository.GetPackage(req);
        }
    }
}
