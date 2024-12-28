using ADODOTNetDAL;
using EFDal;
using InterfaceCustomer;
using InterfaceDal;
using Microsoft.Practices.Unity;

namespace FactoryDal
{
    public static class FactoryUsingUnity<AnyType>
    {
        //Design Pattern: - Unity Pattern to automate Simple Fatory pattern
        private static IUnityContainer objectsOfOurProjects = null; // Unity container is a container where we will create objects like earlier we are doing using Dictionary
        public static AnyType CreateObject(string Type)
        {
            // Design pattern :- Lazy loading. Eager loading
            if (objectsOfOurProjects == null)
            {
                objectsOfOurProjects = new UnityContainer();

                objectsOfOurProjects.RegisterType<IRepository<BaseCustomer>,
                    CustomerDAL>("ADODal");
                objectsOfOurProjects.RegisterType<IRepository<BaseCustomer>,
                     EfCustomerDal>("EFDal");
                objectsOfOurProjects.RegisterType<IUow, ADOUow>("AdoUow");
                objectsOfOurProjects.RegisterType<IUow, EUow>("EfUOW");

            }
            //Design pattern :-  RIP Replace If with Poly
            return objectsOfOurProjects.Resolve<AnyType>(Type);
        }

        //public static AnyType CreateObject(string Type) // make it generic
        //{
        //    if (objectsOfOurProjects == null)
        //    {
        //        objectsOfOurProjects = new UnityContainer();
        //        objectsOfOurProjects.RegisterType<IRepository<BaseCustomer>, 
        //            CustomerDAL>("ADODal"); // registering DAL
        //        objectsOfOurProjects.RegisterType<IRepository<BaseCustomer>,
        //            EfCustomerDal>("EFDal");
        //        objectsOfOurProjects.RegisterType<IUow, ADOUow>("AdoUoW");
        //    }


        //    //Design Pattern:- RIP Replace If with Polymorphism
        //    return objectsOfOurProjects.Resolve<AnyType>(Type,
        //                      new ResolverOverride[]
        //                      {
        //                               new ParameterOverride("_ConnectionString",
        //                                @"Data Source=LAPTOP-VA38D82T;Initial Catalog=DesignPatternsDB;Integrated Security=True;Trust Server Certificate=True")
        //                      });
        //}



    }
}
