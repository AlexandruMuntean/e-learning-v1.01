using E_LearningServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_LearningServices.Services.Interfaces {
    public interface IGroupsManagement {
        #region Groups - CRUD

        /// <summary>
        /// Gets all groups.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        List<Groups> GetAllGroups();

        /// <summary>
        /// Gets all unassociated groups for a userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        List<Groups> GetAllUnassociatedGroups(int userId);

        /// <summary>
        /// Gets the associated groups for the given userId.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        List<Groups> GetAssociatedGroups(int userId);

        /// <summary>
        /// Gets the group by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        Groups GetGroupById(int id);

        /// <summary>
        /// Gets the group by name or description.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        List<Groups> GetGroupByNameOrDescription(string searchTerm, int userId);

        /// <summary>
        /// Gets the associated group by name or description.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        List<Groups> GetAssociatedGroupByNameOrDescription(string searchTerm, int userId);

        /// <summary>
        /// Adds the group.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        Groups AddGroup(Groups group);

        /// <summary>
        /// Edits the group.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <exception cref="CustomException"></exception>
        void EditGroup(Groups group);

        /// <summary>
        /// Deletes the group and the members of the group.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="CustomException"></exception>
        void DeleteGroup(int id);

        #endregion

        #region GroupMembers - CRUD

        /// <summary>
        /// Gets all group members for a given groupId.
        /// </summary>
        /// <param name="groupId">The group identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        List<Users> GetAllGroupMembers(int groupId);

        /// <summary>
        /// Adds the group member.
        /// </summary>
        /// <param name="groupMember">The group member.</param>
        /// <exception cref="CustomException"></exception>
        void AddGroupMember(GroupMembers groupMember);

        /// <summary>
        /// Deletes the group member.
        /// </summary>
        /// <param name="groupId">The group identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <exception cref="CustomException">
        /// </exception>
        void DeleteGroupMember(int groupId, int userId);

        #endregion
    }
}
