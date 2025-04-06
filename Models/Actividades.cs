using System.ComponentModel.DataAnnotations;

namespace EventMaster.Models;

public class Actividades
{
	[Key]
	public int ActividadId { get; set; }
	public string TituloActividad { get; set; }
	public string TipoActividad { get; set; }
	public string Descripcion { get; set; }
}
