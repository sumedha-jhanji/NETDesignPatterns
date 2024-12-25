using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using MiddleLayer;

namespace FactoryCustomer
{

    public static class FactoryUsingUnity
    {
        private static IUnityContainer custs = null; // Unity container is a container where we will create objects like earlier we are doing using Dictionary

        public static BaseCustomer CreateCustomer(string customerType)
        {
            if (custs == null) {
                custs = new UnityContainer();
                custs.RegisterType<BaseCustomer, Customer>("Customer");
                custs.RegisterType<BaseCustomer, Lead>("Lead");

            }


            //Design Pattern:- RIP Replace If with Polymorphism
            return custs.Resolve<BaseCustomer>(customerType);
        }

    }
}
