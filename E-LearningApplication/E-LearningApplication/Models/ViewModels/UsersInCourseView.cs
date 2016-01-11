using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace E_LearningApplication.Models.ViewModels
{
    public class UsersInCourseView
    {
        public int UserId { get; set; }

        public int CourseId { get; set; }


        [Display(Name = "Enrollement Key")]
        [StringLength(14)]
        public string EnrollementKey { get; set; }

        [Display(Name = "Status user at course")]
        [StringLength(30)]
        public string CourseUserstatus { get; set; }

    }
}