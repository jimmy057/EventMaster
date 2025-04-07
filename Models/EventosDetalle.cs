using System.ComponentModel.DataAnnotations;

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
	public double Precio { get; set; }
}
