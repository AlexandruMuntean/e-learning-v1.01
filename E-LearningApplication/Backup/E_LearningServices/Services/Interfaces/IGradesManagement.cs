using E_LearningServices.Models;
using E_LearningServices.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_LearningServices.Services.Interfaces {
    public interface IGradesManagement {
        #region Grades CRUD - Prof functionalities

        /// <summary>
        /// Gets all course given grades.
        /// </summary>
        /// <param name="courseId">The course identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        List<ReceivedGradeDTO> GetAllCourseGivenGrades(int courseId);

        /// <summary>
        /// Gets all course module given grades.
        /// </summary>
        /// <param name="courseId">The course identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        List<ReceivedGradeDTO> GetAllCourseModuleGivenGrades(int courseModuleId);

        /// <summary>
        /// Grades the homework.
        /// </summary>
        /// <param name="grade">The grade.</param>
        /// <param name="dto">The homework grade dto.</param>
        /// <exception cref="CustomException">
        /// </exception>
        void GradeHomework(Grades grade, HomeworkGradeDTO dto);

        #endregion

        #region Grades CRUD - Student functionalities

        /// <summary>
        /// Gets all student received grades.
        /// </summary>
        /// <param name="studentId">The student identifier.</param>
        /// <returns></returns>
        List<ReceivedGradeDTO> GetAllStudentReceivedGrades(int studentId);

        /// <summary>
        /// Gets all group received grades.
        /// </summary>
        /// <param name="groupId">The group identifier.</param>
        /// <returns></returns>
        List<ReceivedGradeDTO> GetAllGroupReceivedGrades(int groupId);

        #endregion
    }
}
