using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace s17248_kolokwium.DTOs.Responses
{
    public class ActionResponse
    {
        public int IdAction { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool NeedsSpecialEquipment { get; set; }
        public ActionResponse(int id, DateTime start, DateTime end, bool special)
        {
            this.IdAction = id;
            this.StartTime = start;
            this.EndTime = end;
            this.NeedsSpecialEquipment = special;
        }
    }
}
