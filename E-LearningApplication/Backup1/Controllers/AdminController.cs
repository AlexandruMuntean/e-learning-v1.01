using E_LearningApplication.CustomExceptions;
using E_LearningApplication.Models;
using E_LearningApplication.Models.DTOs;
using E_LearningApplication.Models.ViewModels;
using E_LearningApplication.Utils;
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
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace E_LearningApplication.Controllers {
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller {
        #region Private fields

        //private readonly string apiMethodsUrl = "https://elearningservices.azurewebsites.net/";
        private readonly string apiMethodsUrl = "http://localhost:42175/";
        private ILoggingUtil logger = new NLogLogger();
        private IViewModelFactory viewModelFactory = new ViewModelFactory();
        private IMailUtil mailUtil = new MailUtil();

        #endregion

        //
        // GET: /Admin/

        public ActionResult Index() {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                List<Logs> logs = new List<Logs>();

                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/admin/GetAllLogs").Result;
                    if (response.IsSuccessStatusCode) {
                        IEnumerable<Logs> l = response.Content.ReadAsAsync<IEnumerable<Logs>>().Result;
                        if (l != null) {
                            logs = l.ToList();
                        }
                        else {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                List<LogsViewModel> lvm = this.viewModelFactory.GetViewModel(logs);
                return PartialView("_Index", lvm);
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Could not complete the operation!";
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed!";
                return View("Error");
            }
        }


        #region Users: View-Search-Details-Block-Unblock-ResetPassword-ImportUsers

        //
        // GET: /Admin/DisplayUsers

        public ActionResult DisplayUsers() {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                List<Users> users = new List<Users>();

                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/user/GetAllUsers").Result;
                    if (response.IsSuccessStatusCode) {
                        IEnumerable<Users> list = response.Content.ReadAsAsync<IEnumerable<Users>>().Result;
                        if (list != null) {
                            users = list.ToList();
                        }
                        else {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                List<UserProfile> userprofiles = this.viewModelFactory.GetViewModel(users);
                return View(userprofiles);
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
        // GET: /Admin/SearchUser(UserName/LastName, string)

        public ActionResult SearchUser(string searchTerm, string searchString) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                List<Users> users = new List<Users>();
                string searchType = Request["searchTerm"];
                if (!string.IsNullOrWhiteSpace(searchString)) {

                    if (searchType.Equals("UserName")) {
                        //username is unique, so only one result is returned
                        using (var client = new HttpClient()) {
                            client.BaseAddress = new Uri(this.apiMethodsUrl);
                            client.DefaultRequestHeaders.Accept.Add(
                                new MediaTypeWithQualityHeaderValue("application/json")
                                );
                            HttpResponseMessage response = client.GetAsync("api/user/GetUserByUserName/?username=" + searchString).Result;
                            if (response.IsSuccessStatusCode) {
                                Users user = response.Content.ReadAsAsync<Users>().Result;
                                if (user != null) {
                                    users.Add(user);
                                }
                                else {
                                    throw new CustomException("Could not complete the operation!");
                                }
                            }
                            else {
                                throw new CustomException("Could not complete the operation!");
                            }
                        }
                    }
                    else if (searchType.Equals("LastName")) {
                        //last name is not necessarily unique, so a list of users may be returned
                        using (var client = new HttpClient()) {
                            client.BaseAddress = new Uri(this.apiMethodsUrl);
                            client.DefaultRequestHeaders.Accept.Add(
                                new MediaTypeWithQualityHeaderValue("application/json")
                                );
                            HttpResponseMessage response = client.GetAsync("api/user/GetUserByLastName/?lastName=" + searchString).Result;
                            if (response.IsSuccessStatusCode) {
                                IEnumerable<Users> list = response.Content.ReadAsAsync<IEnumerable<Users>>().Result;
                                if (list != null) {
                                    users = list.ToList();
                                }
                                else {
                                    throw new CustomException("Could not complete the operation!");
                                }
                            }
                            else {
                                throw new CustomException("Could not complete the operation!");
                            }
                        }
                    }
                }
                List<UserProfile> upl = this.viewModelFactory.GetViewModel(users);
                return View("DisplayUsers", upl);
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
        // GET: /Admin/UserDetails(id = 0)

        public ActionResult UserDetails(int id = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                Users user = new Users();

                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/user/GetUserById/?id=" + id).Result;
                    if (response.IsSuccessStatusCode) {
                        var u = response.Content.ReadAsAsync<Users>().Result;
                        if (u != null) {
                            user.AccessStatus = u.AccessStatus;
                            user.Email = u.Email;
                            user.FirstName = u.FirstName;
                            user.LastName = u.LastName;
                            user.MiddleName = u.MiddleName;
                            user.StudentIdentificationNumber = u.StudentIdentificationNumber;
                            user.UserId = u.UserId;
                            user.UserName = u.UserName;
                        }
                        else {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                UserProfile userprofile = this.viewModelFactory.GetViewModel(user);
                return View(userprofile);
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Could not find user!";
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed!";
                return View("Error");
            }
        }

        //
        // POST: /Admin/ResetPassword(id)
        [HttpPost]
        public ActionResult ResetPassword(int id) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                //get the user whose password needs changing
                Users user = new Users();
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/user/GetUserById/?id=" + id).Result;
                    if (response.IsSuccessStatusCode) {
                        var u = response.Content.ReadAsAsync<Users>().Result;
                        if (u != null) {
                            user.AccessStatus = u.AccessStatus;
                            user.Email = u.Email;
                            user.FirstName = u.FirstName;
                            user.LastName = u.LastName;
                            user.MiddleName = u.MiddleName;
                            user.StudentIdentificationNumber = u.StudentIdentificationNumber;
                            user.UserId = u.UserId;
                            user.UserName = u.UserName;
                        }
                        else {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                //reset the users password
                string resetToken = WebSecurity.GeneratePasswordResetToken(user.UserName, 1);
                string newPassword = PasswordGenerator.GetNewPassword(7);
                WebSecurity.ResetPassword(resetToken, newPassword);

                //get the current admins email address
                Users admin = new Users();
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/user/GetUserByUserName/" + User.Identity.Name).Result;
                    if (response.IsSuccessStatusCode) {
                        var u = response.Content.ReadAsAsync<Users>().Result;
                        if (u != null) {
                            admin.AccessStatus = u.AccessStatus;
                            admin.Email = u.Email;
                            admin.FirstName = u.FirstName;
                            admin.LastName = u.LastName;
                            admin.MiddleName = u.MiddleName;
                            admin.StudentIdentificationNumber = u.StudentIdentificationNumber;
                            admin.UserId = u.UserId;
                            admin.UserName = u.UserName;
                        }
                        else {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                this.mailUtil.SendEmail(user.Email, "E-Learning application: Account details", "The application admin has reset the password for this account." + "Username:" + user.UserName + ". Password:" + newPassword + ". Please try accessing your account. If you encounter trouble please contact the admin at: " + admin.Email);
                return RedirectToAction("DisplayUsers");
            }
            catch (ArgumentException ae) {
                this.logger.Trace(ae, "Username: " + User.Identity.Name);
                ViewBag.Error = "Could not find user!";
                return View("Error");
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Could not find user!";
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed!";
                return View("Error");
            }
        }

        //
        // POST: /Admin/BlockUser(id = 0)
        [HttpPost]
        public ActionResult BlockUser(int id = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                //get the user to be blocked
                Users user = new Users();
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/user/GetUserById/?id=" + id).Result;
                    if (response.IsSuccessStatusCode) {
                        var u = response.Content.ReadAsAsync<Users>().Result;
                        if (u != null) {
                            user.AccessStatus = u.AccessStatus;
                            user.Email = u.Email;
                            user.FirstName = u.FirstName;
                            user.LastName = u.LastName;
                            user.MiddleName = u.MiddleName;
                            user.StudentIdentificationNumber = u.StudentIdentificationNumber;
                            user.UserId = u.UserId;
                            user.UserName = u.UserName;
                        }
                        else {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                //block the user if it's not the currently logged in user
                if (user.UserName != User.Identity.Name) {
                    using (var client = new HttpClient()) {
                        client.BaseAddress = new Uri(this.apiMethodsUrl);
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json")
                            );
                        UsersDTO bo = new UsersDTO {
                            AccessStatus = "Blocked",
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            MiddleName = user.MiddleName,
                            StudentIdentificationNumber = user.StudentIdentificationNumber,
                            UserId = user.UserId,
                            UserName = user.UserName
                        };
                        HttpResponseMessage response = client.PutAsJsonAsync("api/user/UpdateUser?id=" + id, bo).Result;
                        if (!response.IsSuccessStatusCode) {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                }
                return RedirectToAction("DisplayUsers");
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Could not find user!";
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed!";
                return View("Error");
            }
        }

        //
        // POST: /Admin/BlockUser(id = 0)
        [HttpPost]
        public ActionResult UnblockUser(int id = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                //get the user to be blocked
                Users user = new Users();
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/user/GetUserById/?id=" + id).Result;
                    if (response.IsSuccessStatusCode) {
                        var u = response.Content.ReadAsAsync<Users>().Result;
                        if (u != null) {
                            user.AccessStatus = u.AccessStatus;
                            user.Email = u.Email;
                            user.FirstName = u.FirstName;
                            user.LastName = u.LastName;
                            user.MiddleName = u.MiddleName;
                            user.StudentIdentificationNumber = u.StudentIdentificationNumber;
                            user.UserId = u.UserId;
                            user.UserName = u.UserName;
                        }
                        else {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                //block the user if it's not the currently logged in user
                if (user.UserName != User.Identity.Name) {
                    using (var client = new HttpClient()) {
                        client.BaseAddress = new Uri(this.apiMethodsUrl);
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json")
                            );
                        UsersDTO bo = new UsersDTO {
                            AccessStatus = "Unblocked",
                            Email = user.Email,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            MiddleName = user.MiddleName,
                            StudentIdentificationNumber = user.StudentIdentificationNumber,
                            UserId = user.UserId,
                            UserName = user.UserName
                        };
                        HttpResponseMessage response = client.PutAsJsonAsync("api/user/UpdateUser?id=" + id, bo).Result;
                        if (!response.IsSuccessStatusCode) {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                }
                return RedirectToAction("DisplayUsers");
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Could not find user!";
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed!";
                return View("Error");
            }
        }

        //
        // GET: /Admin/ImportUsers
        public ActionResult ImportUsers() {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);

            return View();
        }

        //
        // POST: /Admin/ImportUsers(file)

        [HttpPost]
        public ActionResult ImportUsers(HttpPostedFileBase file) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                string userRole = Request.Form["userRole"].ToString();
                if (file != null && file.ContentLength > 0) {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/UploadedFiles"), fileName);
                    file.SaveAs(path);
                    using (StreamReader sr = new StreamReader(path)) {
                        string csvLine;
                        while ((csvLine = sr.ReadLine()) != null) {
                            RegisterUser(csvLine, userRole);
                        }
                    }
                }
                return RedirectToAction("DisplayUsers");
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed!";
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed!";
                return View("Error");
            }

        }

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="csvLine">The CSV line.</param>
        /// <param name="userRole">The user role.</param>
        /// <exception cref="CustomException">
        /// Insufficient fields to create a user
        /// or
        /// </exception>
        private void RegisterUser(string csvLine, string userRole) {
            try {
                string[] userFields = csvLine.Split(',');
                if (userFields.Length != 6)
                    throw new CustomException("Insufficient fields to create a user");
                string password = PasswordGenerator.GetNewPassword(6);
                //register new user
                RegisterModel model = new RegisterModel() { UserName = userFields[0], FirstName = userFields[1], LastName = userFields[2], MiddleName = userFields[3], Email = userFields[4], StudentIdentificationNumber = userFields[5], Password = password };
                WebMatrix.WebData.WebSecurity.CreateUserAndAccount(model.UserName, model.Password, propertyValues: new {
                    LastName = model.LastName,
                    FirstName = model.FirstName,
                    MiddleName = model.MiddleName,
                    Email = model.Email,
                    StudentIdentificationNumber = model.StudentIdentificationNumber
                });
                //assign chosen role to user
                Roles.AddUserToRole(userFields[0], userRole);
                //notify user by mail
                this.mailUtil.SendEmail(model.Email, "Account detail", "A new password was created for your Elearning platform account. Username:" + model.UserName + " Password:" + model.Password);
            }
            catch (InvalidOperationException ioe) {
                throw new CustomException(ioe.Message);
            }
        }

        #endregion


        #region Logs: View-Search-Delete-Details

        //
        // GET: /Admin/DisplayLogs

        public ActionResult DisplayLogs() {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                List<Logs> logs = new List<Logs>();

                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/admin/GetAllLogs").Result;
                    if (response.IsSuccessStatusCode) {
                        IEnumerable<Logs> l = response.Content.ReadAsAsync<IEnumerable<Logs>>().Result;
                        if (l != null) {
                            logs = l.ToList();
                        }
                        else {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                List<LogsViewModel> lvm = this.viewModelFactory.GetViewModel(logs);
                return View(lvm);
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Could not complete the operation!";
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed!";
                return View("Error");
            }
        }

        //
        // GET: /Admin/SearchLog(eventDatetime, eventLevel, eventInfo)
        public ActionResult SearchLog(string searchEventDatetime, string searchEventLevel, string searchEventInfo) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                List<Logs> logs = new List<Logs>();

                //if admin wants to see the logs from a certain date and a certain level
                if (!string.IsNullOrWhiteSpace(searchEventLevel) && !string.IsNullOrWhiteSpace(searchEventDatetime)) {
                    //convert datetime from string to DateTime
                    string[] temp = searchEventDatetime.Split('/');
                    DateTime datetime = new DateTime(Int32.Parse(temp[2]), Int32.Parse(temp[0]), Int32.Parse(temp[1]));

                    List<Logs> logDates = new List<Logs>();
                    using (var client = new HttpClient()) {
                        client.BaseAddress = new Uri(this.apiMethodsUrl);
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json")
                            );
                        HttpResponseMessage response = client.GetAsync("api/admin/GetLogsByEventDate/?eventDatetime=" + datetime).Result;
                        if (response.IsSuccessStatusCode) {
                            var l = response.Content.ReadAsAsync<IEnumerable<Logs>>().Result;
                            if (l != null) {
                                logDates = l.ToList();
                            }
                            else {
                                throw new CustomException("Fail.");
                            }
                        }
                        else {
                            throw new CustomException("Fail.");
                        }
                    }
                    foreach (var l in logDates) {
                        //if admin wants to see only the info logged by a certain user
                        if (!string.IsNullOrWhiteSpace(searchEventInfo)) {
                            if (l.EventInfo.Contains(searchEventInfo) && l.EventLevel.ToLower().Equals(searchEventLevel.ToLower()))
                                logs.Add(l);
                        }
                        else {
                            if (l.EventLevel.ToLower().Equals(searchEventLevel.ToLower()))
                                logs.Add(l);
                        }
                    }
                }
                else {
                    //if admin wants to see the logs only from a certain date
                    if (!string.IsNullOrWhiteSpace(searchEventDatetime)) {

                        string[] temp = searchEventDatetime.Split('/');
                        DateTime datetime = new DateTime(Int32.Parse(temp[2]), Int32.Parse(temp[0]), Int32.Parse(temp[1]));
                        List<Logs> logDates = new List<Logs>();

                        using (var client = new HttpClient()) {
                            client.BaseAddress = new Uri(this.apiMethodsUrl);
                            client.DefaultRequestHeaders.Accept.Add(
                                new MediaTypeWithQualityHeaderValue("application/json")
                                );
                            HttpResponseMessage response = client.GetAsync("api/admin/GetLogsByEventDate/?eventDatetime=" + datetime).Result;
                            if (response.IsSuccessStatusCode) {
                                var l = response.Content.ReadAsAsync<IEnumerable<Logs>>().Result;
                                if (l != null) {
                                    logDates = l.ToList();
                                }
                                else {
                                    throw new CustomException("Fail.");
                                }
                            }
                            else {
                                throw new CustomException("Fail.");
                            }
                        }
                        foreach (var l in logDates) {
                            if (!string.IsNullOrWhiteSpace(searchEventInfo)) {
                                if (l.EventInfo.Contains(searchEventInfo))
                                    logs.Add(l);
                            }
                            else {
                                logs.Add(l);
                            }
                        }
                    }
                    else {
                        //if admin wants to see the logs only from a certain level and/or a certain user that logged it
                        if (!string.IsNullOrWhiteSpace(searchEventLevel)) {
                            List<Logs> logLevels = new List<Logs>();

                            using (var client = new HttpClient()) {
                                client.BaseAddress = new Uri(this.apiMethodsUrl);
                                client.DefaultRequestHeaders.Accept.Add(
                                    new MediaTypeWithQualityHeaderValue("application/json")
                                    );
                                HttpResponseMessage response = client.GetAsync("api/admin/GetLogsByEventLevel/?eventLevel=" + searchEventLevel).Result;
                                if (response.IsSuccessStatusCode) {
                                    var l = response.Content.ReadAsAsync<IEnumerable<Logs>>().Result;
                                    if (l != null) {
                                        logLevels = l.ToList();
                                    }
                                    else {
                                        throw new CustomException("Fail.");
                                    }
                                }
                                else {
                                    throw new CustomException("Fail.");
                                }
                            }

                            foreach (var l in logLevels) {
                                if (!string.IsNullOrWhiteSpace(searchEventInfo)) {
                                    if (l.EventInfo.Contains(searchEventInfo))
                                        logs.Add(l);
                                }
                                else {
                                    logs.Add(l);
                                }
                            }
                        }
                        else {
                            //search logs by event info -> the user that logged an event
                            if (!string.IsNullOrWhiteSpace(searchEventInfo)) {
                                List<Logs> logUsers = new List<Logs>();

                                using (var client = new HttpClient()) {
                                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                                    client.DefaultRequestHeaders.Accept.Add(
                                        new MediaTypeWithQualityHeaderValue("application/json")
                                        );
                                    HttpResponseMessage response = client.GetAsync("api/admin/GetLogsByEventInfo/?eventInfo=" + searchEventInfo).Result;
                                    if (response.IsSuccessStatusCode) {
                                        var l = response.Content.ReadAsAsync<IEnumerable<Logs>>().Result;
                                        if (l != null) {
                                            logUsers = l.ToList();
                                        }
                                        else {
                                            throw new CustomException("Fail.");
                                        }
                                    }
                                    else {
                                        throw new CustomException("Fail.");
                                    }
                                }

                                foreach (var l in logUsers) {
                                    logs.Add(l);
                                }
                            }
                        }
                    }
                }

                List<LogsViewModel> lvm = this.viewModelFactory.GetViewModel(logs);
                return View("DisplayLogs", lvm);
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
        // POST: /Admin/DeleteLog(id = 0)

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteLog(int id = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.DeleteAsync("api/admin/DeleteLogById/?id=" + id).Result;
                    if (response.IsSuccessStatusCode) {
                        return RedirectToAction("DisplayLogs");
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
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
        // POST: /Admin/DeleteAllLogs()

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteAllLogs() {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.DeleteAsync("api/admin/DeleteAllDisplayedLogs").Result;
                    if (response.IsSuccessStatusCode) {
                        return RedirectToAction("DisplayLogs");
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
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
        // GET: /Admin/LogDetails(id = 0)

        public ActionResult LogDetails(int id = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                using (var db = new ELearningDatabaseEntitiesServer()) {
                    Logs log = new Logs();

                    using (var client = new HttpClient()) {
                        client.BaseAddress = new Uri(this.apiMethodsUrl);
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json")
                            );
                        HttpResponseMessage response = client.GetAsync("api/admin/GetLogById/?id=" + id).Result;
                        if (response.IsSuccessStatusCode) {
                            log = response.Content.ReadAsAsync<Logs>().Result;
                            if (log == null) {
                                throw new CustomException("Fail.");
                            }
                        }
                        else {
                            throw new CustomException("Fail.");
                        }
                    }

                    LogsViewModel lvm = this.viewModelFactory.GetViewModel(log);
                    return View(lvm);
                }
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed!";
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
