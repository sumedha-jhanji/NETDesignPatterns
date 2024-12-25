using InterfaceCustomer;

namespace MiddleLayer
{
    public class Customer : BaseCustomer
    {
        public Customer(IValidation<ICustomer> obj): base(obj) { }
    }
}
