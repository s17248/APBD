using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace s17248_kolokwium.DTOs.Responses
{
    public class FiretruckResponse
    {
        public int IdFireTruck { get; set; }
        public String OperationalNumber { get; set; }
        public Boolean SpecialEquipment { get; set; }
    }
}
