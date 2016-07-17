using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TrainingTrackerAPI.Models;

namespace TrainingTrackerAPI.Mappers
{
    public static class PersonMapper
    {
        public static Person ApiToDataModel(ApiPerson apiPerson)
        {
            return new Person()
            {
                Id = apiPerson.Id,
                Username = apiPerson.Username,
                Email = apiPerson.Email,
                Password = apiPerson.Password,
                FirstName = apiPerson.FirstName,
                LastName = apiPerson.LastName,
                DOB = apiPerson.DOB,
                Gender = (Gender)apiPerson.Gender
            };
        }

        public static ApiPerson DataToApiModel(Person person)
        {
            return new ApiPerson()
            {
                Id = person.Id,
                Username = person.Username,
                Email = person.Email,
                Password = person.Password,
                FirstName = person.FirstName,
                LastName = person.LastName,
                DOB = person.DOB,
                Gender = (ApiGender)person.Gender
            };
        }
    }
}