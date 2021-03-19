using System;
using System.Collections.Generic;
using System.Text;

namespace HeroVSMonster.Core.Interface
{
    public interface IRepository<T>
    {
        //CRUD 
        void Create(T obj);
        T GetByID(int ID);
        List<T> GetAll();
        bool Update(T obj);

        bool Delete(T obj);
    }
}
