using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_LearningApplication.Models.DTOs
{
    public class UsersInCourseDTO
    {
        public int UserId { get; set; }
        public int CourseId { get; set; }
        public string EnrollementKey { get; set; }
        public string CourseUserstatus { get; set; }
    }
}