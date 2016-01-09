using E_LearningServices.Models;
using E_LearningServices.Models.DTOs;
using E_LearningServices.Services;
using E_LearningServices.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace E_LearningServices.Controllers {
    public class GroupsController : ApiController {
        #region PrivateFields

        private IGroupsManagement _groupsManagement = new GroupsManagement();

        #endregion

        #region Groups - CRUD

        [HttpGet]
        public HttpResponseMessage GetAllUnassociatedGroups(int id) {
            try {
                List<Groups> groups = this._groupsManagement.GetAllUnassociatedGroups(id);
                //create the dto list to be returned to the api consumer
                List<GroupDTO> dtoList = new List<GroupDTO>();
                if (groups != null) {
                    foreach (var g in groups) {
                        dtoList.Add(new GroupDTO {
                            GroupId = g.GroupId,
                            GroupName = g.GroupName,
                            GroupType = g.GroupType,
                            GroupDescription = g.GroupDescription,
                            OwnerId = g.OwnerId
                        });
                    }
                }
                return Request.CreateResponse<List<GroupDTO>>(HttpStatusCode.OK, dtoList);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpGet]
        public HttpResponseMessage GetAssociatedGroups(int userId) {
            try {
                List<Groups> groups = this._groupsManagement.GetAssociatedGroups(userId);
                //create the dto list to be returned to the api consumer
                List<GroupDTO> dtoList = new List<GroupDTO>();

                if (groups != null) {
                    foreach (var g in groups) {
                        dtoList.Add(new GroupDTO {
                            GroupId = g.GroupId,
                            GroupName = g.GroupName,
                            GroupType = g.GroupType,
                            GroupDescription = g.GroupDescription,
                            OwnerId = g.OwnerId
                        });
                    }
                }
                return Request.CreateResponse<List<GroupDTO>>(HttpStatusCode.OK, dtoList);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpGet]
        public HttpResponseMessage GetGroupById(int id) {
            try {
                Groups group = this._groupsManagement.GetGroupById(id);

                if (group != null) {
                    //create the dto list to be returned to the api consumer
                    GroupDTO dto = new GroupDTO {
                        GroupDescription = group.GroupDescription,
                        GroupId = group.GroupId,
                        GroupName = group.GroupName,
                        GroupType = group.GroupType,
                        OwnerId = group.OwnerId
                    };

                    return Request.CreateResponse<GroupDTO>(HttpStatusCode.OK, dto);
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
        public HttpResponseMessage GetGroupByNameOrDescription(string searchTerm, int userId) {
            try {
                List<Groups> groups = this._groupsManagement.GetGroupByNameOrDescription(searchTerm, userId);
                //create the dto list to be returned to the api consumer
                List<GroupDTO> dtoList = new List<GroupDTO>();

                if (groups != null) {
                    foreach (var g in groups) {
                        dtoList.Add(new GroupDTO {
                            GroupDescription = g.GroupDescription,
                            GroupId = g.GroupId,
                            GroupName = g.GroupName,
                            GroupType = g.GroupType,
                            OwnerId = g.OwnerId
                        });
                    }
                }
                return Request.CreateResponse<List<GroupDTO>>(HttpStatusCode.OK, dtoList);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpGet]
        public HttpResponseMessage GetAssociatedGroupByNameOrDescription(string searchTerm, int userId) {
            try {
                List<Groups> groups = this._groupsManagement.GetAssociatedGroupByNameOrDescription(searchTerm, userId);
                //create the dto list to be returned to the api consumer
                List<GroupDTO> dtoList = new List<GroupDTO>();

                if (groups != null) {
                    foreach (var g in groups) {
                        dtoList.Add(new GroupDTO {
                            GroupDescription = g.GroupDescription,
                            GroupId = g.GroupId,
                            GroupName = g.GroupName,
                            GroupType = g.GroupType,
                            OwnerId = g.OwnerId
                        });
                    }
                }
                return Request.CreateResponse<List<GroupDTO>>(HttpStatusCode.OK, dtoList);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpPost]
        public HttpResponseMessage AddGroup(GroupDTO dto) {
            try {
                Groups group = new Groups {
                    GroupDescription = dto.GroupDescription,
                    GroupName = dto.GroupName,
                    GroupType = dto.GroupType,
                    OwnerId = dto.OwnerId
                };

                Groups g = this._groupsManagement.AddGroup(group);
                GroupDTO dto1 = new GroupDTO {
                    GroupDescription = g.GroupDescription,
                    GroupId = g.GroupId,
                    GroupName = g.GroupName,
                    GroupType = g.GroupType,
                    OwnerId = g.OwnerId
                };

                return Request.CreateResponse(HttpStatusCode.OK, dto1);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateGroup(int id, GroupDTO dto) {
            try {
                Groups group = new Groups {
                    GroupDescription = dto.GroupDescription,
                    GroupId = dto.GroupId,
                    GroupName = dto.GroupName,
                    GroupType = dto.GroupType,
                    OwnerId = dto.OwnerId
                };

                this._groupsManagement.EditGroup(group);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteGroup(int id) {
            try {
                this._groupsManagement.DeleteGroup(id);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        #endregion

        #region GroupMembers - CRUD

        [HttpGet]
        public HttpResponseMessage GetAllGroupMembers(int groupId) {
            try {
                List<Users> members = this._groupsManagement.GetAllGroupMembers(groupId);
                if (members != null) {
                    //create the dto list to be returned to the api consumer
                    List<UsersDTO> dtoList = new List<UsersDTO>();
                    foreach (var m in members) {
                        dtoList.Add(new UsersDTO {
                            AccessStatus = m.AccessStatus,
                            Email = m.Email,
                            FirstName = m.FirstName,
                            LastName = m.LastName,
                            MiddleName = m.MiddleName,
                            StudentIdentificationNumber = m.StudentIdentificationNumber,
                            UserId = m.UserId,
                            UserName = m.UserName
                        });
                    }

                    return Request.CreateResponse<List<UsersDTO>>(HttpStatusCode.OK, dtoList);
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

        [HttpPost]
        public HttpResponseMessage AddGroupMember(GroupMemberDTO dto) {
            try {
                GroupMembers groupMember = new GroupMembers {
                    GroupId = dto.GroupId,
                    GroupMemberId = dto.GroupMemberId,
                    MemberId = dto.MemberId
                };

                this._groupsManagement.AddGroupMember(groupMember);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpDelete]
        public HttpResponseMessage RemoveGroupMember(int groupId, int userId) {
            try {
                this._groupsManagement.DeleteGroupMember(groupId, userId);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        #endregion
    }
}
