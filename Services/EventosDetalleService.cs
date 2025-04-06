using EventMaster.Data;
using EventMaster.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MegatoursRD.Services;

public class EventosDetalleService(IDbContextFactory<ApplicationDbContext> DbFactory)
{
	public async Task<List<EventosDetalle>> Listar(Expression<Func<EventosDetalle, bool>> criterio)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.EventosDetalle
			.Include(x => x.Evento)
			.Include(x => x.Actividad)
			.AsNoTracking()
			.Where(criterio)
			.ToListAsync();
	}
}

