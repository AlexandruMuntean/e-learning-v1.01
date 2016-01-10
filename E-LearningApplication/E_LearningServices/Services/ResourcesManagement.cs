using E_LearningServices.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using GoogleDataAPI = Google.Apis.Drive.v2.Data;
using System.Threading;
using E_LearningServices.Models;
using E_LearningServices.CustomExceptions;
using E_LearningServices.Utils;

namespace E_LearningServices.Services {
    /// <summary>
    /// Class implementing CRUD operations for handling the application resources
    /// </summary>
    public class ResourcesManagement : IResourcesManagement {
        #region PrivateFields

        private DriveService _service;
        string[] scopes = new string[] { DriveService.Scope.Drive, DriveService.Scope.DriveFile };
        ////the credential to have acces to google drive
        private static string clientId = "784803610556-ik1lslqd89pt0r1ctnue5s53sg10gdnb.apps.googleusercontent.com";
        private static string clientSecret = "4BD3Q7TOriN-ZdEe4VYVb0V6";          //  https://console.developers.google.com
        ////the id of the ElUpToMe folder
        private static readonly string ElUpToMe = "0B3-zDDotAEInbUs3bTJTV3p1Um8";

        #endregion

        #region Resources from Drive

        /// <summary>
        /// Initializes the service.
        /// </summary>
        public void InitializeService() {
        //    // here is where we Request the user to give us access, or use the Refresh Token that was previously stored in %AppData%
            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets {
                ClientId = clientId,
                ClientSecret = clientSecret
           }, scopes,
              Environment.UserName,
             CancellationToken.None,
              new FileDataStore("Daimto.GoogleDrive.Auth.Store")).Result;

            this._service = new DriveService(new BaseClientService.Initializer() {
                HttpClientInitializer = credential,
                ApplicationName = "GoogleDocsAPI",
           });
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourcesManagement"/> class.
        /// </summary>
        public ResourcesManagement() {
            InitializeService();
        }

        /// <summary>
        /// Creates the directory on the drive.
        /// </summary>
        /// <param name="_title">The _title.</param>
        /// <param name="_description">The _description.</param>
        /// <param name="_parent">The _parent.</param>
        /// <returns></returns>
        public GoogleDataAPI.File CreateDirectory(string _title, string _description, string _parent) {

            GoogleDataAPI.File NewDirectory = null;
            // Create metaData for a new Directory
            GoogleDataAPI.File body = new GoogleDataAPI.File();
            body.Title = _title;
            body.Description = _description;
            body.MimeType = "application/vnd.google-apps.folder";
            body.Parents = new List<GoogleDataAPI.ParentReference>() { new GoogleDataAPI.ParentReference() { Id = _parent } };
            try
            {
                FilesResource.InsertRequest request = _service.Files.Insert(body);
                NewDirectory = request.Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }

            return NewDirectory;
        }

