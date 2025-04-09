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
	public int Participantes { get; set; }
	public double Precio { get; set; }
}
