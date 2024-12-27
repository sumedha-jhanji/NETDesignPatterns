using InterfaceCustomer;

namespace MiddleLayer
{
    public class Customer : BaseCustomer
    {
        public Customer()
        {
            CustomerType = "Customer";
        }
        public Customer(IValidation<ICustomer> obj): base(obj) { CustomerType = "Customer"; }
    }
}
