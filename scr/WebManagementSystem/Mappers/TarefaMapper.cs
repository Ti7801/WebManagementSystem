using BibliotecaBusiness.Models;
using WebManagementSystem.ViewModels;

namespace WebManagementSystem.Mappers
{
    public class TarefaMapper
    {
        public static Tarefa Map(TarefaViewModel viewModel)
        {
            Tarefa tarefa = new Tarefa();

            tarefa.Id = viewModel.Id;
            tarefa.Messagem = viewModel.Messagem;
            tarefa.DataLimiteExecucao = viewModel.DataLimiteExecucao;
            tarefa.StatusTarefa = viewModel.StatusTarefa;
            tarefa.GestorId = viewModel.GestorId;
            tarefa.UsuarioId = viewModel.UsuarioId; 

            return tarefa;
        }

        public static TarefaViewModel Map(Tarefa tarefa)
        {
            TarefaViewModel viewModel = new TarefaViewModel();

            viewModel.Id = tarefa.Id;
            viewModel.Messagem = tarefa.Messagem;
            viewModel.DataLimiteExecucao = tarefa.DataLimiteExecucao;
            viewModel.StatusTarefa = tarefa.StatusTarefa;
            viewModel.GestorId = tarefa.GestorId;
            viewModel.UsuarioId = tarefa.UsuarioId;

            return viewModel;
        }

        public static List<TarefaViewModel> Map(List<Tarefa> tarefas)
        {
            List<TarefaViewModel> viewModels = new List<TarefaViewModel>();

            foreach (var tarefa in tarefas)
            {           
                TarefaViewModel viewModel =  Map(tarefa);

                viewModels.Add(viewModel);                                           
            }

            return viewModels;
        }

    }
}
