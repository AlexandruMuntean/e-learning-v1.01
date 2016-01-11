using E_LearningServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_LearningServices.Services.Interfaces {
    public interface IAdminManagement {
        #region Logs - CRUD

        /// <summary>
        /// Gets all logs.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        List<Logs> GetAllLogs();

        /// <summary>
        /// Gets the log by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        Logs GetLogById(int id);

        /// <summary>
        /// Gets the log by datetime.
        /// </summary>
        /// <param name="datetime">The datetime.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        List<Logs> GetLogByDatetime(DateTime datetime);

        /// <summary>
        /// Gets the log by event level.
        /// </summary>
        /// <param name="eventLevel">The event level.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        List<Logs> GetLogByEventLevel(string eventLevel);

        /// <summary>
        /// Gets the logs containing the given information.
        /// </summary>
        /// <param name="eventUserLogged">The event containing the given information.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        List<Logs> GetLogByEventInfo(string eventInfo);

        /// <summary>
        /// Deletes the log.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="CustomException"></exception>
        void DeleteLog(int id);

        /// <summary>
        /// Deletes all logs.
        /// </summary>
        /// <exception cref="CustomException"></exception>
        void DeleteAllDisplayedLogs();

        #endregion

    }
}
