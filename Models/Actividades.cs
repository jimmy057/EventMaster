using System.ComponentModel.DataAnnotations;

namespace EventMaster.Models;

public class Actividades
{
	[Key]
	public int ActividadId { get; set; }
	public string? TituloActividad { get; set; }
	public string? TipoActividad { get; set; }
	public string? Descripcion { get; set; }
	public TimeOnly HoraInicio { get; set; }
	public TimeOnly HoraFin { get; set; }
	public int? IdCliente { get; set; }
	public int? Cupos { get; set; } = 100;
}
