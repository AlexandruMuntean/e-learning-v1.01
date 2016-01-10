using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using E_LearningServices.CustomExceptions;
using Google.Apis.Drive.v2;
using GoogleDataAPI = Google.Apis.Drive.v2.Data;
using E_LearningServices.Models;

namespace E_LearningServices.Services.Interfaces {
    public interface IResourcesManagement {
        #region Resources from Drive

        /// <summary>
        /// Creates the directory on the drive.
        /// </summary>
        /// <param name="_title">The _title.</param>
        /// <param name="_description">The _description.</param>
        /// <param name="_parent">The _parent.</param>
        /// <returns></returns>
        GoogleDataAPI.File CreateDirectory(string _title, string _description, string _parent);

        /// <summary>
        /// Uploads the file to drive.
        /// </summary>
        /// <param name="_uploadFile">The _upload file.</param>
        /// <param name="_parent">The _parent.</param>
        /// <returns></returns>
        GoogleDataAPI.File UploadFile(string _uploadFile, string _parent);

        /// <summary>
        /// Deletes the file from drive.
        /// </summary>
        /// <param name="fileId">The file identifier.</param>
        void DeleteFile(String fileId);

        /// <summary>
        /// Updates the fileon drive.
        /// </summary>
        /// <param name="fileId">The file identifier.</param>
        /// <param name="newTitle">The new title.</param>
        /// <param name="newDescription">The new description.</param>
        /// <param name="newFilename">The new filename.</param>
        /// <param name="newRevision">if set to <c>true</c> [new revision].</param>
        /// <returns></returns>
        //GoogleDataAPI.File updateFile(String fileId, String newTitle, String newDescription, String newFilename, bool newRevision);

        /// <summary>
        /// Downloads the file from drive.
        /// </summary>
        /// <param name="_fileResource">The _file resource.</param>
        /// <param name="_saveTo">The _save to.</param>
        /// <returns></returns>
        //Boolean DownloadFile(GoogleDataAPI.File _fileResource, string _saveTo);

        /// <summary>
        /// Gets all files from drive.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="CustomException"></exception>
        //IList<Google.Apis.Drive.v2.Data.File> GetAllFiles();

        #endregion

        #region Resources from DB

        /// <summary>
        /// Saves the resources to database.
        /// </summary>
        /// <param name="resources">The resources.</param>
        /// <exception cref="CustomException"></exception>
        void SaveResourcesToDb(Resources resources);

        string GetFileIdForDirectory(int courseId);

        /// <summary>
        /// Deletes the course.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="CustomException">
        /// </exception>
        void DeleteCourse(int id);

        /// <summary>
        /// Gets the file identifier for a directory.
        /// </summary>
        /// <param name="CourseId">The course identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">
        /// </exception>
        string GetFileIdForADirectory(int? courseId);

        void DeleteHomeworkResource(string FileName);
        Resources GetResourceByNameAndCourseId(string FileName);
        string GetFileIdForAModule(int moduleId);

        /// <summary>
        /// Gets the module identifier by name and course identifier.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="courseId">The course identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">
        /// </exception>
        int GetModuleIdByNameAndCourseId(string p, int? nullable);

        /// <summary>
        /// Deletes the module.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="CustomException">
        /// </exception>
        void DeleteModule(int id);

        /// <summary>
        /// Gets the course resources.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">
        /// </exception>
        List<Resources> GetCourseResources(int id);

        /// <summary>
        /// Gets the resource by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">
        /// </exception>
        Resources GetResourceById(int id);

        /// <summary>
        /// Deletes the course resource.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="CustomException">
        /// </exception>
        void DeleteCourseResource(int id);

        /// <summary>
        /// Deletes the module resource.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="CustomException">
        /// </exception>
        void DeleteModuleResource(int id);

        /// <summary>
        /// Gets the module resources.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="CustomException">
        /// </exception>
        List<Resources> GetModuleResources(int id);

        #endregion

        Resources GetResourceByHomeworkCode(string id);
    }
}
