using E_LearningServices.CustomExceptions;
using E_LearningServices.Models;
using E_LearningServices.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_LearningServices.Services {
    /// <summary>
    /// Class for implementing the CRUD operations for the admin role: 
    /// - view/delete logs
    /// </summary>
    public class AdminManagement: IAdminManagement {

        #region Logs - CRUD

        /// <summary>
        /// Gets all logs.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public List<Logs> GetAllLogs() {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    return db.Logs
                                .OrderBy(x => x.EventDateTime)
                                .ToList();
                }
            }
            catch (ArgumentNullException ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Gets the log by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public Logs GetLogById(int id) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    return db.Logs
                                .Where(l => l.Id == id)
                                .FirstOrDefault();
                }
            }
            catch (ArgumentNullException ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Gets the log by datetime.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public List<Logs> GetLogByDatetime(DateTime datetime) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    return db.Logs
                                .Where(l => l.EventDateTime.Year == datetime.Year
                                                && l.EventDateTime.Month == datetime.Month
                                                && l.EventDateTime.Day == datetime.Day)
                                .ToList();
                }
            }
            catch (ArgumentNullException ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Gets the log by event level.
        /// </summary>
        /// <param name="eventLevel">The event level.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public List<Logs> GetLogByEventLevel(string eventLevel) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    return db.Logs
                                .Where(l => l.EventLevel.Equals(eventLevel))
                                .ToList();
                }
            }
            catch (ArgumentNullException ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Gets the logs containing the given information.
        /// </summary>
        /// <param name="eventUserLogged">The event containing the given information.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public List<Logs> GetLogByEventInfo(string eventInfo) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    return db.Logs
                                .Where(l => l.EventInfo.Contains(eventInfo))
                                .ToList();
                }
            }
            catch (ArgumentNullException ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the log.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="CustomException"></exception>
        public void DeleteLog(int id) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    Logs log = db.Logs
                                    .Where(l => l.Id == id)
                                    .FirstOrDefault();
                    db.Logs
                        .Remove(log);
                    db.SaveChanges();
                }
            }
            catch (ArgumentNullException ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Deletes all logs.
        /// </summary>
        /// <exception cref="CustomException"></exception>
        public void DeleteAllDisplayedLogs() {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    db.Database.ExecuteSqlCommand("TRUNCATE TABLE Logs");
                    db.SaveChanges();
                }
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
        }

        #endregion

    }
}