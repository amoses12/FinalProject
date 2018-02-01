using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Website.Models
{
    public class Car
    {
        // GET: Car
       public int ID { get; set; }
       public Make Make { get; set; }
       public Model Model { get; set; }
       public int Year { get; set; }
       public Engine Engine { get; set; }
       public Performance Performance { get; set; }
       public Image Image { get; set; }
        public int CarID { get; set; }
        public int MakeID { get; set; }
        public int ModelID { get; set; }
        public int YearID { get; set; }
        public int EngineID { get; set; }
        public int PerformanceID { get; set; }
        public int ImageID { get; set; }

        public Car()
        {
            this.Make = new Make();
            this.Model = new Model();
            this.Engine = new Engine();
            this.Performance = new Performance();
            this.Image = new Image();
        } 
    }
}