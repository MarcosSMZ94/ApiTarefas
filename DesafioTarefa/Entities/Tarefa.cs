
namespace DesafioTarefa.Entities;

public class Tarefa
{
    public int Id { get; private init; }
    public required string Titulo { get; set; }
    public required string Descricao { get; set; }
    public DateTime Data { get; set; }
    public Status Status { get; set; }
}