using Geography.Business.State.Models;
using System.Collections.Generic;
using System;
using Geography.Business.Country.Types;

namespace Geography.Business.Country.Models
{
    public class CountryUpdateModel
    {
        public CountryUpdateModel()
        {
            States = new List<StateUpdateModel>();
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public string IsoCode { get; set; }
        public Continent Continent { get; set; }
        public List<StateUpdateModel> States { get; set; }
    }
}
