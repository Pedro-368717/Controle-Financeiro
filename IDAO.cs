namespace ControleFinanceiro.DAO
{
    public interface IDAO<T>
    {
        void inserir(T entidade);
        void Alterar(T entidade);
        void Excluir(T entidade);
        T BuscarPorId(int id);
        List<T> BuscarTodos();
    }
}