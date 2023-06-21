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
        public ZoneResp ModifyZone(ZoneReq req, string act, int userid);
        public List<TR_Zone> GetZoneList(ZoneSearchReq req);
        public TR_Zone GetZone(int IdZone);
    }
}
