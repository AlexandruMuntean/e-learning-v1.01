using E_LearningServices.CustomExceptions;
using E_LearningServices.Models;
using E_LearningServices.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_LearningServices.Services {
    /// <summary>
    /// Class implementing CRUD operations for forums, messages, discussions.
    /// </summary>
    public class ForumManagement: IForumManagement {
        #region Forums - CRUD

        /// <summary>
        /// Gets all forums.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public List<Forums> GetAllForums() {
            try {
                List<Forums> forums;
                using (var db = new ELearningDatabaseEntities()) {
                    forums = db.Forums
                                    .ToList();
                }

                return forums;
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
        }

        /// <summary>
        /// Get Forum by a specified id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Forum</returns>
        public Forums GetForumById(int id) {
            try {
                Forums toBeReturned;
                using (var db = new ELearningDatabaseEntities()) {
                    toBeReturned = db.Forums
                                        .FirstOrDefault(x => x.ForumId == id);
                }

                return toBeReturned;
            }
            catch (ArgumentNullException ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Add the specified forum.
        /// </summary>
        /// <param name="course">The forum.</param>
        /// <exception cref="CustomException"></exception>
        public void AddForum(Forums forums) {
            if (forums.Category.Equals(""))
                throw new CustomException("Category cannot be empty");
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    db.Forums.Add(forums);
                    db.SaveChanges();
                }
            }
            catch (InvalidOperationException ex) {
                throw new CustomException(ex.Message);
            }

        }

        /// <summary>
        /// Edit a category from the specified Forum
        /// </summary>
        /// <param name="forum"></param>
        public void EditCategory(Forums forum) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    db.Forums.Attach(forum);
                    db.Entry(forum).State = System.Data.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
            catch (InvalidOperationException ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Delete a forum with the specified Id
        /// </summary>
        /// <param name="id"></param>
        public void DeleteForum(int? id) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    List<Messages> messages = db.Messages
                                                    .Where(x => x.ForumId == id)
                                                    .ToList();
                    foreach (Messages m in messages) {
                        db.Messages.Remove(m);
                        db.SaveChanges();
                    }
                    Forums forum = db.Forums
                                        .Where(x => x.ForumId == id)
                                        .First();
                    db.Forums.Remove(forum);
                    db.SaveChanges();
                }
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
            catch (InvalidOperationException ex) {
                throw new CustomException(ex.Message);
            }
        }

        #endregion

        #region Messages - CRUD

        /// <summary>
        /// Add a message in Forum
        /// </summary>
        /// <param name="message"></param>
        public void AddMessage(Messages message) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    db.Messages.Add(message);
                    db.SaveChanges();
                }
            }
            catch (InvalidOperationException ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Delete a message from a forum
        /// </summary>
        /// <param name="idMessage"></param>
        public void DeleteMessage(int? idMessage) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    Messages message = db.Messages
                                        .Where(x => x.MessageId == idMessage)
                                        .First();
                    db.Messages.Remove(message);
                    db.SaveChanges();
                }
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
            catch (InvalidOperationException ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Update a message in a forum
        /// </summary>
        /// <param name="message"></param>

        public void UpdateMessage(Messages message) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    db.Messages.Attach(message);
                    db.Entry(message).State = System.Data.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
            catch (InvalidOperationException ioe) {
                throw new CustomException(ioe.Message);
            }
        }

        /// <summary>
        /// Get the message with specificated id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Messages GetMessageById(int? id) {
            try {
                Messages toBeReturned;
                using (var db = new ELearningDatabaseEntities()) {
                    toBeReturned = db.Messages
                                        .FirstOrDefault(x => x.MessageId == id);
                }

                return toBeReturned;
            }
            catch (ArgumentNullException ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Get all messages for a Forum
        /// </summary>
        /// <param name="idForum"></param>
        /// <returns></returns>
        public List<Messages> GetAllMessages(int? idForum) {
            try {
                List<Messages> messages = new List<Messages>();
                using (var db = new ELearningDatabaseEntities()) {
                    var result = from message in db.Messages
                                 join forum in db.Forums on message.ForumId equals forum.ForumId
                                 where forum.ForumId == idForum
                                 select message;
                    foreach (Messages r in result)
                        messages.Add(r);
                }

                return messages;
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
            catch (Exception ex) {
                throw new CustomException(ex.Message);
            }
        }

        #endregion

    }
}