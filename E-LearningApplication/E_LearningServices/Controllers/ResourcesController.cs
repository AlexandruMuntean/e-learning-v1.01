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
    public class ResourcesController : ApiController {
        #region PrivateFields

        private IResourcesManagement _resourcesManagement = new ResourcesManagement();
        private ICourseManagement _courseManagement = new CourseManagement();
        private static readonly string ElUpToMe = "0B3-zDDotAEInbUs3bTJTV3p1Um8";

        #endregion

        #region CourseResources - CRUD

        [HttpGet]
        public HttpResponseMessage GetAllCourseResources(int id) {
            try {
                List<Resources> resources = this._resourcesManagement.GetCourseResources(id);
                if (resources != null) {
                    //create the dto list to be returned to the api consumer
                    List<ResourcesDTO> dtoList = new List<ResourcesDTO>();
                    foreach (var r in resources) {
                        dtoList.Add(new ResourcesDTO {
                            CourseId = r.CourseId,
                            FileId = r.FileId,
                            FileName = r.FileName,
                            ModuleID = r.ModuleID,
                            ResourceId = r.ResourceId,
                            ResourceType = r.ResourceType
                        });
                    }
                    return Request.CreateResponse<List<ResourcesDTO>>(HttpStatusCode.OK, dtoList);
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
        public HttpResponseMessage GetCourseResourceById(int id) {
            try {
                Resources resource = this._resourcesManagement.GetResourceById(id);

                if (resource != null) {
                    ResourcesDTO dto = new ResourcesDTO {
                        CourseId = resource.CourseId,
                        FileId = resource.FileId,
                        FileName = resource.FileName,
                        ModuleID = resource.ModuleID,
                        ResourceId = resource.ResourceId,
                        ResourceType = resource.ResourceType
                    };

                    return Request.CreateResponse<ResourcesDTO>(HttpStatusCode.OK, dto);
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
        public HttpResponseMessage AddCourseToResources(CoursesDTO dto) {
            try {
                //adding a course to drive
                //var result = this._resourcesManagement.CreateDirectory(course.CourseName, "", ElUpToMe);
                Resources resources = new Resources {
                    ResourceType = "directory",
                    FileLocation = "",
                    FileId = ElUpToMe, //result.Id,
                    FileName = dto.CourseName, //result.Title,
                    CourseId = _courseManagement.GetCourseIdByCode(dto.CourdeCode),
                    ModuleID = -1
                };
                _resourcesManagement.SaveResourcesToDb(resources);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteCourseFromResources(int id) {
            try {
                //delete course resources
                this._resourcesManagement.DeleteCourse(id);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteCourseResource(int id) {
            try {
                //delete the resource
                this._resourcesManagement.DeleteCourseResource(id);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteModuleResource(int id) {
            try {
                //delete the resource
                this._resourcesManagement.DeleteModuleResource(id);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpPost]
        public HttpResponseMessage UploadResourcesForCourses(int id, FileDTO dto) {
            try {
                //var directoryFather = _resourcesManagement.GetFileIdForADirectory(id);
                //var result = this._resourcesManagement.UploadFile(dto.fileName, directoryFather);
                Resources resources = new Resources {
                    ResourceType = "file",
                    FileLocation = "",
                    FileId = dto.parentId.ToString(), //result.Id,
                    FileName = dto.fileName,
                    CourseId = dto.parentId,
                    ModuleID = -1
                };
                this._resourcesManagement.SaveResourcesToDb(resources);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateResourcesForCourse(int id, FileDTO dto) {
            try {
                //delete old resource
                //var directoryFather = _resourcesManagement.GetFileIdForADirectory(id);
                this._resourcesManagement.DeleteCourseResource(id);
                //add new resource
                //var result = this._resourcesManagement.UploadFile(dto.fileName, directoryFather);
                Resources resources = new Resources {
                    ResourceType = "file",
                    FileLocation = "",
                    FileId = dto.parentId.ToString(), //result.Id,
                    FileName = dto.fileName, //result.Title,
                    CourseId = dto.parentId,
                    ModuleID = -1
                };
                this._resourcesManagement.SaveResourcesToDb(resources);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        #endregion

        #region ModuleResources - CRUD

        [HttpGet]
        public HttpResponseMessage GetAllModuleResources(int id) {
            try {
                List<Resources> resources = this._resourcesManagement.GetModuleResources(id);
                if (resources != null) {
                    //create the dto list to be returned to the api consumer
                    List<ResourcesDTO> dtoList = new List<ResourcesDTO>();
                    foreach (var r in resources) {
                        dtoList.Add(new ResourcesDTO {
                            CourseId = r.CourseId,
                            FileId = r.FileId,
                            FileName = r.FileName,
                            ModuleID = r.ModuleID,
                            ResourceId = r.ResourceId,
                            ResourceType = r.ResourceType
                        });
                    }
                    return Request.CreateResponse<List<ResourcesDTO>>(HttpStatusCode.OK, dtoList);
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
        public HttpResponseMessage AddModuleToResources(CourseModuleDTO dto) {
            try {
                //add the associated module resource to db
                //var directoryFather = _resourcesManagement.GetFileIdForADirectory(module.CourseId);
                //var result = this._resourcesManagement.CreateDirectory(module.ModuleName, "", directoryFather);
                Resources resources = new Resources {
                    ResourceType = "module",
                    FileLocation = "",
                    FileId = dto.CourseId.ToString(), //result.Id,
                    FileName = dto.ModuleName, //result.Title,
                    CourseId = dto.CourseId ?? default(int),
                    ModuleID = _resourcesManagement.GetModuleIdByNameAndCourseId(dto.ModuleName, dto.CourseId)
                };
                this._resourcesManagement.SaveResourcesToDb(resources);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteModuleFromResources(int id) {
            try {
                //delete the associated resources
                this._resourcesManagement.DeleteModule(id);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpPost]
        public HttpResponseMessage UploadResourcesForModule(int id, FileDTO dto) {
            try {
                //var directoryFather = _resourcesManagement.GetFileIdForADirectory(dto.parentId);
                //var result = this._resourcesManagement.UploadFile(dto.fileName, directoryFather);
                Resources resources = new Resources {
                    ResourceType = "file",
                    FileLocation = "",
                    FileId = dto.parentId.ToString(), //result.Id,
                    FileName = dto.fileName, //result.Title,
                    CourseId = dto.rootId,
                    ModuleID = dto.parentId
                };
                this._resourcesManagement.SaveResourcesToDb(resources);

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception) {
                // Log exception code goes here  
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occured while executing method.");
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateResourcesForModule(int id, FileDTO dto) {
            try {
                //delete old resource
                //var directoryFather = _resourcesManagement.GetFileIdForADirectory(dto.parentId);
                this._resourcesManagement.DeleteModuleResource(id);
                //add new resource
                //var result = this._resourcesManagement.UploadFile(dto.fileName, directoryFather);
                Resources resources = new Resources {
                    ResourceType = "file",
                    FileLocation = "",
                    FileId = dto.parentId.ToString(), //result.Id,
                    FileName = dto.fileName, //result.Title,
                    CourseId = dto.rootId, // sau -1? mai vedem
                    ModuleID = dto.parentId
                };
                this._resourcesManagement.SaveResourcesToDb(resources);

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
