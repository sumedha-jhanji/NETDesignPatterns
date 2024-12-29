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

                //for Strategy pattern
                //objectsOfOurProjects.RegisterType<BaseCustomer, Customer>("Customer", new InjectionConstructor(new CustomerValidation())); // IOC and DI
                //objectsOfOurProjects.RegisterType<BaseCustomer, Lead>("Lead", new InjectionConstructor(new LeadValidation()));

                //for Decorator Pattern
                IValidation<ICustomer> custValidate = new PhoneValidation(new CustomerBasicValidation());
                objectsOfOurProjects.RegisterType<BaseCustomer, OrgCustomer>("Lead", new InjectionConstructor(custValidate, "Lead"));

                custValidate = new CustomerBasicValidation();
                objectsOfOurProjects.RegisterType<BaseCustomer, OrgCustomer>("SelfService", new InjectionConstructor(custValidate, "Self Service"));

                custValidate = new CustomerAddressValidation(new CustomerBasicValidation());
                objectsOfOurProjects.RegisterType<BaseCustomer, OrgCustomer>("HomeDelivery", new InjectionConstructor(custValidate, "Home Delivery"));


                custValidate = new CustomerAddressValidation(new CustomerBillValidation(new PhoneValidation(new CustomerBasicValidation())));
                objectsOfOurProjects.RegisterType<BaseCustomer, OrgCustomer>("Customer", new InjectionConstructor(custValidate, "Customer"));
            }


            //Design Pattern:- RIP Replace If with Polymorphism
            return objectsOfOurProjects.Resolve<AnyType>(Type);
        }



    }
}
