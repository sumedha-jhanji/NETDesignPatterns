//using MiddleLayer;

//namespace FactoryCustomer
//{
//    //responsibility is to create an object
//    // polymorphism - where a parent class can pointto child class at run time

//    //Design Pattern: - Simple Fatory Pattern - It helps to centralize the object creation and thus help to achieve a decoupled system
//    public static class Factory
//    {
//        private static Lazy<Dictionary<string, BaseCustomer>> custs = new Lazy<Dictionary<string, BaseCustomer>>(() => SetCustomerObject()); //Design Pattern:- Lazy Loading using Lazy Keyword
//                                                                                                                                             // private static Dictionary<string, BaseCustomer> custs = new Dictionary<string, BaseCustomer>(); //Design Pattern:- Lazy Loading without using Lazy Keyword

//        //static Factory()
//        //{
//        //    custs.Add("Customer", new Customer());
//        //    custs.Add("Lead", new Lead());
//        //}

//        public static BaseCustomer CreateCustomer(string customerType)
//        {
//            //if (customerType == "Customer")
//            //{
//            //   return new Customer();
//            //}
//            //else
//            //{
//            //    return new Lead();
//            //}

//            //Design Pattern:- Lazy Loading without using Lazy Keyword
//            //if (custs.Count == 0) {
//            //    custs.Add("Customer", new Customer());
//            //    custs.Add("Lead", new Lead());
//            //}

//            //Design Pattern:- RIP Replace If with Polymorphism
//            return custs.Value[customerType];
//        }

//        private static Dictionary<string, BaseCustomer> SetCustomerObject()
//        {
//            Dictionary<string, BaseCustomer> customers = new Dictionary<string, BaseCustomer>();
//            customers.Add("Customer", new Customer());
//            customers.Add("Lead", new Lead());
//            return customers;
//        }
//    }
//}
