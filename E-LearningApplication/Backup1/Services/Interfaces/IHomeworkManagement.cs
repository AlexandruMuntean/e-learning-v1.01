using E_LearningServices.Models;
using E_LearningServices.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_LearningServices.CustomExceptions;

namespace E_LearningServices.Services.Interfaces {
    public interface IHomeworkManagement {
        #region Homework CRUD - Prof functionalities

        /// <summary>
        /// Gets the homework by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        Homeworks GetHomeworkById(int id);

        /// <summary>
        /// Gets all course homework.
        /// </summary>
        /// <param name="courseId">The course identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        List<Homeworks> GetAllCourseHomework(int courseId);

        /// <summary>
        /// Gets the course assignements.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="courseId">The course identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        List<HomeworkAssignements> GetCourseAssignements(int id, int courseId);

        /// <summary>
        /// Gets the name of the recipient for an assignement.
        /// </summary>
        /// <param name="studentId">The student identifier.</param>
        /// <param name="groupId">The group identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        string GetRecipientName(int? studentId, int? groupId);

        /// <summary>
        /// Gets the course module assignements.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="courseModuleId">The course module identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        List<HomeworkAssignements> GetCourseModuleAssignements(int id, int courseModuleId);

        /// <summary>
        /// Gets all course module homework.
        /// </summary>
        /// <param name="courseModuleId">The course module identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        List<Homeworks> GetAllCourseModuleHomework(int courseModuleId);

        /// <summary>
        /// Gets the answer for assignement.
        /// </summary>
        /// <param name="assignementId">The assignement identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        Answers GetAnswerForAssignement(int assignementId);

        string GetNameOfFile(int HomeworkId);

        /// <summary>
        /// Adds the homework.
        /// </summary>
        /// <param name="hw">The hw.</param>
        /// <exception cref="CustomException"></exception>
        void AddHomework(Homeworks hw);

        /// <summary>
        /// Deletes the homework.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="CustomException">
        /// </exception>
        void DeleteHomework(int id);

        /// <summary>
        /// Edits the homework.
        /// </summary>
        /// <param name="hw">The hw.</param>
        /// <exception cref="CustomException"></exception>
        void EditHomework(Homeworks hw);

        /// <summary>
        /// Assigns the homework.
        /// </summary>
        /// <param name="dto"></param>
        /// <exception cref="CustomException">
        /// </exception>
        void AssignHomework(HomeworkAssignementDTO dto);

        /// <summary>
        /// Unassigns the homework.
        /// </summary>
        /// <param name="dto"></param>
        /// <exception cref="CustomException">
        /// </exception>
        void UnassignHomework(HomeworkAssignementDTO dto);

        #endregion

        #region Homework CRUD - Student functionalities

        /// <summary>
        /// Gets all student assigned homework.
        /// </summary>
        /// <param name="studentId">The student identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        List<AssignementDTO> GetAllStudentAssignedHomework(int studentId);

        /// <summary>
        /// Gets all group assigned homework.
        /// </summary>
        /// <param name="groupId">The group identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        List<AssignementDTO> GetAllGroupAssignedHomework(int groupId);

        /// <summary>
        /// Submits the homework.
        /// </summary>
        /// <param name="assignementId">The assignement identifier.</param>
        /// <param name="answer">The answer.</param>
        /// <exception cref="CustomException">
        /// </exception>
        void SubmitHomework(int assignementId, Answers answer);

        #endregion

    }
}
