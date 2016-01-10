using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E_LearningApplication.Utils {
    public static class PasswordGenerator {
        private static Random random = new Random();

        /// <summary>
        /// Gets the new password of specified length.
        /// </summary>
        /// <param name="length">The length.</param>
        /// <returns></returns>
        public static string GetNewPassword(int length) {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}