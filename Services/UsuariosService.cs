using EventMaster.Data;
using Microsoft.EntityFrameworkCore;

namespace EventMaster.Services;

public class UsuariosService(IDbContextFactory<ApplicationDbContext> DbFactory)
{
	public async Task<bool> ValidarEmail(string email)
	{
		email.Trim().ToLower();
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Users
			.AnyAsync(x => x.Email.Trim().ToLower().Contains(email));
	}
}


