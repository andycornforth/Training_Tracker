using Business;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace TrainingTrackerAPI.Controllers
{
    public class PersonController : ApiController
    {
        private IPersonBusiness _personBusiness;

        public PersonController()
        {
            _personBusiness = new PersonBusiness();
        }

        public PersonController(IPersonBusiness personBusiness)
        {
            _personBusiness = personBusiness;
        }

        public IHttpActionResult AddPerson(Person person)
        {
            _personBusiness.AddPersonToDatabase(person);
            return Ok();
        }
    }
}
