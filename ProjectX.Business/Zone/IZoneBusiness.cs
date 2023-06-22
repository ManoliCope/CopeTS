using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Zone;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Business.Zone
{
    public interface IZoneBusiness
    {
        public ZoneResp ModifyZone(ZoneReq req, string act, int userid);
        public List<TR_Zone> GetZoneList(ZoneSearchReq req);
        public ZoneResp GetZone(int IdZone);
    }
}
