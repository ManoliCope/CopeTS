using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Zone;
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
        public ZoneResp ModifyZone(ZoneResp req)
        {
            return _zoneRepository.ModifyZone(req);
        }
        public List<TR_Zone> GetZoneList(ZoneReq req)
        {
            return _zoneRepository.GetZoneList(req);
        }
        public TR_Zone GetZone(int IdZone)
        {
            return _zoneRepository.GetZone(IdZone); 
        }
    }
}
