using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Zone;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.Zone
{
    public interface IZoneBusiness
    {
        public ZoneResp ModifyZone(ZoneResp req);
        public List<TR_Zone> GetZone(ZoneReq req);
    }
}
