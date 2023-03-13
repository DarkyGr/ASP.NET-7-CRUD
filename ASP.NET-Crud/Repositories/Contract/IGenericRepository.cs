namespace ASP.NET_Crud.Repositories.Contract
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> List();

        Task<bool> Save(T model);

        Task<bool> Edit(T model);

        Task<bool> Delete(T model);

    }
}
