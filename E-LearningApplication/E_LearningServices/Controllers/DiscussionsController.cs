using E_LearningServices.Models;
using E_LearningServices.Models.DTOs;
using E_LearningServices.Services;
using E_LearningServices.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web.Http;

namespace E_LearningServices.Controllers {
    public class DiscussionsController : ApiController {
        #region PrivateFields

        private IForumManagement _forumManagement = new ForumManagement();

        #endregion

        #region Forums - CRUD

        [HttpGet]
        public HttpResponseMessage GetAllForums() {
            try {
                List<Forums> forums = this._forumManagement.GetAllForums();
                if (forums != null) {
                    //create the dto list to be returned to the api consumer
                    List<ForumDTO> dtoList = new List<ForumDTO>();
                    foreach (var forum in forums) {
                        dtoList.Add(new ForumDTO {
                            ForumId = forum.ForumId,
                            Category = forum.Category,
                            OwnerId = forum.OwnerId
                        });
                    }

                    return Request.CreateResponse<List<ForumDTO>>(HttpStatusCode.OK, dtoList);
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
        public HttpResponseMessage GetForumById(int id) {
            try {
                Forums forum = this._forumManagement.GetForumById(id);

                if (forum != null) {
                    ForumDTO dto = new ForumDTO { 
                        ForumId = forum.ForumId,
                        Category = forum.Category,
                        OwnerId = forum.OwnerId
                    };

                    return Request.CreateResponse<ForumDTO>(HttpStatusCode.OK, dto);
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
        public HttpResponseMessage AddForum(ForumDTO dto) {
            try {
                Forums forum = new Forums {
                    ForumId = dto.ForumId,
                    Category = dto.Category,
                    OwnerId = dto.OwnerId
                };

                this._forumManagement.AddForum(forum);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateForum(int id, ForumDTO dto) {
            try {
                Forums forum = new Forums { 
                    ForumId = dto.ForumId,
                    Category = dto.Category,
                    OwnerId = dto.OwnerId
                };
                
                this._forumManagement.EditCategory(forum);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteForum(int id) {
            try {
                //delete forum
                this._forumManagement.DeleteForum(id);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        #endregion

        #region Messages - CRUD

        [HttpGet]
        public HttpResponseMessage GetAllMessages(int forumId) {
            try {
                List<Messages> messages = this._forumManagement.GetAllMessages(forumId);
                if (messages != null) {
                    //create the dto list to be returned to the api consumer
                    List<MessageDTO> dtoList = new List<MessageDTO>();
                    foreach (var m in messages) {
                        MessageDTO dto = new MessageDTO();
                        dto.MessageId = m.MessageId;
                        dto.MessageContent = m.MessageContent;
                        dto.ConversationId = m.ConversationId;
                        dto.DiscusionId = m.DiscusionId;
                        dto.ForumId = m.ForumId;
                        dto.UserId = m.UserId;

                        DateTime messageData = m.MesageData ?? DateTime.Now;
                        dto.MesageData = messageData;

                        dtoList.Add(dto);
                    }

                    return Request.CreateResponse<List<MessageDTO>>(HttpStatusCode.OK, dtoList);
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
        public HttpResponseMessage GetMessageById(int id) {
            try {
                Messages message = this._forumManagement.GetMessageById(id);

                if (message != null) {
                    DateTime messageData = message.MesageData ?? DateTime.Now;

                    MessageDTO dto = new MessageDTO {
                        MessageId = message.MessageId,
                        MessageContent = message.MessageContent,
                        MesageData = messageData,
                        ConversationId = message.ConversationId,
                        DiscusionId = message.DiscusionId,
                        ForumId = message.ForumId,
                        UserId = message.UserId
                    };

                    return Request.CreateResponse<MessageDTO>(HttpStatusCode.OK, dto);
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
        public HttpResponseMessage AddMessage(MessageDTO dto) {
            try {
                Messages message = new Messages { 
                    MessageId = dto.MessageId,
                    MesageData = dto.MesageData,
                    MessageContent = dto.MessageContent,
                    ConversationId = dto.ConversationId,
                    DiscusionId = dto.DiscusionId,
                    ForumId = dto.ForumId,
                    UserId = dto.UserId
                };

                this._forumManagement.AddMessage(message);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateMessage(int id, MessageDTO dto) {
            try {
                Messages message = new Messages { 
                    MessageId = dto.MessageId,
                    MessageContent = dto.MessageContent,
                    MesageData = dto.MesageData,
                    ConversationId = dto.ConversationId,
                    DiscusionId = dto.DiscusionId,
                    ForumId = dto.ForumId,
                    UserId = dto.UserId
                };

                this._forumManagement.UpdateMessage(message);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteMessage(int id) {
            try {
                //delete message
                this._forumManagement.DeleteMessage(id);

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
