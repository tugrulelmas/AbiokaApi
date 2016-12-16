using AbiokaApi.ApplicationService.Messaging;
using AbiokaApi.Domain;
using AbiokaApi.Infrastructure.Common.Authentication;
using AbiokaApi.Infrastructure.Common.Exceptions;
using AbiokaApi.UnitTest.Service.Mock;
using Moq;
using NUnit.Framework;
using System;
using System.Net;

namespace AbiokaApi.UnitTest.Service
{
    [TestFixture]
    public class UserServiceTest
    {
        [Test]
        public void Login_Throws_User_Not_Found() {
            var loginRequest = new LoginRequest {
                Email = "test@abioka.com",
                Password = "1234"
            };
            var userService = UserServiceMock.Create();
            userService.UserSecurityRepositoryMock.Setup(us => us.GetByEmail(loginRequest.Email)).Returns((UserSecurity)null);
            var exception = Assert.Throws<DenialException>(() => userService.Login(loginRequest));

            Assert.AreEqual(exception.Message, "UserNotFound");
            Assert.AreEqual(exception.StatusCode, HttpStatusCode.NotFound);
        }

        [Test]
        public void Login_Throws_Wrong_Password() {
            var userSecurity = new UserSecurity {
                Email = "test@abioka.com",
                Password = "1234",
            };
            userSecurity.Password = userSecurity.GetHashedPassword(userSecurity.Password);

            var userService = UserServiceMock.Create();
            userService.UserSecurityRepositoryMock.Setup(us => us.GetByEmail(userSecurity.Email)).Returns(userSecurity);
            var exception = Assert.Throws<DenialException>(() => userService.Login(new LoginRequest {
                Email = userSecurity.Email,
                Password = "123"
            }));

            Assert.AreEqual(exception.Message, "WrongPassword");
            Assert.AreEqual(exception.StatusCode, HttpStatusCode.BadRequest);
        }

        [Test]
        public void Login_Throws_User_IsDeleted() {
            var password = "1234";
            var userSecurity = new UserSecurity {
                Email = "test@abioka.com",
                Password = password,
                IsDeleted = true
            };
            userSecurity.Password = userSecurity.GetHashedPassword(userSecurity.Password);

            var userService = UserServiceMock.Create();
            userService.UserSecurityRepositoryMock.Setup(us => us.GetByEmail(userSecurity.Email)).Returns(userSecurity);
            var exception = Assert.Throws<DenialException>(() => userService.Login(new LoginRequest {
                Email = userSecurity.Email,
                Password = password
            }));

            Assert.AreEqual(exception.Message, "UserIsNotActive");
            Assert.AreEqual(exception.StatusCode, HttpStatusCode.BadRequest);
        }

        [Test]
        public void Login_Set_Token() {
            var password = "1234";
            var userSecurity = new UserSecurity {
                Id = Guid.NewGuid(),
                Email = "test@abioka.com",
                Password = password
            };
            userSecurity.Password = userSecurity.GetHashedPassword(userSecurity.Password);

            var userService = UserServiceMock.Create();
            userService.UserSecurityRepositoryMock.Setup(us => us.GetByEmail(userSecurity.Email)).Returns(userSecurity);
            var token = Guid.NewGuid().ToString();
            userService.AbiokaTokenMock.Setup(t => t.Encode(It.Is<UserClaim>(uc => uc.Email == userSecurity.Email && uc.Id == userSecurity.Id))).Returns(token);
            userService.Login(new LoginRequest {
                Email = userSecurity.Email,
                Password = password
            });

            Assert.AreEqual(userSecurity.Token, token);
            userService.UserSecurityRepositoryMock.Verify(us => us.Update(userSecurity), Times.Once());
        }
        /*
        [Test]
        public void Add_Throws_User_Is_Already_Registered() {
            var addUserRequest = new AddUserRequest {
                Email = "test@abioka.com",
                Password = "1234"
            };

            var userService = UserServiceMock.Create();
            userService.UserSecurityRepositoryMock.Setup(us => us.GetByEmail(addUserRequest.Email)).Returns(new UserSecurity());
            var exception = Assert.Throws<DenialException>(() => userService.Add(addUserRequest));

            Assert.AreEqual(exception.Message, "UserIsAlreadyRegistered");
            Assert.AreEqual(exception.StatusCode, HttpStatusCode.BadRequest);
        }
        */

        [Test]
        public void Add_Calls_Repository_Add_Method_And_Returns_User() {
            var addUserRequest = new AddUserRequest {
                Email = "test@abioka.com",
                Password = "1234"
            };

            var userService = UserServiceMock.Create();
            userService.UserSecurityRepositoryMock.Setup(us => us.GetByEmail(addUserRequest.Email)).Returns((UserSecurity)null);
            var user = userService.Add(addUserRequest);
            
            var password = new UserSecurity { Email = addUserRequest.Email }.GetHashedPassword(addUserRequest.Password);

            userService.UserSecurityRepositoryMock.Verify(us => us.Add(It.Is<UserSecurity>(e => e.Email == addUserRequest.Email && e.AuthProvider == AuthProvider.Local && e.Password == password)), Times.Once());
            Assert.AreEqual(user.Email, addUserRequest.Email);
        }
    }
}
