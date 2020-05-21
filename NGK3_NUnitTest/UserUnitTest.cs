using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ngkopgavea.Controllers;
using ngkopgavea.Models;
using ngkopgavea.RepositoryPattern;
using NSubstitute;
using NUnit.Framework;
using static BCrypt.Net.BCrypt;
 

namespace NGK3_NUnitTest
{
    public class UserUnitTest
    {
        private UserController _uut;
        private IUserRepository _repository;
        
        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<IUserRepository>();
            _uut = new UserController();
        }

        [Test]
        public void LoginFails_UserDoesNotExist()
        {
            //Arrange
            var request = new UserDTO()
            {
                Username = "SebastianFindesIkke",
                Password = "123123"
            };

            _repository.Get("SebastianFindesIkke").Returns((true)); 

            //Act
            var result = _uut.Authenticate(request);

            //Assert
            Assert.That(result.Result.Value == null); //Virker denne? Hilsen en lidt forvirret Gustabovich

        }

        [Test]
        public void LoginFails_WrongPassword()
        {
            //Arrange
            var request = new UserDTO()
            {
                Username = "SebastianVejrmand",
                Password = "DettePasswordFindesIkke"
            };

            var account = new User
            {
                Id = 3,
                Username = "SebastianVejrmand",
                Password = HashPassword("EtGodtPassword")
            };

            //Act
            var result = _uut.Authenticate(request);

            //Assert
            Assert.That(result.Result.Value.Password);
            

        }



        /* Not working due to failure to read the static Settings class. Throws a nullreferenceexception.
        [Test]
        public void LoginSuccess()
        {
            //Arrange
            var request = new AccountRequest()
            {
                UserName = "Jens",
                Password = "1234jajaja"
            };
            
            var account = new Account
            {
                Id = 3,
                UserName = "Jens",
                PasswordHash = HashPassword("1234jajaja", 10)
            };
            
            _repository.GetByUserName("Jens").Returns(account);
            
            //Act
            var result = _uut.Login(request).Result as ObjectResult;
            //Assert
            var jsonActual = JsonSerializer.Serialize(result.Value);
            Assert.That(jsonActual.Contains("jwt"));
        }
        */

        [Test]
        public void Register_Fails_UserAlreadyExists()
        {
            //Arrange
            var request = new AccountRequest()
            {
                UserName = "Jens",
                Password = "1234jajaja"
            };

            var account = new Account
            {
                Id = 3,
                UserName = "Jens",
                PasswordHash = HashPassword("1234jajaja", 10)
            };

            _repository.GetByUserName("Jens").Returns(account);

            //Act
            var result = _uut.Register(request).Result as ObjectResult;

            //Assert
            var expected = new { success = false, message = "UserName already exists, try logging in." };
            var jsonActual = JsonSerializer.Serialize(result.Value);
            var jsonExpected = JsonSerializer.Serialize(expected);

            Assert.That(jsonActual, Is.EqualTo(jsonExpected));
        }
    }
}