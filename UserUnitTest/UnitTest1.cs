using ZealandPetShop.Models.Login;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UserUnitTesting
{
    [TestClass]
    public class UserTests
    {
        private User _user;

        [TestInitialize]
        public void Setup()
        {
            _user = new User
            {
                Email = "valid@example.com",
                Password = "AnyPassword",
                FirstName = "John",
                LastName = "Doe",
                Phone = "12345678",
                Address = "123 Main St"
            };
        }

        [TestMethod]
        public void TestValidUser()
        {
            // Act & Assert
            Assert.AreEqual("valid@example.com", _user.Email);
            Assert.AreEqual("AnyPassword", _user.Password);
            Assert.AreEqual("John", _user.FirstName);
            Assert.AreEqual("Doe", _user.LastName);
            Assert.AreEqual("12345678", _user.Phone);
            Assert.AreEqual("123 Main St", _user.Address);
        }

        [TestMethod]
        public void TestInvalidEmail()
        {
            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => _user.Email = "invalid-email");
            Assert.ThrowsException<ArgumentException>(() => _user.Email = "admin");
            _user.Email = "email@gmail.com";
            Assert.AreEqual("email@gmail.com", _user.Email);
        }

        [TestMethod]
        public void TestInvalidFirstName()
        {
            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => _user.FirstName = "John123");
            Assert.ThrowsException<ArgumentException>(() => _user.FirstName = "John!");
            Assert.ThrowsException<ArgumentException>(() => _user.FirstName = "J0hn");

            _user.FirstName = "Jøn";
            Assert.AreEqual("Jøn", _user.FirstName);
        }

        [TestMethod]
        public void TestInvalidLastName()
        {
            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => _user.LastName = "Doe123");
            Assert.ThrowsException<ArgumentException>(() => _user.LastName = "Doe!");
            Assert.ThrowsException<ArgumentException>(() => _user.LastName = "D0e");

            _user.LastName = "Døe";
            Assert.AreEqual("Døe", _user.LastName);
        }

        [TestMethod]
        public void TestInvalidPhone()
        {
            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() => _user.Phone = "12345");
            Assert.ThrowsException<ArgumentException>(() => _user.Phone = "123456789");
            Assert.ThrowsException<ArgumentException>(() => _user.Phone = "abcdefgh");
            Assert.ThrowsException<ArgumentException>(() => _user.Phone = "12a45678");

            _user.Phone = "12344321";
            Assert.AreEqual("12344321", _user.Phone);
        }
    }
}