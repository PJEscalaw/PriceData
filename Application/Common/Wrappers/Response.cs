using Application.Common.Exceptions;

namespace Application.Common.Wrappers
{
    public class Response<T>
    {
        public Response()
        {
            StatusCode = 200; //success (OK)
            Succeeded = true;
        }
        public Response(T data, string message = null)
        {
            StatusCode = 200; //success (OK)
            Succeeded = true;
            Message = message;
            Data = data;
        }

        public Response(int statusCode, string message)
        {
            throw new ResponseException(statusCode, message);
        }
        public int StatusCode { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }

        public object Errors { get; }

        public T Data { get; set; }

    }
}
