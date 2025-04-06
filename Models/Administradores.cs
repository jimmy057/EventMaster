using EventMaster.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventMaster.Models;

public class Administradores
{
	[Key]
	public int AdminId { get; set; }
	public string UsuarioId { get; set; }
	[ForeignKey("UsuarioId")]
	public ApplicationUser Usuario { get; set; }
	public int NombreUsuario { get; set; }
	public DateTime FechaCreacion { get; set; }
}
