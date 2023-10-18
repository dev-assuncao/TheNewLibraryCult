namespace LibraryCult.Core.Data
{
    public interface IUnityOfWork
    {
        Task<bool> Commit();
    }
}
