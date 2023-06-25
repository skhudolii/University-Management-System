using University.Core.Enums;

namespace University.Core.Response.Interfeces
{
    public interface IBaseResponse<T>
    {
        string Description { get; }
        StatusCode StatusCode { get; }
        T Data { get; }
    }
}
