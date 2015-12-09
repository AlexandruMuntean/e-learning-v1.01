using E_LearningApplication.CustomExceptions;
using E_LearningApplication.Models;
using E_LearningApplication.Models.ViewModels;
using E_LearningApplication.ViewModelFactories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_LearningApplication.ViewModelFactories {
    /// <summary>
    /// Factory for creating the appropriate viewmodel according to the parameter passed to the method
    /// </summary>
    public class ViewModelFactory : IViewModelFactory {
        #region User view models

        /// <summary>
        /// Gets the view model for a single UserProfile from a single User.
        /// </summary>
        /// <param name="user">The user to be transformed into a user profile.</param>
        /// <returns></returns>
        /// <exception cref="E_LearningApplication.Controllers.CustomException"></exception>
        public UserProfile GetViewModel(Users user) {
            try {
                UserProfile userprofile = new UserProfile();
                userprofile.UserId = user.UserId;
                userprofile.UserName = user.UserName;
                userprofile.FirstName = user.FirstName;
                userprofile.MiddleName = user.MiddleName;
                userprofile.LastName = user.LastName;
                userprofile.Email = user.Email;
                userprofile.StudentIdentificationNumber = user.StudentIdentificationNumber;
                userprofile.AccessStatus = user.AccessStatus;
                return userprofile;
            }
            catch (Exception ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Gets the list of view models for a list of users.
        /// </summary>
        /// <param name="users">The users to be transformed into user profiles.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public List<UserProfile> GetViewModel(List<Users> users) {
            try {
                List<UserProfile> userProfiles = new List<UserProfile>();
                foreach (var u in users) {
                    UserProfile up = GetViewModel(u);
                    userProfiles.Add(up);
                }
                return userProfiles;
            }
            catch (Exception ex) {
                throw new CustomException(ex.Message);
            }
        }

        #endregion

        #region Logs view models

        /// <summary>
        /// Gets the view model for a log.
        /// </summary>
        /// <param name="log">The log to be transformed into a log viewmodel.</param>
        /// <returns></returns>
        /// <exception cref="E_LearningApplication.Controllers.CustomException"></exception>
        public LogsViewModel GetViewModel(Logs log) {
            try {
                LogsViewModel lvm = new LogsViewModel();
                lvm.Id = log.Id;
                lvm.EventDateTime = log.EventDateTime;
                lvm.EventInfo = log.EventInfo;
                lvm.EventLevel = log.EventLevel;
                return lvm;
            }
            catch (Exception ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Gets the list of view models for the list logs.
        /// </summary>
        /// <param name="logs">The logs to be transformed into logs viewmodels.</param>
        /// <returns></returns>
        /// <exception cref="E_LearningApplication.Controllers.CustomException"></exception>
        public List<LogsViewModel> GetViewModel(List<Logs> logs) {
            try {
                List<LogsViewModel> logsViewModels = new List<LogsViewModel>();
                foreach (var l in logs) {
                    LogsViewModel lvm = GetViewModel(l);
                    logsViewModels.Add(lvm);
                }
                return logsViewModels;
            }
            catch (Exception ex) {
                throw new CustomException(ex.Message);
            }
        }

        #endregion

        #region Course/CourseModules view models

        /// <summary>
        /// Gets the view model for a course.
        /// </summary>
        /// <param name="course">The course.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public CoursesViewModel GetViewModel(Courses course) {
            try {
                CoursesViewModel cvm = new CoursesViewModel();
                cvm.CourdeCode = course.CourdeCode;
                cvm.CourseId = course.CourseId;
                cvm.CourseName = course.CourseName;
                cvm.NumberOfCredits = course.NumberOfCredits;
                cvm.SyllabusId = course.SyllabusId;
                cvm.OwnerId = course.OwnerId;

                return cvm;
            }
            catch (Exception ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Gets the view model for a list of courses.
        /// </summary>
        /// <param name="courseModules">The course modules.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public List<CoursesViewModel> GetViewModel(List<Courses> courseModules) {
            try {
                List<CoursesViewModel> cvm = new List<CoursesViewModel>();
                foreach (var cm in courseModules) {
                    cvm.Add(GetViewModel(cm));
                }

                return cvm;
            }
            catch (Exception ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Gets the view model for a course module.
        /// </summary>
        /// <param name="courseModule">The course module.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public CourseModuleViewModel GetViewModel(CourseModule courseModule) {
            try {
                CourseModuleViewModel cmvm = new CourseModuleViewModel();
                cmvm.ModuleId = courseModule.ModuleId;
                cmvm.ModuleName = courseModule.ModuleName;
                cmvm.PreviousModuleId = courseModule.PreviousModuleId;
                cmvm.Moduledatetime = courseModule.Moduledatetime;
                cmvm.CourseId = courseModule.CourseId;

                return cmvm;
            }
            catch (Exception ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Gets the view model for a list of course modules.
        /// </summary>
        /// <param name="courseModules">The course modules.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public List<CourseModuleViewModel> GetViewModel(List<CourseModule> courseModules) {
            try {
                List<CourseModuleViewModel> cmvm = new List<CourseModuleViewModel>();
                foreach (var cm in courseModules) {
                    cmvm.Add(GetViewModel(cm));
                }

                return cmvm;
            }
            catch (Exception ex) {
                throw new CustomException(ex.Message);
            }
        }

        #endregion

        #region Resource view models

        /// <summary>
        /// Gets the view model for a resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public ResourceViewModel GetViewModel(Resources resource) {
            try {
                ResourceViewModel rvm = new ResourceViewModel();
                rvm.CourseId = resource.CourseId;
                rvm.FileId = resource.FileId;
                rvm.FileName = resource.FileName;
                rvm.ModuleID = resource.ModuleID;
                rvm.ResourceId = resource.ResourceId;
                rvm.ResourceType = resource.ResourceType;

                return rvm;
            }
            catch (Exception ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Gets the view model for a list of resources.
        /// </summary>
        /// <param name="resources">The resources.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public List<ResourceViewModel> GetViewModel(List<Resources> resources) {
            try {
                List<ResourceViewModel> rvml = new List<ResourceViewModel>();
                foreach (var r in resources) {
                    rvml.Add(GetViewModel(r));
                }
                return rvml;
            }
            catch (Exception ex) {
                throw new CustomException(ex.Message);
            }
        }

        #endregion

        #region Discussions view models

        /// <summary>
        /// Gets the view model for a forum.
        /// </summary>
        /// <param name="forum">The forum.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public ForumsViewModel GetViewModel(Forums forum) {
            try {
                ForumsViewModel fvm = new ForumsViewModel();
                fvm.ForumId = forum.ForumId;
                fvm.Category = forum.Category;

                return fvm;
            }
            catch (Exception ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Gets the view model for a list of forums.
        /// </summary>
        /// <param name="forums">The forums.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public List<ForumsViewModel> GetViewModel(List<Forums> forums) {
            try {
                List<ForumsViewModel> fvml = new List<ForumsViewModel>();
                foreach (var f in forums) {
                    fvml.Add(GetViewModel(f));
                }
                return fvml;
            }
            catch (Exception ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Gets the view model for a message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public MessagesViewModel GetViewModel(Messages message) {
            try {
                MessagesViewModel mvm = new MessagesViewModel();
                mvm.MessageId = message.MessageId;

                DateTime messageData = message.MesageData ?? DateTime.Now;
                mvm.MesageData = messageData;

                mvm.MessageContent = message.MessageContent;
                Users messageUser = new Users();
                using (var db = new ELearningDatabaseEntitiesServer()) {
                    messageUser = db.Users
                            .Where(u => u.UserId == message.UserId)
                            .First();
                }
                mvm.user = messageUser;
                return mvm;
            }
            catch (Exception ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Gets the view model for a list of messages.
        /// </summary>
        /// <param name="messages">The messages.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public List<MessagesViewModel> GetViewModel(List<Messages> messages) {
            try {
                List<MessagesViewModel> lmvm = new List<MessagesViewModel>();
                foreach (var m in messages) {
                    lmvm.Add(GetViewModel(m));
                }

                return lmvm;
            }
            catch (Exception ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Gets the view model for all the messages in a forum.
        /// </summary>
        /// <param name="forum">The forum.</param>
        /// <param name="messages">The messages.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public AllMessagesViewModel GetViewModel(Forums forum, List<Messages> messages) {
            try {
                ForumsViewModel fvm = GetViewModel(forum);
                List<MessagesViewModel> mvm = GetViewModel(messages);
                AllMessagesViewModel amvm = new AllMessagesViewModel();
                amvm.mvm = mvm;
                amvm.forum = forum;
                return amvm;
            }
            catch (Exception ex) {
                throw new CustomException(ex.Message);
            }
        }

        #endregion
    }
}
