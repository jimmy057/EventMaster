using EventMaster.Data;
using EventMaster.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MegatoursRD.Services;

public class AdministradoresService(IDbContextFactory<ApplicationDbContext> DbFactory)
{
	public async Task<bool> Existe(int id)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Administradores.AnyAsync(c => c.AdminId == id);
	}
	private async Task<bool> Insertar(Administradores admin)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		contexto.Administradores.Add(admin);
		return await contexto.SaveChangesAsync() > 0;
	}
	private async Task<bool> Modificar(Administradores admin)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		contexto.Administradores.Update(admin);
		return await contexto.SaveChangesAsync() > 0;
	}

	public async Task<bool> Eliminar(int id)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Administradores
			.Where(c => c.AdminId == id)
			.ExecuteDeleteAsync() > 0;
	}

	public async Task<bool> Guardar(Administradores admin)
	{
		if (!await Existe(admin.AdminId))
			return await Insertar(admin);
		else
			return await Modificar(admin);
	}

	public async Task<List<Administradores>> Listar(Expression<Func<Administradores, bool>> criterio)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Administradores
			.Include(x => x.Usuario)
			.AsNoTracking()
			.Where(criterio)
			.ToListAsync();
	}

	public async Task<Administradores?> BuscarPorUsuarioId(string id)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Administradores
			.Include(x => x.Usuario)
			.AsNoTracking()
			.FirstOrDefaultAsync(x => x.UsuarioId == id);
	}

	public async Task<Administradores?> Buscar(int id)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Administradores
			.AsNoTracking()
			.FirstOrDefaultAsync(c => c.AdminId == id);
	}
}

