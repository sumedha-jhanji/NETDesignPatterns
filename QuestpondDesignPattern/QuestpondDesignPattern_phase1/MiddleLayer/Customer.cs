namespace MiddleLayer
{
    public class Customer : BaseCustomer
    {
        public override void Validate()
        {
            if(CustomerName.Length == 0)
            {
                throw new Exception("Customer Name is required");
            }
            if (CustomerAddress.Length == 0)
            {
                throw new Exception("Customer Address is required");
            }
            if (PhoneNumber.Length == 0)
            {
                throw new Exception("Phone Number is required");
            }
            if (BillDate > DateTime.Now)
            {
                throw new Exception("Bill Date is not properd");
            }
            if (BillAmount == 0)
            {
                throw new Exception("Bill Amount is required");
            }
        }
    }
}
