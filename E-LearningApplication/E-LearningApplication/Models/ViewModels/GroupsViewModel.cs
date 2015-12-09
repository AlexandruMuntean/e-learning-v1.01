using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace E_LearningApplication.Models.ViewModels {
    public class GroupsViewModel {
        public int GroupId { get; set; }

        [Display(Name="Group name")]
        [StringLength(30)]
        public string GroupName { get; set; }

        [Display(Name="Group description")]
        [StringLength(100)]
        public string GroupDescription { get; set; }

        [Display(Name="Group type")]
        [StringLength(30)]
        public string GroupType { get; set; }
    }
}