using Microsoft.AspNetCore.Mvc;
using To_do_List.Context;
using To_do_List.Models;

namespace To_do_List.Controllers {
    public class TarefaController : Controller{
        public readonly ToDoListContext _context;

        public TarefaController(ToDoListContext context) {
            _context = context;
        }

        public IActionResult Index() { 
            var tarefas = _context.Tarefas.ToList();
            
            return View(tarefas);
        }

        public IActionResult Criar() {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa) {
            if(ModelState.IsValid) {
                _context.Tarefas.Add(tarefa);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(tarefa);
        }

        public IActionResult Editar(int id) { 
            var tarefa = _context.Tarefas.Find(id);
            return tarefa == null ? NotFound() : View(tarefa); 
        }

        [HttpPost]
        public IActionResult Editar(Tarefa tarefa) {
            var tarefaBanco = _context.Tarefas.Find(tarefa.Id);

            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Status = tarefa.Status;

            _context.Tarefas.Update(tarefaBanco);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Detalhes(int id) {
            var tarefa = _context.Tarefas.Find(id);
            return tarefa == null ? NotFound() : View(tarefa); 
        }

        public IActionResult Deletar(int id) {
            var tarefa = _context.Tarefas.Find(id);

            if(tarefa == null) return NotFound();

            _context.Tarefas.Remove(tarefa);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet("Tarefa/Pesquisar")]
        public IActionResult Pesquisar(string tipoPesquisa, string parametro) {
            IEnumerable<Tarefa> tarefas;
            switch(tipoPesquisa) {
                case "ObterPorTitulo":
                    tarefas = _context.Tarefas.Where(x => x.Titulo.Contains(parametro));
                    return View(tarefas);
                case "ObterPorData":
                    DateTime data = DateTime.Parse(parametro);
                    tarefas = _context.Tarefas.Where(x => x.Data == data);
                    return View(tarefas);
                case "ObterPorStatus":
                    if(parametro == "Pendente") {
                        tarefas = _context.Tarefas.Where(x => x.Status == TarefaStatus.Pendente);
                    } else {
                        tarefas = _context.Tarefas.Where(x => x.Status == TarefaStatus.Finalizada);
                    }
                    return View(tarefas);
            }
            return View();
        }
        /*
        [HttpGet("Tarefa/ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo) {
            var tarefas = _context.Tarefas.Where(x => x.Titulo.Contains(titulo));
            return Ok(tarefas);
        }
        */
    }
}
