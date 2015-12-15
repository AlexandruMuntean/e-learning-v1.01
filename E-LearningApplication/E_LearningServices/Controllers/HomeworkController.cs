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
    public class HomeworkController : ApiController {
        #region PrivateFields

        private IHomeworkManagement _homeworkManagement = new HomeworkManagement();

        #endregion

        #region Homework CRUD - Prof functionalities

        [HttpGet]
        public HttpResponseMessage GetHomeworkById(int id) {
            try {
                Homeworks hw = this._homeworkManagement.GetHomeworkById(id);

                if (hw != null) {
                    HomeworkDTO dto = new HomeworkDTO();
                    dto.HomeworkAccessSpan = hw.HomeworkAccessSpan;
                    dto.HomeworkDeadline = hw.HomeworkDeadline;
                    dto.HomeworkDescription = hw.HomeworkDescription;
                    dto.HomeworkId = hw.HomeworkId;
                    dto.HomeworkName = hw.HomeworkName;
                    dto.HomeworkPoints = hw.HomeworkPoints;
                    dto.HomeworkSubmissionType = hw.HomeworkSubmissionType;
                    dto.HomeworkType = hw.HomeworkType;
                    dto.CourseId = hw.CourseId;
                    dto.CourseModuleId = hw.CourseModuleId;
                    dto.OwnerId = hw.OwnerId;

                    return Request.CreateResponse<HomeworkDTO>(HttpStatusCode.OK, dto);
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
        public HttpResponseMessage GetAnswerForAssignement(int id) {
            try {
                Answers answer = this._homeworkManagement.GetAnswerForAssignement(id);

                if (answer != null) {
                    AnswerDTO dto1 = new AnswerDTO();
                    dto1.AnswerId = answer.AnswerId;
                    dto1.AnswerType = answer.AnswerType;
                    dto1.AnswerValue = answer.AnswerValue;

                    return Request.CreateResponse<AnswerDTO>(HttpStatusCode.OK, dto1);
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
        public HttpResponseMessage GetAllCourseHomework(int id) {
            try {
                List<Homeworks> homework = this._homeworkManagement.GetAllCourseHomework(id);
                if (homework != null) {
                    //create the dto list to be returned to the api consumer
                    List<HomeworkDTO> dtoList = new List<HomeworkDTO>();
                    foreach (var h in homework) {
                        dtoList.Add(new HomeworkDTO {
                            HomeworkAccessSpan = h.HomeworkAccessSpan,
                            HomeworkDeadline = h.HomeworkDeadline,
                            HomeworkDescription = h.HomeworkDescription,
                            HomeworkId = h.HomeworkId,
                            HomeworkName = h.HomeworkName,
                            HomeworkPoints = h.HomeworkPoints,
                            HomeworkSubmissionType = h.HomeworkSubmissionType,
                            HomeworkType = h.HomeworkType,
                            CourseId = h.CourseId,
                            CourseModuleId = h.CourseModuleId,
                            OwnerId = h.OwnerId
                        });
                    }

                    return Request.CreateResponse<List<HomeworkDTO>>(HttpStatusCode.OK, dtoList);
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
        public HttpResponseMessage GetCourseAssignements(int id, int courseId) {
            try {
                List<HomeworkAssignements> homework = this._homeworkManagement.GetCourseAssignements(id, courseId);
                if (homework != null) {
                    //create the dto list to be returned to the api consumer
                    List<HomeworkAssignementDTO> dtoList = new List<HomeworkAssignementDTO>();
                    foreach (var h in homework) {
                        var recipient = this._homeworkManagement.GetRecipientName(h.StudentId, h.GroupId);
                        dtoList.Add(new HomeworkAssignementDTO {
                            AnswerId = h.AnswerId,
                            AssignementId = h.AssignementId,
                            CourseId = h.CourseId,
                            CourseModuleId = h.CourseModuleId,
                            GradeId = h.GradeId,
                            GroupId = h.GroupId,
                            HomeworkId = h.HomeworkId,
                            StudentId = h.StudentId,
                            RecipientName = recipient
                        });
                    }

                    return Request.CreateResponse<List<HomeworkAssignementDTO>>(HttpStatusCode.OK, dtoList);
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
        public HttpResponseMessage GetCourseModuleAssignements(int id, int courseModuleId) {
            try {
                List<HomeworkAssignements> homework = this._homeworkManagement.GetCourseModuleAssignements(id, courseModuleId);
                if (homework != null) {
                    //create the dto list to be returned to the api consumer
                    List<HomeworkAssignementDTO> dtoList = new List<HomeworkAssignementDTO>();
                    foreach (var h in homework) {
                        var recipient = this._homeworkManagement.GetRecipientName(h.StudentId, h.GroupId);
                        dtoList.Add(new HomeworkAssignementDTO {
                            AnswerId = h.AnswerId,
                            AssignementId = h.AssignementId,
                            CourseId = h.CourseId,
                            CourseModuleId = h.CourseModuleId,
                            GradeId = h.GradeId,
                            GroupId = h.GroupId,
                            HomeworkId = h.HomeworkId,
                            StudentId = h.StudentId,
                            RecipientName = recipient
                        });
                    }

                    return Request.CreateResponse<List<HomeworkAssignementDTO>>(HttpStatusCode.OK, dtoList);
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
        public HttpResponseMessage GetAllCourseModuleHomework(int id) {
            try {
                List<Homeworks> homework = this._homeworkManagement.GetAllCourseModuleHomework(id);
                if (homework != null) {
                    //create the dto list to be returned to the api consumer
                    List<HomeworkDTO> dtoList = new List<HomeworkDTO>();
                    foreach (var h in homework) {
                        dtoList.Add(new HomeworkDTO {
                            HomeworkAccessSpan = h.HomeworkAccessSpan,
                            HomeworkDeadline = h.HomeworkDeadline,
                            HomeworkDescription = h.HomeworkDescription,
                            HomeworkId = h.HomeworkId,
                            HomeworkName = h.HomeworkName,
                            HomeworkPoints = h.HomeworkPoints,
                            HomeworkSubmissionType = h.HomeworkSubmissionType,
                            HomeworkType = h.HomeworkType,
                            CourseId = h.CourseId,
                            CourseModuleId = h.CourseModuleId,
                            OwnerId = h.OwnerId
                        });
                    }

                    return Request.CreateResponse<List<HomeworkDTO>>(HttpStatusCode.OK, dtoList);
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
        public HttpResponseMessage AddHomework(HomeworkDTO dto) {
            try {
                Homeworks hw = new Homeworks();
                hw.HomeworkAccessSpan = dto.HomeworkAccessSpan;
                hw.HomeworkDeadline = dto.HomeworkDeadline;
                hw.HomeworkDescription = dto.HomeworkDescription;
                hw.HomeworkId = dto.HomeworkId;
                hw.HomeworkName = dto.HomeworkName;
                hw.HomeworkPoints = dto.HomeworkPoints;
                hw.HomeworkSubmissionType = dto.HomeworkSubmissionType;
                hw.HomeworkType = dto.HomeworkType;
                hw.CourseId = dto.CourseId;
                hw.CourseModuleId = dto.CourseModuleId;
                hw.OwnerId = dto.OwnerId;

                this._homeworkManagement.AddHomework(hw);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteHomework(int id) {
            try {
                this._homeworkManagement.DeleteHomework(id);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateHomework(int id, HomeworkDTO dto) {
            try {
                Homeworks hw = new Homeworks();
                hw.HomeworkAccessSpan = dto.HomeworkAccessSpan;
                hw.HomeworkDeadline = dto.HomeworkDeadline;
                hw.HomeworkDescription = dto.HomeworkDescription;
                hw.HomeworkId = dto.HomeworkId;
                hw.HomeworkName = dto.HomeworkName;
                hw.HomeworkPoints = dto.HomeworkPoints;
                hw.HomeworkSubmissionType = dto.HomeworkSubmissionType;
                hw.HomeworkType = dto.HomeworkType;
                hw.CourseId = dto.CourseId;
                hw.CourseModuleId = dto.CourseModuleId;
                hw.OwnerId = dto.OwnerId;

                this._homeworkManagement.EditHomework(hw);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpPut]
        public HttpResponseMessage AssignHomework(int id, HomeworkAssignementDTO dto) {
            try {
                this._homeworkManagement.AssignHomework(dto);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpPut]
        public HttpResponseMessage UnassignHomework(int id, HomeworkAssignementDTO dto) {
            try {
                this._homeworkManagement.UnassignHomework(dto);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        #endregion

        #region Homework CRUD - Student functionalities

        [HttpGet]
        public HttpResponseMessage GetAllStudentAssignedHomework(int id) {
            try {
                //create the dto list to be returned to the api consumer
                List<AssignementDTO> dtoList = this._homeworkManagement.GetAllStudentAssignedHomework(id);
                if (dtoList != null) {
                    return Request.CreateResponse<List<AssignementDTO>>(HttpStatusCode.OK, dtoList);
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
        public HttpResponseMessage GetAllGroupAssignedHomework(int id) {
            try {
                List<AssignementDTO> dtoList = this._homeworkManagement.GetAllGroupAssignedHomework(id);
                if (dtoList != null) {
                    return Request.CreateResponse<List<AssignementDTO>>(HttpStatusCode.OK, dtoList);
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

        [HttpPut]
        public HttpResponseMessage SubmitHomework(int assignementId, AnswerDTO dto) {
            try {
                Answers answer = new Answers();
                answer.AnswerId = dto.AnswerId;
                answer.AnswerType = dto.AnswerType;
                answer.AnswerValue = dto.AnswerValue;

                this._homeworkManagement.SubmitHomework(assignementId, answer);

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
