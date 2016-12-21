using NHibernate;
using System;

namespace AbiokaApi.Repository
{
    public interface IDisposableUnitOfWork : IDisposable
    {
        /// <summary>
        /// Opens the session.
        /// </summary>
        /// <returns></returns>
        IDisposableUnitOfWork OpenSession();

        /// <summary>
        /// Gets the session.
        /// </summary>
        /// <value>
        /// The session.
        /// </value>
        ISession Session { get; }

        /// <summary>
        /// Commits this instance.
        /// </summary>
        void Commit();
    }
}
