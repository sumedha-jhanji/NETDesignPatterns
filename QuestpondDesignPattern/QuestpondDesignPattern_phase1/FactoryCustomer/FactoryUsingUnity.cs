using InterfaceCustomer;
using InterfaceDal;
using Microsoft.Practices.Unity;
using MiddleLayer;
using ValidationAlgorithms;

namespace FactoryCustomer
{

    public static class FactoryUsingUnity<AnyType>
    {
        //Design Pattern: - Unity Pattern to automate Simple Fatory pattern
        private static IUnityContainer objectsOfOurProjects = null; // Unity container is a container where we will create objects like earlier we are doing using Dictionary

        public static AnyType CreateObject(string Type) // make it generic
        {
            if (objectsOfOurProjects == null) {
                objectsOfOurProjects = new UnityContainer();
                objectsOfOurProjects.RegisterType<ICustomer, Customer>("Customer", new InjectionConstructor(new CustomerValidation())); // IOC and DI
                objectsOfOurProjects.RegisterType<ICustomer, Lead>("Lead", new InjectionConstructor(new LeadValidation()));

            }


            //Design Pattern:- RIP Replace If with Polymorphism
            return objectsOfOurProjects.Resolve<AnyType>(Type);
        }



    }
}
