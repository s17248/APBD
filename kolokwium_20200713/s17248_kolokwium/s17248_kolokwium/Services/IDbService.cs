﻿using s17248_kolokwium.DTOs.Requests;
using s17248_kolokwium.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace s17248_kolokwium.Services
{
    public interface IDbService
    {
        public FirefighterResponse GetFirefighter(int IdFirefighter);
        public IEnumerable<FirefighterActionResponse> GetFirefigtherActions(int IdFirefighter);
        public FiretruckResponse AssignFiretruckToAction(int IdAction, FiretruckRequest request);
        public ActionResponse GetAction(int id);
    }
}