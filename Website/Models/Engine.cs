using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Website.Models
{
    public class Engine
    {
        // GET: Engine
        public int EngineID { get; set; }
        public int Cylinders { get; set; }
        public string Charger { get; set; }
        public int Horsepower { get; set; }
    }
}