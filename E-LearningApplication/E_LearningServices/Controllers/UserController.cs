using E_LearningServices.CustomExceptions;
using E_LearningServices.Models;
using E_LearningServices.Services;
using E_LearningServices.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;

namespace E_LearningServices.Controllers {
    public class UserController : ApiController {
        #region PrivateFields

        private IUserManagement _userManagement = new UserManagement();

        #endregion

        [HttpGet]
        public HttpResponseMessage GetAllUsers() {
            try {
                List<Users> users = this._userManagement.GetAllUsers();
                List<UsersDTO> dtoList = new List<UsersDTO>();
                if (users != null) {
                    foreach (var user in users) {
                        dtoList.Add(new UsersDTO {
                            UserId = user.UserId,
                            AccessStatus = user.AccessStatus,
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            MiddleName = user.MiddleName,
                            StudentIdentificationNumber = user.StudentIdentificationNumber,
                            UserName = user.UserName
                        });
                    }
                }
                return Request.CreateResponse<List<UsersDTO>>(HttpStatusCode.OK, dtoList);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpGet]
        public HttpResponseMessage GetAllSubscribedUsers(int courseId) {
            try {
                List<Users> users = this._userManagement.GetAllSubscribedUsers(courseId);
                List<UsersDTO> dtoList = new List<UsersDTO>();
                if (users != null) {
                    foreach (var user in users) {
                        dtoList.Add(new UsersDTO {
                            UserId = user.UserId,
                            AccessStatus = user.AccessStatus,
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            MiddleName = user.MiddleName,
                            StudentIdentificationNumber = user.StudentIdentificationNumber,
                            UserName = user.UserName
                        });
                    }
                }
                return Request.CreateResponse<List<UsersDTO>>(HttpStatusCode.OK, dtoList);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpGet]
        public HttpResponseMessage GetUserById(int id) {
            try {
                Users user = this._userManagement.GetUserById(id);
                if (user != null) {
                    UsersDTO dto = new UsersDTO {
                        UserId = user.UserId,
                        AccessStatus = user.AccessStatus,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        MiddleName = user.MiddleName,
                        StudentIdentificationNumber = user.StudentIdentificationNumber,
                        UserName = user.UserName
                    };

                    return Request.CreateResponse<UsersDTO>(HttpStatusCode.OK, dto);
                }
                else {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Resource Not Found");
                }
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpGet]
        public HttpResponseMessage GetUserByUserName(string username) {
            try {
                Users user = this._userManagement.GetUserByUserName(username);
                UsersDTO dto = new UsersDTO();
                if (user != null) {
                    dto.UserId = user.UserId;
                    dto.AccessStatus = user.AccessStatus;
                    dto.Email = user.Email;
                    dto.FirstName = user.FirstName;
                    dto.LastName = user.LastName;
                    dto.MiddleName = user.MiddleName;
                    dto.StudentIdentificationNumber = user.StudentIdentificationNumber;
                    dto.UserName = user.UserName;
                }
                return Request.CreateResponse<UsersDTO>(HttpStatusCode.OK, dto);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpGet]
        public HttpResponseMessage GetUserByLastName(string lastName) {
            try {
                List<Users> users = this._userManagement.GetUserByLastName(lastName);
                List<UsersDTO> dtoList = new List<UsersDTO>();
                if (users != null) {
                    foreach (var user in users) {
                        dtoList.Add(new UsersDTO {
                            UserId = user.UserId,
                            AccessStatus = user.AccessStatus,
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            MiddleName = user.MiddleName,
                            StudentIdentificationNumber = user.StudentIdentificationNumber,
                            UserName = user.UserName
                        });
                    }
                }
                return Request.CreateResponse<List<UsersDTO>>(HttpStatusCode.OK, dtoList);

            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateUser(int id, UsersDTO dto) {
            try {
                Users user = new Users();
                user.AccessStatus = dto.AccessStatus;
                user.Email = dto.Email;
                user.FirstName = dto.FirstName;
                user.LastName = dto.LastName;
                user.MiddleName = dto.MiddleName;
                user.StudentIdentificationNumber = dto.StudentIdentificationNumber;
                user.UserId = id;
                user.UserName = dto.UserName;

                this._userManagement.UpdateUser(user);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }
    }
}
