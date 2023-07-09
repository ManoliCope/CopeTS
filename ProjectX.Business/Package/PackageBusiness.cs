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
            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success, req.Id == 0 ? SuccessCodeValues.Add : SuccessCodeValues.Update, "Package");
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
            resp.Id = repores.P_Id;
            resp.ProductId = repores.PR_Id;
            resp.Name = repores.P_Name;
            resp.ZoneID = repores.P_ZoneID;
            resp.Remove_deductable = repores.P_Remove_deductable;
            resp.Adult_No = repores.P_Adult_No;
            resp.Children_No = repores.P_Children_No;
            resp.PA_Included = repores.P_PA_Included;

            resp.Child_Max_Age = repores.P_Child_Max_Age;
            resp.Adult_Max_Age = repores.P_Adult_Max_Age;
            resp.Special_Case = repores.P_Special_Case;

            return resp;
           
        }
    }
}
