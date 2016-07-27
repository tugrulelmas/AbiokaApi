using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
