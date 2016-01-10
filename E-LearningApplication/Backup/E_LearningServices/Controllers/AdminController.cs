using E_LearningServices.CustomExceptions;
using E_LearningServices.Models;
using E_LearningServices.Services;
using E_LearningServices.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace E_LearningServices.Controllers {
    public class AdminController : ApiController {
        #region PrivateFields

        private IAdminManagement _adminManagement = new AdminManagement();

        #endregion

        [HttpGet]
        public HttpResponseMessage GetAllLogs() {
            try {
                List<Logs> dtoList = this._adminManagement.GetAllLogs();
                if (dtoList != null) {
                    return Request.CreateResponse<List<Logs>>(HttpStatusCode.OK, dtoList);
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
        public HttpResponseMessage GetLogsByEventDate(DateTime eventDatetime) {
            try {
                List<Logs> dtoList = this._adminManagement.GetLogByDatetime(eventDatetime);
                if (dtoList != null) {
                    return Request.CreateResponse<List<Logs>>(HttpStatusCode.OK, dtoList);
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

        public HttpResponseMessage GetLogsByEventLevel(string eventLevel) {
            try {
                List<Logs> dtoList = this._adminManagement.GetLogByEventLevel(eventLevel);
                if (dtoList != null) {
                    return Request.CreateResponse<List<Logs>>(HttpStatusCode.OK, dtoList);
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
        public HttpResponseMessage GetLogsByEventInfo(string eventInfo) {
            try {
                List<Logs> dtoList = this._adminManagement.GetLogByEventInfo(eventInfo);
                if (dtoList != null) {
                    return Request.CreateResponse<List<Logs>>(HttpStatusCode.OK, dtoList);
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
        public HttpResponseMessage GetLogById(int id) {
            try {
                Logs dto = this._adminManagement.GetLogById(id);
                if (dto != null) {
                    return Request.CreateResponse<Logs>(HttpStatusCode.OK, dto);
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

        [HttpDelete]
        public HttpResponseMessage DeleteLogById(int id) {
            try {
                this._adminManagement.DeleteLog(id);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteAllDisplayedLogs() {
            try {
                this._adminManagement.DeleteAllDisplayedLogs();

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }
    }
}
