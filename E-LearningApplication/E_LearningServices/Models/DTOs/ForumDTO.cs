using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_LearningServices.Models.DTOs {
    public class ForumDTO {
        public int ForumId { get; set; }

        public string Category { get; set; }

        public Nullable<int> OwnerId { get; set; }
    }
}