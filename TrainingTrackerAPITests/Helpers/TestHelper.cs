﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingTrackerAPI.Models;

namespace TrainingTrackerAPITests.Helpers
{
    public static class TestHelper
    {
        public static ApiPerson GetTestApiPerson()
        {
            return GetTestApiPerson("Username", "Password1");
        }

        public static ApiPerson GetTestApiPerson(string username, string password, string firstName = "Test", string lastName = "User", string email = "test@user.com")
        {
            return new ApiPerson()
            {
                Username = username,
                Password = password,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                DOB = new DateTime(1993, 1, 22),
                Gender = ApiGender.MALE
            };
        }

        public static Person GetTestDataPerson(string username)
        {
            return new Person()
            {
                Username = username,
                Password = "password",
                FirstName = "firstName",
                LastName = "lastName",
                Email = "email@address.com",
                DOB = new DateTime(1993, 1, 22),
                Gender = Gender.MALE
            };
        }

        internal static ApiLog GetTestApiLog()
        {
            return new ApiLog();
        }

        public static List<Log> GetTestDataLogList()
        {
            return new List<Log>()
            {
                new Log() {
                    Title = "Test log",
                    DateAdded = DateTime.Now
                },
            };
        }
    }
}