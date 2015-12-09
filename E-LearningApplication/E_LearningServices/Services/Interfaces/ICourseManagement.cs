using E_LearningServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_LearningServices.Services.Interfaces {
    public interface ICourseManagement {
        #region Courses - CRUD

        /// <summary>
        /// Gets all courses.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        List<Courses> GetAllCourses();

        /// <summary>
        /// Gets all owned courses.
        /// </summary>
        /// <param name="id">The identifier of the owner.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        List<Courses> GetAllOwnedCourses(int id);

        /// <summary>
        /// Adds the specified course.
        /// </summary>
        /// <param name="course">The course.</param>
        /// <exception cref="CustomException"></exception>
        void AddCourse(Courses course);

        /// <summary>
        /// Gets the course by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        Courses GetCourseById(int id);

        /// <summary>
        /// Gets the course identifier by code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">
        /// </exception>
        int GetCourseIdByCode(string code);

        /// <summary>
        /// Edits the specified course.
        /// </summary>
        /// <param name="course">The course.</param>
        /// <exception cref="CustomException"></exception>
        void EditCourse(Courses course);

        /// <summary>
        /// Deletes the course with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="CustomException">
        /// </exception>
        void DeleteCourse(int id);

        #endregion

        #region CourseModules - CRUD

        /// <summary>
        /// Gets all modules for the course with the specified identifier.
        /// </summary>
        /// <param name="courseId">The course identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        List<CourseModule> GetAllModules(int courseId);

        /// <summary>
        /// Adds the specified module.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <exception cref="CustomException"></exception>
        void AddModule(CourseModule courseModule);

        /// <summary>
        /// Deletes the module with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="CustomException">
        /// </exception>
        void DeleteModule(int id);

        /// <summary>
        /// Gets the couse module by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">
        /// </exception>
        CourseModule GetCouseModuleById(int id);

        /// <summary>
        /// Updates the course module.
        /// </summary>
        /// <param name="courseModule">The course module.</param>
        /// <exception cref="CustomException">
        /// </exception>
        void UpdateCourseModule(CourseModule courseModule);

        #endregion
    }
}
