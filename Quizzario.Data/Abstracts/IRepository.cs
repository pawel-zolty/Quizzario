using System.Collections.Generic;

namespace Quizzario.Data.Abstracts
{
    public interface IRepository<T>
    {
        IEnumerable<T> All { get; }
        T GetById(string id);
    }
}
