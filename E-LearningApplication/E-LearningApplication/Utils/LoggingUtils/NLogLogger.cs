using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NLog;

namespace E_LearningApplication.Utils.LoggingUtils {
    public class NLogLogger : ILoggingUtil {
        private static Logger _logger;

        public NLogLogger() {
            //_logger = LogManager.GetCurrentClassLogger();
        }

        public void Info(string message) {
            //_logger.Info(message);
        }

        public void Info(Exception ex, string message, object[] args = null) {
            //_logger.Info(ex, message, args);
        }

        public void Trace(string message) {
            _logger.Trace(message);
        }

        public void Trace(Exception ex, string message, object[] args = null) {
            //_logger.Trace(ex, message, args);
        }

        public void Debug(string message) {
            //_logger.Debug(message);
        }

        public void Debug(Exception ex, string message, object[] args = null) {
            //_logger.Debug(ex, message, args);
        }

        public void Warn(string message) {
            //_logger.Warn(message);
        }

        public void Warn(Exception ex, string message, object[] args = null) {
            //_logger.Warn(ex, message, args);
        }

        public void Error(string message) {
            //_logger.Error(message);
        }

        public void Error(Exception ex, string message, object[] args = null) {
            //_logger.Error(ex, message, args);
        }

        public void Fatal(string message) {
            //_logger.Fatal(message);
        }

        public void Fatal(Exception ex, string message, object[] args = null) {
            //_logger.Fatal(ex, message, args);
        }
    }
}