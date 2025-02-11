using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BikeStore.Application.APIResponse
{
    //non generic response
    public class ApiResponse 
    {
        public string Status { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public object? Data { get; set; }
        public List<string> Errors { get; set; } = new();
        public Dictionary<string, object> Metadata { get; set; } = new();
    }


    //Generic Response
    public class ApiResponse<T>
    {
        public string Status { get; set; }         
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }      
        public T? Data { get; set; }              
        public List<string> Errors { get; set; } = new(); 
        public Dictionary<string, object> Metadata { get; set; } = new();

       
        public static ApiResponse<T> Success(string message,HttpStatusCode statusCode ,T data = default, Dictionary<string, object> metadata = null)
        {
            return new ApiResponse<T>
            {
                Status = "success",
                Message = message,
                Data = data,
                StatusCode=statusCode,
                Metadata = metadata ?? new Dictionary<string, object>()
            };
        }

        public static ApiResponse<T> Error(string message, HttpStatusCode statusCode, List<string> errors = null, Dictionary<string, object> metadata = null)
        {
            return new ApiResponse<T>
            {
                Status = "error",
                Message = message,
                StatusCode=statusCode,
                Errors = errors ?? new List<string>(),
                Metadata = metadata ?? new Dictionary<string, object>()
            };
        }
    }

   


}
