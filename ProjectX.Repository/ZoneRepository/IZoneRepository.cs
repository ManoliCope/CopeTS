using ProjectX.Entities.bModels;
using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Zone;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Repository.ZoneRepository
{
    public interface IZoneRepository
    {
        public ZoneResp ModifyZone(ZoneResp req);
        public List<TR_Zone> GetZone(ZoneReq req);
    }
}
