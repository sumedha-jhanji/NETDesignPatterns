using System.ComponentModel.DataAnnotations;

namespace InterfaceCustomer
{
    public class BaseCustomer : ICustomer
    {
        private IValidation<ICustomer> _validation = null;

        // Design pattern :- memento pattern ( Revert old state)
        private ICustomer _OldCopy = null;
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

        public void Clone()
        {
            if (_OldCopy == null)
            {
                // Design pattern :- Prototype pattern (Clone)
                //_OldCopy = this; // clone urself first. This is happening "BYREF". Change in current object will also imapct old copy also
                _OldCopy = (ICustomer)this.MemberwiseClone(); // this will do BYVAL. copies all the values and create a new copy
            }
            
        }

        // Design pattern :- memento pattern ( Revert old state)
        public void Revert()
        {
            this.CustomerName = _OldCopy.CustomerName;
            this.CustomerAddress = _OldCopy.CustomerAddress;
            this.BillDate = _OldCopy.BillDate;
            this.BillAmount = _OldCopy.BillAmount;
            this.CustomerType = _OldCopy.CustomerType;
            this.PhoneNumber = _OldCopy.PhoneNumber;
        }
    }
}
