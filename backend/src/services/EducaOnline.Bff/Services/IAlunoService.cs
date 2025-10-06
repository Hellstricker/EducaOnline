using EducaOnline.Bff.Extensions;
using EducaOnline.Bff.Models;
using Microsoft.Extensions.Options;

namespace EducaOnline.Bff.Services
{
    public interface IAlunoService
    {
        Task<MatriculaDto?> ObterMatricula(Guid id);
    }

    public class AlunoService : Service, IAlunoService
    {
        private readonly HttpClient _httpClient;

        public AlunoService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.AlunoUrl);
        }

        public async Task<MatriculaDto?> ObterMatricula(Guid id)
        {
            var response = await _httpClient.GetAsync($"/api/Alunos/{id}/matricula");
            TratarErrosResponse(response);
            return await DeserializarObjetoResponse<MatriculaDto>(response);
        }
    }    
}
