using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventMaster.Models;

public class Eventos
{
	[Key]
	public int EventoId { get; set; }
	public int ClienteId { get; set; }
	public Clientes? Cliente { get; set; }
	public string TituloEvento { get; set; }
	public string Direccion { get; set; }
	public int Cupos { get; set; }
	public double PrecioEntradaEvento { get; set; }
	public DateTime FechaInicio { get; set; }
	public string Nota { get; set; }
	public List<EventosDetalle> ListaDetalle { get; set; }
	[NotMapped]
	public string EstadoEvento
	{
		get
		{
			return FechaInicio > DateTime.Now ? "Pendiente" : "Realizado";
		}
	}
}


private sealed class InputModel
{
	[Required]
	[Display(Name = "NombreCliente")]
	public string NombreCliente { get; set; } = "";

	[Required]
	[Display(Name = "Direccion")]
	public string Direccion { get; set; } = "";

	[Required]
	[Display(Name = "Precio")]
	public string PrecioEntradaEvento { get; set; } = "";

	[Required]
	[Display(Name = "FechaInicio")]
	public DateTime FechaInicio { get; set; }

	[Required]
	[Display(Name = "HoraInicio")]
	public TimeOnly HoraInicio { get; set; }

	[Required]
	[Display(Name = "HoraFin")]
	public TimeOnly HoraFin { get; set; }
}