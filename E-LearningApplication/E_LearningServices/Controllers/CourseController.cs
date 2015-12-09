using E_LearningServices.CustomExceptions;
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
    public class CourseController : ApiController {
        #region PrivateFields

        private ICourseManagement _courseManagement = new CourseManagement();

        #endregion

        #region Courses - CRUD

        [HttpGet]
        public HttpResponseMessage GetAllCourses() {
            try {
                List<Courses> courses = this._courseManagement.GetAllCourses();
                if (courses != null) {
                    //create the dto list to be returned to the api consumer
                    List<CoursesDTO> dtoList = new List<CoursesDTO>();
                    foreach (var course in courses) {
                        dtoList.Add(new CoursesDTO {
                            CourdeCode = course.CourdeCode,
                            CourseId = course.CourseId,
                            CourseName = course.CourseName,
                            NumberOfCredits = course.NumberOfCredits,
                            OwnerId = course.OwnerId,
                            SyllabusId = course.SyllabusId
                        });
                    }

                    return Request.CreateResponse<List<CoursesDTO>>(HttpStatusCode.OK, dtoList);
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
        public HttpResponseMessage GetAllOwnedCourses(int id) {
            try {
                List<Courses> courses = this._courseManagement.GetAllOwnedCourses(id);
                if (courses != null) {
                    //create the dto list to be returned to the api consumer
                    List<CoursesDTO> dtoList = new List<CoursesDTO>();
                    foreach (var course in courses) {
                        dtoList.Add(new CoursesDTO {
                            CourdeCode = course.CourdeCode,
                            CourseId = course.CourseId,
                            CourseName = course.CourseName,
                            NumberOfCredits = course.NumberOfCredits,
                            OwnerId = course.OwnerId,
                            SyllabusId = course.SyllabusId
                        });
                    }

                    return Request.CreateResponse<List<CoursesDTO>>(HttpStatusCode.OK, dtoList);
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
        public HttpResponseMessage AddCourse(CoursesDTO dto) {
            try {
                Courses course = new Courses();
                course.CourdeCode = dto.CourdeCode;
                course.CourseName = dto.CourseName;
                course.NumberOfCredits = dto.NumberOfCredits;
                course.OwnerId = dto.OwnerId;
                course.SyllabusId = dto.SyllabusId;

                this._courseManagement.AddCourse(course);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpGet]
        public HttpResponseMessage GetCourseById(int id) {
            try {
                Courses course = this._courseManagement.GetCourseById(id);

                if (course != null) {
                    CoursesDTO dto = new CoursesDTO {
                        CourdeCode = course.CourdeCode,
                        CourseId = course.CourseId,
                        CourseName = course.CourseName,
                        NumberOfCredits = course.NumberOfCredits,
                        OwnerId = course.OwnerId,
                        SyllabusId = course.SyllabusId
                    };

                    return Request.CreateResponse<CoursesDTO>(HttpStatusCode.OK, dto);
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
        public HttpResponseMessage UpdateCourse(int id, CoursesDTO dto) {
            try {
                Courses course = new Courses();
                course.CourdeCode = dto.CourdeCode;
                course.CourseId = dto.CourseId;
                course.CourseName = dto.CourseName;
                course.NumberOfCredits = dto.NumberOfCredits;
                course.SyllabusId = dto.SyllabusId;
                course.OwnerId = dto.OwnerId;

                this._courseManagement.EditCourse(course);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteCourse(int id) {
            try {
                //delete course modules
                List<CourseModule> cmlist = this._courseManagement.GetAllModules(id);
                foreach (var cm in cmlist) {
                    this._courseManagement.DeleteModule(cm.ModuleId);
                }

                //delete course
                this._courseManagement.DeleteCourse(id);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        #endregion

        #region Modules - CRUD

        [HttpGet]
        public HttpResponseMessage GetAllModules(int courseId) {
            try {
                List<CourseModule> courseModules = this._courseManagement.GetAllModules(courseId);
                if (courseModules != null) {
                    //create the dto list to be returned to the api consumer
                    List<CourseModuleDTO> dtoList = new List<CourseModuleDTO>();
                    foreach (var module in courseModules) {
                        dtoList.Add(new CourseModuleDTO {
                            CourseId = module.CourseId,
                            GradeId = module.GradeId,
                            Moduledatetime = module.Moduledatetime,
                            ModuleId = module.ModuleId,
                            ModuleName = module.ModuleName,
                            PreviousModuleId = module.PreviousModuleId
                        });
                    }
                    return Request.CreateResponse<List<CourseModuleDTO>>(HttpStatusCode.OK, dtoList);
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
        public HttpResponseMessage GetCourseModuleById(int id) {
            try {
                CourseModule courseModule = this._courseManagement.GetCouseModuleById(id);
                if (courseModule != null) {
                    //create the dto to be returned to the api consumer
                    CourseModuleDTO dto = new CourseModuleDTO {
                        CourseId = courseModule.CourseId,
                        GradeId = courseModule.GradeId,
                        Moduledatetime = courseModule.Moduledatetime,
                        ModuleId = courseModule.ModuleId,
                        ModuleName = courseModule.ModuleName,
                        PreviousModuleId = courseModule.PreviousModuleId
                    };

                    return Request.CreateResponse<CourseModuleDTO>(HttpStatusCode.OK, dto);
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
        public HttpResponseMessage AddModule(CourseModuleDTO dto) {
            try {
                //add the module to db
                CourseModule module = new CourseModule();
                module.CourseId = dto.CourseId;
                module.Moduledatetime = dto.Moduledatetime;
                module.GradeId = dto.GradeId;
                module.ModuleName = dto.ModuleName;
                module.PreviousModuleId = dto.PreviousModuleId;
                this._courseManagement.AddModule(module);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateCourseModule(CourseModuleDTO dto) {
            try {
                CourseModule cm = new CourseModule {
                    CourseId = dto.CourseId,
                    GradeId = dto.GradeId,
                    Moduledatetime = dto.Moduledatetime,
                    ModuleId = dto.ModuleId,
                    ModuleName = dto.ModuleName,
                    PreviousModuleId = dto.PreviousModuleId
                };
                this._courseManagement.UpdateCourseModule(cm);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteModule(int id) {
            try {
                //delete the module
                this._courseManagement.DeleteModule(id);

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
