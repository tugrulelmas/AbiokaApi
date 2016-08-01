namespace AbiokaApi.Repository
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Begins the transaction.
        /// </summary>
        void BeginTransaction();

        /// <summary>
        /// Commits this instance.
        /// </summary>
        void Commit();
    }
}
