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
    }
}
