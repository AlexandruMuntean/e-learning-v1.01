using E_LearningServices.CustomExceptions;
using E_LearningServices.Models;
using E_LearningServices.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_LearningServices.Services {
    public class GroupsManagement : IGroupsManagement {
        #region Groups - CRUD

        /// <summary>
        /// Gets all groups.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public List<Groups> GetAllGroups() {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    return db.Groups.ToList();
                }
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
        }

        /// <summary>
        /// Gets all unassociated groups for a userId.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public List<Groups> GetAllUnassociatedGroups(int userId) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    var unassociatedGroups = (from g in db.Groups
                                             where !(
                                             from m in db.GroupMembers
                                             where m.MemberId == userId
                                             select m.GroupId)
                                             .Contains(g.GroupId)
                                             select g)
                                             .ToList();

                    return unassociatedGroups;
                }
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
        }

        /// <summary>
        /// Gets the associated groups for the given userId.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public List<Groups> GetAssociatedGroups(int userId) {
            try {
                List<Groups> associatedGroups = new List<Groups>();
                using (var db = new ELearningDatabaseEntities()) {
                    //get the membership for the userId
                    List<GroupMembers> groupMembers = db.GroupMembers
                                                            .Where(gm => gm.MemberId == userId)
                                                            .ToList();
                    //get all the groups to which the user is subscribed
                    foreach (var gm in groupMembers) {
                        associatedGroups.Add(
                            db.Groups
                                .Where(g => g.GroupId == gm.GroupId)
                                .First()
                            );
                    }
                }
                return associatedGroups;
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
        }

        /// <summary>
        /// Gets the group by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public Groups GetGroupById(int id) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    return db.Groups
                                .Where(g => g.GroupId == id)
                                .First();
                }
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
        }

        /// <summary>
        /// Gets the group by name or description.
        /// </summary>
        /// <param name="searchTerm">The search term.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public List<Groups> GetGroupByNameOrDescription(string searchTerm) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    return db.Groups
                                .Where(g => g.GroupDescription.Contains(searchTerm) || g.GroupName.Contains(searchTerm))
                                .ToList();
                }
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
        }

        /// <summary>
        /// Adds the group.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public Groups AddGroup(Groups group) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    db.Groups.Add(group);
                    db.SaveChanges();
                }
                return group;
            }
            catch (InvalidOperationException ioe) {
                throw new CustomException(ioe.Message);
            }
        }

        /// <summary>
        /// Edits the group.
        /// </summary>
        /// <param name="group">The group.</param>
        /// <exception cref="CustomException"></exception>
        public void EditGroup(Groups group) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    db.Groups.Attach(group);
                    db.Entry(group).State = System.Data.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (InvalidOperationException ioe) {
                throw new CustomException(ioe.Message);
            }
        }

        /// <summary>
        /// Deletes the group and the members of the group.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="CustomException"></exception>
        public void DeleteGroup(int id) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    //delete the members of the group to be deleted
                    List<GroupMembers> membersOfDeletedGroup = db.GroupMembers
                                                                    .Where(m => m.GroupId == id)
                                                                    .ToList();
                    foreach (var m in membersOfDeletedGroup) {
                        db.GroupMembers.Remove(m);
                    }
                    db.SaveChanges();

                    //delete the group
                    Groups group = db.Groups
                                        .Where(g => g.GroupId == id)
                                        .First();
                    db.Groups.Remove(group);
                    db.SaveChanges();
                }
            }
            catch (InvalidOperationException ioe) {
                throw new CustomException(ioe.Message);
            }
        }

        #endregion

        #region GroupMembers - CRUD

        /// <summary>
        /// Gets all group members for a given groupId.
        /// </summary>
        /// <param name="groupId">The group identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public List<Users> GetAllGroupMembers(int groupId) {
            try {
                List<Users> groupMembers = new List<Users>();
                using (var db = new ELearningDatabaseEntities()) {
                    //get the membership for the group
                    List<GroupMembers> membership = db.GroupMembers
                                                            .Where(gm => gm.GroupId == groupId)
                                                            .ToList();
                    //get the users from the group based on the membership
                    foreach (var m in membership) {
                        groupMembers.Add(
                            db.Users
                                .Where(u => u.UserId == m.MemberId)
                                .First()
                            );
                    }
                }
                return groupMembers;
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
        }

        /// <summary>
        /// Adds the group member.
        /// </summary>
        /// <param name="groupMember">The group member.</param>
        /// <exception cref="CustomException"></exception>
        public void AddGroupMember(GroupMembers groupMember) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    var member = db.GroupMembers
                                        .Where(m => m.MemberId == groupMember.MemberId && m.GroupId == groupMember.GroupId)
                                        .FirstOrDefault();
                    //add the membership only if it doesn't already exist
                    if (member == null) {
                        db.GroupMembers.Add(groupMember);
                        db.SaveChanges();
                    }
                }
            }
            catch (InvalidOperationException ioe) {
                throw new CustomException(ioe.Message);
            }
        }

        /// <summary>
        /// Deletes the group member.
        /// </summary>
        /// <param name="groupId">The group identifier.</param>
        /// <param name="userId">The user identifier.</param>
        /// <exception cref="CustomException">
        /// </exception>
        public void DeleteGroupMember(int groupId, int userId) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    GroupMembers groupMember = db.GroupMembers
                                                    .Where(gm => gm.GroupId == groupId && gm.MemberId == userId)
                                                    .First();
                    db.GroupMembers.Remove(groupMember);
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

        #endregion
    }
}