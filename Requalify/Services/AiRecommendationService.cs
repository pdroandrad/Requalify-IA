using OpenAI;
using OpenAI.Chat;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Requalify.Services
{
    public class AiRecommendationService
    {
        private readonly ChatClient _chatClient;

        public AiRecommendationService(IConfiguration config)
        {
            var apiKey = config["OPENAI_API_KEY"];
            if (string.IsNullOrWhiteSpace(apiKey))
                throw new InvalidOperationException("API key para OpenAI não configurada.");

            // Criando ChatClient com modelo “gpt-4o-mini” ou outro disponível
            _chatClient = new ChatClient(model: "gpt-4o-mini", apiKey: apiKey);
        }

        public async Task<string> GerarSugestaoCursos(string areaInteresse, string nivelExperiencia)
        {
            // Monta prompt
            var prompt = $@"
Você é um consultor de carreira especializado em requalificação profissional.
Área de interesse: {areaInteresse}
Nível de experiência: {nivelExperiencia}
Sugira 3 cursos relevantes para requalificação.";

            // Faz a chamada
            var completion = await _chatClient.CompleteChatAsync(prompt);

            // A resposta contém várias “Content” em completion.Value.Content
            var textoGerado = completion.Value.Content[0].Text;

            return textoGerado;
        }
    }
}
