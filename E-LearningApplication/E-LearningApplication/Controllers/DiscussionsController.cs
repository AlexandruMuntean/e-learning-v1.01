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
    [Authorize]
    public class DiscussionsController : Controller {
        #region Private fields

        //private readonly string apiMethodsUrl = "https://elearningservices.azurewebsites.net/";
        private readonly string apiMethodsUrl = "http://localhost:42175/";
        private ILoggingUtil logger = new NLogLogger();
        private IViewModelFactory viewModelFactory = new ViewModelFactory();
        private IMailUtil mailUtil = new MailUtil();

        #endregion

        #region Forums
        //
        // GET: /Discussions/Forum
        public ActionResult Forum() {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                List<Forums> forums = new List<Forums>();
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/discussions/GetAllForums").Result;
                    if (response.IsSuccessStatusCode) {
                        var list = response.Content.ReadAsAsync<IEnumerable<Forums>>().Result;
                        if (list != null) {
                            forums = list.ToList();
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                List<ForumsViewModel> fvml = this.viewModelFactory.GetViewModel(forums);
                return View(fvml);
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.";
                return View("Error");
            }
        }

        //
        // GET: /Discussions/CreateForum

        public ActionResult CreateForum() {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            return View();
        }

        //
        // POST: /Discussions/CreateForum(forum)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateForum(ForumsViewModel forumViewModel) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                var _userId = Session["UserId"];
                var _sessionUser = Convert.ToInt32(_userId);

                ForumDTO dto = new ForumDTO {
                    ForumId = forumViewModel.ForumId,
                    Category = forumViewModel.Category,
                    OwnerId = _sessionUser
                };

                //add new forum
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    //add to db
                    HttpResponseMessage response = client.PostAsJsonAsync("api/discussions/AddForum/?forum=", dto).Result;
                    if (!response.IsSuccessStatusCode) {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                return RedirectToAction("Forum");
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.\n" + ce.Message;
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed!";
                return View("Error");
            }
        }

        //
        // GET: /Discussions/EditForum(id = 0)

        public ActionResult EditForum(int id = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                //get the forum to be edited
                Forums forum = new Forums();
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/discussions/GetForumById/?id=" + id).Result;
                    if (response.IsSuccessStatusCode) {
                        var f = response.Content.ReadAsAsync<Forums>().Result;
                        if (f != null) {
                            forum.ForumId = f.ForumId;
                            forum.Category = f.Category;
                            forum.OwnerId = f.OwnerId;
                        }
                        else {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                ForumsViewModel fvm = this.viewModelFactory.GetViewModel(forum);
                return View(fvm);
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
        // POST: /Discussions/EditForum(forum)

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditForum(ForumsViewModel forumViewModel) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                //create the course dto to be passed to the api services
                ForumDTO dto = new ForumDTO {
                    ForumId = forumViewModel.ForumId,
                    Category = forumViewModel.Category,
                    OwnerId = forumViewModel.OwnerId
                };

                //edit the forum
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.PutAsJsonAsync("api/discussions/UpdateForum/?id=" + forumViewModel.ForumId, dto).Result;
                    if (!response.IsSuccessStatusCode) {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                return RedirectToAction("Forum");
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
        // GET: /Discussions/DeleteForum(id = 0)

        public ActionResult DeleteForum(int id = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                //delete the forum
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.DeleteAsync("api/discussions/DeleteForum/?id=" + id).Result;
                    if (!response.IsSuccessStatusCode) {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                return RedirectToAction("Forum");
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

        #region Comments

        //
        // GET: /Discussions/DisplayComments(id)
        public ActionResult DisplayComments(int id = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                Forums forum = new Forums();
                List<Messages> messages = new List<Messages>();

                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    //get the forum containing the comments
                    HttpResponseMessage responseForum = client.GetAsync("api/discussions/GetForumById/?id=" + id).Result;
                    //get the comments in the forum
                    HttpResponseMessage responseComments = client.GetAsync("api/discussions/GetAllMessages/?forumId=" + id).Result;
                    if (responseForum.IsSuccessStatusCode && responseComments.IsSuccessStatusCode) {
                        var f = responseForum.Content.ReadAsAsync<Forums>().Result;
                        if (f != null) {
                            forum.ForumId = f.ForumId;
                            forum.Category = f.Category;
                            forum.OwnerId = f.OwnerId;
                        }
                        else {
                            throw new CustomException("Could not complete the operation!");
                        }
                        var list = responseComments.Content.ReadAsAsync<IEnumerable<Messages>>().Result;
                        if (list != null) {
                            messages = list.ToList();
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                AllMessagesViewModel amvm = this.viewModelFactory.GetViewModel(forum, messages);
                return View(amvm);
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
        // GET: /Discussions/AddComment()

        public ActionResult AddComment() {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            return View();
        }

        //
        // POST: /Discussions/AddComment(message)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddComment(MessagesViewModel messageViewModel, int id = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                var _userId = Session["UserId"];
                var _sessionUser = Convert.ToInt32(_userId);

                MessageDTO dto = new MessageDTO();
                dto.MessageId = messageViewModel.MessageId;
                dto.MessageContent = messageViewModel.MessageContent;
                dto.ForumId = id;
                dto.MesageData = System.DateTime.Now;
                dto.UserId = _sessionUser;

                //add new message
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.PostAsJsonAsync("api/discussions/AddMessage/?message=", dto).Result;
                    if (!response.IsSuccessStatusCode) {
                        throw new CustomException("Could not complete the operation!");
                    }
                }
                
                return RedirectToAction("DisplayComments", new { id = id });
            }
            catch (CustomException ce) {
                this.logger.Trace(ce, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed! Try again.\n" + ce.Message;
                return View("Error");
            }
            catch (Exception ex) {
                this.logger.Trace(ex, "Username: " + User.Identity.Name);
                ViewBag.Error = "Operation could not be completed!\n" + ex.Message;
                return View("Error");
            }
        }

        //
        // GET: /Discussions/EditComment(id = 0)

        public ActionResult EditComment(int id = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                Messages message = new Messages();

                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/discussions/GetMessageById/?id=" + id).Result;
                    if (response.IsSuccessStatusCode) {
                        var m = response.Content.ReadAsAsync<Messages>().Result;
                        if (m != null) {
                            message.MessageId = m.MessageId;
                            message.MessageContent = m.MessageContent;
                            message.MesageData = m.MesageData;
                            message.ConversationId = m.ConversationId;
                            message.DiscusionId = m.DiscusionId;
                            message.ForumId = m.ForumId;
                            message.UserId = m.UserId;
                        }
                        else {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                MessagesViewModel mvm = this.viewModelFactory.GetViewModel(message);
                return View(mvm);
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
        // POST: /Discussions/EditComment(message)

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditComment(MessagesViewModel messageViewModel, int id = 0, int idForum = 0, int userId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                MessageDTO dto = new MessageDTO { 
                    MessageId = messageViewModel.MessageId,
                    MessageContent = messageViewModel.MessageContent,
                    MesageData = System.DateTime.Now,
                    UserId = messageViewModel.user.UserId,
                    ForumId = idForum
                };

                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.PutAsJsonAsync("api/discussions/UpdateMessage/?id=" + messageViewModel.MessageId, dto).Result;
                    if (!response.IsSuccessStatusCode) {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                return RedirectToAction("DisplayComments", new { id = idForum});
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
        // GET: /Discussions/DeleteComment(id = 0)

        public ActionResult DeleteComment(int id = 0, int idForum = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.DeleteAsync("api/discussions/DeleteMessage/?id=" + id).Result;
                    if (!response.IsSuccessStatusCode) {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                return RedirectToAction("DisplayComments", new { id = idForum});
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

        #region Mails

        //
        // GET: /Discussions/ComposeMail
        public ActionResult ComposeMail() {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            return View();
        }

        //
        // POST: /Discussions/SendMail(mailViewModel)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendMail(MailViewModel model) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                if (ModelState.IsValid) {
                    List<string> receivers = model.Receivers.Split(';').ToList();
                    foreach (string r in receivers) {
                        this.mailUtil.SendEmail(r, model.MailSubject, model.MailBody, model.Sender);
                    }
                    return View("Index");
                }
                else {
                    return View("ComposeMail", model);
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

        #endregion


    }
}
