using E_LearningApplication.CustomExceptions;
using E_LearningApplication.Models;
using E_LearningApplication.Models.DTOs;
using E_LearningApplication.Models.ViewModels;
using E_LearningApplication.Utils.LoggingUtils;
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
    [Authorize]
    public class GradesController : Controller {
        #region Private fields

        //private readonly string apiMethodsUrl = "https://elearningservices.azurewebsites.net/";
        private readonly string apiMethodsUrl = "http://localhost:42175/";
        private ILoggingUtil logger = new NLogLogger();
        private IViewModelFactory viewModelFactory = new ViewModelFactory();

        #endregion

        //
        // GET: /Grades/

        public ActionResult Index() {
            return View();
        }

        #region Prof functionalities

        //
        // GET: /Grades/GradeCourseHomework(courseId = 0, hwId = 0)
        [Authorize(Roles = "Prof")]
        public ActionResult GradeCourseHomework(int assignementId = 0, int homeworkId = 0, int courseId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                //get the answer to be graded
                Answers answer = new Answers();
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/homework/GetAnswerForAssignement/?id=" + assignementId).Result;
                    if (response.IsSuccessStatusCode) {
                        var a = response.Content.ReadAsAsync<Answers>().Result;
                        if (a != null) {
                            answer.AnswerId = a.AnswerId;
                            answer.AnswerType = a.AnswerType;
                            answer.AnswerValue = a.AnswerValue;
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }
                Tuple<AnswersViewModel, int, int, int> viewModel = new Tuple<AnswersViewModel, int, int, int>(
                    this.viewModelFactory.GetViewModel(answer),
                    assignementId,
                    homeworkId,
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
        // POST: /Grades/GradeCourseHomework(gradeValue = 0, courseId = 0)
        [Authorize(Roles = "Prof")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CourseHomeworkGrading(int assignementId = 0, int answerId = 0, int homeworkId = 0, int courseId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                string gradeFromView = Request["gradeValue"];
                if (gradeFromView != null) {
                    decimal grade = Decimal.Parse(gradeFromView);
                    //create the grade dto to be passed to the api services
                    HomeworkGradeDTO dto = new HomeworkGradeDTO();
                    dto.Gradedatetime = System.DateTime.Now;
                    dto.GradeValue = grade;
                    dto.AnswerId = answerId;
                    dto.CourseId = courseId;
                    dto.HomeworkId = homeworkId;
                    dto.AssignementId = assignementId;

                    using (var client = new HttpClient()) {
                        client.BaseAddress = new Uri(this.apiMethodsUrl);
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json")
                            );
                        HttpResponseMessage response = client.PutAsJsonAsync("api/grades/GradeHomework/?assignementId=" + assignementId, dto).Result;
                        if (!response.IsSuccessStatusCode) {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }

                    return RedirectToAction("DisplayAllCourseHomework", "Homework", new { id = courseId });
                }
                else {
                    ViewBag.Error = "Grade the submitted work!";
                    return View("Error");
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
        // GET: /Grades/GradeCourseModuleHomework(courseModuleId = 0, hwId = 0)
        [Authorize(Roles = "Prof")]
        public ActionResult GradeCourseModuleHomework(int assignementId = 0, int homeworkId = 0, int courseModuleId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                //get the answer to be graded
                Answers answer = new Answers();
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/homework/GetAnswerForAssignement/?id=" + assignementId).Result;
                    if (response.IsSuccessStatusCode) {
                        var a = response.Content.ReadAsAsync<Answers>().Result;
                        if (a != null) {
                            answer.AnswerId = a.AnswerId;
                            answer.AnswerType = a.AnswerType;
                            answer.AnswerValue = a.AnswerValue;
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }
                Tuple<AnswersViewModel, int, int, int> viewModel = new Tuple<AnswersViewModel, int, int, int>(
                    this.viewModelFactory.GetViewModel(answer),
                    assignementId,
                    homeworkId,
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
        // POST: /Grades/GradeCourseModuleHomework(answerId = 0, homeworkId = 0, courseModuleId = 0)
        [Authorize(Roles = "Prof")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CourseModuleHomeworkGrading(int assignementId = 0, int answerId = 0, int homeworkId = 0, int courseModuleId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                int grade = Int32.Parse(Request["gradeValue"]);
                if (grade > 0) {
                    //create the grade dto to be passed to the api services
                    HomeworkGradeDTO dto = new HomeworkGradeDTO();
                    dto.Gradedatetime = System.DateTime.Now;
                    dto.GradeValue = grade;
                    dto.AnswerId = answerId;
                    dto.CourseModuleId = courseModuleId;
                    dto.HomeworkId = homeworkId;
                    dto.AssignementId = assignementId;

                    using (var client = new HttpClient()) {
                        client.BaseAddress = new Uri(this.apiMethodsUrl);
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json")
                            );
                        HttpResponseMessage response = client.PutAsJsonAsync("api/grades/GradeHomework/?answerId=" + answerId, dto).Result;
                        if (!response.IsSuccessStatusCode) {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }

                    return RedirectToAction("DisplayAllCourseModuleHomework", "Homework", new { id = courseModuleId });
                }
                else {
                    ViewBag.Error = "Grade the submitted work!";
                    return View("Error");
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
        // GET: /Grades/DisplayCourseGivenGrades(courseId = 0)
        [Authorize(Roles="Prof")]
        public ActionResult DisplayCourseGivenGrades(int courseId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                List<ReceivedGradeViewModel> grades = new List<ReceivedGradeViewModel>();
                
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/grades/GetAllCourseGivenGrades/?id=" + courseId).Result;
                    if (response.IsSuccessStatusCode) {
                        var list = response.Content.ReadAsAsync<IEnumerable<ReceivedGradeViewModel>>().Result;
                        if (list != null) {
                            grades = list.ToList();
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                return View(grades);
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
        // GET: /Grades/DisplayCourseModuleGivenGrades(courseModuleId = 0)
        [Authorize(Roles = "Prof")]
        public ActionResult DisplayCourseModuleGivenGrades(int courseModuleId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                List<ReceivedGradeViewModel> grades = new List<ReceivedGradeViewModel>();

                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/grades/GetAllCourseModuleGivenGrades/?id=" + courseModuleId).Result;
                    if (response.IsSuccessStatusCode) {
                        var list = response.Content.ReadAsAsync<IEnumerable<ReceivedGradeViewModel>>().Result;
                        if (list != null) {
                            grades = list.ToList();
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                return View(grades);
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

        #endregion

        #region Student functionalities

        //
        // GET: /Grades/DisplayStudentReceivedGrades()
        [Authorize(Roles = "Student")]
        public ActionResult DisplayStudentReceivedGrades() {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                List<ReceivedGradeViewModel> grades = new List<ReceivedGradeViewModel>();
                var _userId = Session["UserId"];
                var _sessionUser = Convert.ToInt32(_userId);

                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/grades/GetAllStudentReceivedGrades/?id=" + _sessionUser).Result;
                    if (response.IsSuccessStatusCode) {
                        var list = response.Content.ReadAsAsync<IEnumerable<ReceivedGradeViewModel>>().Result;
                        if (list != null) {
                            grades = list.ToList();
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                return View(grades);
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
        // GET: /Grades/DisplayGroupReceivedGrades(groupId = 0)
        public ActionResult DisplayGroupReceivedGrades(int groupId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                List<ReceivedGradeViewModel> grades = new List<ReceivedGradeViewModel>();

                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/grades/GetAllGroupReceivedGrades/?id=" + groupId).Result;
                    if (response.IsSuccessStatusCode) {
                        var list = response.Content.ReadAsAsync<IEnumerable<ReceivedGradeViewModel>>().Result;
                        if (list != null) {
                            grades = list.ToList();
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                return View(grades);
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

        #endregion

    }
}
