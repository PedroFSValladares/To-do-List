using Microsoft.EntityFrameworkCore;
using To_do_List.Models;

namespace To_do_List.Context {
    public class ToDoListContext : DbContext{
        public ToDoListContext(DbContextOptions<ToDoListContext> options) : base(options) { }

        public DbSet<Tarefa> Tarefas { get; set; }
    }
}
