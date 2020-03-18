using System.Net;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

using MobileApiCore.ViewModels;
using MobileApiCore.Presenter;
using ReposytoryPattern;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MobileApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private AppDbContext _db;
        public CarsController(AppDbContext db)
        {
            _db = db;
        }


        public IActionResult Get()
        {
            List<CarViewModel> cars = new List<CarViewModel>();
            JsonContentResult result = new JsonContentResult();

            foreach(var car in _db.Cars)
            {
                CarViewModel carNew = new CarViewModel
                {
                    Mark = car.Mark,
                    Year = car.Year
                };

                cars.Add(carNew);
            }
            result.StatusCode = (int)HttpStatusCode.OK;

            result.Content = JsonConvert.SerializeObject(cars, Formatting.Indented, 
                new JsonSerializerSettings { 
                    ContractResolver = new CamelCasePropertyNamesContractResolver(), NullValueHandling = NullValueHandling.Ignore 
                });
            return result;
        }

    }
}