using ProjectX.Entities;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Zone;
using ProjectX.Entities.Resources;
using ProjectX.Repository.ZoneRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.Zone
{
    public class ZoneBusiness : IZoneBusiness
    {
        IZoneRepository _zoneRepository;

        public ZoneBusiness(IZoneRepository zoneRepository)
        {
            _zoneRepository = zoneRepository;
        }
        public ZoneResp ModifyZone(ZoneReq req, string act, int userid)
        {
            ZoneResp response = new ZoneResp();
            response = _zoneRepository.ModifyZone(req, act, userid);
            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success, req.id == 0 ? SuccessCodeValues.Add : SuccessCodeValues.Update, "Zone");
            return response;

        }
        public List<TR_Zone> GetZoneList(ZoneSearchReq req)
        {
            return _zoneRepository.GetZoneList(req);
        }
        public ZoneResp GetZone(int IdZone)
        {
           
            TR_Zone repores = _zoneRepository.GetZone(IdZone);
            ZoneResp resp = new ZoneResp();
            resp.id = repores.Z_Id;
            resp.title = repores.Z_Title;
            resp.destinationId = repores.Z_Destination_Id;

            return resp;
        }
    }
}
