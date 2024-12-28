﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceDal
{
    //Design Pattern:- Generic Repository Pattern
    public interface IRepository<AnyType>
    {
        void SetUow(IUow uow);// used to send UOW inside this repository
        void Add(AnyType obj); // in memory  addition
        void Update(AnyType obj); // in-memory update
        List<AnyType> Search();

        void Save(); // physical commit
    }

    //Design Pattern:- Unit of Work Pattern
    public interface IUow
    {
        void Commit();
        void Rollback();
    }
}