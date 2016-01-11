using E_LearningServices.CustomExceptions;
using E_LearningServices.Models;
using E_LearningServices.Models.DTOs;
using E_LearningServices.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_LearningServices.Services {
    public class GradesManagement : IGradesManagement {
        #region Grades CRUD - Prof functionalities

        /// <summary>
        /// Gets all course given grades.
        /// </summary>
        /// <param name="courseId">The course identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public List<ReceivedGradeDTO> GetAllCourseGivenGrades(int courseId) {
            try {
                List<ReceivedGradeDTO> dtoList = new List<ReceivedGradeDTO>();
                using (var db = new ELearningDatabaseEntities()) {
                    var assignements = db.HomeworkAssignements
                                            .Where(a => a.CourseId == courseId)
                                            .ToList();
                    foreach (var a in assignements) {
                        var answer = db.Answers
                                            .Where(ans => ans.AnswerId == a.AnswerId)
                                            .FirstOrDefault();
                        var grade = db.Grades
                                            .Where(g => g.GradeId == a.GradeId)
                                            .FirstOrDefault();
                        var homework = db.Homeworks
                                            .Where(h => h.HomeworkId == a.HomeworkId)
                                            .FirstOrDefault();
                        var recipient = "";
                        if (a.StudentId != null && a.StudentId > 0) {
                            var student = db.Users
                                                .Where(u => u.UserId == a.StudentId)
                                                .First();
                            recipient = recipient + student.UserName + "-" + student.LastName + " " + student.FirstName;
                            if (a.GroupId != null && a.GroupId > 0) {
                                var group = db.Groups
                                                .Where(g => g.GroupId == a.GroupId)
                                                .First();
                                recipient = recipient + "; " + group.GroupName;
                            }
                        }
                        else if (a.GroupId != null && a.GroupId > 0) {
                            var group = db.Groups
                                            .Where(g => g.GroupId == a.GroupId)
                                            .First();
                            recipient = recipient + group.GroupName;
                        }

                        if (answer != null && grade != null && homework != null) {
                            dtoList.Add(new ReceivedGradeDTO {
                                AnswerId = answer.AnswerId,
                                AnswerValue = answer.AnswerValue,
                                GradeId = grade.GradeId,
                                Gradedatetime = grade.Gradedatetime,
                                GradeValue = grade.GradeValue,
                                HomeworkDeadline = homework.HomeworkDeadline,
                                HomeworkDescription = homework.HomeworkDescription,
                                HomeworkName = homework.HomeworkName,
                                HomeworkPoints = homework.HomeworkPoints,
                                RecipientName = recipient
                            });
                        }
                    }
                }

                return dtoList;
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
        }

        /// <summary>
        /// Gets all course module given grades.
        /// </summary>
        /// <param name="courseId">The course identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public List<ReceivedGradeDTO> GetAllCourseModuleGivenGrades(int courseModuleId) {
            try {
                List<ReceivedGradeDTO> dtoList = new List<ReceivedGradeDTO>();
                using (var db = new ELearningDatabaseEntities()) {
                    var assignements = db.HomeworkAssignements
                                            .Where(a => a.CourseModuleId == courseModuleId)
                                            .ToList();
                    foreach (var a in assignements) {
                        var answer = db.Answers
                                            .Where(ans => ans.AnswerId == a.AnswerId)
                                            .FirstOrDefault();
                        var grade = db.Grades
                                            .Where(g => g.GradeId == a.GradeId)
                                            .FirstOrDefault();
                        var homework = db.Homeworks
                                            .Where(h => h.HomeworkId == a.HomeworkId)
                                            .FirstOrDefault();
                        var recipient = "";
                        if (a.StudentId != null && a.StudentId > 0) {
                            var student = db.Users
                                                .Where(u => u.UserId == a.StudentId)
                                                .First();
                            recipient = recipient + student.UserName + "-" + student.LastName + " " + student.FirstName;
                            if (a.GroupId != null && a.GroupId > 0) {
                                var group = db.Groups
                                                .Where(g => g.GroupId == a.GroupId)
                                                .First();
                                recipient = recipient + "; " + group.GroupName;
                            }
                        }
                        else if (a.GroupId != null && a.GroupId > 0) {
                            var group = db.Groups
                                            .Where(g => g.GroupId == a.GroupId)
                                            .First();
                            recipient = recipient + group.GroupName;
                        }

                        if (answer != null && grade != null && homework != null) {
                            dtoList.Add(new ReceivedGradeDTO {
                                AnswerId = answer.AnswerId,
                                AnswerValue = answer.AnswerValue,
                                GradeId = grade.GradeId,
                                Gradedatetime = grade.Gradedatetime,
                                GradeValue = grade.GradeValue,
                                HomeworkDeadline = homework.HomeworkDeadline,
                                HomeworkDescription = homework.HomeworkDescription,
                                HomeworkName = homework.HomeworkName,
                                HomeworkPoints = homework.HomeworkPoints,
                                RecipientName = recipient
                            });
                        }
                    }
                }

                return dtoList;
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
        }

        /// <summary>
        /// Grades the homework.
        /// </summary>
        /// <param name="grade">The grade.</param>
        /// <param name="dto">The homework grade dto.</param>
        /// <exception cref="CustomException">
        /// </exception>
        public void GradeHomework(Grades grade, HomeworkGradeDTO dto) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    HomeworkAssignements ha = db.HomeworkAssignements
                                                    .Where(h => h.AssignementId == dto.AssignementId)
                                                    .First();

                    //if the homework was already graded, we just update the grade value
                    if (ha.GradeId != null && ha.GradeId > 0) {
                        Grades gr = db.Grades
                                            .Where(g => g.GradeId == ha.GradeId)
                                            .First();
                        gr.GradeValue = grade.GradeValue;
                        gr.Gradedatetime = grade.Gradedatetime;
                        db.Grades.Attach(gr);
                        db.Entry(gr).State = System.Data.EntityState.Modified;
                        db.SaveChanges();
                    }
                    else {
                        //add the new grade
                        db.Grades.Add(grade);
                        db.SaveChanges();

                        //attach the grade to the homework
                        ha.GradeId = grade.GradeId;
                        db.HomeworkAssignements.Attach(ha);
                        db.Entry(ha).State = System.Data.EntityState.Modified;
                        db.SaveChanges();
                    }
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

        #region Grades CRUD - Student functionalities

        /// <summary>
        /// Gets all student received grades.
        /// </summary>
        /// <param name="studentId">The student identifier.</param>
        /// <returns></returns>
        public List<ReceivedGradeDTO> GetAllStudentReceivedGrades(int studentId) {
            try {
                List<ReceivedGradeDTO> dtoList = new List<ReceivedGradeDTO>();
                using (var db = new ELearningDatabaseEntities()) {
                    var assignements = db.HomeworkAssignements
                                            .Where(a => a.StudentId == studentId)
                                            .ToList();
                    foreach (var a in assignements) {
                        var answer = db.Answers
                                            .Where(ans => ans.AnswerId == a.AnswerId)
                                            .FirstOrDefault();
                        var grade = db.Grades
                                            .Where(g => g.GradeId == a.GradeId)
                                            .FirstOrDefault();
                        var homework = db.Homeworks
                                            .Where(h => h.HomeworkId == a.HomeworkId)
                                            .FirstOrDefault();
                        string subject = "";
                        if (a.CourseId != null && a.CourseId > 0) {
                            subject = db.Courses
                                            .Where(c => c.CourseId == a.CourseId)
                                            .First()
                                            .CourdeCode;
                        }
                        else if (a.CourseModuleId != null && a.CourseModuleId > 0) {
                            subject = db.CourseModule
                                            .Where(cm => cm.ModuleId == a.CourseModuleId)
                                            .First()
                                            .ModuleName;
                        }

                        if (answer != null && grade != null && homework != null) {
                            dtoList.Add(new ReceivedGradeDTO {
                                AnswerId = answer.AnswerId,
                                AnswerValue = answer.AnswerValue,
                                GradeId = grade.GradeId,
                                Gradedatetime = grade.Gradedatetime,
                                GradeValue = grade.GradeValue,
                                HomeworkDeadline = homework.HomeworkDeadline,
                                HomeworkDescription = homework.HomeworkDescription,
                                HomeworkName = homework.HomeworkName,
                                HomeworkPoints = homework.HomeworkPoints,
                                StudentId = studentId,
                                SubjectCode = subject
                            });
                        }
                    }
                }

                return dtoList;
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
        }

        /// <summary>
        /// Gets all group received grades.
        /// </summary>
        /// <param name="groupId">The group identifier.</param>
        /// <returns></returns>
        public List<ReceivedGradeDTO> GetAllGroupReceivedGrades(int groupId) {
            try {
                List<ReceivedGradeDTO> dtoList = new List<ReceivedGradeDTO>();
                using (var db = new ELearningDatabaseEntities()) {
                    var assignements = db.HomeworkAssignements
                                            .Where(a => a.GroupId == groupId)
                                            .ToList();
                    foreach (var a in assignements) {
                        var answer = db.Answers
                                            .Where(ans => ans.AnswerId == a.AnswerId)
                                            .FirstOrDefault();
                        var grade = db.Grades
                                            .Where(g => g.GradeId == a.GradeId)
                                            .FirstOrDefault();
                        var homework = db.Homeworks
                                            .Where(h => h.HomeworkId == a.HomeworkId)
                                            .FirstOrDefault();
                        string subject = "";
                        if (a.CourseId != null && a.CourseId > 0) {
                            subject = db.Courses
                                            .Where(c => c.CourseId == a.CourseId)
                                            .First()
                                            .CourdeCode;
                        }
                        else if (a.CourseModuleId != null && a.CourseModuleId > 0) {
                            subject = db.CourseModule
                                            .Where(cm => cm.ModuleId == a.CourseModuleId)
                                            .First()
                                            .ModuleName;
                        }

                        if (answer != null && grade != null && homework != null) {
                            dtoList.Add(new ReceivedGradeDTO {
                                AnswerId = answer.AnswerId,
                                AnswerValue = answer.AnswerValue,
                                GradeId = grade.GradeId,
                                Gradedatetime = grade.Gradedatetime,
                                GradeValue = grade.GradeValue,
                                HomeworkDeadline = homework.HomeworkDeadline,
                                HomeworkDescription = homework.HomeworkDescription,
                                HomeworkName = homework.HomeworkName,
                                HomeworkPoints = homework.HomeworkPoints,
                                GroupId = groupId,
                                SubjectCode = subject
                            });
                        }
                    }
                }

                return dtoList;
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
        }

        #endregion
    }
}