        /// <summary>
        /// Gets the type of the MIME.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns></returns>
        private static string GetMimeType(string fileName)
        {
            try
            {
                string mimeType = "application/unknown";
                string ext = System.IO.Path.GetExtension(fileName).ToLower();
                Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
                if (regKey != null && regKey.GetValue("Content Type") != null)
                    mimeType = regKey.GetValue("Content Type").ToString();
                return mimeType;
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Uploads the file to drive.
        /// </summary>
        /// <param name="_uploadFile">The _upload file.</param>
        /// <param name="_parent">The _parent.</param>
        /// <returns></returns>
        public GoogleDataAPI.File UploadFile(string _uploadFile, string _parent)
        {
            try
            {
                if (System.IO.File.Exists(_uploadFile))
                {
                    GoogleDataAPI.File body = new GoogleDataAPI.File();
                    body.Title = System.IO.Path.GetFileName(_uploadFile);
                    body.Description = "File uploaded by me";
                    body.MimeType = GetMimeType(_uploadFile);
                    body.Parents = new List<GoogleDataAPI.ParentReference>() { new GoogleDataAPI.ParentReference() { Id = _parent } };

                    // File's content.
                    byte[] byteArray = System.IO.File.ReadAllBytes(_uploadFile);
                    System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArray);
                    try
                    {
                        FilesResource.InsertMediaUpload request = _service.Files.Insert(body, stream, GetMimeType(_uploadFile));
                        request.Upload();
                        return request.ResponseBody;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("An error occurred: " + e.Message);
                        return null;
                    }
                }
                else
                {
                    Console.WriteLine("File does not exist: " + _uploadFile);
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the file from drive.
        /// </summary>
        /// <param name="fileId">The file identifier.</param>
        public void DeleteFile(String fileId)
        {
            try
            {
                _service.Files.Delete(fileId).Execute();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred when trying delete the file: " + e.Message);
            }
        }

        /// <summary>
        /// Updates the file on drive.
        /// </summary>
        /// <param name="fileId">The file identifier.</param>
        /// <param name="newTitle">The new title.</param>
        /// <param name="newDescription">The new description.</param>
        /// <param name="newFilename">The new filename.</param>
        /// <param name="newRevision">if set to <c>true</c> [new revision].</param>
        /// <returns></returns>
        public GoogleDataAPI.File updateFile(String fileId, String newTitle, String newDescription, String newFilename, bool newRevision)
        {
            try
            {
                // First retrieve the file from the API.
                GoogleDataAPI.File file = _service.Files.Get(fileId).Execute();

                // File's new metadata.
                file.Title = newTitle;
                file.Description = newDescription;
                file.MimeType = GetMimeType(newFilename);

                // File's new content.
                byte[] byteArray = System.IO.File.ReadAllBytes(newFilename);
                System.IO.MemoryStream stream = new System.IO.MemoryStream(byteArray);
                // Send the request to the API.
                FilesResource.UpdateMediaUpload request = _service.Files.Update(file, fileId, stream, file.MimeType);
                request.NewRevision = newRevision;
                request.Upload();

                GoogleDataAPI.File updatedFile = request.ResponseBody;
                return updatedFile;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
                return null;
            }

        }

        /// <summary>
        /// Downloads the file from drive.
        /// </summary>
        /// <param name="_fileResource">The _file resource.</param>
        /// <param name="_saveTo">The _save to.</param>
        /// <returns></returns>
        public Boolean DownloadFile(GoogleDataAPI.File _fileResource, string _saveTo)
        {

            if (!String.IsNullOrEmpty(_fileResource.DownloadUrl))
            {
                try
                {
                    var x = _service.HttpClient.GetByteArrayAsync(_fileResource.DownloadUrl);
                    byte[] arrBytes = x.Result;
                    System.IO.File.WriteAllBytes(_saveTo, arrBytes);
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("An error occurred: " + e.Message);
                    return false;
                }
            }
            else
            {
                // The file doesn't have any content stored on Drive.
                return false;
            }
        }

        /// <summary>
        /// Gets all files from drive.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        public IList<Google.Apis.Drive.v2.Data.File> GetAllFiles()
        {
            try
            {
                FilesResource.ListRequest listRequest = _service.Files.List();
                IList<Google.Apis.Drive.v2.Data.File> files = listRequest.Execute()
                    .Items;
                return files;
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message);
            }
        }

        #endregion

        #region Resources from DB

        /// <summary>
        /// Saves the resources to database.
        /// </summary>
        /// <param name="resources">The resources.</param>
        /// <exception cref="CustomException"></exception>
        public void SaveResourcesToDb(Resources resources) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    db.Resources.Add(resources);
                    db.SaveChanges();
                }
            }
            catch (Exception ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the course.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="CustomException">
        /// </exception>
        public void DeleteCourse(int id) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    var result = db.Resources
                                        .Where(x => x.CourseId == id)
                                        .First();
                    if (result != null) 
                    {
                        //sterg din interior spre exterior
                        var resourceList = db.Resources.Where(x => x.CourseId == result.CourseId).ToList();

                        foreach (var resursa in resourceList)
                        {
                                DeleteFile(resursa.FileId);
                                db.Resources.Remove(resursa);
                        }
                        
                        string fileId = result.FileId;
                        DeleteFile(fileId);
                        db.Resources.Remove(result);
                        db.SaveChanges();
                    }
                }
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
            catch (InvalidOperationException ioe) {
                throw new CustomException(ioe.Message);
            }
            catch (Exception ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the module.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="CustomException">
        /// </exception>
        public void DeleteModule(int id) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    var result = db.Resources
                                        .First(x => x.ModuleID == id);
                    if (result != null)
                    {
                        var resourceList = db.Resources.Where(x => x.ModuleID == result.ModuleID).ToList();

                        foreach (var resursa in resourceList)
                        {
                            DeleteFile(resursa.FileId);
                            db.Resources.Remove(resursa);
                        }

                        string fileId = result.FileId;
                        //for drive
                        this.DeleteFile(fileId);
                        db.Resources.Remove(result);
                        db.SaveChanges();
                    }
                }
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
            catch (InvalidOperationException ioe) {
                throw new CustomException(ioe.Message);
            }
            catch (Exception ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Gets the file identifier for a directory.
        /// </summary>
        /// <param name="CourseId">The course identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">
        /// </exception>
        public string GetFileIdForADirectory(int? courseId) {
            try {

                int courserValue = courseId.GetValueOrDefault();
                using (var db = new ELearningDatabaseEntities()) {
                    var result = db.Resources
                                        .Where(x => x.CourseId == courserValue)
                                        .First();
                    if (result != null) {
                        return result.FileId;
                    }
                }
                return string.Empty;
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
            catch (Exception ex) {
                throw new CustomException(ex.Message);
            }
        }

        public Resources GetResourceByNameAndCourseId(string FileName)
        {
            try
            {
                using (var db = new ELearningDatabaseEntities())
                {
                    return db.Resources
                        .Where(r => r.FileName == FileName)
                        .FirstOrDefault();
                }
            }
            catch (ArgumentNullException ane)
            {
                throw new CustomException(ane.Message);
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message);
            }
        }

        public string GetFileIdForDirectory(int courseId)
        {
            try
            {

               using (var db = new ELearningDatabaseEntities())
                {
                    var result = db.Resources
                                        .Where(x => x.CourseId == courseId)
                                        .First();
                    if (result != null)
                    {
                        return result.FileId;
                    }
                }
                return string.Empty;
            }
            catch (ArgumentNullException ane)
            {
                throw new CustomException(ane.Message);
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message);
            }
        }

        public string GetFileIdForAModule(int moduleId)
        {
            try
            {
                using (var db = new ELearningDatabaseEntities())
                {
                    var result = db.Resources
                                        .Where(x => x.ModuleID == moduleId)
                                        .First();
                    if (result != null)
                    {
                        return result.FileId;
                    }
                }
                return string.Empty;
            }
            catch (ArgumentNullException ane)
            {
                throw new CustomException(ane.Message);
            }
            catch (Exception ex)
            {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Gets the module identifier by name and course identifier.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="courseId">The course identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">
        /// </exception>
        public int GetModuleIdByNameAndCourseId(string name, int? courseId) {
            try {
                int courseID = courseId ?? default(int);
                using (var db = new ELearningDatabaseEntities()) {
                    var result = db.CourseModule
                                        .Where(x => x.CourseId == courseID && x.ModuleName == name)
                                        .First();
                    if (result != null) {
                        return result.ModuleId;
                    }
                }

                return default(int);
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
            catch (Exception ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Gets the course resources.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">
        /// </exception>
        public List<Resources> GetCourseResources(int id) {
            try {
                var resourceType = ResourceEnum.File.ToString();
                using (var db = new ELearningDatabaseEntities()) {
                    return db.Resources
                                .Where(r => r.CourseId == id && r.ResourceType.Equals(resourceType))
                                .ToList();
                }
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
            catch (Exception ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Gets the resource by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">
        /// </exception>
        public Resources GetResourceById(int id) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    return db.Resources
                                .Where(r => r.ResourceId == id)
                                .First();
                }
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
            catch (Exception ex) {
                throw new CustomException(ex.Message);
            }
        }

        public Resources GetResourceByHomeworkCode(string id) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    return db.Resources.Where(x => x.FileName == id).First();
                }
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
            catch (Exception ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the course resource.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="CustomException">
        /// </exception>
        public void DeleteCourseResource(int id) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    var resource = db.Resources
                                        .Where(r => r.ResourceId == id)
                                        .First();
                    if (resource != null){
                        string fildeId = resource.FileId;
                        DeleteFile(fildeId);
                        db.Resources.Remove(resource);
                        db.SaveChanges();
                    }
                }
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
            catch (Exception ex) {
                throw new CustomException(ex.Message);
            }
        }

        public void DeleteHomeworkResource(string fileName) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    var resource = db.Resources.Where(x => x.FileName == fileName).First();
                    if (resource != null) {
                        string fildeId = resource.FileId;
                        DeleteFile(fildeId);
                        db.Resources.Remove(resource);
                        db.SaveChanges();
                    }
                }
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
            catch (Exception ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the module resource.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="CustomException">
        /// </exception>
        public void DeleteModuleResource(int id) {
            try {
                using (var db = new ELearningDatabaseEntities()) {
                    var resource = db.Resources
                                        .Where(r => r.ResourceId == id)
                                        .First();
                    if (resource != null) {
                        string fileId = resource.FileId;
                        DeleteFile(fileId);
                        db.Resources.Remove(resource);
                        db.SaveChanges();
                    }
                }
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
            catch (Exception ex) {
                throw new CustomException(ex.Message);
            }
        }

        /// <summary>
        /// Gets the module resources.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">
        /// </exception>
        public List<Resources> GetModuleResources(int id) {
            try {
                var resourceType = ResourceEnum.File.ToString();
                using (var db = new ELearningDatabaseEntities()) {
                    return db.Resources
                                    .Where(r => r.ModuleID == id && r.ResourceType.Equals(resourceType))
                                    .ToList();
                }
            }
            catch (ArgumentNullException ane) {
                throw new CustomException(ane.Message);
            }
            catch (Exception ex) {
                throw new CustomException(ex.Message);
            }
        }

        #endregion

    }
}
