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
    public class GroupsController : Controller {
        #region Private fields

        //private readonly string apiMethodsUrl = "https://elearningservices.azurewebsites.net/";
        private readonly string apiMethodsUrl = "http://localhost:42175/";
        private ILoggingUtil logger = new NLogLogger();
        private IViewModelFactory viewModelFactory = new ViewModelFactory();

        #endregion

        //
        // GET: /Groups/

        public ActionResult Index() {
            return View();
        }

        #region Groups

        //
        // GET: /Groups/DisplayAllUnassociatedGroups()

        public ActionResult DisplayAllGroups() {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                var _userId = Session["UserId"];
                var _sessionUser = Convert.ToInt32(_userId);

                List<Groups> groups = new List<Groups>();
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/groups/GetAllUnassociatedGroups/?id=" + _sessionUser).Result;
                    if (response.IsSuccessStatusCode) {
                        var list = response.Content.ReadAsAsync<IEnumerable<Groups>>().Result;
                        if (list != null) {
                            groups = list.ToList();
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                List<GroupsViewModel> gvm = this.viewModelFactory.GetViewModel(groups);
                return View(gvm);
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
        // GET: /Groups/DisplayAssociatedGroups()

        public ActionResult DisplayAssociatedGroups() {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                //get all associated groups to the currently logged in user
                List<Groups> groups = new List<Groups>();

                var _userId = Session["UserId"];
                var _sessionUser = Convert.ToInt32(_userId);

                #region get associated groups
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/groups/GetAssociatedGroups/?userId=" + _sessionUser).Result;
                    if (response.IsSuccessStatusCode) {
                        var list = response.Content.ReadAsAsync<IEnumerable<Groups>>().Result;
                        if (list != null) {
                            groups = list.ToList();
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }
                #endregion

                List<GroupsViewModel> gvm = this.viewModelFactory.GetViewModel(groups);
                return View(gvm);
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
        // GET: /Groups/SearchAllGroups(groupName/groupDescription)

        public ActionResult SearchAllGroups(string searchString) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                List<Groups> groups = new List<Groups>();
                if (!string.IsNullOrWhiteSpace(searchString)) {
                    using (var client = new HttpClient()) {
                        client.BaseAddress = new Uri(this.apiMethodsUrl);
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json")
                            );
                        HttpResponseMessage response = client.GetAsync("api/groups/GetGroupByNameOrDescription/?searchTerm=" + searchString).Result;
                        if (response.IsSuccessStatusCode) {
                            var list = response.Content.ReadAsAsync<IEnumerable<Groups>>().Result;
                            if (list != null) {
                                groups = list.ToList();
                            }
                        }
                        else {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                }
                List<GroupsViewModel> gvm = this.viewModelFactory.GetViewModel(groups);
                return View("DisplayAllGroups", gvm);
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
        // GET: /Groups/SearchAssociatedGroups(groupName/groupDescription)

        public ActionResult SearchAssociatedGroups(string searchString) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                List<Groups> groups = new List<Groups>();
                if (!string.IsNullOrWhiteSpace(searchString)) {
                    using (var client = new HttpClient()) {
                        client.BaseAddress = new Uri(this.apiMethodsUrl);
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json")
                            );
                        HttpResponseMessage response = client.GetAsync("api/groups/GetGroupByNameOrDescription/?searchTerm=" + searchString).Result;
                        if (response.IsSuccessStatusCode) {
                            var list = response.Content.ReadAsAsync<IEnumerable<Groups>>().Result;
                            if (list != null) {
                                groups = list.ToList();
                            }
                        }
                        else {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                }
                List<GroupsViewModel> gvm = this.viewModelFactory.GetViewModel(groups);
                return View("DisplayAssociatedGroups", gvm);
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
        // GET: /Groups/CreateGroup()

        public ActionResult CreateGroup() {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            return View();
        }

        //
        // POST: /Groups/CreateGroup(groupViewModel)

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGroup(GroupsViewModel groupViewModel) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                var _userId = Session["UserId"];
                var _sessionUser = Convert.ToInt32(_userId);

                Groups group = new Groups();
                #region create new group

                //create the dto for the new group
                GroupDTO dto = new GroupDTO();
                dto.GroupDescription = groupViewModel.GroupDescription;
                dto.GroupName = groupViewModel.GroupName;
                dto.GroupType = groupViewModel.GroupType;
                dto.OwnerId = _sessionUser;

                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.PostAsJsonAsync("api/groups/AddGroup/?group=", dto).Result;
                    if (response.IsSuccessStatusCode) {
                        var g = response.Content.ReadAsAsync<Groups>().Result;
                        if (g != null) {
                            group.GroupDescription = g.GroupDescription;
                            group.GroupId = g.GroupId;
                            group.GroupName = g.GroupName;
                            group.GroupType = g.GroupType;
                            group.OwnerId = g.OwnerId;
                        }
                        else {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }

                }

                #endregion

                #region subscribe to created group

                //create dto for the new member
                GroupMemberDTO dto1 = new GroupMemberDTO();
                dto1.GroupId = group.GroupId;
                dto1.MemberId = _sessionUser;

                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.PostAsJsonAsync("api/groups/AddGroupMember/?groupMember=", dto1).Result;
                    if (!response.IsSuccessStatusCode) {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                #endregion

                return RedirectToAction("DisplayAssociatedGroups");
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
        // GET: /Groups/GroupDetails(id = 0)

        public ActionResult GroupDetails(int id = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                //get the group to be edited
                Groups group = new Groups();
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/groups/GetGroupById/?id=" + id).Result;
                    if (response.IsSuccessStatusCode) {
                        var g = response.Content.ReadAsAsync<Groups>().Result;
                        if (g != null) {
                            group.GroupDescription = g.GroupDescription;
                            group.GroupId = g.GroupId;
                            group.GroupName = g.GroupName;
                            group.GroupType = g.GroupType;
                            group.OwnerId = g.OwnerId;
                        }
                        else {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                GroupsViewModel gvm = this.viewModelFactory.GetViewModel(group);
                return View(gvm);
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
        // GET: /Groups/EditGroup(id = 0)

        public ActionResult EditGroup(int id = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                //get the group to be edited
                Groups group = new Groups();
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/groups/GetGroupById/?id=" + id).Result;
                    if (response.IsSuccessStatusCode) {
                        var g = response.Content.ReadAsAsync<Groups>().Result;
                        if (g != null) {
                            group.GroupDescription = g.GroupDescription;
                            group.GroupId = g.GroupId;
                            group.GroupName = g.GroupName;
                            group.GroupType = g.GroupType;
                            group.OwnerId = g.OwnerId;
                        }
                        else {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                GroupsViewModel gvm = this.viewModelFactory.GetViewModel(group);
                return View(gvm);
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
        // POST: /Groups/EditGroup(groupViewModel)

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditGroup(GroupsViewModel groupViewModel) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                //create the group dto to be passed to the api services
                GroupDTO dto = new GroupDTO();
                dto.GroupDescription = groupViewModel.GroupDescription;
                dto.GroupId = groupViewModel.GroupId;
                dto.GroupName = groupViewModel.GroupName;
                dto.GroupType = groupViewModel.GroupType;
                dto.OwnerId = groupViewModel.OwnerId;

                //edit the group
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.PutAsJsonAsync("api/groups/UpdateGroup/?id=" + groupViewModel.GroupId, dto).Result;
                    if (!response.IsSuccessStatusCode) {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                return RedirectToAction("DisplayAssociatedGroups");
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
        // POST: /Groups/DeleteGroup(id = 0)

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteGroup(int id = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                //delete the group
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.DeleteAsync("api/groups/DeleteGroup/?id=" + id).Result;
                    if (!response.IsSuccessStatusCode) {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                return RedirectToAction("DisplayAssociatedGroups");
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

        #region GroupMembers

        //
        // GET: /Groups/DisplayGroupMembers(id = 0)

        public ActionResult DisplayGroupMembers(int id = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                List<Users> groupMembers = new List<Users>();
                using (var client = new HttpClient()) {
                    client.BaseAddress = new Uri(this.apiMethodsUrl);
                    client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                        );
                    HttpResponseMessage response = client.GetAsync("api/groups/GetAllGroupMembers/?groupId=" + id).Result;
                    if (response.IsSuccessStatusCode) {
                        var list = response.Content.ReadAsAsync<IEnumerable<Users>>().Result;
                        if (list != null) {
                            groupMembers = list.ToList();
                        }
                    }
                    else {
                        throw new CustomException("Could not complete the operation!");
                    }
                }

                Tuple<List<UserProfile>, int> viewModel = new Tuple<List<UserProfile>, int>(
                    this.viewModelFactory.GetViewModel(groupMembers),
                    id
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
        // GET: /Groups/AddMemberToGroup(groupId = 0)

        public ActionResult AddMemberToGroup(int groupId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                if (groupId > 0) {
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
                    Tuple<List<UserProfile>, int> viewModel = new Tuple<List<UserProfile>, int>(
                        this.viewModelFactory.GetViewModel(users),
                        groupId
                        );

                    return View(viewModel);
                }
                else {
                    this.logger.Trace("Specified group was not found! --> Username: " + User.Identity.Name);
                    ViewBag.Error = "Specified group was not found!";
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
        // POST: /Groups/AddMemberToGroup(groupId)

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMember(int groupId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                int userId = Int32.Parse(Request["newMember"]);
                if (groupId > 0 && userId > 0) {
                    //create dto for the new member
                    GroupMemberDTO dto = new GroupMemberDTO();
                    dto.GroupId = groupId;
                    dto.MemberId = userId;

                    using (var client = new HttpClient()) {
                        client.BaseAddress = new Uri(this.apiMethodsUrl);
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json")
                            );
                        HttpResponseMessage response = client.PostAsJsonAsync("api/groups/AddGroupMember/?groupMember=", dto).Result;
                        if (!response.IsSuccessStatusCode) {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                    return RedirectToAction("DisplayGroupMembers", new { id = groupId });
                }
                else {
                    this.logger.Trace("Operation could not be completed! Try again. --> Username: " + User.Identity.Name);
                    ViewBag.Error = "Operation could not be completed! Try again.";
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
        // POST: /Groups/RemoveMemberFromGroup(groupId = 0, userId = 0)

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveMemberFromGroup(int groupId = 0, int userId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                if (groupId > 0 && userId > 0) {
                    using (var client = new HttpClient()) {
                        client.BaseAddress = new Uri(this.apiMethodsUrl);
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json")
                            );
                        HttpResponseMessage response = client.DeleteAsync("api/groups/RemoveGroupMember/?groupId=" + groupId + "&userId=" + userId).Result;
                        if (!response.IsSuccessStatusCode) {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                    return RedirectToAction("DisplayGroupMembers", new { id = groupId });
                }
                else {
                    this.logger.Trace("Operation could not be completed! Try again. --> Username: " + User.Identity.Name);
                    ViewBag.Error = "Operation could not be completed! Try again.";
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
        // POST: /Groups/SubscribeToGroup(groupId = 0)

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SubscribeToGroup(int groupId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                if (groupId > 0) {
                    var _userId = Session["UserId"];
                    var _sessionUser = Convert.ToInt32(_userId);

                    //create dto for the new member
                    GroupMemberDTO dto = new GroupMemberDTO();
                    dto.GroupId = groupId;
                    dto.MemberId = _sessionUser;

                    using (var client = new HttpClient()) {
                        client.BaseAddress = new Uri(this.apiMethodsUrl);
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json")
                            );
                        HttpResponseMessage response = client.PostAsJsonAsync("api/groups/AddGroupMember/?groupMember=", dto).Result;
                        if (!response.IsSuccessStatusCode) {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                    return RedirectToAction("DisplayAssociatedGroups");
                }
                else {
                    this.logger.Trace("Operation could not be completed! Try again. --> Username: " + User.Identity.Name);
                    ViewBag.Error = "Operation could not be completed! Try again.";
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
        // POST: /Groups/UnsubscribeFromGroup(groupId = 0)

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UnsubscribeFromGroup(int groupId = 0) {
            this.logger.Info("Entering: " + System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.FullName + ": " + System.Reflection.MethodBase.GetCurrentMethod().Name + " --> " + User.Identity.Name);
            try {
                if (groupId > 0) {
                    var _userId = Session["UserId"];
                    var _sessionUser = Convert.ToInt32(_userId);

                    using (var client = new HttpClient()) {
                        client.BaseAddress = new Uri(this.apiMethodsUrl);
                        client.DefaultRequestHeaders.Accept.Add(
                            new MediaTypeWithQualityHeaderValue("application/json")
                            );
                        HttpResponseMessage response = client.DeleteAsync("api/groups/RemoveGroupMember/?groupId=" + groupId + "&userId=" + _sessionUser).Result;
                        if (!response.IsSuccessStatusCode) {
                            throw new CustomException("Could not complete the operation!");
                        }
                    }
                    return RedirectToAction("DisplayAssociatedGroups");
                }
                else {
                    this.logger.Trace("Operation could not be completed! Try again. --> Username: " + User.Identity.Name);
                    ViewBag.Error = "Operation could not be completed! Try again.";
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

        #endregion

    }
}
