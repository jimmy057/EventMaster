using EventMaster.Data;
using EventMaster.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EventMaster.Services;

public class EventosService(IDbContextFactory<ApplicationDbContext> DbFactory)
{
	public async Task<bool> Existe(int id)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Eventos.AnyAsync(c => c.EventoId == id);
	}
	private async Task<bool> Insertar(Eventos evento)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		contexto.Eventos.Add(evento);
		return await contexto.SaveChangesAsync() > 0;
	}
	private async Task<bool> Modificar(Eventos evento)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		contexto.Eventos.Update(evento);
		return await contexto.SaveChangesAsync() > 0;
	}

	public async Task<bool> Eliminar(int id)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Eventos
			.Where(c => c.EventoId == id)
			.ExecuteDeleteAsync() > 0;
	}

	public async Task<bool> Guardar(Eventos evento)
	{
		if (!await Existe(evento.EventoId))
			return await Insertar(evento);
		else
			return await Modificar(evento);
	}

	public async Task<List<Eventos>> Listar(Expression<Func<Eventos, bool>> criterio)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Eventos
			.Include(x => x.Cliente)
			.Include(x => x.ListaDetalle)
			.AsNoTracking()
			.Where(criterio)
			.ToListAsync();
	}

	public async Task<Eventos?> Buscar(int id)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		var evento = await contexto.Eventos
			.Include(x => x.ListaDetalle)
			.AsNoTracking()
			.FirstOrDefaultAsync(c => c.EventoId == id);

		if (evento != null && evento.ListaDetalle == null)
		{
			evento.ListaDetalle = new List<EventosDetalle>();
		}

		return evento;
	}
}

