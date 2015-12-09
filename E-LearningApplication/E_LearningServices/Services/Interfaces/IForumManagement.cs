using E_LearningServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_LearningServices.Services.Interfaces {
    public interface IForumManagement {
        #region Forum - CRUD
        /// <summary>
        /// Get all forums
        /// </summary>
        /// <returns>Lista de Forums</returns>
        List<Forums> GetAllForums();

        /// <summary>
        /// Get a forum by the specified id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Forum</returns>
        Forums GetForumById(int id);

        /// <summary>
        /// Add the specified forum
        /// </summary>
        /// <param name="forums">The forum</param>
        /// <exception cref="CustomException"></exception>
        void AddForum(Forums forums);

        /// <summary>
        /// Delete the forum with specified Id
        /// </summary>
        /// <param name="id">The identifier for id</param>
        void DeleteForum(int? id);

        /// <summary>
        /// Edit the category from specified Forum
        /// </summary>
        /// <param name="forum"></param>
        void EditCategory(Forums forum);

        #endregion

        #region Messages - CRUD

        /// <summary>
        /// Get all Comments for a Forum category 
        /// </summary>
        /// <returns>Lista de Masaje</returns>
        List<Messages> GetAllMessages(int? id);

        /// <summary>
        /// Get message with the specificated id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Messages GetMessageById(int? id);

        /// <summary>
        /// Add a message in a forum
        /// </summary>
        /// <param name="message">The message</param>
        void AddMessage(Messages message);

        /// <summary>
        /// Delete the message with specified idMessage from the specified forum(idForum)
        /// </summary>
        /// <param name="idMessage"></param>
        void DeleteMessage(int? idMessage);

        /// <summary>
        /// Update message from a Forum
        /// </summary>
        /// <param name="message"></param>
        void UpdateMessage(Messages message);

        #endregion
    }
}
