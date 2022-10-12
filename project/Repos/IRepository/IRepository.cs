using System;
namespace SD.Models.Repositories.Main
{
    public interface IRepository<T, TId>
    {
        TId Create(T data);

        T GetbyId(TId id);

        bool Update(T data, T newData);

        bool Delete(TId id);
    }
}

