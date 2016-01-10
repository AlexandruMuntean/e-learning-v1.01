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
    public class GradesController : ApiController {
        #region PrivateFields

        private IGradesManagement _gradesManagement = new GradesManagement();

        #endregion

        #region Grads CRUD - Prof functionalities

        [HttpGet]
        public HttpResponseMessage GetAllCourseGivenGrades(int id) {
            try {
                List<ReceivedGradeDTO> dtoList = this._gradesManagement.GetAllCourseGivenGrades(id);

                return Request.CreateResponse<List<ReceivedGradeDTO>>(HttpStatusCode.OK, dtoList);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpGet]
        public HttpResponseMessage GetAllCourseModuleGivenGrades(int id) {
            try {
                List<ReceivedGradeDTO> dtoList = this._gradesManagement.GetAllCourseModuleGivenGrades(id);
                
                    return Request.CreateResponse<List<ReceivedGradeDTO>>(HttpStatusCode.OK, dtoList);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpPut]
        public HttpResponseMessage GradeHomework(int assignementId, HomeworkGradeDTO dto) {
            try {
                Grades grade = new Grades();
                grade.GradeId = dto.GradeId;
                grade.GradeValue = dto.GradeValue;
                grade.Gradedatetime = dto.Gradedatetime;

                this._gradesManagement.GradeHomework(grade, dto);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }
        
        #endregion

        #region Grads CRUD - Student functionalities

        [HttpGet]
        public HttpResponseMessage GetAllStudentReceivedGrades(int id) {
            try {
                List<ReceivedGradeDTO> dtoList = this._gradesManagement.GetAllStudentReceivedGrades(id);
                if (dtoList != null) {
                    return Request.CreateResponse<List<ReceivedGradeDTO>>(HttpStatusCode.OK, dtoList);
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
        public HttpResponseMessage GetAllGroupReceivedGrades(int id) {
            try {
                List<ReceivedGradeDTO> dtoList = this._gradesManagement.GetAllGroupReceivedGrades(id);
                if (dtoList != null) {
                    return Request.CreateResponse<List<ReceivedGradeDTO>>(HttpStatusCode.OK, dtoList);
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

        #endregion

    }
}
