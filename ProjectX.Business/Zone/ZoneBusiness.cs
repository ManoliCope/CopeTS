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
        public List<TR_Zone> GetZone(ZoneReq req)
        {
            return _zoneRepository.GetZone(req);
        }
    }
}
