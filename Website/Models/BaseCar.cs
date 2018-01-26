using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Website.Models
{
    public class BaseCar
    {
        // GET: Car
        public int ID { get; set; }
        public int Make { get; set; }
        public int Model { get; set; }
        public int Year { get; set; }
        public int Engine { get; set; }
        public int Performance { get; set; }
        public int Image { get; set; }
    }
}


