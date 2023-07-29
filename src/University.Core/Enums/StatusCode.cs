namespace University.Core.Enums
{
    public enum StatusCode
    {
        // Successful responses
        OK = 200,
        NoContent = 204,

        // Client error responses
        NotFound = 404,
        PreconditionFailed = 412,

        // Server error responses
        InternalServerError = 500
    }
}
