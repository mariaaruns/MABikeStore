using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Application.Common
{
    public static class AppConstant
    {
        public const string CREATED_SUCCESS = "Created successfully";
        public const string UPDATED_SUCCESS = "Updated successfully";
        public const string DELETED_SUCCESS = "Deleted successfully";

        public const string CRAETE_FAILED = "Create failed";
        public const string UPDATE_FAILED = "Update failed";
        public const string DELETE_FAILED = "Delete failed";
        public const string RECORDS_NOT_FOUND = "Records not found";


        #region UserRoles
        public const string ADMIN      = "ADMIN";
        public const string SUPERADMIN = "SUPERADMIN";
        public const string TECHNICIAN = "TECHNICIAN";

        #endregion

        #region staticFilePath

        public const string AVATAR_PATH = "~/Assets/Uploads/Avatars";
        #endregion



        #region statuscodeMessage
        public const string SUCCESS = "Request processed successfully.";
        public const string CREATED = "Resource successfully created.";
        public const string NOCONTENT = "Request processed successfully, but no content is returned.";
        public const string SERVER_ERROR = "Internals server error";
        public const string BADREQUEST = "The request is invalid or malformed.";
        public const string UNAUTHORIZE = "Authentication is required or has failed.";
        public const string FORBIDDEN = "Access to the requested resource is denied.";
        public const string NOTFOUND = "The requested resource could not be found.";
        public const string MethodNotAllowed = "The HTTP method is not supported for the requested resource.";
        public const string CONFLICT = "A conflict occurred, such as duplicate data or resource already exists.";
        public const string UNPROCESSABLE_ENTITY = "The request was well-formed but could not be processed.";
        #endregion

        #region validation mesage
        public const string USER_ALREADY_EXIST = "User with the provided details already exists.";
        public const string INVALID_INPUT = "One or more fields in the input are invalid.";
        public const string TOKEN_EXPIRED = "The provided token is no longer valid.";
        public const string INSUFFICIENT_PERMISSION = "The user does not have the required permissions.";
        public const string RATE_LIMIT_EXCEED = "Too many requests have been made in a short time.";
        #endregion
    }
}
