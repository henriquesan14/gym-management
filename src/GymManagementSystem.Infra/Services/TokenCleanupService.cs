using GymManagementSystem.Application.Auth;
using GymManagementSystem.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymManagementSystem.Infra.Services;

public class TokenCleanupService(GymManagementDbContext dbContext, ILogger<TokenCleanupService> logger) : ITokenCleanupService
{
    public async Task CleanupExpiredAndRevokedTokensAsync()
    {
        logger.LogInformation("⏰ Iniciando job de limpeza de tokens revogados e expirados em {Date}", DateTime.Now);
        var deletedCount = await dbContext.RefreshTokens
            .Where(r => r.IsRevoked || r.IsExpired).ExecuteDeleteAsync();

        logger.LogInformation("✅ Job de limpeza de tokens revogados e expirados concluído com sucesso ({Count} tokens apagados)", deletedCount);
    }
}
