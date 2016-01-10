using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_LearningApplication.Models.ViewModels {
    public class AllMessagesViewModel {
        public AllMessagesViewModel() {
            mvm = new List<MessagesViewModel>();
        }

        public List<MessagesViewModel> mvm { get; set; }

        public Forums forum { get; set; }
    }
}