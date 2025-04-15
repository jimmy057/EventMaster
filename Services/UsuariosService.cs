using EventMaster.Data;
using EventMaster.Models;
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

	public async Task<Clientes?> BuscarClientePorUserId(string userId)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();

		try
		{
			return await contexto.Clientes
					.FirstOrDefaultAsync(x => x.UsuarioId == userId);
		}
		catch (Exception e)
		{
			Console.WriteLine($"No hay ningun cliente registrad con ese ID de usuario: { e.Message}");
			return null;
		}
	}
}


