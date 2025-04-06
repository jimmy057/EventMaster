namespace EventMaster.Models;

public class Filtro
{
	public string? CriterioBusqueda { get; set; }
	public string? valorEnFiltro { get; set; }
	public string? MensajeError { get; set; }
	public DateTime? Desde { get; set; }
	public DateTime? Hasta { get; set; }
}
