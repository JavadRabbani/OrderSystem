using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedKernel.Dto
{
    public class ApiResult<T>
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public List<string> Errors { get; set; } = new();

        public T Data { get; set; }

        public static ApiResult<T> Ok(T data, string? message = null) =>
            new()
            {
                Success = true,
                Data = data,
                Message = message
            };

        public static ApiResult<T> Fail(string message, List<string>? errors = null) =>
            new()
            {
                Success = false,
                Message = message,
                Errors = errors ?? new List<string>()
            };
    }
}