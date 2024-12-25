using InterfaceCustomer;

namespace ValidationAlgorithms
{
    public class CustomerValidation : IValidation<ICustomer>
    {

        public void Validate(ICustomer obj)
        {
            if (obj.CustomerName.Length == 0)
            {
                throw new Exception("Customer Name is required");
            }
            if (obj.PhoneNumber.Length == 0)
            {
                throw new Exception("Phone number is required");
            }
            if (obj.BillAmount == 0)
            {
                throw new Exception("Bill Amount is required");
            }
            if (obj.BillDate > DateTime.Now)
            {
                throw new Exception("Bill date  is not proper");
            }
            if (obj.CustomerAddress.Length == 0)
            {
                throw new Exception("Address required");
            }
        }
    }
    public class LeadValidation : IValidation<ICustomer>
    {

        public void Validate(ICustomer obj)
        {
            if (obj.CustomerName.Length == 0)
            {
                throw new Exception("Customer Name is required");
            }
            if (obj.PhoneNumber.Length == 0)
            {
                throw new Exception("Phone number is required");
            }
        }
    }
}
