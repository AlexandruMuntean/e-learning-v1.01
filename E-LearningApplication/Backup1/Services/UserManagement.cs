using E_LearningServices.CustomExceptions;
using E_LearningServices.Models;
using E_LearningServices.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace E_LearningServices.Services {
    /// <summary>
    /// Class for implementing CRUD operations for users
    /// </summary>
    public class UserManagement : IUserManagement {
        #region Users - CRUD

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public List<Users> GetAllUsers() {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    return db.Users
                                .ToList();
                }
            }
            catch (ArgumentNullException ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Gets the user by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public Users GetUserById(int id) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    return db.Users
                                .Where(u => u.UserId == id)
                                .FirstOrDefault();
                }
            }
            catch (ArgumentNullException ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Gets the user by the username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public Users GetUserByUserName(string username) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    return db.Users
                                .Where(u => u.UserName.Equals(username))
                                .FirstOrDefault();
                }
            }
            catch (ArgumentNullException ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Gets the users by their last name, or the users whose last name starts with the given parameter.
        /// </summary>
        /// <param name="lastName">The last name.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public List<Users> GetUserByLastName(string lastName) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    return db.Users
                                .Where(u => u.LastName.Contains(lastName))
                                .ToList();
                }
            }
            catch (ArgumentNullException ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <exception cref="CustomException"></exception>
        public void UpdateUser(Users user) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    if (user != null) {
                        db.Users.Attach(user);
                        db.Entry(user).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
            }
            catch (ArgumentNullException ex) {
                throw new CustomException(ex.Message);
            }
        }

        #endregion

    }
}