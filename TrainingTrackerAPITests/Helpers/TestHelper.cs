using Models;
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

        public static List<Exercise> GetTestDataExerciseList()
        {
            return new List<Exercise>()
            {
                new Exercise()
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

        public static Exercise GetTestDataExercise(string title = "test title")
        {
            return new Exercise()
            {
                Id = 1,
                Title = title
            };
        }

        internal static ApiLog GetTestApiLog(string title = "Test")
        {
            return new ApiLog()
            {
                Title = title
            };
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

        public static Log GetTestDataLog(string title = "Test")
        {
            return new Log()
            {
                Title = title
            };
        }
    }
}
