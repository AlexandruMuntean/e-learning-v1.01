using E_LearningApplication.CustomExceptions;
using E_LearningApplication.Models;
using E_LearningApplication.Models.DTOs;
using E_LearningApplication.Models.ViewModels;
using E_LearningApplication.Utils;
using E_LearningApplication.Utils.LoggingUtils;
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
using System.Web.Security;

namespace E_LearningApplication.Controllers {
    [Authorize]
    public class HomeworkController : Controller {
        #region Private fields

        //private readonly string apiMethodsUrl = "https://elearningservices.azurewebsites.net/";
        private readonly string apiMethodsUrl = "http://localhost:42175/";
        private ILoggingUtil logger = new NLogLogger();
        private IViewModelFactory viewModelFactory = new ViewModelFactory();

        #endregion

        /// <summary>
        /// Generates the homework code.
        /// </summary>
        /// <returns></returns>
        private static string GenerateHomeworkCode() {
            Guid guid = Guid.NewGuid();
            string guidString = Convert.ToBase64String(guid.ToByteArray());
            guidString = guidString.Replace("=", "");
            guidString = guidString.Replace("+", "");
            return guidString;

        }

        #region Prof functionalities

        //
        // GET: /Homework/DisplayAllCourseHomework(id = 0)
        [Authorize(Roles = "Prof")]
        public ActionResult DisplayAllCourseHomework(int id = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                List<Homeworks> homeworks = new List<Homeworks>();

                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/homework/GetAllCourseHomework/?id=" + id).Result;
                    if (response.IsSuccessStatusCode) {
                        var list = response.Content.ReadAsAsync<IEnumerable<Homeworks>>().Result;
                        if (list != null) {
                            homeworks = list.ToList();
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                List<HomeworksViewModel> hvm = this.viewModelFactory.GetViewModel(homeworks);
                return View(hvm);
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
        // GET: /Homework/DisplayAllCourseModuleHomework()
        [Authorize(Roles = "Prof")]
        public ActionResult DisplayAllCourseModuleHomework(int id = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                List<Homeworks> homeworks = new List<Homeworks>();

                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/homework/GetAllCourseModuleHomework/?id=" + id).Result;
                    if (response.IsSuccessStatusCode) {
                        var list = response.Content.ReadAsAsync<IEnumerable<Homeworks>>().Result;
                        if (list != null) {
                            homeworks = list.ToList();
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                List<HomeworksViewModel> hvm = this.viewModelFactory.GetViewModel(homeworks);
                return View(hvm);
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
        // GET: /Homework/CreateCourseHomework(id = 0)
        [Authorize(Roles = "Prof")]
        public ActionResult CreateCourseHomework(int id = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            ViewBag.CourseId = id;
            return View();
        }

        //
        // POST: /Homework/CreateCourseHomework(hwViewModel, courseId)
        [Authorize(Roles = "Prof")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCourseHomework(HomeworksViewModel homeworkViewModel, HttpPostedFileBase file, int courseId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                var _userId = Session["UserId"];
                var _sessionUser = Convert.ToInt32(_userId);

                HomeworkDTO dto = new HomeworkDTO();
                dto.HomeworkAccessSpan = homeworkViewModel.HomeworkAccessSpan;
                dto.HomeworkDeadline = homeworkViewModel.HomeworkDeadline;
                dto.HomeworkDescription = homeworkViewModel.HomeworkDescription;
                dto.HomeworkId = homeworkViewModel.HomeworkId;
                dto.HomeworkName = homeworkViewModel.HomeworkName;
                dto.HomeworkPoints = homeworkViewModel.HomeworkPoints;
                dto.HomeworkCode = GenerateHomeworkCode();
                dto.CourseId = homeworkViewModel.CourseId;
                dto.CourseModuleId = homeworkViewModel.CourseModuleId;
                dto.OwnerId = _sessionUser;
                //save homework to db
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response =
                        client.PostAsJsonAsync("api/homework/AddHomework/?homework=", dto).Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new CustomException("Could not complete the operation!");
                    }

                    //save homework to drive
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/UploadedFiles"), fileName);
                    file.SaveAs(path);
                    //////upload homework
                    FileDTO fileDto = new FileDTO {parentId = courseId, fileName = dto.HomeworkCode, filePath = path};
                    HttpResponseMessage responseDrive =
                        client.PostAsJsonAsync("api/homework/UploadResourcesForHomework/?id=" + courseId, fileDto).Result;
                    if (!responseDrive.IsSuccessStatusCode)
                    {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                return RedirectToAction("DisplayAllCourseHomework", new { id = courseId });
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
        // GET: /Homework/CreateCourseModuleHomework(id = 0)
        [Authorize(Roles = "Prof")]
        public ActionResult CreateCourseModuleHomework(int id = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            ViewBag.CourseModuleId = id;
            return View();
        }

        private string GetUrlDownload(string FileId)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("https://docs.google.com/uc?id=").Append(FileId).Append("&export=download");
            return sb.ToString();
        }

        public ActionResult DowloadCourseResource(string id)
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
                    HttpResponseMessage response = client.GetAsync("api/homework/GetHomeworkResourceById/?id=" + id).Result;
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
        // POST: /Homework/CreateCourseModuleHomework(hwViewModel, courseModuleId)
        [Authorize(Roles = "Prof")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCourseModuleHomework(HomeworksViewModel homeworkViewModel, HttpPostedFileBase file, int courseModuleId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                var _userId = Session["UserId"];
                var _sessionUser = Convert.ToInt32(_userId);

                HomeworkDTO dto = new HomeworkDTO();
                dto.HomeworkAccessSpan = homeworkViewModel.HomeworkAccessSpan;
                dto.HomeworkDeadline = homeworkViewModel.HomeworkDeadline;
                dto.HomeworkDescription = homeworkViewModel.HomeworkDescription;
                dto.HomeworkId = homeworkViewModel.HomeworkId;
                dto.HomeworkName = homeworkViewModel.HomeworkName;
                dto.HomeworkPoints = homeworkViewModel.HomeworkPoints;
                dto.HomeworkCode = GenerateHomeworkCode();
                dto.CourseId = homeworkViewModel.CourseId;
                dto.CourseModuleId = homeworkViewModel.CourseModuleId;
                dto.OwnerId = _sessionUser;
                //save homework to db
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response =
                        client.PostAsJsonAsync("api/homework/AddHomework/?homework=", dto).Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new CustomException("Could not complete the operation!");
                    }

                    //save homework to drive
                    var fileName = file.FileName;
                    var path = Path.Combine(Server.MapPath("~/App_Data/UploadedFiles"), fileName);
                    file.SaveAs(path);
                    //////upload homework
                     FileDTO fileDto = new FileDTO { rootId = homeworkViewModel.CourseId.GetValueOrDefault() , parentId = courseModuleId, fileName = dto.HomeworkCode, filePath = path};
                    HttpResponseMessage responseDrive =
                        client.PostAsJsonAsync("api/homework/UploadResourcesForHomeworkModule/?id=" + courseModuleId, fileDto).Result;
                    if (!responseDrive.IsSuccessStatusCode)
                    {
                        throw new CustomException("Could not complete the operation!");
                    }
                    
                }
                return RedirectToAction("DisplayAllCourseModuleHomework", new { id = courseModuleId });
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
        // POST: /Homework/DeleteCourseHomework(id = 0, courseId = 0)
        [Authorize(Roles = "Prof")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCourseHomework(int id = 0, int courseId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );

                    HttpResponseMessage responseDrive = client.DeleteAsync("api/homework/DeleteCourseHomework/?id=" + id).Result;
                    if (!responseDrive.IsSuccessStatusCode)
                    {
                        throw new CustomException("Could not complete the operation!");
                    }

                    HttpResponseMessage response = client.DeleteAsync("api/homework/DeleteHomework/?id=" + id).Result;
                    if (!response.IsSuccessStatusCode) {
                        throw new CustomException("Could not complete the operation!");
                    }
                    
                }

                return RedirectToAction("DisplayAllCourseHomework", new { id = courseId });
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
        // POST: /Homework/DeleteCourseModuleHomework(id = 0)
        [Authorize(Roles = "Prof")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCourseModuleHomework(int id = 0, int courseModuleId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );

                    HttpResponseMessage responseDrive = client.DeleteAsync("api/homework/DeleteModuleHomework/?id=" + id).Result;
                    if (!responseDrive.IsSuccessStatusCode)
                    {
                        throw new CustomException("Could not complete the operation!");
                    }

                    HttpResponseMessage response = client.DeleteAsync("api/homework/DeleteHomework/?id=" + id).Result;
                    if (!response.IsSuccessStatusCode) {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                return RedirectToAction("DisplayAllCourseModuleHomework", new { id = courseModuleId });
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
        // GET: /Homework/CourseHomeworkDetails(id = 0, courseId = 0)
        [Authorize(Roles = "Prof")]
        public ActionResult CourseHomeworkDetails(int id = 0, int courseId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                Homeworks hw = new Homeworks();
                List<HomeworkAssignementDTO> ha = new List<HomeworkAssignementDTO>();
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response1 = client.GetAsync("api/homework/GetHomeworkById/?id=" + id).Result;
                    HttpResponseMessage response2 = client.GetAsync("api/homework/GetCourseAssignements/?id=" + id + "&courseId=" + courseId).Result;
                    if (response1.IsSuccessStatusCode && response2.IsSuccessStatusCode) {
                        var h = response1.Content.ReadAsAsync<Homeworks>().Result;
                        if (h != null) {
                            hw.HomeworkAccessSpan = h.HomeworkAccessSpan;
                            hw.HomeworkDeadline = h.HomeworkDeadline;
                            hw.HomeworkDescription = h.HomeworkDescription;
                            hw.HomeworkId = h.HomeworkId;
                            hw.HomeworkName = h.HomeworkName;
                            hw.HomeworkPoints = h.HomeworkPoints;
                            hw.HomeworkCode = h.HomeworkCode;
                            hw.CourseId = h.CourseId;
                            hw.CourseModuleId = h.CourseModuleId;
                            hw.OwnerId = h.OwnerId;
                        }
                        else {
                            throw new CustomException("Could not complete the operation!");
                        }
                        var list = response2.Content.ReadAsAsync<IEnumerable<HomeworkAssignementDTO>>().Result;
                        if (list != null) {
                            ha = list.ToList();
                        }

                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                Tuple<HomeworksViewModel, List<HomeworkAssignementViewModel>, int> viewModel = new Tuple<HomeworksViewModel, List<HomeworkAssignementViewModel>, int>(
                    this.viewModelFactory.GetViewModel(hw),
                    this.viewModelFactory.GetViewModel(ha),
                    courseId
                    );
                return View(viewModel);
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
        // GET: /Homework/CourseModuleHomeworkDetails(id = 0, courseModuleId = 0)
        [Authorize(Roles = "Prof")]
        public ActionResult CourseModuleHomeworkDetails(int id = 0, int courseModuleId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                Homeworks hw = new Homeworks();
                List<HomeworkAssignementDTO> ha = new List<HomeworkAssignementDTO>();
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response1 = client.GetAsync("api/homework/GetHomeworkById/?id=" + id).Result;
                    HttpResponseMessage response2 = client.GetAsync("api/homework/GetCourseModuleAssignements/?id=" + id + "&courseModuleId=" + courseModuleId).Result;
                    if (response1.IsSuccessStatusCode && response2.IsSuccessStatusCode) {
                        var h = response1.Content.ReadAsAsync<Homeworks>().Result;
                        if (h != null) {
                            hw.HomeworkAccessSpan = h.HomeworkAccessSpan;
                            hw.HomeworkDeadline = h.HomeworkDeadline;
                            hw.HomeworkDescription = h.HomeworkDescription;
                            hw.HomeworkId = h.HomeworkId;
                            hw.HomeworkName = h.HomeworkName;
                            hw.HomeworkPoints = h.HomeworkPoints;
                            hw.HomeworkCode = h.HomeworkCode;
                            hw.CourseId = h.CourseId;
                            hw.CourseModuleId = h.CourseModuleId;
                            hw.OwnerId = h.OwnerId;
                        }
                        else {
                            throw new CustomException("Could not complete the operation!");
                        }
                        var list = response2.Content.ReadAsAsync<IEnumerable<HomeworkAssignementDTO>>().Result;
                        if (list != null) {
                            ha = list.ToList();
                        }

                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                Tuple<HomeworksViewModel, List<HomeworkAssignementViewModel>, int> viewModel = new Tuple<HomeworksViewModel, List<HomeworkAssignementViewModel>, int>(
                    this.viewModelFactory.GetViewModel(hw),
                    this.viewModelFactory.GetViewModel(ha),
                    courseModuleId
                    );
                return View(viewModel);
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
        // GET: /Homework/EditCourseHomework(id = 0, courseId = 0)
        [Authorize(Roles = "Prof")]
        public ActionResult EditCourseHomework(int id = 0, int courseId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                Homeworks hw = new Homeworks();
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/homework/GetHomeworkById/?id=" + id).Result;
                    if (response.IsSuccessStatusCode) {
                        var h = response.Content.ReadAsAsync<Homeworks>().Result;
                        if (h != null) {
                            hw.HomeworkAccessSpan = h.HomeworkAccessSpan;
                            hw.HomeworkDeadline = h.HomeworkDeadline;
                            hw.HomeworkDescription = h.HomeworkDescription;
                            hw.HomeworkId = h.HomeworkId;
                            hw.HomeworkName = h.HomeworkName;
                            hw.HomeworkPoints = h.HomeworkPoints;
                            hw.HomeworkCode = h.HomeworkCode;
                            hw.CourseId = h.CourseId;
                            hw.CourseModuleId = h.CourseModuleId;
                            hw.OwnerId = h.OwnerId;
                        }
                        else {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                HomeworksViewModel hvm = this.viewModelFactory.GetViewModel(hw);
                return View(hvm);
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
        // POST: /Homework/EditCourseHomework(hwViewModel, courseId)
        [Authorize(Roles = "Prof")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCourseHomework(HomeworksViewModel homeworkViewModel, HttpPostedFileBase file, int courseId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                var _userId = Session["UserId"];
                var _sessionUser = Convert.ToInt32(_userId);

                //create the homework dto to be passed to the api services
                HomeworkDTO dto = new HomeworkDTO();
                dto.HomeworkAccessSpan = homeworkViewModel.HomeworkAccessSpan;
                dto.HomeworkDeadline = homeworkViewModel.HomeworkDeadline;
                dto.HomeworkDescription = homeworkViewModel.HomeworkDescription;
                dto.HomeworkId = homeworkViewModel.HomeworkId;
                dto.HomeworkName = homeworkViewModel.HomeworkName;
                dto.HomeworkPoints = homeworkViewModel.HomeworkPoints;
                dto.HomeworkCode = homeworkViewModel.HomeworkCode;
                dto.CourseId = homeworkViewModel.CourseId;
                dto.CourseModuleId = homeworkViewModel.CourseModuleId;
                dto.OwnerId = _sessionUser;
                //update homework on db
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response =
                        client.PutAsJsonAsync("api/homework/UpdateHomework/?id=" + homeworkViewModel.HomeworkId, dto)
                            .Result;
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new CustomException("Could not complete the operation!");
                    }

                    if (file != null)
                    {
                        //update homework on drive
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/App_Data/UploadedFiles"), fileName);
                        file.SaveAs(path);
                        //////upload homework
                        FileDTO fileDto = new FileDTO
                        {
                            parentId = courseId,
                            fileName = dto.HomeworkCode,
                            filePath = path
                        };
                        HttpResponseMessage responseDrive =
                            client.PostAsJsonAsync("api/homework/UpdateResourcesForHomework/?id=" + homeworkViewModel.HomeworkId, fileDto)
                                .Result;
                        if (!responseDrive.IsSuccessStatusCode)
                        {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                }

                return RedirectToAction("DisplayAllCourseHomework", new { id = courseId });
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
        // GET: /Homework/EditCourseModuleHomework(id = 0, courseModuleId = )
        [Authorize(Roles = "Prof")]
        public ActionResult EditCourseModuleHomework(int id = 0, int courseModuleId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                Homeworks hw = new Homeworks();
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/homework/GetHomeworkById/?id=" + id).Result;
                    if (response.IsSuccessStatusCode) {
                        var h = response.Content.ReadAsAsync<Homeworks>().Result;
                        if (h != null) {
                            hw.HomeworkAccessSpan = h.HomeworkAccessSpan;
                            hw.HomeworkDeadline = h.HomeworkDeadline;
                            hw.HomeworkDescription = h.HomeworkDescription;
                            hw.HomeworkId = h.HomeworkId;
                            hw.HomeworkName = h.HomeworkName;
                            hw.HomeworkPoints = h.HomeworkPoints;
                            hw.HomeworkCode = h.HomeworkCode;
                            hw.CourseId = h.CourseId;
                            hw.CourseModuleId = h.CourseModuleId;
                            hw.OwnerId = h.OwnerId;
                        }
                        else {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                HomeworksViewModel hvm = this.viewModelFactory.GetViewModel(hw);
                return View(hvm);
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
        // POST: /Homework/EditCourseModuleHomework(hwViewModel, courseModuleId = 0)
        [Authorize(Roles = "Prof")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCourseModuleHomework(HomeworksViewModel homeworkViewModel, HttpPostedFileBase file, int courseModuleId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                var _userId = Session["UserId"];
                var _sessionUser = Convert.ToInt32(_userId);

                //create the homework dto to be passed to the api services
                HomeworkDTO dto = new HomeworkDTO();
                dto.HomeworkAccessSpan = homeworkViewModel.HomeworkAccessSpan;
                dto.HomeworkDeadline = homeworkViewModel.HomeworkDeadline;
                dto.HomeworkDescription = homeworkViewModel.HomeworkDescription;
                dto.HomeworkId = homeworkViewModel.HomeworkId;
                dto.HomeworkName = homeworkViewModel.HomeworkName;
                dto.HomeworkPoints = homeworkViewModel.HomeworkPoints;
                dto.HomeworkCode = homeworkViewModel.HomeworkCode;
                dto.CourseId = homeworkViewModel.CourseId;
                dto.CourseModuleId = homeworkViewModel.CourseModuleId;
                dto.OwnerId = _sessionUser;
                //update homework on db
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response =
                        client.PutAsJsonAsync("api/homework/UpdateHomework/?id=" + homeworkViewModel.HomeworkId, dto)
                            .Result;

                    if (!response.IsSuccessStatusCode)
                    {
                        throw new CustomException("Could not complete the operation!");
                    }

                    if (file != null)
                    {
                        //update homework on drive
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath("~/App_Data/UploadedFiles"), fileName);
                        file.SaveAs(path);
                        //////upload homework
                        FileDTO fileDto = new FileDTO
                        {
                            rootId = homeworkViewModel.CourseId.GetValueOrDefault(),
                            parentId = courseModuleId,
                            fileName = dto.HomeworkCode,
                            filePath = path
                        };
                        HttpResponseMessage responseDrive =
                            client.PostAsJsonAsync("api/homework/UpdateResourcesForHomeworkinModules/?id=" + homeworkViewModel.HomeworkId, fileDto)
                                .Result;
                        if (!responseDrive.IsSuccessStatusCode)
                        {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                }

                return RedirectToAction("DisplayAllCourseModuleHomework", new { id = courseModuleId });
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
        // GET: /Homework/AssignCourseHomework(id = 0, courseId = 0)
        [Authorize(Roles = "Prof")]
        public ActionResult AssignCourseHomework(int id = 0, int courseId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                var _userId = Session["UserId"];
                var _sessionUser = Convert.ToInt32(_userId);

                //get the possible students and groups to assign homework to
                List<Users> users = new List<Users>();
                List<Groups> groups = new List<Groups>();

                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    //to do: switch from getAllUsers to getUsersInCourse after enrollment is implemented
                    HttpResponseMessage response1 = client.GetAsync("api/user/GetAllUsers").Result;
                    HttpResponseMessage response2 = client.GetAsync("api/groups/GetAssociatedGroups/?userId=" + _sessionUser).Result;
                    if (response1.IsSuccessStatusCode && response2.IsSuccessStatusCode) {
                        var list1 = response1.Content.ReadAsAsync<IEnumerable<Users>>().Result;
                        if (list1 != null) {
                            var allUsers = list1.ToList();
                            foreach (var u in allUsers) {
                                if (Roles.IsUserInRole(u.UserName, "Student")) {
                                    users.Add(u);
                                }
                            }

                        }
                        var list2 = response2.Content.ReadAsAsync<IEnumerable<Groups>>().Result;
                        if (list2 != null) {
                            groups = list2.ToList();
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }
                Tuple<List<UserProfile>, List<GroupsViewModel>, int, int> viewModel = new Tuple<List<UserProfile>, List<GroupsViewModel>, int, int>(
                        this.viewModelFactory.GetViewModel(users),
                        this.viewModelFactory.GetViewModel(groups),
                        id,
                        courseId
                        );

                return View(viewModel);
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
        // POST: /Homework/CourseHomeworkAssignement(id = 0, courseId = 0)
        [Authorize(Roles = "Prof")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CourseHomeworkAssignement(int id = 0, int courseId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                int studentId = Int32.Parse(Request["studentId"]);
                int groupId = Int32.Parse(Request["groupId"]);
                bool ok = false;
                HomeworkAssignementDTO dto = new HomeworkAssignementDTO();
                dto.HomeworkId = id;
                dto.CourseId = courseId;

                if (studentId > 0) {
                    dto.StudentId = studentId;
                    if (groupId > 0) {
                        dto.GroupId = groupId;
                    }
                    ok = true;
                }
                else {
                    if (groupId > 0) {
                        dto.GroupId = groupId;
                        ok = true;
                    }
                }

                if (ok) {
                    using (var client = new HttpClient()) {
                        client.BaseAddress = new Uri(this.apiMethodsUrl);
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json")
                            );
                        HttpResponseMessage response = client.PutAsJsonAsync("api/homework/AssignHomework/?id=" + id, dto).Result;
                        if (!response.IsSuccessStatusCode) {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                }
                else {
                    ViewBag.Error = "Choose a recipient for the homework!";
                    return View("Error");
                }

                return RedirectToAction("DisplayAllCourseHomework", new { id = courseId });
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
        // GET: /Homework/AssignCourseModuleHomework(id = 0, courseModuleId = 0)
        [Authorize(Roles = "Prof")]
        public ActionResult AssignCourseModuleHomework(int id = 0, int courseModuleId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                var _userId = Session["UserId"];
                var _sessionUser = Convert.ToInt32(_userId);

                //get the possible students and groups to assign homework to
                List<Users> users = new List<Users>();
                List<Groups> groups = new List<Groups>();

                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    //to do: switch from getAllUsers to getUsersInCourse after enrollment is implemented
                    HttpResponseMessage response1 = client.GetAsync("api/user/GetAllUsers").Result;
                    HttpResponseMessage response2 = client.GetAsync("api/groups/GetAssociatedGroups/?userId=" + _sessionUser).Result;
                    if (response1.IsSuccessStatusCode && response2.IsSuccessStatusCode) {
                        var list1 = response1.Content.ReadAsAsync<IEnumerable<Users>>().Result;
                        if (list1 != null) {
                            users = list1.ToList();
                        }
                        var list2 = response2.Content.ReadAsAsync<IEnumerable<Groups>>().Result;
                        if (list2 != null) {
                            groups = list2.ToList();
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }
                Tuple<List<UserProfile>, List<GroupsViewModel>, int, int> viewModel = new Tuple<List<UserProfile>, List<GroupsViewModel>, int, int>(
                        this.viewModelFactory.GetViewModel(users),
                        this.viewModelFactory.GetViewModel(groups),
                        id,
                        courseModuleId
                        );

                return View(viewModel);
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
        // POST: /Homework/CourseModuleHomeworkAssignement(id = 0)
        [Authorize(Roles = "Prof")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CourseModuleHomeworkAssignement(int id = 0, int courseModuleId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                int studentId = Int32.Parse(Request["studentId"]);
                int groupId = Int32.Parse(Request["groupId"]);
                bool ok = false;
                HomeworkAssignementDTO dto = new HomeworkAssignementDTO();
                dto.HomeworkId = id;
                dto.CourseModuleId = courseModuleId;

                if (studentId > 0) {
                    dto.StudentId = studentId;
                    if (groupId > 0) {
                        dto.GroupId = groupId;
                    }
                    ok = true;
                }
                else {
                    if (groupId > 0) {
                        dto.GroupId = groupId;
                        ok = true;
                    }
                }

                if (ok) {
                    using (var client = new HttpClient()) {
                        client.BaseAddress = new Uri(this.apiMethodsUrl);
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json")
                            );
                        HttpResponseMessage response = client.PutAsJsonAsync("api/homework/AssignHomework/?id=" + id, dto).Result;
                        if (!response.IsSuccessStatusCode) {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                }
                else {
                    ViewBag.Error = "Choose a recipient for the homework!";
                    return View("Error");
                }

                return RedirectToAction("DisplayAllCourseModuleHomework", new { id = courseModuleId });
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
        // POST: /Homework/UnassignCourseHomework(id = 0, courseId = 0)
        [Authorize(Roles = "Prof")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UnassignCourseHomework(int homeworkId = 0, int assignementId = 0, int courseId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                //create the homework dto to be passed to the api services
                HomeworkAssignementDTO dto = new HomeworkAssignementDTO();
                dto.HomeworkId = homeworkId;
                dto.CourseId = courseId;
                dto.AssignementId = assignementId;

                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.PutAsJsonAsync("api/homework/UnassignHomework/?id=" + homeworkId, dto).Result;
                    if (!response.IsSuccessStatusCode) {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                return RedirectToAction("DisplayAllCourseHomework", new { id = courseId });
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
        // POST: /Homework/UnassignCourseModuleHomework(id = 0, courseModuleId = 0)
        [Authorize(Roles = "Prof")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UnassignCourseModuleHomework(int homeworkId = 0, int assignementId = 0, int courseModuleId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                //create the homework dto to be passed to the api services
                HomeworkAssignementDTO dto = new HomeworkAssignementDTO();
                dto.HomeworkId = homeworkId;
                dto.CourseModuleId = courseModuleId;
                dto.AssignementId = assignementId;

                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.PutAsJsonAsync("api/homework/UnassignHomework/?id=" + homeworkId, dto).Result;
                    if (!response.IsSuccessStatusCode) {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                return RedirectToAction("DisplayAllCourseModuleHomework", new { id = courseModuleId });
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

        #region Student functionalities

        //
        // GET: /Homework/DisplayStudentAssignedHomework(id = 0)
        [Authorize(Roles = "Student")]
        public ActionResult DisplayStudentAssignedHomework() {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                var _userId = Session["UserId"];
                var _sessionUser = Convert.ToInt32(_userId);

                List<AssignementViewModel> homeworks = new List<AssignementViewModel>();
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/homework/GetAllStudentAssignedHomework/?id=" + _sessionUser).Result;
                    if (response.IsSuccessStatusCode) {
                        var list = response.Content.ReadAsAsync<IEnumerable<AssignementViewModel>>().Result;
                        if (list != null) {
                            homeworks = list.ToList();
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                return View(homeworks);
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
        // GET: /Homework/SubmitStudentHomework(assignementViewModel)
        [Authorize(Roles = "Student")]
        public ActionResult SubmitStudentHomework(int homeworkId = 0, int assignementId = 0, int recipientId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                AssignementViewModel avm = new AssignementViewModel();
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/homework/GetHomeworkById/?id=" + homeworkId).Result;
                    if (response.IsSuccessStatusCode) {
                        var h = response.Content.ReadAsAsync<Homeworks>().Result;
                        if (h != null) {
                            avm.HomeworkDeadline = h.HomeworkDeadline;
                            avm.HomeworkDescription = h.HomeworkDescription;
                            avm.HomeworkId = h.HomeworkId;
                            avm.HomeworkName = h.HomeworkName;
                            avm.HomeworkPoints = h.HomeworkPoints;
                            avm.HomeworkCode = h.HomeworkCode;
                            avm.AssignementId = assignementId;
                            avm.RecipientId = recipientId;
                        }
                        else {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                return View(avm);
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
        // POST: /Homework/SubmitStudentHomework(assignementId, file)
        [Authorize(Roles = "Student")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitStudentHomework(int assignementId, HttpPostedFileBase file) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                //create the dto for the new answer
                AnswerDTO dto = new AnswerDTO();
                dto.AnswerType = SubmissionTypeEnum.File.ToString();
                dto.AnswerValue = GenerateHomeworkCode();
                //save submission in db
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.PutAsJsonAsync("api/homework/SubmitHomework/?assignementId=" + assignementId, dto).Result;
                    if (!response.IsSuccessStatusCode) {
                        throw new CustomException("Could not complete the operation!");
                    }
                }
                //save submission on drive
                var fileName = dto.AnswerValue;
                var path = Path.Combine(Server.MapPath("~/App_Data/UploadedFiles"), fileName);
                file.SaveAs(path);
                //////upload submission

                return RedirectToAction("DisplayStudentAssignedHomework");
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
        // GET: /Homework/DisplayGroupAssignedHomework(id = 0)

        public ActionResult DisplayGroupAssignedHomework(int groupId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                List<AssignementViewModel> homeworks = new List<AssignementViewModel>();
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/homework/GetAllGroupAssignedHomework/?id=" + groupId).Result;
                    if (response.IsSuccessStatusCode) {
                        var list = response.Content.ReadAsAsync<IEnumerable<AssignementViewModel>>().Result;
                        if (list != null) {
                            homeworks = list.ToList();
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                return View(homeworks);
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
        // GET: /Homework/SubmitGroupHomework(assignementViewModel)

        public ActionResult SubmitGroupHomework(int homeworkId = 0, int assignementId = 0, int recipientId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                AssignementViewModel avm = new AssignementViewModel();
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/homework/GetHomeworkById/?id=" + homeworkId).Result;
                    if (response.IsSuccessStatusCode) {
                        var h = response.Content.ReadAsAsync<Homeworks>().Result;
                        if (h != null) {
                            avm.HomeworkDeadline = h.HomeworkDeadline;
                            avm.HomeworkDescription = h.HomeworkDescription;
                            avm.HomeworkId = h.HomeworkId;
                            avm.HomeworkName = h.HomeworkName;
                            avm.HomeworkPoints = h.HomeworkPoints;
                            avm.HomeworkCode = h.HomeworkCode;
                            avm.AssignementId = assignementId;
                            avm.RecipientId = recipientId;
                        }
                        else {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                return View(avm);
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
        // POST: /Homework/SubmitGroupHomework(assignementId, file)

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubmitGroupHomework(int assignementId, HttpPostedFileBase file) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                //create the dto for the new answer
                AnswerDTO dto = new AnswerDTO();
                dto.AnswerType = SubmissionTypeEnum.File.ToString();
                dto.AnswerValue = GenerateHomeworkCode();
                //save submission to db
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.PutAsJsonAsync("api/homework/SubmitHomework/?assignementId=" + assignementId, dto).Result;
                    if (!response.IsSuccessStatusCode) {
                        throw new CustomException("Could not complete the operation!");
                    }

                }
                //save submission to drive
                var fileName = dto.AnswerValue;
                var path = Path.Combine(Server.MapPath("~/App_Data/UploadedFiles"), fileName);
                file.SaveAs(path);
                //////upload submission

                return RedirectToAction("DisplayGroupAssignedHomework");
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
