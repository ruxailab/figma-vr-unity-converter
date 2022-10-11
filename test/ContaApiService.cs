using System.Threading.Tasks;
using Refit;

namespace Teste
{
    public interface ContaApiService
    {
        [Get("/v1/me")]
        Task<ContaResponse> GetContaAsync([Header("X-Figma-Token")] string token);

    }
}