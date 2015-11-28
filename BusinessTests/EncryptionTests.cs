using Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessTests
{
    [TestClass]
    public class EncryptionTests
    {
        [TestMethod]
        public void GenerateRandomPassword()
        {
            var password = Encryption.GeneratePassword();

            Assert.IsNotNull(password);
            Assert.AreEqual(10, password.Length);
        }

        [TestMethod]
        public void HashString()
        {
            var password = "Password1";
            var str = Encryption.HashWithSalt(password);

            Assert.IsNotNull(str);
            Assert.AreNotEqual(password, str);
            Assert.AreEqual(44, str.Length);
        }
    }
}
