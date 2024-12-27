using InterfaceCustomer;

namespace MiddleLayer
{
    public class Lead : BaseCustomer
    {
        public Lead()
        {
            CustomerType = "Lead";
        }
        public Lead(IValidation<ICustomer> obj) : base(obj) {
            CustomerType = "Lead";
        }
    }
}
