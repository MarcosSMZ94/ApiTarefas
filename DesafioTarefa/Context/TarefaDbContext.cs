using DesafioTarefa.Entities;
using Microsoft.EntityFrameworkCore;

namespace DesafioTarefa.Context;

public class TarefaDbContext(DbContextOptions<TarefaDbContext> options) : DbContext(options)
{
    public DbSet<Tarefa> Tarefas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tarefa>().Property(d => d.Descricao)
            .IsRequired()
            .HasMaxLength(255);
        modelBuilder.Entity<Tarefa>().Property(t => t.Titulo)
            .IsRequired()
            .HasMaxLength(255);
        modelBuilder.Entity<Tarefa>().Property(d => d.Data)
            .IsRequired();
        modelBuilder.Entity<Tarefa>().Property(s => s.Status)
            .IsRequired()
            .HasConversion<int>();
        
        base.OnModelCreating(modelBuilder);
    }
}