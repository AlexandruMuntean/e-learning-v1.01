using E_LearningApplication.CustomExceptions;
using E_LearningApplication.Models;
using E_LearningApplication.Models.DTOs;
using E_LearningApplication.Models.ViewModels;
using E_LearningApplication.Utils.LoggingUtils;
using E_LearningApplication.Utils.MailUtil;
using E_LearningApplication.ViewModelFactories;
using E_LearningApplication.ViewModelFactories.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace E_LearningApplication.Controllers {
    [Authorize(Roles = "Prof")]
    public class ProfController : Controller {
        #region Private fields

        //private readonly string apiMethodsUrl = "https://elearningservices.azurewebsites.net/";
        private readonly string apiMethodsUrl = "http://localhost:42175/";
        private ILoggingUtil logger = new NLogLogger();
        private IViewModelFactory viewModelFactory = new ViewModelFactory();
        private IMailUtil mailUtil = new MailUtil();

        #endregion

        //
        // GET: /Prof/

        public ActionResult Index() {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                //get all courses owned by the currently logged in user
                List<Courses> courses = new List<Courses>();

                var _userId = Session["UserId"];
                var _sessionUser = Convert.ToInt32(_userId);

                #region get owned courses
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/course/GetAllOwnedCourses/?id=" + _sessionUser).Result;
                    if (response.IsSuccessStatusCode) {
                        IEnumerable<Courses> list = response.Content.ReadAsAsync<IEnumerable<Courses>>().Result;
                        if (list != null) {
                            courses = list.ToList();
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }
                #endregion

                List<CoursesViewModel> cvm = this.viewModelFactory.GetViewModel(courses);
                return PartialView("_Index", cvm);
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.";
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.";
                return View("Error");
            }
        }

        #region Courses-CRUD

        //
        // GET: /Prof/DisplayAllCourses()

        public ActionResult DisplayAllCourses() {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                List<Courses> courses = new List<Courses>();
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/course/GetAllCourses").Result;
                    if (response.IsSuccessStatusCode) {
                        IEnumerable<Courses> list = response.Content.ReadAsAsync<IEnumerable<Courses>>().Result;
                        if (list != null) {
                            courses = list.ToList();
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                List<CoursesViewModel> cvm = this.viewModelFactory.GetViewModel(courses);
                return View(cvm);
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.";
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.";
                return View("Error");
            }
        }

        //
        // GET: /Prof/DisplayCourses

        public ActionResult DisplayCourses() {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                //get all courses owned by the currently logged in user
                List<Courses> courses = new List<Courses>();

                var _userId = Session["UserId"];
                var _sessionUser = Convert.ToInt32(_userId);

                #region get owned courses
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/course/GetAllOwnedCourses/?id=" + _sessionUser).Result;
                    if (response.IsSuccessStatusCode) {
                        IEnumerable<Courses> list = response.Content.ReadAsAsync<IEnumerable<Courses>>().Result;
                        if (list != null) {
                            courses = list.ToList();
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }
                #endregion

                List<CoursesViewModel> cvm = this.viewModelFactory.GetViewModel(courses);
                return View(cvm);
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.";
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.";
                return View("Error");
            }
        }

        //
        // GET: /Prof/CourseDetails(id = 0)

        public ActionResult CourseDetails(int id = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                Courses course = new Courses();
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/course/GetCourseById/?id=" + id).Result;
                    if (response.IsSuccessStatusCode) {
                        var c = response.Content.ReadAsAsync<Courses>().Result;
                        if (c != null) {
                            course.CourdeCode = c.CourdeCode;
                            course.CourseId = c.CourseId;
                            course.CourseName = c.CourseName;
                            course.NumberOfCredits = c.NumberOfCredits;
                            course.OwnerId = c.OwnerId;
                            course.SyllabusId = c.SyllabusId;
                        }
                        else {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                CoursesViewModel cvm = this.viewModelFactory.GetViewModel(course);
                return View(cvm);
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.";
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.";
                return View("Error");
            }
        }

        //
        // GET: /Prof/CreateCourse

        public ActionResult CreateCourse() {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            return View();
        }

        //
        // POST: /Prof/CreateCourse(courseViewModel)

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCourse(CoursesViewModel courseViewModel) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {

                //create the dto for the new course
                #region add new course

                var _userId = Session["UserId"];
                var _sessionUser = Convert.ToInt32(_userId);

                CoursesDTO dto = new CoursesDTO();
                dto.CourdeCode = courseViewModel.CourdeCode;
                dto.CourseName = courseViewModel.CourseName;
                dto.NumberOfCredits = courseViewModel.NumberOfCredits;
                dto.SyllabusId = courseViewModel.SyllabusId;
                dto.OwnerId = _sessionUser;

                //add new course
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    //add course to db
                    HttpResponseMessage responseFromDb = client.PostAsJsonAsync("api/course/AddCourse/?course=", dto).Result;
                    if (!responseFromDb.IsSuccessStatusCode) {
                        throw new CustomException("Could not complete the operation!");
                    }
                    //add course to drive
                    HttpResponseMessage responseFromResources = client.PostAsJsonAsync("api/resources/AddCourseToResources/?course=", dto).Result;
                    if (!responseFromResources.IsSuccessStatusCode) {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                #endregion

                return RedirectToAction("DisplayCourses");
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.";
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed!";
                return View("Error");
            }
        }

        //
        // GET: /Prof/EditCourse(id = 0)

        public ActionResult EditCourse(int id = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                //get the course to be edited
                Courses course = new Courses();
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/course/GetCourseById/?id=" + id).Result;
                    if (response.IsSuccessStatusCode) {
                        var c = response.Content.ReadAsAsync<Courses>().Result;
                        if (c != null) {
                            course.CourdeCode = c.CourdeCode;
                            course.CourseId = c.CourseId;
                            course.CourseName = c.CourseName;
                            course.NumberOfCredits = c.NumberOfCredits;
                            course.OwnerId = c.OwnerId;
                            course.SyllabusId = c.SyllabusId;
                        }
                        else {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                CoursesViewModel cvm = this.viewModelFactory.GetViewModel(course);
                return View(cvm);
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.";
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed!";
                return View("Error");
            }
        }

        //
        // POST: /Prof/EditCourse(course)

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCourse(CoursesViewModel courseViewModel) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                //create the course dto to be passed to the api services
                CoursesDTO dto = new CoursesDTO();
                dto.CourdeCode = courseViewModel.CourdeCode;
                dto.CourseId = courseViewModel.CourseId;
                dto.CourseName = courseViewModel.CourseName;
                dto.NumberOfCredits = courseViewModel.NumberOfCredits;
                dto.SyllabusId = courseViewModel.SyllabusId;
                dto.OwnerId = courseViewModel.OwnerId;

                //edit the course
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.PutAsJsonAsync("api/course/UpdateCourse/?id=" + courseViewModel.CourseId, dto).Result;
                    if (!response.IsSuccessStatusCode) {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                return RedirectToAction("DisplayCourses");
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.";
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed!";
                return View("Error");
            }
        }

        //
        // POST: /Prof/DeleteCourse(id = 0)

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCourse(int id = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                //delete the course
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    //delete from db
                    HttpResponseMessage responseFromDb = client.DeleteAsync("api/course/DeleteCourse/?id=" + id).Result;
                    if (!responseFromDb.IsSuccessStatusCode) {
                        throw new CustomException("Could not complete the operation!");
                    }
                    //delete from resources
                    HttpResponseMessage responseFromResources = client.DeleteAsync("api/resources/DeleteCourseFromResources/?id=" + id).Result;
                    if (!responseFromResources.IsSuccessStatusCode) {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                return RedirectToAction("DisplayCourses");
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.";
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed!";
                return View("Error");
            }
        }

        #endregion

        #region Course Resources - CRUD

        //
        // GET: /Prof/DisplayCourseResources(id = 0)

        [HttpGet]
        public ActionResult DisplayCourseResources(int id = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                //get the course resources
                List<Resources> resources = new List<Resources>();
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/resources/GetAllCourseResources/?id=" + id).Result;
                    if (response.IsSuccessStatusCode) {
                        IEnumerable<Resources> list = response.Content.ReadAsAsync<IEnumerable<Resources>>().Result;
                        if (list != null) {
                            resources = list.ToList();
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                List<ResourceViewModel> rvm = this.viewModelFactory.GetViewModel(resources);
                return View(rvm);
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.";
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed!";
                return View("Error");
            }
        }

        //
        // GET: /Prof/AddCourseResource(id = 0)

        public ActionResult AddCourseResource(int id = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);

            if (id > 0)
                return View(id);
            else
                return RedirectToAction("DisplayCourses");
        }

        //
        // POST: /Prof/UploadResourcesforCourse(file, id = 0)

        [HttpPost]
        public ActionResult UploadResourcesforCourse(HttpPostedFileBase file, int id = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );

                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/UploadedFiles"), fileName);
                    file.SaveAs(path);

                    FileDTO dto = new FileDTO { parentId = id, fileName = file.FileName , filePath = path};
                    HttpResponseMessage response =
                        client.PostAsJsonAsync("api/resources/UploadResourcesForCourses/?id=" + id, dto).Result;
                    if (!response.IsSuccessStatusCode) {
                        throw new CustomException("Could not complete the operation!");
                    }
                }
                return RedirectToAction("DisplayCourses");
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.";
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed!";
                return View("Error");
            }
        }

        //
        // POST: /Prof/DeleteCourseResource(id = 0)

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCourseResource(int id = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                //delete the course resource
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );

                    HttpResponseMessage response = client.DeleteAsync("api/resources/DeleteCourseResource/?id=" + id).Result;
                    if (!response.IsSuccessStatusCode) {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                return RedirectToAction("DisplayCourses");
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.";
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed!";
                return View("Error");
            }
        }

        private string GetUrlDownload(string FileId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("https://docs.google.com/uc?id=").Append(FileId).Append("&export=download");
            return sb.ToString();
        }

        public ActionResult DowloadCourseResource(int id = 0)
        {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try
            {
                //get the course resource
                Resources resource = new Resources();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/resources/GetCourseResourceById/?id=" + id).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        Resources r = response.Content.ReadAsAsync<Resources>().Result;
                        if (r != null)
                        {
                            resource.CourseId = r.CourseId;
                            resource.FileId = r.FileId;
                            resource.FileName = r.FileName;
                            resource.ModuleID = r.ModuleID;
                            resource.ResourceId = r.ResourceId;
                            resource.ResourceType = r.ResourceType;

                            string UrlDownload = GetUrlDownload(resource.FileId);
                            resource.FileLocation = UrlDownload;
                        }
                        else
                        {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                    else
                    {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                ResourceViewModel rvm = viewModelFactory.GetViewModel(resource);
                return View(rvm);
            }
            catch (CustomException ce)
            {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.";
                return View("Error");
            }
            catch (Exception ex)
            {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed!";
                return View("Error");
            }
        }

        //
        // GET: /Prof/ReplaceCourseResource(id = 0)

        public ActionResult ReplaceCourseResource(int id = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                //get the course resource
                Resources resource = new Resources();
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/resources/GetCourseResourceById/?id=" + id).Result;
                    if (response.IsSuccessStatusCode) {
                        Resources r = response.Content.ReadAsAsync<Resources>().Result;
                        if (r != null) {
                            resource.CourseId = r.CourseId;
                            resource.FileId = r.FileId;
                            resource.FileName = r.FileName;
                            resource.ModuleID = r.ModuleID;
                            resource.ResourceId = r.ResourceId;
                            resource.ResourceType = r.ResourceType;
                        }
                        else {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                ResourceViewModel rvm = this.viewModelFactory.GetViewModel(resource);
                return View(rvm);
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.";
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed!";
                return View("Error");
            }
        }

        //
        // POST: /Prof/ReplaceResourcesforCourse(file, id = 0)

        [HttpPost]
        public ActionResult ReplaceResourcesforCourse(HttpPostedFileBase file, int courseId = 0, int resourceId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );

                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/UploadedFiles"), fileName);
                    file.SaveAs(path);

                    FileDTO dto = new FileDTO { parentId = courseId, fileName = file.FileName , filePath = path};

                    HttpResponseMessage response =
                        client.PutAsJsonAsync("api/resources/UpdateResourcesForCourse?id=" + resourceId, dto).Result;
                    if (!response.IsSuccessStatusCode) {
                        throw new CustomException("Could not complete the operation!");
                    }
                }
                return RedirectToAction("DisplayCourseResources", new { id = courseId });
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.";
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed!";
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult DownloadResourcesforCourse(HttpPostedFileBase file, int courseId = 0, int resourceId = 0)
        {
            return null;
        }

        #endregion

        #region Modules-CRUD

        //
        // GET: /Prof/DisplayModules(id = 0)

        public ActionResult DisplayModules(int id = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                //get all course modules
                List<CourseModule> courseModules = new List<CourseModule>();
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/course/GetAllModules/?courseId=" + id).Result;
                    if (response.IsSuccessStatusCode) {
                        var list = response.Content.ReadAsAsync<IEnumerable<CourseModule>>().Result;
                        if (list != null) {
                            courseModules = list.ToList();
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                List<CourseModuleViewModel> cmvm = this.viewModelFactory.GetViewModel(courseModules);
                return View(cmvm);
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.";
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed!";
                return View("Error");
            }
        }

        //
        // GET: /Prof/ModuleDetails(id = 0)

        public ActionResult ModuleDetails(int id = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                CourseModule courseModule = new CourseModule();
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/course/GetCourseModuleById/?id=" + id).Result;
                    if (response.IsSuccessStatusCode) {
                        var cm = response.Content.ReadAsAsync<CourseModule>().Result;
                        if (cm != null) {
                            courseModule.CourseId = cm.CourseId;
                            courseModule.GradeId = cm.GradeId;
                            courseModule.Moduledatetime = cm.Moduledatetime;
                            courseModule.ModuleId = cm.ModuleId;
                            courseModule.ModuleName = cm.ModuleName;
                            courseModule.PreviousModuleId = cm.PreviousModuleId;
                        }
                        else {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                CourseModuleViewModel cmvm = this.viewModelFactory.GetViewModel(courseModule);
                return View(cmvm);
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.";
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed!";
                return View("Error");
            }
        }

        //
        // GET: /Prof/CreateModule

        public ActionResult CreateModule(int id = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                if (id != 0) {
                    CourseModuleViewModel cmvm = new CourseModuleViewModel();
                    cmvm.CourseId = id;
                    return View(cmvm);
                }
                else {
                    throw new CustomException("Error creating course module.");
                }
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.";
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed!";
                return View("Error");
            }
        }

        //
        // POST: /Prof/CreateModule(courseModuleViewModel)

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateModule(CourseModuleViewModel courseModuleViewModel) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                //create course module dto to be passed to the api services
                CourseModuleDTO dto = new CourseModuleDTO();
                dto.Moduledatetime = courseModuleViewModel.Moduledatetime;
                dto.ModuleName = courseModuleViewModel.ModuleName;
                dto.PreviousModuleId = courseModuleViewModel.PreviousModuleId;
                dto.CourseId = courseModuleViewModel.CourseId;
                

                //add the created course module
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    //add to db
                    HttpResponseMessage responseFromDb = client.PostAsJsonAsync("api/course/AddModule", dto).Result;
                    if (!responseFromDb.IsSuccessStatusCode) {
                        throw new CustomException("Could not complete the operation!");
                    }
                    //add to resources
                    HttpResponseMessage responseFromResources = client.PostAsJsonAsync("api/resources/AddModuleToResources", dto).Result;
                    if (!responseFromResources.IsSuccessStatusCode) {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                return RedirectToAction("DisplayCourses");
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.";
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed!";
                return View("Error");
            }
        }

        //
        // POST: /Prof/DeleteModule(id = 0)

        [HttpPost]
        public ActionResult DeleteModule(int id = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                //delete the module
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    //delete module from db
                    HttpResponseMessage responseFromDb = client.DeleteAsync("api/course/DeleteModule/?id=" + id).Result;
                    if (!responseFromDb.IsSuccessStatusCode) {
                        throw new CustomException("Could not complete the operation!");
                    }
                    //delete module from resources
                    HttpResponseMessage responseFromResources = client.DeleteAsync("api/resources/DeleteModuleFromResources/?id=" + id).Result;
                    if (!responseFromResources.IsSuccessStatusCode) {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                return RedirectToAction("DisplayCourses");
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.";
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed!";
                return View("Error");
            }
        }

        //
        // GET: /Prof/EditModule(id = 0)

        public ActionResult EditModule(int id = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                //get the course module to be edited
                CourseModule courseModule = new CourseModule();
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/course/GetCourseModuleById/?id=" + id).Result;
                    if (response.IsSuccessStatusCode) {
                        var cm = response.Content.ReadAsAsync<CourseModule>().Result;
                        if (cm != null) {
                            courseModule.CourseId = cm.CourseId;
                            courseModule.Moduledatetime = cm.Moduledatetime;
                            courseModule.ModuleId = cm.ModuleId;
                            courseModule.ModuleName = cm.ModuleName;
                            courseModule.PreviousModuleId = cm.PreviousModuleId;
                        }
                        else {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                CourseModuleViewModel cmvm = this.viewModelFactory.GetViewModel(courseModule);
                return View(cmvm);
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.";
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed!";
                return View("Error");
            }
        }

        //
        // POST: /Prof/EditModule(courseModuleViewModel)

        [HttpPost]
        public ActionResult EditModule(CourseModuleViewModel courseModuleViewModel) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                //create the course dto to be passed to the api services
                CourseModuleDTO dto = new CourseModuleDTO();
                dto.CourseId = courseModuleViewModel.CourseId;
                dto.Moduledatetime = courseModuleViewModel.Moduledatetime;
                dto.ModuleId = courseModuleViewModel.ModuleId;
                dto.ModuleName = courseModuleViewModel.ModuleName;
                dto.PreviousModuleId = courseModuleViewModel.PreviousModuleId;

                //edit the course
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.PutAsJsonAsync("api/course/UpdateCourseModule/?id=" + courseModuleViewModel.ModuleId, dto).Result;
                    if (!response.IsSuccessStatusCode) {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                return RedirectToAction("DisplayCourses");
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.";
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed!";
                return View("Error");
            }
        }

        #endregion

        #region Module Resources - CRUD

        //
        // GET: /Prof/DisplayModuleResources(id = 0)

        public ActionResult DisplayModuleResources(int id = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                //get the course resources
                List<Resources> resources = new List<Resources>();
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/resources/GetAllModuleResources/?id=" + id).Result;
                    if (response.IsSuccessStatusCode) {
                        IEnumerable<Resources> list = response.Content.ReadAsAsync<IEnumerable<Resources>>().Result;
                        if (list != null) {
                            resources = list.ToList();
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                List<ResourceViewModel> rvm = this.viewModelFactory.GetViewModel(resources);
                return View(rvm);
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.";
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed!";
                return View("Error");
            }
        }

        //
        // GET: /Prof/AddModuleResource(id = 0)

        public ActionResult AddModuleResource(int moduleId = 0, int courseId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                if (moduleId > 0 && courseId > 0) {
                    Tuple<int, int> viewModel = new Tuple<int, int>(moduleId, courseId);
                    return View(viewModel);
                }
                else {
                    return RedirectToAction("DisplayCourses");
                }
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.";
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed!";
                return View("Error");
            }
        }

        //
        // POST: /Prof/UploadResourcesforModule(file, id = 0)

        [HttpPost]
        public ActionResult UploadResourcesforModule(HttpPostedFileBase file, int moduleId = 0, int courseId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );

                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/UploadedFiles"), fileName);
                    file.SaveAs(path);

                    FileDTO dto = new FileDTO { rootId = courseId, parentId = moduleId, fileName = file.FileName, filePath = path };

                    HttpResponseMessage response =
                        client.PostAsJsonAsync("api/resources/UploadResourcesForModule/?id=" + moduleId, dto).Result;
                    if (!response.IsSuccessStatusCode) {
                        throw new CustomException("Could not complete the operation!");
                    }
                }
                return RedirectToAction("DisplayModules", new { id = courseId });
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.";
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed!";
                return View("Error");
            }
        }

        //
        // POST: /Prof/DeleteModuleResource(id = 0)

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteModuleResource(int id = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                //delete the module resource
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );

                    HttpResponseMessage response = client.DeleteAsync("api/resources/DeleteModuleResource/?id=" + id).Result;
                    if (!response.IsSuccessStatusCode) {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                return RedirectToAction("DisplayCourses");
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.";
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed!";
                return View("Error");
            }
        }

        //
        // GET: /Prof/ReplaceModuleResource(id = 0)

        public ActionResult ReplaceModuleResource(int id = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                //get the course resource
                Resources resource = new Resources();
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/resources/GetCourseResourceById/?id=" + id).Result;
                    if (response.IsSuccessStatusCode) {
                        Resources r = response.Content.ReadAsAsync<Resources>().Result;
                        if (r != null) {
                            resource.CourseId = r.CourseId;
                            resource.FileId = r.FileId;
                            resource.FileName = r.FileName;
                            resource.ModuleID = r.ModuleID;
                            resource.ResourceId = r.ResourceId;
                            resource.ResourceType = r.ResourceType;
                        }
                        else {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                ResourceViewModel rvm = this.viewModelFactory.GetViewModel(resource);
                return View(rvm);
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.";
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed!";
                return View("Error");
            }
        }

        //
        // POST: /Prof/ReplaceResourcesforModule(file, id = 0)

        [HttpPost]
        public ActionResult ReplaceResourcesforModule(HttpPostedFileBase file, int moduleId = 0, int resourceId = 0, int courseId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );

                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/UploadedFiles"), fileName);
                    file.SaveAs(path);

                    FileDTO dto = new FileDTO { rootId = courseId, parentId = moduleId, fileName = file.FileName , filePath = path};

                    HttpResponseMessage response =
                        client.PutAsJsonAsync("api/resources/UpdateResourcesForModule/?id=" + resourceId, dto).Result;
                    if (!response.IsSuccessStatusCode) {
                        throw new CustomException("Could not complete the operation!");
                    }
                }
                return RedirectToAction("DisplayModules", new { id = courseId });
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.";
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed!";
                return View("Error");
            }
        }

        #endregion

    }
}
