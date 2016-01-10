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
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;

namespace E_LearningApplication.Controllers {
    [Authorize(Roles="Student")]
    public class StudentController : Controller {
        #region Private fields

        //private readonly string apiMethodsUrl = "https://elearningservices.azurewebsites.net/";
        private readonly string apiMethodsUrl = "http://localhost:42175/";
        private ILoggingUtil logger = new NLogLogger();
        private IViewModelFactory viewModelFactory = new ViewModelFactory();
        private IMailUtil mailUtil = new MailUtil();

        #endregion

        //
        // GET: /Student/

        public ActionResult Index() {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                var _userId = Session["UserId"];
                var _sessionUser = Convert.ToInt32(_userId);

                List<Courses> courses = new List<Courses>();
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/course/GetMyCourses/?id=" + _sessionUser).Result;
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

        //
        // GET: /Student/DisplayAllAvailableCourses()

        public ActionResult DisplayAllAvailableCourses() {
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

                var _userId = Session["UserId"];
                var _sessionUser = Convert.ToInt32(_userId);
                List<Courses> myCourses = new List<Courses>();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/course/GetMyCourses/?id=" + _sessionUser).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        IEnumerable<Courses> list = response.Content.ReadAsAsync<IEnumerable<Courses>>().Result;
                        if (list != null)
                        {
                            myCourses = list.ToList();
                        }
                    }
                    else
                    {
                        throw new CustomException("Could not complete the operation!");
                    }
                }
                List<CoursesViewModel> cvm = this.viewModelFactory.GetViewModel(courses);
                foreach (var course in cvm)
                    foreach (var mycourse in myCourses)
                        if (course.CourseId.Equals(mycourse.CourseId))
                        {
                            course.userInCourse = new UsersInCourse();
                            course.userInCourse.CourseId = course.CourseId;
                            course.userInCourse.UserId = _sessionUser;

                        }

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
        // GET: /Student/DisplayMyCourses()

        public ActionResult DisplayMyCourses() {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                var _userId = Session["UserId"];
                var _sessionUser = Convert.ToInt32(_userId);

                List<Courses> courses = new List<Courses>();
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/course/GetMyCourses/?id=" + _sessionUser).Result;
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
        // GET: /Student/CourseDetails(id = 0)

        public ActionResult MyCourseDetails(int id = 0) {
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
                            course.enrollementKey = c.enrollementKey;
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
        // GET: /Student/DisplayMyCourseResources(id = 0)

        [HttpGet]
        public ActionResult DisplayMyCourseResources(int id = 0) {
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
        // GET: /Student/DisplayMyModules(id = 0)

        public ActionResult DisplayMyModules(int id = 0) {
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
        // GET: /Student/MyModuleDetails(id = 0)

        public ActionResult MyModuleDetails(int id = 0) {
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
        // GET: /Student/DisplayMyModuleResources(id = 0)

        public ActionResult DisplayMyModuleResources(int id = 0) {
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

        #region enroll and unenroll
        //
        // GET: /Student/EnrollToCourse

        public ActionResult EnrollToCourse(int id = 0)
        {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            var _userId = Session["UserId"];
            var _sessionUser = Convert.ToInt32(_userId);
            UsersInCourseView userInCourseView = new UsersInCourseView();
            userInCourseView.CourseId = id;
            userInCourseView.UserId = (int)_userId;
            return View(userInCourseView);
        }

        //
        // POST: /Student/EnrollInACourse(userInCourse)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EnrollToCourse(UsersInCourseView userInCourseView)
        {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/course/GetCourseById/?id=" + userInCourseView.CourseId).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        Courses course = response.Content.ReadAsAsync<Courses>().Result;
                        //verify if enrollement key is correct
                        if (course.enrollementKey != null && !course.enrollementKey.Equals(userInCourseView.EnrollementKey))
                            throw new CustomException("Enrollment key is incorrect");
                    }
                    else
                    {
                        throw new CustomException("Could not complete the operation!");
                    }
                }
                UsersInCourseDTO dto = new UsersInCourseDTO();
                dto.CourseId = userInCourseView.CourseId;
                dto.UserId = userInCourseView.UserId;
                dto.EnrollementKey = userInCourseView.EnrollementKey;
                dto.CourseUserstatus = userInCourseView.EnrollementKey;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );

                    HttpResponseMessage response = client.PostAsJsonAsync("api/course/EnrollStudentInCourse/?usersInCourse=", dto).Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new CustomException("Could not complete the operation!");
                    }
                }
                return RedirectToAction("DisplayAllAvailableCourses");
            }
            catch (CustomException ce)
            {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = ce.Message;
                return View("Error");
            }
            catch (Exception ex)
            {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.";
                return View("Error");
            }
        }

        //
        // GET: /Student/UnenrolCourse(id = 0)

        public ActionResult UnenrollFromCourse(int id = 0)
        {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.DeleteAsync("api/course/UnenrollCourse/?id=" + id).Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                return RedirectToAction("DisplayMyCourses");
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

        #endregion

    }
}
