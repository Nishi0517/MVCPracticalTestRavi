using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Asp.NETMVCCRUD.Models;
using System.Data.Entity;

namespace Asp.NETMVCCRUD.Controllers
{
    public class EmployeeController : Controller
    {
        //DBModel db = new DBModel();
        TestEntities db = new TestEntities();
        // GET: /Employee/
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult GetData()
        {
            //using (DBModel db = new DBModel())
            //{
                //Getting Employee Data From Employee table 
                List<RaviEmp> empList = db.RaviEmps.ToList<RaviEmp>();

            // Fetch the  country , state names and city names from the database using the countryId ,stateId and cityId

            var countryIds = empList.Select(e => e.Country_Id).Distinct().ToList();
            var stateIds = empList.Select(e => e.State_Id).Distinct().ToList();
            var cityIds = empList.Select(e => e.City_ID).Distinct().ToList();


            var counties = db.RaviCountries.Where(s => countryIds.Contains(s.Country_Id)).ToList();
            var states = db.RaviStates.Where(s => stateIds.Contains(s.State_Id)).ToList();
            var cities = db.RaviCities.Where(c => cityIds.Contains(c.City_ID)).ToList();

            // Create a new list with the desired properties (including state and city names)
            var empData = empList.Select(e => new
            {
                e.EmployeeID,
                e.Name,
                e.Email,
                e.Phone,
                e.Gender,
                Country = counties.FirstOrDefault(s=>s.Country_Id==e.Country_Id)?.Country_Name,
                State = states.FirstOrDefault(s => s.State_Id == e.State_Id)?.State_Name,
                City = cities.FirstOrDefault(c => c.City_ID == e.City_ID)?.City_Name
            }).ToList();

            return Json(new { data = empData }, JsonRequestBehavior.AllowGet);
            //}
        }


        [HttpGet]
        public ActionResult AddOrEdit(int id = 0)
        {
            List<RaviCountry> country = db.RaviCountries.ToList();
            ViewBag.country = new SelectList(country, "Country_Id", "Country_Name");
            List<RaviState> states = db.RaviStates.ToList();
            ViewBag.states = new SelectList(states, "State_Id", "State_Name");
            List<RaviCity> city = db.RaviCities.ToList();
            ViewBag.city = new SelectList(city, "City_ID", "City_Name");
            if (id == 0)
                return View(new RaviEmp());
            else
            {
                //using (DBModel db = new DBModel())
                //{
                    return View(db.RaviEmps.Where(x => x.EmployeeID==id).FirstOrDefault<RaviEmp>());
                //}
            }
        }
        public JsonResult GetCityList(int State_Id)
        {
            //Retrvive City List 
            db.Configuration.ProxyCreationEnabled = false;
            List<RaviCity> cities = db.RaviCities.Where(x => x.State_Id == State_Id).ToList();

            return Json(cities,JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStateList(int Country_Id)
        {
            //Retrvive State List 
            db.Configuration.ProxyCreationEnabled = false;
            List<RaviState> cities = db.RaviStates.Where(x => x.Country_Id == Country_Id).ToList();

            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddOrEdit(RaviEmp emp)
        {
            //using (DBModel db = new DBModel())
            //{ //Insert and Update Accordingly 

                if (emp.EmployeeID == 0)
                {
                    db.RaviEmps.Add(emp);
                    db.SaveChanges();
                    return Json(new { success = true, message = "Saved Successfully" }, JsonRequestBehavior.AllowGet);
                }
                else {

                    db.Entry(emp).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true, message = "Updated Successfully" }, JsonRequestBehavior.AllowGet);
                }
            //}


        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            //using (DBModel db = new DBModel())
            //{
                //delete from Employee tablew 
                RaviEmp emp = db.RaviEmps.Where(x => x.EmployeeID == id).FirstOrDefault<RaviEmp>();
                db.RaviEmps.Remove(emp);
                db.SaveChanges();
                return Json(new { success = true, message = "Deleted Successfully" }, JsonRequestBehavior.AllowGet);
            //}
        }
    }
}