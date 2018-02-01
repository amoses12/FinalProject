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

        public ActionResult CarDetails()
        {
            return View("/Views/Cars/CarDetails.cshtml");
        }

        public ActionResult CarImages()
        {
            return View("/Views/Cars/CarImages.cshtml");
        }

        public ActionResult GetAllCars()
        {
            List<Car> cars = new List<Car>();
            cars = db.Query<Car>("Get_All_Car", commandType: CommandType.StoredProcedure).ToList();

            foreach(Car car in cars)
            {
                car.Make = db.Query<Make>("Get_Make", new { MakeID = car.MakeID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                car.Model = db.Query<Model>("Get_Model", new { ModelID = car.ModelID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                car.Engine = db.Query<Engine>("Get_Engine", new { EngineID = car.EngineID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                car.Performance = db.Query<Performance>("Get_Performance", new { PerformanceID = car.PerformanceID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                car.Image = db.Query<Image>("Get_Image", new { ImageID = car.ImageID }, commandType: CommandType.StoredProcedure).FirstOrDefault();

            }
            return Json(cars, JsonRequestBehavior.AllowGet);
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
                               @EngineID = NewCar.Engine.EngineID,
                               @HorsePower = NewCar.Model.HorsePower,
                               @ZeroToSixty = NewCar.Performance.ZeroToSixty,
                               @TopSpeed = NewCar.Performance.TopSpeed,
                               @SixtyToZero = NewCar.Performance.SixtyToZero,
                               @QuarterMile = NewCar.Performance.QuarterMile,
                               @ImageSrc = NewCar.Image.ImageSrc
                           },
                           commandType: CommandType.StoredProcedure);
            
            db.Execute(command);

        }
        public void UpdateCar(Car carToUpdate)
        {
            CommandDefinition command = new CommandDefinition("Update_Car",
                new
                {
                    @ID = carToUpdate.ID,
                    @MakeID = carToUpdate.Make.MakeID,
                    @ModelID = carToUpdate.Model.ModelID,
                    @Model = carToUpdate.Model.ModelName,
                    @Year = carToUpdate.Year,
                    @Drive = carToUpdate.Model.Drive,
                    @Displacement = carToUpdate.Model.Displacement,
                    @EngineID = carToUpdate.Engine.EngineID,
                    @HorsePower = carToUpdate.Model.HorsePower,
                    @PerformanceID = carToUpdate.Performance.PerformanceID,
                    @ZeroToSixty = carToUpdate.Performance.ZeroToSixty,
                    @TopSpeed = carToUpdate.Performance.TopSpeed,
                    @SixtyToZero = carToUpdate.Performance.SixtyToZero,
                    @QuarterMile = carToUpdate.Performance.QuarterMile,
                    @ImageId = carToUpdate.Image.ImageID,
                    @ImageSrc = carToUpdate.Image.ImageSrc
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
            CommandDefinition command = new CommandDefinition("Delete_Make", new { @MakeID = make.MakeID }, commandType: CommandType.StoredProcedure);
            db.Execute(command);
        }

        public ActionResult GetEngines()
        {
            return Json(db.Query<Engine>("Get_Engines", commandType: CommandType.StoredProcedure).ToList(), JsonRequestBehavior.AllowGet);
        }

        public void AddEngine(Engine engine)
        {
            CommandDefinition command = new CommandDefinition("Add_Engine", new { @EngineName = engine.EngineName }, commandType: CommandType.StoredProcedure);
            db.Execute(command);
        }

        public void DeleteEngine(Engine engine)
        {
            CommandDefinition command = new CommandDefinition("Delete_Engine", new { @ID = engine.EngineID }, commandType: CommandType.StoredProcedure);
            db.Execute(command);
        }

        public ActionResult GetCarById(int id)
        {
            Car car = new Car();
            car = db.Query<Car>("Get_Car_Detail", new { @ID = id }, commandType: CommandType.StoredProcedure).FirstOrDefault();


            car.Make = db.Query<Make>("Get_Make", new { MakeID = car.MakeID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            car.Model = db.Query<Model>("Get_Model", new { ModelID = car.ModelID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            car.Engine = db.Query<Engine>("Get_Engine", new { EngineID = car.EngineID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            car.Performance = db.Query<Performance>("Get_Performance", new { PerformanceID = car.PerformanceID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            car.Image = db.Query<Image>("Get_Image", new { ImageID = car.ImageID }, commandType: CommandType.StoredProcedure).FirstOrDefault();


            return Json(car, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SortByMake(Make make)
        {
            List<Car> cars = new List<Car>();

            cars = db.Query<Car>("Sort_By_Make", new { @MakeID = make.MakeID }, commandType: CommandType.StoredProcedure).ToList();

            foreach (Car car in cars)
            {
                car.Make = db.Query<Make>("Get_Make", new { MakeID = car.MakeID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                car.Model = db.Query<Model>("Get_Model", new { ModelID = car.ModelID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                car.Engine = db.Query<Engine>("Get_Engine", new { EngineID = car.EngineID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                car.Performance = db.Query<Performance>("Get_Performance", new { PerformanceID = car.PerformanceID }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                car.Image = db.Query<Image>("Get_Image", new { ImageID = car.ImageID }, commandType: CommandType.StoredProcedure).FirstOrDefault();

            }
            return Json(cars, JsonRequestBehavior.AllowGet);
        }
    

        

    }
}