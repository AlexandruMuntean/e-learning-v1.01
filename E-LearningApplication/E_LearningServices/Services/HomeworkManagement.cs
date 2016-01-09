using E_LearningServices.CustomExceptions;
using E_LearningServices.Models;
using E_LearningServices.Models.DTOs;
using E_LearningServices.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_LearningServices.Services {
    public class HomeworkManagement : IHomeworkManagement {
        #region Homework CRUD - Prof functionalities

        /// <summary>
        /// Gets the homework by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public Homeworks GetHomeworkById(int id) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    return db.Homeworks
                                .Where(h => h.HomeworkId == id)
                                .First();
                }
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
        }

        /// <summary>
        /// Gets all course homework.
        /// </summary>
        /// <param name="courseId">The course identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public List<Homeworks> GetAllCourseHomework(int courseId) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    return db.Homeworks
                                .Where(h => h.CourseId == courseId)
                                .ToList();
                }
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
        }

        /// <summary>
        /// Gets the course assignements.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="courseId">The course identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public List<HomeworkAssignements> GetCourseAssignements(int id, int courseId) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    return db.HomeworkAssignements
                                .Where(a => a.HomeworkId == id && a.CourseId == courseId)
                                .ToList();
                }
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
        }

        /// <summary>
        /// Gets the name of the recipient for an assignement.
        /// </summary>
        /// <param name="studentId">The student identifier.</param>
        /// <param name="groupId">The group identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public string GetRecipientName(int? studentId, int? groupId) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    string recipient = "";
                    if (studentId != null && studentId > 0) {
                        var student = db.Users
                                            .Where(u => u.UserId == studentId)
                                            .First();
                        recipient = recipient + student.StudentIdentificationNumber + "-" + student.LastName + " " + student.FirstName;
                        if (groupId != null && groupId > 0) {
                            var group = db.Groups
                                            .Where(g => g.GroupId == groupId)
                                            .First();
                            recipient = recipient + "; " + group.GroupName;
                        }
                    }
                    else if (groupId != null && groupId > 0) {
                        var group = db.Groups
                                        .Where(g => g.GroupId == groupId)
                                        .First();
                        recipient = recipient + group.GroupName;
                    }
                    return recipient;
                }
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
        }

        /// <summary>
        /// Gets the course module assignements.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="courseModuleId">The course module identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public List<HomeworkAssignements> GetCourseModuleAssignements(int id, int courseModuleId) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    return db.HomeworkAssignements
                                .Where(a => a.HomeworkId == id && a.CourseModuleId == courseModuleId)
                                .ToList();
                }
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
        }

        /// <summary>
        /// Gets all course module homework.
        /// </summary>
        /// <param name="courseModuleId">The course module identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public List<Homeworks> GetAllCourseModuleHomework(int courseModuleId) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    return db.Homeworks
                                .Where(h => h.CourseModuleId == courseModuleId)
                                .ToList();
                }
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
        }

        /// <summary>
        /// Gets the answer for assignement.
        /// </summary>
        /// <param name="assignementId">The assignement identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public Answers GetAnswerForAssignement(int assignementId) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    var ha = db.HomeworkAssignements
                                    .Where(a => a.AssignementId == assignementId)
                                    .First();
                    return db.Answers
                                .Where(a => a.AnswerId == ha.AnswerId)
                                .First();
                }
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
        }

        public string GetNameOfFile(int HomeworkId) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    var result = db.Homeworks.Where(x => x.HomeworkId == HomeworkId).First();
                    if (result != null)
                        return result.HomeworkCode;
                }
                return string.Empty;
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
        }

        /// <summary>
        /// Adds the homework.
        /// </summary>
        /// <param name="hw">The hw.</param>
        /// <exception cref="CustomException"></exception>
        public void AddHomework(Homeworks hw) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    db.Homeworks.Add(hw);
                    db.SaveChanges();
                }
            }
            catch (InvalidOperationException ioe) {
                throw new CustomException(ioe.Message);
            }
        }

        /// <summary>
        /// Deletes the homework.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="CustomException">
        /// </exception>
        public void DeleteHomework(int id) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    //delete the assignements for the homework
                    var assignementsToDelete = db.HomeworkAssignements
                                                    .Where(a => a.HomeworkId == id)
                                                    .ToList();
                    foreach (var a in assignementsToDelete) {
                        db.HomeworkAssignements.Remove(a);
                    }
                    db.SaveChanges();
                    //delete the homework
                    Homeworks hw = db.Homeworks
                                        .Where(h => h.HomeworkId == id)
                                        .First();
                    db.Homeworks.Remove(hw);
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
        /// Edits the homework.
        /// </summary>
        /// <param name="hw">The hw.</param>
        /// <exception cref="CustomException"></exception>
        public void EditHomework(Homeworks hw) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    db.Homeworks.Attach(hw);
                    db.Entry(hw).State = System.Data.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (InvalidOperationException ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Assigns the homework.
        /// </summary>
        /// <param name="dto"></param>
        /// <exception cref="CustomException">
        /// </exception>
        public void AssignHomework(HomeworkAssignementDTO dto) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    HomeworkAssignements ha = db.HomeworkAssignements
                                                    .Where(a => a.AssignementId == dto.AssignementId)
                                                    .FirstOrDefault();
                    if (ha != null) {
                        if (dto.StudentId != null && dto.StudentId > 0) ha.StudentId = dto.StudentId;
                        if (dto.GroupId != null && dto.GroupId > 0) ha.GroupId = dto.GroupId;

                        db.HomeworkAssignements.Attach(ha);
                        db.Entry(ha).State = System.Data.EntityState.Modified;
                        db.SaveChanges();
                    }
                    else {
                        HomeworkAssignements newAssignement = new HomeworkAssignements();
                        newAssignement.AnswerId = dto.AnswerId;
                        newAssignement.AssignementId = dto.AssignementId;
                        newAssignement.CourseId = dto.CourseId;
                        newAssignement.CourseModuleId = dto.CourseModuleId;
                        newAssignement.GradeId = dto.GradeId;
                        newAssignement.GroupId = dto.GroupId;
                        newAssignement.HomeworkId = dto.HomeworkId;
                        newAssignement.StudentId = dto.StudentId;

                        db.HomeworkAssignements.Add(newAssignement);
                        db.SaveChanges();
                    }
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
        /// Unassigns the homework.
        /// </summary>
        /// <param name="dto"></param>
        /// <exception cref="CustomException">
        /// </exception>
        public void UnassignHomework(HomeworkAssignementDTO dto) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    HomeworkAssignements ha = db.HomeworkAssignements
                                                    .Where(a => a.AssignementId == dto.AssignementId)
                                                    .First();
                    db.HomeworkAssignements.Remove(ha);
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

        #region Homework CRUD - Student functionalities

        /// <summary>
        /// Gets all student assigned homework.
        /// </summary>
        /// <param name="studentId">The student identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public List<AssignementDTO> GetAllStudentAssignedHomework(int studentId) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    List<HomeworkAssignements> ha = db.HomeworkAssignements
                                                        .Where(a => a.StudentId == studentId)
                                                        .ToList();

                    List<AssignementDTO> dto = new List<AssignementDTO>();
                    foreach (var x in ha) {
                        Homeworks hw = db.Homeworks
                                            .Where(h => h.HomeworkId == x.HomeworkId)
                                            .First();
                        string code = "";
                        if (x.CourseId != null && x.CourseId > 0) {
                            code = db.Courses
                                        .Where(c => c.CourseId == x.CourseId)
                                        .Select(c => c.CourdeCode)
                                        .First();
                        }
                        else if (x.CourseModuleId != null && x.CourseModuleId > 0) {
                            code = db.CourseModule
                                        .Where(cm => cm.ModuleId == x.CourseModuleId)
                                        .Select(cm => cm.ModuleName)
                                        .First();
                        }
                        dto.Add(new AssignementDTO {
                            AnswerId = x.AnswerId,
                            AssignementId = x.AssignementId,
                            GradeId = x.GradeId,
                            HomeworkDeadline = hw.HomeworkDeadline,
                            HomeworkDescription = hw.HomeworkDescription,
                            HomeworkId = hw.HomeworkId,
                            HomeworkName = hw.HomeworkName,
                            HomeworkPoints = hw.HomeworkPoints,
                            HomeworkCode = hw.HomeworkCode,
                            RecipientId = studentId,
                            SubjectCode = code
                        });
                    }
                    return dto;
                }
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
        }

        /// <summary>
        /// Gets all group assigned homework.
        /// </summary>
        /// <param name="groupId">The group identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public List<AssignementDTO> GetAllGroupAssignedHomework(int groupId) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    List<HomeworkAssignements> ha = db.HomeworkAssignements
                                                        .Where(a => a.GroupId == groupId)
                                                        .ToList();

                    List<AssignementDTO> dto = new List<AssignementDTO>();
                    foreach (var x in ha) {
                        Homeworks hw = db.Homeworks
                                            .Where(h => h.HomeworkId == x.HomeworkId)
                                            .First();
                        string code = "";
                        if (x.CourseId != null && x.CourseId > 0) {
                            code = db.Courses
                                        .Where(c => c.CourseId == x.CourseId)
                                        .Select(c => c.CourdeCode)
                                        .First();
                        }
                        else if (x.CourseModuleId != null && x.CourseModuleId > 0) {
                            code = db.CourseModule
                                        .Where(cm => cm.ModuleId == x.CourseModuleId)
                                        .Select(cm => cm.ModuleName)
                                        .First();
                        }
                        dto.Add(new AssignementDTO {
                            AnswerId = x.AnswerId,
                            AssignementId = x.AssignementId,
                            GradeId = x.GradeId,
                            HomeworkDeadline = hw.HomeworkDeadline,
                            HomeworkDescription = hw.HomeworkDescription,
                            HomeworkId = hw.HomeworkId,
                            HomeworkName = hw.HomeworkName,
                            HomeworkPoints = hw.HomeworkPoints,
                            HomeworkCode = hw.HomeworkCode,
                            RecipientId = groupId,
                            SubjectCode = code
                        });
                    }
                    return dto;
                }
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
        }

        /// <summary>
        /// Submits the homework.
        /// </summary>
        /// <param name="assignementId">The assignement identifier.</param>
        /// <param name="answer">The answer.</param>
        /// <exception cref="CustomException">
        /// </exception>
        public void SubmitHomework(int assignementId, Answers answer) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    HomeworkAssignements ha = db.HomeworkAssignements
                                                    .Where(a => a.AssignementId == assignementId)
                                                    .First();

                    //if answer doesn't already exist
                    if (ha.AnswerId == null) {
                        //add the new answer
                        db.Answers.Add(answer);
                        db.SaveChanges();

                        //add the newly created answer to the homework
                        ha.AnswerId = answer.AnswerId;
                        db.HomeworkAssignements.Attach(ha);
                        db.Entry(ha).State = System.Data.EntityState.Modified;
                        db.SaveChanges();
                    }
                    else {
                        //else only update the existing answer
                        Answers ans = db.Answers
                                            .Where(a => a.AnswerId == ha.AnswerId)
                                            .First();
                        ans.AnswerValue = answer.AnswerValue;
                        db.Answers.Attach(ans);
                        db.Entry(ans).State = System.Data.EntityState.Modified;
                        db.SaveChanges();
                    }
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
    }
}