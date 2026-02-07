using System;
using System.Collections.Generic;
using System.Text;

namespace CoreBanking.Domain.Interfaces
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}