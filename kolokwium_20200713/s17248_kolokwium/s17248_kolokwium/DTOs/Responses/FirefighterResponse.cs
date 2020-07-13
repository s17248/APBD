using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace s17248_kolokwium.DTOs.Responses
{
    public class FirefighterResponse
    {
        public int IdFirefighter { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public FirefighterResponse(int id, String first, String last)
        {
            this.IdFirefighter = id;
            this.FirstName = first;
            this.LastName = last;
        }
    }
}
