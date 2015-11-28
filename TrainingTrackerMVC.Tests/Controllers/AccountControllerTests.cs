using Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Hosting;
using TrainingTrackerMVC.Controllers;
using TrainingTrackerMVC.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net;
using Microsoft.Owin.Security;
using Microsoft.Owin;

namespace TrainingTrackerMVC.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTests
    {
        //private AccountController _accountController;
        //private Mock<PersonBusiness> _personBusiness;


        //[TestInitialize]
        //public void SetUp()
        //{
        //    _personBusiness = new Mock<PersonBusiness>();
        //}

        //[TestMethod]
        //public void RegisterValidUserExpectNoError()
        //{
        //    var model = new RegisterViewModel()
        //    {
        //        Email = "andycornforth@live.co.uk",
        //        Password = "Password1",
        //        ConfirmPassword = "Password1",
        //        Dob = new DateTime(1993, 1, 22),
        //        FirstName = "Andy",
        //        LastName = "Cornforth",
        //        Gender = Gender.MALE
        //    };

        //    var context = new ApplicationDbContext();
        //    var store = new UserStore<ApplicationUser>(context);
        //    var applicationUserManager = new ApplicationUserManager(store);
        //    //var authenticaitonManager = new AuthenticationManager();

        //    //var http = new HttpContext(new HttpRequest("","",""), new HttpResponse(TextWriter.Null));

        //    var applicaitonSignInManager = new ApplicationSignInManager(applicationUserManager, null);
        //    //var user = new ApplicationUser(); 

        //    HttpContext.Current = new HttpContext(new SimpleWorkerRequest("", "", "", null, new StringWriter()));

        //    _accountController = new AccountController(applicationUserManager, applicaitonSignInManager, _personBusiness.Object);

        //    //_accountController.HttpContext.

        //    var result = _accountController.Register(model);
        //}
    }
}
