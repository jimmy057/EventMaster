using EventMaster.Data;
using EventMaster.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MegatoursRD.Services;

public class ClientesServices(IDbContextFactory<ApplicationDbContext> DbFactory)
{
	public async Task<bool> Existe(int id)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Clientes.AnyAsync(c => c.ClienteId == id);
	}
	private async Task<bool> Insertar(Clientes cliente)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		contexto.Clientes.Add(cliente);
		return await contexto.SaveChangesAsync() > 0;
	}
	private async Task<bool> Modificar(Clientes cliente)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		contexto.Clientes.Update(cliente);
		return await contexto.SaveChangesAsync() > 0;
	}

	public async Task<bool> Eliminar(int id)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Clientes
			.Where(c => c.ClienteId == id)
			.ExecuteDeleteAsync() > 0;
	}

	public async Task<bool> Guardar(Clientes cliente)
	{
		if (!await Existe(cliente.ClienteId))
			return await Insertar(cliente);
		else
			return await Modificar(cliente);
	}

	public async Task<List<Clientes>> Listar(Expression<Func<Clientes, bool>> criterio)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Clientes
			.Include(x => x.Usuario)
			.AsNoTracking()
			.Where(criterio)
			.ToListAsync();
	}

	public async Task<Clientes?> BuscarPorUsuarioId(string id)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Clientes
			.Include(x => x.Usuario)
			.AsNoTracking()
			.FirstOrDefaultAsync(x => x.UsuarioId == id);
	}

	public async Task<Clientes?> Buscar(int id)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Clientes
			.AsNoTracking()
			.FirstOrDefaultAsync(c => c.ClienteId == id);
	}
}

