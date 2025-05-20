using BibliotecaBusiness.Models;
using WebManagementSystem.ViewModels;

namespace WebManagementSystem.Mappers
{
    public class TarefaMapper
    {
        public static Tarefa Map(CadastrarTarefaViewModel viewModel)
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

        public static CadastrarTarefaViewModel Map(Tarefa tarefa)
        {
            CadastrarTarefaViewModel viewModel = new CadastrarTarefaViewModel();

            viewModel.Id = tarefa.Id;
            viewModel.Messagem = tarefa.Messagem;
            viewModel.DataLimiteExecucao = tarefa.DataLimiteExecucao;
            viewModel.StatusTarefa = tarefa.StatusTarefa;
            viewModel.GestorId = tarefa.GestorId;
            viewModel.UsuarioId = tarefa.UsuarioId;

            return viewModel;
        }

        public static List<CadastrarTarefaViewModel> Map(List<Tarefa> tarefas)
        {
            List<CadastrarTarefaViewModel> viewModels = new List<CadastrarTarefaViewModel>();

            foreach (var tarefa in tarefas)
            {           
                CadastrarTarefaViewModel viewModel =  Map(tarefa);

                viewModels.Add(viewModel);                                           
            }

            return viewModels;
        }
    }
}
