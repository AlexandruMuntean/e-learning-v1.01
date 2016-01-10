using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_LearningApplication.CustomExceptions {
    class CustomException : Exception {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomException"/> class.
        /// </summary>
        public CustomException() {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public CustomException(string message)
            : base(message) {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner.</param>
        public CustomException(string message, Exception inner)
            : base(message, inner) {
        }
    }
}
