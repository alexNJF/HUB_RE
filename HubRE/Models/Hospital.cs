using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HubRE.Models
{
    public class Hospital
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<int> hospital_waite_queue ;
    }
}