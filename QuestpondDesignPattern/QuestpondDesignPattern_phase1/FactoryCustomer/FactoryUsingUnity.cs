using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterfaceCustomer;
using Microsoft.Practices.Unity;
using MiddleLayer;
using ValidationAlgorithms;

namespace FactoryCustomer
{

    public static class FactoryUsingUnity
    {
        //Design Pattern: - Unity Pattern to automate Simple Fatory pattern
        private static IUnityContainer customers = null; // Unity container is a container where we will create objects like earlier we are doing using Dictionary

        public static ICustomer CreateCustomer(string customerType)
        {
            if (customers == null) {
                customers = new UnityContainer();
                customers.RegisterType<ICustomer, Customer>("Customer", new InjectionConstructor(new CustomerValidation())); // IOC and DI
                customers.RegisterType<ICustomer, Lead>("Lead", new InjectionConstructor(new LeadValidation()));

            }


            //Design Pattern:- RIP Replace If with Polymorphism
            return customers.Resolve<ICustomer>(customerType);
        }

    }
}
