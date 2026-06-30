using GymManagementSystem.Application.Auth;
using GymManagementSystem.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GymManagementSystem.Infra.Services;

public sealed class TokenCleanupService(GymManagementDbContext dbContext, ILogger<TokenCleanupService> logger) : ITokenCleanupService
{
    public async Task CleanupExpiredAndRevokedTokensAsync()
    {
        logger.LogInformation("⏰ Iniciando job de limpeza de tokens revogados e expirados em {Date}", DateTime.Now);
        var deletedCount = await dbContext.RefreshTokens
            .Where(r => r.RevokedAt !=  null || r.ExpiresAt <= DateTime.Now).ExecuteDeleteAsync();

        logger.LogInformation("✅ Job de limpeza de tokens revogados e expirados concluído com sucesso ({Count} tokens apagados)", deletedCount);
    }
}
