using E_LearningApplication.Models;
using E_LearningApplication.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_LearningApplication.ViewModelFactories.Interfaces {
    /// <summary>
    /// Interface for a factory that creates the appropriate viewmodel according to the parameter passed to the method
    /// </summary>
    public interface IViewModelFactory {
        #region User view models
        
        /// <summary>
        /// Gets the view model for a single UserProfile from a single User.
        /// </summary>
        /// <param name="user">The user to be transformed into a user profile.</param>
        /// <returns></returns>
        /// <exception cref="E_LearningApplication.Controllers.CustomException"></exception>
        UserProfile GetViewModel(Users user);

        /// <summary>
        /// Gets the list of view models for a list of users.
        /// </summary>
        /// <param name="users">The users to be transformed into user profiles.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        List<UserProfile> GetViewModel(List<Users> users);

        #endregion

        #region Logs view models

        /// <summary>
        /// Gets the view model for a log.
        /// </summary>
        /// <param name="log">The log to be transformed into a log viewmodel.</param>
        /// <returns></returns>
        /// <exception cref="E_LearningApplication.Controllers.CustomException"></exception>
        LogsViewModel GetViewModel(Logs log);

        /// <summary>
        /// Gets the list of view models for the list logs.
        /// </summary>
        /// <param name="logs">The logs to be transformed into logs viewmodels.</param>
        /// <returns></returns>
        /// <exception cref="E_LearningApplication.Controllers.CustomException"></exception>
        List<LogsViewModel> GetViewModel(List<Logs> logs);

        #endregion

        #region Course/CourseModules view models

        /// <summary>
        /// Gets the view model for a course.
        /// </summary>
        /// <param name="course">The course.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        CoursesViewModel GetViewModel(Courses course);

        /// <summary>
        /// Gets the view model for a list of courses.
        /// </summary>
        /// <param name="courseModules">The course modules.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        List<CoursesViewModel> GetViewModel(List<Courses> courseModules);

        /// <summary>
        /// Gets the view model for a course module.
        /// </summary>
        /// <param name="courseModule">The course module.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        CourseModuleViewModel GetViewModel(CourseModule courseModule);

        /// <summary>
        /// Gets the view model for a list of course modules.
        /// </summary>
        /// <param name="courseModules">The course modules.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        List<CourseModuleViewModel> GetViewModel(List<CourseModule> courseModules);

        #endregion

        #region Resource view models

        /// <summary>
        /// Gets the view model for a resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        ResourceViewModel GetViewModel(Resources resource);

        /// <summary>
        /// Gets the view model for a list of resources.
        /// </summary>
        /// <param name="resources">The resources.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        List<ResourceViewModel> GetViewModel(List<Resources> resources);

        #endregion

        #region Discussions view models

        /// <summary>
        /// Gets the view model for a forum.
        /// </summary>
        /// <param name="forum">The forum.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        ForumsViewModel GetViewModel(Forums forum);

        /// <summary>
        /// Gets the view model for a list of forums.
        /// </summary>
        /// <param name="forums">The forums.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        List<ForumsViewModel> GetViewModel(List<Forums> forums);

        /// <summary>
        /// Gets the view model for a message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        MessagesViewModel GetViewModel(Messages message);

        /// <summary>
        /// Gets the view model for a list of messages.
        /// </summary>
        /// <param name="messages">The messages.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        List<MessagesViewModel> GetViewModel(List<Messages> messages);

        /// <summary>
        /// Gets the view model for all the messages in a forum.
        /// </summary>
        /// <param name="forum">The forum.</param>
        /// <param name="messages">The messages.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        AllMessagesViewModel GetViewModel(Forums forum, List<Messages> messages);

        #endregion
    }
}
