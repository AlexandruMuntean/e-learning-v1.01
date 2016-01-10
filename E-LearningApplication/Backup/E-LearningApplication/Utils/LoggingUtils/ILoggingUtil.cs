using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_LearningApplication.Utils.LoggingUtils {
    public interface ILoggingUtil {
        void Info(string message);

        void Info(Exception ex, string message, object[] args = null);

        void Trace(string message);

        void Trace(Exception ex, string message, object[] args = null);

        void Debug(string message);

        void Debug(Exception ex, string message, object[] args = null);

        void Warn(string message);

        void Warn(Exception ex, string message, object[] args = null);

        void Error(string message);

        void Error(Exception ex, string message, object[] args = null);

        void Fatal(string message);

        void Fatal(Exception ex, string message, object[] args = null);
    }
}
