using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace s17248_kolokwium.DTOs.Responses
{
    public class ActionFiretruckResponse
    {
        public int IdAction { get; set; }
        public int IdFireTruck { get; set; }
        public ActionFiretruckResponse()
        { }
        public ActionFiretruckResponse(int action, int firetruck)
        {
            this.IdAction = action;
            this.IdFireTruck = firetruck;
        }
    }
}
