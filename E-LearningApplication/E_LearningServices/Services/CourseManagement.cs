using E_LearningServices.CustomExceptions;
using E_LearningServices.Models;
using E_LearningServices.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_LearningServices.Services {
    /// <summary>
    /// Class for implementing CRUD operations for courses and course modules
    /// </summary>
    public class CourseManagement: ICourseManagement {
        #region Courses - CRUD

        /// <summary>
        /// Gets all courses.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public List<Courses> GetAllCourses() {
            try {
                List<Courses> courses;
                using (var db = new ELearningDatabaseEntities()) {
                    courses = db.Courses
                                    .ToList();
                }

                return courses;
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
        }

        /// <summary>
        /// Gets all owned courses.
        /// </summary>
        /// <param name="id">The identifier of the owner.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public List<Courses> GetAllOwnedCourses(int id) {
            try {
                List<Courses> courses;
                using (var db = new ELearningDatabaseEntities()) {
                    courses = db.Courses
                                    .Where(c => c.OwnerId == id)
                                    .ToList();
                }

                return courses;
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
        }

        /// <summary>
        /// Gets my courses.
        /// </summary>
        /// <param name="id">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public List<Courses> GetMyCourses(int id) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    List<Courses> courses = new List<Courses>();
                    List<int> courseIds = db.UsersInCourse
                                                .Where(u => u.UserId == id)
                                                .Select(u => u.CourseId)
                                                .ToList();
                    foreach (var x in courseIds) {
                        courses.Add(
                            db.Courses
                                .Where(c => c.CourseId == x)
                                .First()
                            );
                    }

                    return courses;
                }
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
        }

        /// <summary>
        /// Gets the course identifier by code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">
        /// </exception>
        public int GetCourseIdByCode(string code) {
            try {
                int result = -1;
                using (var db = new ELearningDatabaseEntities()) {
                    result = db.Courses.Where(x => x.CourdeCode == code).First().CourseId;
                }
                return result;
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
            catch (Exception ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Adds the specified course.
        /// </summary>
        /// <param name="course">The course.</param>
        /// <exception cref="CustomException"></exception>
        public void AddCourse(Courses course) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    db.Courses.Add(course);
                    db.SaveChanges();
                }
            }
            catch (InvalidOperationException ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Gets the course by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public Courses GetCourseById(int id) {
            try {
                Courses toBeReturned;
                using (var db = new ELearningDatabaseEntities()) {
                    toBeReturned = db.Courses
                                        .FirstOrDefault(x => x.CourseId == id);
                }

                return toBeReturned;
            }
            catch (ArgumentNullException ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Edits the specified course.
        /// </summary>
        /// <param name="course">The course.</param>
        /// <exception cref="CustomException"></exception>
        public void EditCourse(Courses course) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    db.Courses.Attach(course);
                    db.Entry(course).State = System.Data.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (InvalidOperationException ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the course with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="CustomException">
        /// </exception>
        public void DeleteCourse(int id) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    Courses c = db.Courses
                                        .Where(x => x.CourseId == id)
                                        .First();
                    db.Courses.Remove(c);
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

        #region CourseModules - CRUD

        /// <summary>
        /// Gets all modules for the course with the specified identifier.
        /// </summary>
        /// <param name="courseId">The course identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public List<CourseModule> GetAllModules(int courseId) {
            try {
                List<CourseModule> tobeReturned;
                using (var db = new ELearningDatabaseEntities()) {
                    tobeReturned = db.CourseModule
                                        .Where(x => x.CourseId == courseId)
                                        .ToList();
                }
                return tobeReturned;
            }
            catch (ArgumentNullException ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Adds the specified module.
        /// </summary>
        /// <param name="module">The module.</param>
        /// <exception cref="CustomException"></exception>
        public void AddModule(CourseModule module) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    db.CourseModule.Add(module);
                    db.SaveChanges();
                }
            }
            catch (InvalidOperationException ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the module with the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="CustomException">
        /// </exception>
        public void DeleteModule(int id) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    CourseModule cm = db.CourseModule
                                            .Where(x => x.ModuleId == id)
                                            .First();
                    db.CourseModule.Remove(cm);
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
        /// Gets the couse module by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">
        /// </exception>
        public CourseModule GetCouseModuleById(int id) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    return db.CourseModule
                                    .Where(cm => cm.ModuleId == id)
                                    .First();
                }
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
            catch (Exception ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Updates the course module.
        /// </summary>
        /// <param name="courseModule">The course module.</param>
        /// <exception cref="CustomException">
        /// </exception>
        public void UpdateCourseModule(CourseModule courseModule) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    db.CourseModule.Attach(courseModule);
                    db.Entry(courseModule).State = System.Data.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (InvalidOperationException ioe) {
                throw new CustomException(ioe.Message);
            }
            catch (Exception ex) {
                throw new CustomException(ex.Message);
            }
        }

        #endregion

    }
}