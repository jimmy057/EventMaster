﻿using System.ComponentModel.DataAnnotations;
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
	public double PrecioEntradaEvento { get; set; }
	public DateTime FechaInicio { get; set; } = DateTime.Now;
	public string? Nota { get; set; } = "";
	public List<EventosDetalle> ListaDetalle { get; set; } = new();
	[NotMapped]
	public string EstadoEvento
	{
		get
		{
			return FechaInicio > DateTime.Now ? "Pendiente" : "Realizado";
		}
	}
}