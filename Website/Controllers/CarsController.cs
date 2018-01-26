using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Website.Models;
using Dapper;
namespace Website.Controllers
{
    public class CarsController : Controller
    {
        IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
        // GET: Cars
        public ActionResult Index()
        {
            return View("CarLibrary.cshtml");
        }

        public ActionResult GetAllCars()
        {
            return Json(db.Query<Car>("Get_All_Cars", commandType: CommandType.StoredProcedure).ToList(), JsonRequestBehavior.AllowGet);
        }

        public void CreateCar(Car NewCar)
        {
         
                CommandDefinition command = new CommandDefinition("Create_Car",
                           new
                           {
                               @Make = NewCar.Make.MakeName,
                               @Model = NewCar.Model.ModelName,
                               @Year = NewCar.Year,
                               @Drive = NewCar.Model.Drive,
                               @Displacement = NewCar.Model.Displacement,
                               @Cylinders = NewCar.Engine.Cylinders,
                               @Charger = NewCar.Engine.Charger,
                               @HorsePower = NewCar.Model.HorsePower,
                               @ZeroToSixty = NewCar.Performance.ZeroToSixty,
                               @TopSpeed = NewCar.Performance.TopSpeed,
                               @SixtyToZero = NewCar.Performance.SixtyToZero,
                               @QuarterMile = NewCar.Performance.QuarterMile,
                               @ImageId = NewCar.Image.ImageID
                           },
                           commandType: CommandType.StoredProcedure);
            
            db.Execute(command);

        }

        public void DeleteCar(int Id)
        {
            string sql = "Select * from Cars Where ID = @ID";

            BaseCar car = new BaseCar();

            car = db.Query<BaseCar>(sql, new { @ID = Id }).FirstOrDefault();

            CommandDefinition command = new CommandDefinition("Delete_Car", new { @ID = Id }, commandType: CommandType.StoredProcedure);
            db.Execute(command);
        }

        public ActionResult GetMakes()
        {
            return Json(db.Query<Make>("Get_Makes", commandType: CommandType.StoredProcedure).ToList(), JsonRequestBehavior.AllowGet);
        }

        public void AddMake(string make)
        {
            CommandDefinition command = new CommandDefinition("Add_Make", new { @MakeName = make }, commandType: CommandType.StoredProcedure);
            db.Execute(command);
        }

        public void DeleteMake(Make make)
        {
            CommandDefinition command = new CommandDefinition("Delete_Make", new { @MakeID = make.ID }, commandType: CommandType.StoredProcedure);
            db.Execute(command);
        }

        public ActionResult GetEngines()
        {
            return Json(db.Query<Engine>("Get_Engines", commandType: CommandType.StoredProcedure).ToList(), JsonRequestBehavior.AllowGet);
        }

        public void AddEngine(Engine engine)
        {
            CommandDefinition command = new CommandDefinition("Add_Engine", new { @Cylinders = engine.Cylinders, @Charger = engine.Charger }, commandType: CommandType.StoredProcedure);
            db.Execute(command);
        }

        public void DeleteEngine(Engine engine)
        {
            CommandDefinition command = new CommandDefinition("Delete_Engine", new { @ID = engine.EngineID }, commandType: CommandType.StoredProcedure);
            db.Execute(command);
        }

        public ActionResult GetCarById(int Id)
        {
            return Json(db.Query<Car>("Get_Car_Details", new { @ID = Id }, commandType: CommandType.StoredProcedure), JsonRequestBehavior.AllowGet);
        }

    }
}