using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Website.Models
{
    public class Performance
    {
        // GET: Performance
        public int PerformanceID {get; set;}
        public float ZeroToSixty { get; set; }
        public int TopSpeed { get; set; }
        public float SixtyToZero { get; set; }
        public float QuarterMile { get; set; }

    }
}