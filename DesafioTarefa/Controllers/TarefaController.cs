using System.Runtime.CompilerServices;
using DesafioTarefa.Context;
using DesafioTarefa.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
namespace DesafioTarefa.Controllers;

[ApiController]
[Route("[controller]")]
public class TarefaController(TarefaDbContext context) : ControllerBase
{
    [HttpGet("{tarefaId}")]
    public async Task<IActionResult> ObterTarefaPorId(int tarefaId)
    {
        var tarefa = await context.Tarefas.FirstOrDefaultAsync(t => t.Id == tarefaId);
        if (tarefa == null)
            return NotFound();

        return Ok(tarefa);
    }

    [HttpGet("ObterTodos")]
    public async Task<IActionResult> ObterTarefas()
    {
        var tarefas = await context.Tarefas.ToListAsync();

        return Ok(tarefas);
    }

    [HttpGet("ObterPorTitulo")]
    public async Task<IActionResult> ObterTarefaPorTitulo(string tarefaTitulo)
    {
        var tarefa = await context.Tarefas.FirstOrDefaultAsync(t => t.Titulo == tarefaTitulo);
        if(tarefa == null)
            return NotFound();
        
        return Ok(tarefa);
    }

    [HttpGet("ObterPorData")]
    public async Task<IActionResult> ObterTarefaPorData(DateTime tarefaData)
    {
        var tarefa = await context.Tarefas.FindAsync(tarefaData);
        if (tarefa == null)
            return NotFound();
        
        return Ok(tarefa);
    }

    [HttpGet("ObterPorStatus")]
    public async Task<IActionResult> ObterTarefaPorStatus(Status status)
    {
        var tarefa = await context.Tarefas.FindAsync(status);
        if (tarefa == null)
            return NotFound();
        
        return Ok(tarefa);
    }

    [HttpPost]
    public async Task<IActionResult> InserirTarefa(Tarefa tarefa)
    {
        tarefa.Data = DateTime.Now;
        await context.Tarefas.AddAsync(tarefa);
        await context.SaveChangesAsync();
        return Ok(tarefa);
    }

    [HttpPut("{tarefaId}")]
    public async Task<IActionResult> AtualizarTarefa(int tarefaId, [FromBody] Tarefa tarefa)
    {
        var tarefaDb = await context.Tarefas.FirstOrDefaultAsync(t => t.Id == tarefaId);
        if (tarefaDb == null)
            return NotFound();
        
        tarefaDb.Data = tarefa.Data;
        tarefaDb.Titulo = tarefa.Titulo;
        tarefaDb.Status = tarefa.Status;
        tarefaDb.Descricao = tarefa.Descricao;
        await context.SaveChangesAsync();
        return Ok(tarefaDb);
    }

    [HttpDelete("{tarefaId}")]
    public async Task<ActionResult> DeletarTarefa(int tarefaId)
    {
        var tarefaDb = await context.Tarefas.FirstOrDefaultAsync(t => t.Id == tarefaId);
        if (tarefaDb == null)
            return NotFound();
        
        context.Remove(tarefaDb);
        await context.SaveChangesAsync();
        return Ok();
    }
}