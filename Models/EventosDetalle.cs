using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventMaster.Models;

public class EventosDetalle
{
	[Key]
	public int DetalleId { get; set; }
	public int EventoId { get; set; }
	public Eventos Evento { get; set; }
	public int ActividadId { get; set; }
	public Actividades Actividad { get; set; }
	public TimeOnly HoraInicio { get; set; }
	public TimeOnly HoraFin { get; set; }
}
