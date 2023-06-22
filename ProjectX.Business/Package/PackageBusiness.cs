using ProjectX.Entities;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Package;
using ProjectX.Entities.Resources;
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
        public PackResp ModifyPackage(PackReq req, string act, int userid)
        {
            PackResp response = new PackResp();
            response = _packageRepository.ModifyPackage(req, act, userid);
            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success, req.id == 0 ? SuccessCodeValues.Add : SuccessCodeValues.Update, "Package");
            return response;
        }
        public List<TR_Package> GetPackageList(PackSearchReq req)
        {
            return _packageRepository.GetPackageList(req);
        }
        public PackResp GetPackage(int IdPackage)
        {
            TR_Package repores = _packageRepository.GetPackage(IdPackage);
            PackResp resp = new PackResp();
            resp.id = repores.P_Id;
            resp.name = repores.P_Name;
            resp.zoneId = repores.P_ZoneID;
            resp.remove_deductable = repores.P_Remove_deductable;
            resp.adult_no = repores.P_Adult_No;
            resp.children_no = repores.P_Children_No;
            resp.pa_included = repores.P_PA_Included;
            return resp;
           
        }
    }
}
