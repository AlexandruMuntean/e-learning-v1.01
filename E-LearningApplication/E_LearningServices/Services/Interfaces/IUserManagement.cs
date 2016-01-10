using E_LearningServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_LearningServices.CustomExceptions;

namespace E_LearningServices.Services.Interfaces {
    public interface IUserManagement {
        #region Users - CRUD

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        List<Users> GetAllUsers();

        /// <summary>
        /// Gets all subscribed users to a course.
        /// </summary>
        /// <param name="courseId">The course identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        List<Users> GetAllSubscribedUsers(int courseId);

        /// <summary>
        /// Gets the user by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        Users GetUserById(int id);

        /// <summary>
        /// Gets the user by the username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        Users GetUserByUserName(string username);

        /// <summary>
        /// Gets the users by their last name, or the users whose last name starts with the given parameter.
        /// </summary>
        /// <param name="lastName">The last name.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        List<Users> GetUserByLastName(string lastName);

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <exception cref="CustomException"></exception>
        void UpdateUser(Users user);

        #endregion

    }
}
