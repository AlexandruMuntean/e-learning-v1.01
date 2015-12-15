using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_LearningServices.Models.DTOs {
    public class GroupMemberDTO {
        public int GroupMemberId { get; set; }
        public Nullable<int> GroupId { get; set; }
        public Nullable<int> MemberId { get; set; }
    }
}