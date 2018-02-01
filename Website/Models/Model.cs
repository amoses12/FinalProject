using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Website.Models
{
    public class Model
    {
        // GET: Model
        public int ModelID { get; set; }
        public string ModelName { get; set; }
        public string Drive { get; set; }
        public float Displacement { get; set; }
        public int HorsePower { get; set; }
    }
}