namespace MicroShopPOC.Extensions.Endpoints.Abstractions
{
    public interface IEndpointMapper
    {
        Task Map(WebApplication app);
    }
}
