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

	public async Task AfectarCupos(List<Actividades> actividadesActualesEnDetalle,
								   Eventos evento,
								   bool esEdicion,
								   List<Actividades>? actividadesAntesDeEditar = null,
								   List<Actividades>? actividadesEliminadas = null,
								   List<Actividades>? actividadesNuevas = null)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();

		if (esEdicion)
		{
			var actividadIds = actividadesActualesEnDetalle.Select(a => a.ActividadId).ToList();
			var listaDetalleActual = await contexto.EventosDetalle
				.Where(a => actividadIds.Contains(a.ActividadId))
				.ToListAsync();

			var ids = actividadesAntesDeEditar.Select(a => a.ActividadId).ToList();
			var listaDetalleAntesDeEditar = await contexto.EventosDetalle
				.Where(a => ids.Contains(a.ActividadId))
				.ToListAsync();

			if (evento.ListaDetalle != null && actividadesNuevas != null && actividadesNuevas.Count > 0)
			{
				foreach (var actividad in actividadesNuevas)
				{
					var detalle = evento.ListaDetalle.FirstOrDefault(d => d.ActividadId == actividad.ActividadId);
					if (detalle != null && actividad.ActividadId == detalle.ActividadId)
					{
						detalle.Evento = null;
						contexto.Attach(actividad);
						actividad.Cupos -= detalle.Participantes;

						// revisar si ya existe en la BD
						var detalleExistente = await contexto.EventosDetalle
							.FirstOrDefaultAsync(d => d.EventoId == evento.EventoId && d.ActividadId == detalle.ActividadId);

						if (detalleExistente != null)
						{
							// Ya existe: actualiza campos
							detalleExistente.Participantes += detalle.Participantes;
							detalleExistente.Precio += detalle.Precio;
							// etc., si hay más campos
						}
						else
						{
							// No existe, lo insertamos
							contexto.EventosDetalle.Add(detalle);
						}
					}

				}
			}

			var listaDetalleEliminado = new List<EventosDetalle>();
			if (actividadesEliminadas != null && actividadesEliminadas.Count > 0)
			{
				listaDetalleEliminado = await contexto.EventosDetalle
				   .Where(d => actividadesEliminadas.Select(a => a.ActividadId).Contains(d.ActividadId))
				   .ToListAsync();

				foreach (var detalle in listaDetalleAntesDeEditar)
				{
					var detalleAux = listaDetalleEliminado.FirstOrDefault(d => d.ActividadId == detalle.ActividadId);
					var actividadAux = actividadesEliminadas.FirstOrDefault(a => a.ActividadId == detalle.ActividadId);
					if (actividadAux == null || detalleAux == null || actividadAux.ActividadId != detalleAux.ActividadId)
						continue;
					contexto.Attach(actividadAux);
					actividadAux.Cupos += detalleAux.Participantes;
					contexto.EventosDetalle.Remove(detalleAux);
				}
			}

		}
		else
		{
			foreach (var detalle in evento.ListaDetalle)
			{
				var actividad = actividadesActualesEnDetalle.FirstOrDefault(x => x.ActividadId == detalle.ActividadId);
				if (actividad != null && detalle.Participantes > 0)
				{
					contexto.Attach(actividad);
					actividad.Cupos -= detalle.Participantes;
				}
			}
		}
		await contexto.SaveChangesAsync();
	}
	public async Task RecuperarCuposAlEliminar(Eventos evento, List<Actividades> actividadesActualesEnDetalle)
	{
		await using var contexto = await DbFactory.CreateDbContextAsync();

		foreach (var detalle in evento.ListaDetalle)
		{
			var actividad = actividadesActualesEnDetalle.FirstOrDefault(x => x.ActividadId == detalle.ActividadId);
			if (actividad != null && detalle.Participantes > 0)
			{
				contexto.Attach(actividad);
				actividad.Cupos += detalle.Participantes;
			}
		}
		await contexto.SaveChangesAsync();
	}
}


