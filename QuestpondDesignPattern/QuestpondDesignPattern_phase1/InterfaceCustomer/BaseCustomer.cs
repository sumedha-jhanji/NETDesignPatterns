using System.ComponentModel.DataAnnotations;

namespace InterfaceCustomer
{
    public class BaseCustomer : ICustomer
    {
        private IValidation<ICustomer> _validation = null;
        public BaseCustomer(IValidation<ICustomer> obj) // injecting validation object
        {
            _validation = obj;
        }

        [Key]
        public int Id { get; set; }
        public string CustomerType { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string PhoneNumber { get; set; }
        public decimal BillAmount { get; set; }
        public DateTime BillDate { get; set; }      

        public BaseCustomer()
        {
            CustomerName = "";
            CustomerAddress = "";
            PhoneNumber = "";
            BillAmount = 0;
            BillDate = DateTime.Now;
        }

        public virtual void Validate()
        {
            _validation.Validate(this);
        }
    }
}
