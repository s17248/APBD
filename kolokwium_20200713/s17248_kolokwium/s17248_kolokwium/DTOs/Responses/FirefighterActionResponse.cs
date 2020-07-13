using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace s17248_kolokwium.DTOs.Responses
{
    public class FirefighterActionResponse
    {
        public int IdAction { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public FirefighterActionResponse(int id, DateTime start, DateTime end)
        {
            this.IdAction = id;
            this.StartTime = start;
            this.EndTime = end;
        }
    }
}
