using EventMaster.Data;
using EventMaster.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EventMaster.Services;

public class ActividadesService(IDbContextFactory<ApplicationDbContext> DbFactory)
{
	public async Task<bool> Existe(int id)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Actividades.AnyAsync(c => c.ActividadId == id);
	}
	private async Task<bool> Insertar(Actividades actividad)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		contexto.Actividades.Add(actividad);
		return await contexto.SaveChangesAsync() > 0;
	}
	private async Task<bool> Modificar(Actividades actividad)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		contexto.Actividades.Update(actividad);
		return await contexto.SaveChangesAsync() > 0;
	}

	public async Task<bool> Eliminar(int id)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Actividades
			.Where(c => c.ActividadId == id)
			.ExecuteDeleteAsync() > 0;
	}

	public async Task<bool> Guardar(Actividades actividad)
	{
		if (!await Existe(actividad.ActividadId))
			return await Insertar(actividad);
		else
			return await Modificar(actividad);
	}

	public async Task<List<Actividades>> Listar(Expression<Func<Actividades, bool>> criterio)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Actividades
			.AsNoTracking()
			.Where(criterio)
			.ToListAsync();
	}

	public async Task<bool> ExisteTipo(string tipo)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Actividades.AnyAsync(c => c.TipoActividad.Trim().ToLower() == tipo.Trim().ToLower());
	}
	public async Task<bool> ExisteTitulo(string titulo)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Actividades.AnyAsync(c => c.TituloActividad.Trim().ToLower() == titulo.Trim().ToLower());
	}

	public async Task<Actividades?> Buscar(int id)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();
		return await contexto.Actividades
			.AsNoTracking()
			.FirstOrDefaultAsync(c => c.ActividadId == id);
	}

	public async Task AfectarCupos(int[] idsActividadesEditadas, int[] idsActividadesEnDetalleActual, Eventos evento, bool esEdicion)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();

		if (esEdicion)
		{
			var detalleAntesDeEditar = await contexto.EventosDetalle
				.Where(d => idsActividadesEditadas.Contains(d.ActividadId))
				.ToListAsync();

			var detalleActual = await contexto.EventosDetalle
				.Where(d => idsActividadesEnDetalleActual.Contains(d.ActividadId))
				.ToListAsync();

			var actividadesAnteriores = await contexto.Actividades
				.Where(a => detalleAntesDeEditar.Select(d => d.ActividadId).Contains(a.ActividadId))
				.ToListAsync();

			foreach (var detalle in detalleAntesDeEditar)
			{
				var actividad = contexto.Actividades.FirstOrDefault(a => a.ActividadId == detalle.ActividadId);
				if (actividad != null)
				{
					actividad.Cupos += detalle.Participantes; // Recuperar cupos
				}
			}
			contexto.EventosDetalle.RemoveRange(detalleAntesDeEditar);
		}
		else
		{
			var listaActividades = await contexto.Actividades
				.Where(x => idsActividadesEditadas.Contains(x.ActividadId))
				.ToListAsync();

			foreach (var detalle in evento.ListaDetalle)
			{
				var actividad = listaActividades.FirstOrDefault(x => x.ActividadId == detalle.ActividadId);
				if (actividad != null && detalle.Participantes > 0)
				{
					actividad.Cupos -= detalle.Participantes;
				}
			}

		}
			await contexto.SaveChangesAsync();
	}
}


