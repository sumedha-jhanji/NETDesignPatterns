using InterfaceCustomer;

namespace ValidationAlgorithms
{
    //Design Pattern:- Decorator Pattern

    // created base validation
    public class CustomerBasicValidation : IValidation<ICustomer> 
    {
        public void Validate(ICustomer obj)
        {
            if (obj.CustomerName.Length == 0)
            {
                throw new Exception("Customer Name is required");
            }
        }
    }

    //will connect individual validations to create a decorator
    public class ValidationLinker : IValidation<ICustomer>
    {
        private IValidation<ICustomer> _nextValidator = null; // link list needs to know what is the next validator to call
        public ValidationLinker(IValidation<ICustomer> validator) // will be injectede from outside (DI and IOC)
        {
            _nextValidator  = validator;
        }
        public virtual void Validate(ICustomer obj)
        {
            _nextValidator.Validate(obj);
        }
    }

    public class PhoneValidation : ValidationLinker
    {
        public PhoneValidation(IValidation<ICustomer> validator):base(validator)
        {
            
        }
        public override void Validate(ICustomer obj)
        {
            base.Validate(obj); // this will call the top of the cake
            if (obj.PhoneNumber.Length == 0)
            {
                throw new Exception("Phone number is required");
            }
        }
    }

    public class CustomerAddressValidation : ValidationLinker
    {
        public CustomerAddressValidation(IValidation<ICustomer> validator) : base(validator)
        {

        }
        public override void Validate(ICustomer obj)
        {
            base.Validate(obj); // this will call the top of the cake
            if (obj.CustomerAddress.Length == 0)
            {
                throw new Exception("Address required");
            }
        }
    }

    public class CustomerBillValidation : ValidationLinker
    {
        public CustomerBillValidation(IValidation<ICustomer> validator) : base(validator)
        {

        }
        public override void Validate(ICustomer obj)
        {
            base.Validate(obj); // this will call the top of the cake
            if (obj.BillAmount == 0)
            {
                throw new Exception("Bill Amount is required");
            }
            if (obj.BillDate >=  DateTime.Now)
            {
                throw new Exception("Bill date  is not proper");
            }
        }
    }


    //Design Pattern:- Stragtegy Pattern
    //public class CustomerValidation : IValidation<ICustomer>
    //{

    //    public void Validate(ICustomer obj)
    //    {
    //        if (obj.CustomerName.Length == 0)
    //        {
    //            throw new Exception("Customer Name is required");
    //        }
    //        if (obj.PhoneNumber.Length == 0)
    //        {
    //            throw new Exception("Phone number is required");
    //        }
    //        if (obj.BillAmount == 0)
    //        {
    //            throw new Exception("Bill Amount is required");
    //        }
    //        if (obj.BillDate > DateTime.Now)
    //        {
    //            throw new Exception("Bill date  is not proper");
    //        }
    //        if (obj.CustomerAddress.Length == 0)
    //        {
    //            throw new Exception("Address required");
    //        }
    //    }
    //}

    //Design Pattern:- Stragtegy Pattern
    //public class LeadValidation : IValidation<ICustomer>
    //{

    //    public void Validate(ICustomer obj)
    //    {
    //        if (obj.CustomerName.Length == 0)
    //        {
    //            throw new Exception("Customer Name is required");
    //        }
    //        if (obj.PhoneNumber.Length == 0)
    //        {
    //            throw new Exception("Phone number is required");
    //        }
    //    }
    //}
}
