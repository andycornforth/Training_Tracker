using Business;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TrainingTrackerAPI.Mappers;
using TrainingTrackerAPI.Models;

namespace TrainingTrackerAPI.Controllers
{
    public class PersonController : ApiController
    {
        private IPersonBusiness _personBusiness;

        public PersonController(IPersonBusiness personBusiness)
        {
            _personBusiness = personBusiness;
        }

        [HttpPost]
        public IHttpActionResult AddPerson(ApiPerson person)
        {
            var dataPerson = PersonMapper.ApiToDataModel(person);
            _personBusiness.AddPersonToDatabase(dataPerson);

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetPersonByUsername(string username)
        {
            var person = _personBusiness.GetPersonByUsername(username);

            var apiPerson = PersonMapper.DataToApiModel(person);

            return Ok(apiPerson);
        }
    }
}
