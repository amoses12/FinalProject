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
        public ActionResult CarLibrary()
        {
            return View("/Views/Cars/CarLibrary.cshtml");
        }

        public ActionResult ManageCars()
        {
            return View("/Views/Manage/ManageCars.cshtml");
        }

        public ActionResult Details()
        {
            return View("/Views/Cars/CarDetails");
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
                               @MakeID = NewCar.Make.ID,
                               @Model = NewCar.Model.ModelName,
                               @Year = NewCar.Year,
                               @Drive = NewCar.Model.Drive,
                               @Displacement = NewCar.Model.Displacement,
                               @Engine = NewCar.Engine.EngineID,
                               @HorsePower = NewCar.Model.HorsePower,
                               @ZeroToSixty = NewCar.Performance.ZeroToSixty,
                               @TopSpeed = NewCar.Performance.TopSpeed,
                               @SixtyToZero = NewCar.Performance.SixtyToZero,
                               @QuarterMile = NewCar.Performance.QuarterMile,
                               @ImageId = NewCar.Image.ImageSrc
                           },
                           commandType: CommandType.StoredProcedure);
            
            db.Execute(command);

        }
        public void UpdateCar(Car carToUpdate)
        {
            CommandDefinition command = new CommandDefinition("Update_Car",
                new
                {
                    @MakeID = carToUpdate.Make.ID,
                    @Model = carToUpdate.Model.ModelName,
                    @Year = carToUpdate.Year,
                    @Drive = carToUpdate.Model.Drive,
                    @Displacement = carToUpdate.Model.Displacement,
                    @Engine = carToUpdate.Engine.EngineID,
                    @HorsePower = carToUpdate.Model.HorsePower,
                    @ZeroToSixty = carToUpdate.Performance.ZeroToSixty,
                    @TopSpeed = carToUpdate.Performance.TopSpeed,
                    @SixtyToZero = carToUpdate.Performance.SixtyToZero,
                    @QuarterMile = carToUpdate.Performance.QuarterMile,
                    @ImageId = carToUpdate.Image.ImageSrc
                }, commandType: CommandType.StoredProcedure);
            db.Execute(command);
        }

        public void DeleteCar(Car car)
        {
            

            CommandDefinition command = new CommandDefinition("Delete_Car", new { @ID = car.ID, @ModelID = car.ModelID, @PerformanceID = car.PerformanceID, @ImageID = car.ImageID }, commandType: CommandType.StoredProcedure);
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