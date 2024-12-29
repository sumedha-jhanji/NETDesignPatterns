## Design Pattern
- like singleton where we know how to and where to write things.
- We know pseudocode
- patterns at code level
- examples: Factory, Singleton, Iterator

## Architectural pattern
- u know the block level diagram like MVC, MVP, MVVM

## Architectural style
- principles
- divide project into smaller ones
- example REST, SOA, Microservice, IOC

# Design Patterns
- time tested solutions to architecture problems
- time tested practices for OOP problems

## Categories
- **Design Pattern is OOPS**
- Structural Design pattern: Template/class creation
- Creational Design patterm: Instantiation (like using new keyword)
- Behavioral Design Pattern: Runtime problems (actually dealing with entities for operations whe they are running like type casting etc)

## How to design a project
- identify nouns. They will be entities
- identify pronouns. They will become properties of entities
- identify the actions of entity. They will become methods/ations of entity
- identify the relation amoung entities
  - "is a" relationship (there will be parent chid relationship, so entites might get inherited from common class)
  - "uses/has a" relations
 
## Polymorphism
- object ACTS differetly under differemt conditions
- to achieve decoupling

## Simple Factory Pattern
- It helps to centralize the object creation and thus help to achieve a decoupled system
- Example
- Say we have 2 classes - Customer and Lead. Based on condition, we need to create object of either one.
- Bad Solution
```csharp
if (comboBox1.Text == "Customer")
{
    _customer = new Customer();
}
else
{
    _customer = new Lead();
}
```
- Using simple factory pattern we can create a class that will have the centralize authority to create a object. As and when we require, we can use that class for object reference.
1. Entities - BaseCustomer, Customer, Lead
```csharp
  public class BaseCustomer
  {
      public string CustomerName { get; set; }
      public string CustomerAddress { get; set; }
      public string PhoneNumber { get; set; }
      public decimal BillAmount { get; set; }
      public DateTime BillDate { get; set; }

      public virtual void Validate()
      {
          throw new Exception("Not implemented");
      }
  }

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

 public class Lead : BaseCustomer
 {
     public override void Validate()
     {
         if (CustomerName.Length == 0)
         {
             throw new Exception("Customer Name is required");
         }
         if (PhoneNumber.Length == 0)
         {
             throw new Exception("Phone Number is required");
         }
     }
 }
```
2. Factory Class
```csharp
namespace FactoryCustomer
{
    public static class Factory
    {
        public static BaseCustomer CreateCustomer(string customerType)
        {
            if (customerType == "Customer")
            {
               return new Customer();
            }
            else
            {
                return new Lead();
            }
        }
    }
}
```

3. In UI
```csharp
 private BaseCustomer _customer = null; 
 _customer = Factory.CreateCustomer(comboBox1.Text);
_customer.Validate();
```

- It can be automated using DI frameworks like unity, ninject, windsor etc

## RIP (Replace IF with Polymorphism) pattern
- Above code is still having an issue i.e we are using if statement which means we have not used polymorphism to fullest
- Replace **Factory class** as below
```csharp
 public static class Factory
 {
     private static Dictionary<string, BaseCustomer> custs = new Dictionary<string, BaseCustomer>();

     static Factory()
     {
         custs.Add("Customer", new Customer());
         custs.Add("Lead", new Lead());
     }

     public static BaseCustomer CreateCustomer(string customerType)
     {
         return custs[customerType];
     }
 }
```

## Lazy Loading Pattern
- Loads objects on demand
- in above **Factory class**, customer object will get loaded evey time in constructor, but we want to load that on demand
1. First Way
```csharp
 public static class Factory
 {
     private static Dictionary<string, BaseCustomer> custs = new Dictionary<string, BaseCustomer>();
     public static BaseCustomer CreateCustomer(string customerType)
     {
         //Design Pattern:- Lazy Loading
         if (custs.Count == 0) {
             custs.Add("Customer", new Customer());
             custs.Add("Lead", new Lead());
         }

         //Design Pattern:- RIP Replace If with Polymorphism
         return custs[customerType];
     }
 }
```

2. 2nd way is using Lazy keyword
```csharp
 public static class Factory
 {
     private static Lazy<Dictionary<string, BaseCustomer>> custs = new Lazy<Dictionary<string, BaseCustomer>>(() => SetCustomerObject());   //Design Pattern:- Lazy Loading

     public static BaseCustomer CreateCustomer(string customerType)
     {
         //Design Pattern:- RIP Replace If with Polymorphism
         return custs.Value[customerType];
     }

     private static Dictionary<string, BaseCustomer> SetCustomerObject()
     {
         Dictionary<string, BaseCustomer> customers = new Dictionary<string, BaseCustomer>();
         customers.Add("Customer", new Customer());
         customers.Add("Lead", new Lead());
         return customers;
     }
 }
```

## Unity Pattern
- used to automate the simple factory pattern
- **Steps**
- Add "Unity" nuget package
- replace factory class code with below:
```csharp
private static IUnityContainer custs = null; // Unity container is a container where we will create objects like earlier we are doing using Dictionary

public static BaseCustomer CreateCustomer(string customerType)
{
    if (custs == null) {
        custs = new UnityContainer();
        custs.RegisterType<BaseCustomer, Customer>("Customer");
        custs.RegisterType<BaseCustomer, Lead>("Lead");

    }


    //Design Pattern:- RIP Replace If with Polymorphism
    return custs.Resolve<BaseCustomer>(customerType);
}
```

- **Note:** Till now we have done logical decoupling not the physical decoupling as out BaseCustomer, Customer, Lead all lie in same project nd we have added complete reference of MiddeLayer project to our UI project. To resolve it we need to use Interface
- Steps:
1. create new project
2. define interface
```csharp
public interface ICustomer
{
    string CustomerName { get; set; }
    string CustomerAddress { get; set; }
    string PhoneNumber { get; set; }
    decimal BillAmount { get; set; }
    DateTime BillDate { get; set; }

    void Validate();

}
```
3. Implement ICustomer in BaseCustomer
4. In Factory class replace BaseCustomer with ICustomer
```csharp
 public static ICustomer CreateCustomer(string customerType)
 {
     if (customers == null) {
         customers = new UnityContainer();
         customers.RegisterType<ICustomer, Customer>("Customer");
         customers.RegisterType<ICustomer, Lead>("Lead");

     }


     //Design Pattern:- RIP Replace If with Polymorphism
     return customers.Resolve<ICustomer>(customerType);
 }
```
5. In UI app, remove MiddleLayer reference and add reference of Interface project. Replace BaseCustomer with ICustomer.

## Inversion of Control (IOC) PRINCIPLE (synonym to Single Responsibility Principle(SRP) and Separation of Concern (SOC))
- Till now our validations are tighly coupled so if tomorrow we want todefgien differentstrategies for validations that cannot be possible.
- To resolve that we need to move or invert the work to some other entity that can control that work in better way. i.e. invert unnecessary control from a class and give it to other entity.

## Strategy Pattern 
- behaviourial design pattern
- helps to select the alogorithms on runtime

## Example of IOC, DI, Strategy Pattern
1. Create Interface for validation
```csharp
// Design pattern :- Stratergy pattern helps to choose lgorithms dynamically
public interface IValidation<AnyType>
{
    void Validate(AnyType obj);
}
```

2. create a class that will implement IValidation and define different algorithms
```csharp
public class CustomerValidation : IValidation<ICustomer>
{

  // note earlier this logic is written in appropriate classes.
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
        if (obj.BillDate >= DateTime.Now)
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
```

3. Modify BaseCustomer as below
```csharp
public class BaseCustomer :ICustomer
{
    private IValidation<ICustomer> _validation = null;
    public BaseCustomer(IValidation<ICustomer> obj) // injecting validation object - IOC, DI
    {
        _validation = obj;
    }

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
```

4. Modify Factory class's Create method as below
```csharp
 public static ICustomer CreateCustomer(string customerType)
 {
     if (customers == null) {
         customers = new UnityContainer();
         customers.RegisterType<ICustomer, Customer>("Customer", new InjectionConstructor(new CustomerValidation())); // IOC and DI
         customers.RegisterType<ICustomer, Lead>("Lead", new InjectionConstructor(new LeadValidation()));

     }


     //Design Pattern:- RIP Replace If with Polymorphism
     return customers.Resolve<ICustomer>(customerType);
 }
```

## Repository Pattern
- Decounple the model from Data access layer
- In our app till now, UI will talk with MiddleLayer using InterfaceCustomer and Middlelayer will talk with DAL using InterfaceDal
- **Generic Repository Pattern:** can accept any type of object

![image](https://github.com/user-attachments/assets/6005ddaf-86c0-4252-9cbc-73bff3919605)


## Template Pattern
- behavioral design pattern that defines the skeleton of an algorithm in the superclass but lets subclasses override specific steps of the algorithm without changing its structure.
- Say we have DAL with ADO.NET, so we will be having  fixed sequence: open connection, execute command, close connection. Also open and close connection code is not going to change.
- So we can define a base class that will have fied sequence and common code and a method that can have different implementations
- **Key Concepts:**
- Abstract Class: The template method pattern typically involves an abstract class that defines the template method, which outlines the steps of the algorithm.
- Template Method: A method in the abstract class that defines the sequence of steps of the algorithm. Some of these steps may call abstract methods that subclasses must implement.
- Concrete Classes: Subclasses that implement the abstract methods defined in the abstract class, providing specific behavior for the steps of the algorithm.
- example: Page life cycle
```csharp
//Design Pattern:- Template Pattern
public abstract class TemplateADO<AnyType> : AbstractDAL<AnyType> //Template
{
    protected SqlConnection objConn = null;
    protected SqlCommand objComm = null;
    public TemplateADO(string connectionstring) : base(connectionstring)
    {
        
    }
    private void OpenConnection()
    {
        objConn=new SqlConnection(ConnectionString);
        objConn.Open();
        objComm = new SqlCommand();
        objComm.Connection = objConn;
    }

    protected abstract void ExecuteCommand(AnyType obj); // we want chid classes to define this method
    
    private void CloseConnection()
    {
        objConn.Close();
    }
    public void Execute(AnyType obj)  // template method which ahs define fixed sequence
    {
        //fixed sequence
        OpenConnection();
        ExecuteCommand(obj);
        CloseConnection();

    }

    public override void Save()
    {
        foreach(AnyType o in AnyTypes)
        {
            Execute(o);
        }
    }
}

 public class CustomerDAL : TemplateADO<ICustomer>
 {
     public CustomerDAL(string _ConnectionString)
        : base(_ConnectionString)
     {

     }

     protected override void ExecuteCommand(ICustomer obj)
     {
         objComm.CommandText = "";
     }
 }
```

## Entity Framework
- implements ORM (Object Relational Mapping)
  - objects: Entities like Customer, Supplier
  - Mapping: DAL
  - RDBMS: Tables
- uses ADO.NET internally
- liek a wrapper over ADO.NET, simplifies ADO.NET API
- rather than dealing into data set, adapter, we deal with domain entities
- needs unique key(for this use [Key] dataannotation on ID property
- Drawback: we cannot map table with interface. We need to have atleast an abstract class in between 9a strongly typed class. In our app, BaseCustomer class has been converted into abstract class which will map with RDBMS table structure rather than interface
- **steps to add EF**
1. Create a class which will implement "DbContext"
```csharp
public class EFDalAbstract<AnyType> : DbContext, IDal<AnyType> where AnyType : class
{
    public void Add(AnyType obj) //in-memory
    {
        Set<AnyType>().Add(obj);
    }

    public void Save() 
    {
        SaveChanges();// physical Commit
    }

    public List<AnyType> Search()
    {
        return Set<AnyType>().AsQueryable<AnyType>().ToList<AnyType>();
    }

    public void Update(AnyType obj)
    {
        throw new NotImplementedException();
    }
}
```
2. create a specific object EF class where mapping code will be written in OnModelCreating()
```csharp
public class EfCustomerDal : EFDalAbstract<BaseCustomer>
{
    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        //mapping code
        modelBuilder.Entity<BaseCustomer>().ToTable("tblCustomer");
        modelBuilder.Entity<BaseCustomer>().Map<Customer>(m => m.Requires("CustomerType").HasValue("Customer"));
        modelBuilder.Entity<BaseCustomer>().Map<Lead>(m => m.Requires("CustomerType").HasValue("Lead"));
        modelBuilder.Entity<BaseCustomer>().Ignore(t => t.CustomerType);
    }
}
```

3. In FactoryDal, register EFDal
```csharp
objectsOfOurProjects.RegisterType<IDal<BaseCustomer>,
   EfCustomerDal>("EFDal");
```

- **Note:** As EF has a drawback, so to make app work with both ADO.NET and EF, replace reference of Icustoer with BaseCustomer in registrations and UI.

![image](https://github.com/user-attachments/assets/0ac2f209-ac83-4892-b206-b7d679e87e43)

## Adapter Pattern
- help client to communicate classes which are having  incompatible methods/function names in a standard uniform way
- **Types**
- Class Adapter Pattern
- Object Adapter Pattern

1. Class Adapter Pattern
- Say for EF, it provides SaveChanges()/AsQueryable(). Similarly, ADO.NET has "ExecuteNonQuery()/ExecuteReader()" for that. But our project is using Save() for the same on top of it encapsulating those relevant franewok method calls. So here we can use Adapter Pattern
- **steps to impement Adpater pattern for EF**
- created interafce IDal
- created class EFDalAbstract
- inherit from IDAl ad DbContext
- implemented Save() from IDAL
- called ef SaveChanges() inside Save().

2. Object Adapter Pattern
```csharp
public interface IExport
{
    void Export();
}
public class WordExport : IExport
{
    public void Export()
    {
        throw new NotImplementedException();
    }
}
public class ExcelExport : IExport
{
    public void Export()
    {
        throw new NotImplementedException();
    }
}

//Third Party incompatibel Dll
public class PdfExport
{
    public void Save()
    {
        throw new NotImplementedException();
    }
}

//Object Adapter Pattern
public class PdfObjectAdapter : IExport
{
    //Internally it calls save
    public void Export()
    {
    //Create instance of PdfExport
        PdfExport c = new PdfExport();
        c.Save();
    }
}

//actual usage
IExport exp = new ExcelExport();
exp.Export();

exp = new WordExport();
exp.Export();

IExport objAdapter = new PdfObjectAdapter();
objAdapter.Export();
```

## UOW (Unit of Work)
- go hand on hand with repository patterh
- resolves the repository pattern problem: Repository considers each of the instance as "Separate self contained transaction". In order to make ste of transactions as one transactiuon unit, we need UOW
- like Repository decouples the Data access technology, similarly UOW should decouple transaction technology
- **Steps**
1. Create UOW interface
```csharp
public interface IUow
{
    void Commit();
    void Rollback();
}
```
2. UOW will start a transaction and then inject that transaction object inside repository and wave all repositories into a centralized transaction

![image](https://github.com/user-attachments/assets/6ae467d1-dde8-4f27-bdbb-1b03aaa9d03f)   
![image](https://github.com/user-attachments/assets/010728b9-205d-4e59-b865-7a362e347670)
```csharp
For ADO.NET
  public class ADOUow : IUow
 {
     public SqlConnection Connection { get; set; }
     public SqlTransaction Transaction { get; set; }
     public ADOUow()
     {
         Connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Conn"].ConnectionString);
         Connection.Open();
         Transaction = Connection.BeginTransaction();
     }

     public void Commit()
     {
        Transaction.Commit();
        Connection.Close();
     }

     public void Rollback() //Design Pattern: - object adapter pattern
     {
         Transaction.Dispose();
         Connection.Close();
     }
 }
```csharp

- In IRepository Interface add new method
```csharp
 void SetUow(IUow uow);// used to send UOW inside this repository
```

- In TemplateADO
```csharp
public abstract class TemplateADO<AnyType> : AbstractDAL<AnyType> //Template
{
    protected SqlConnection objConn = null;
    protected SqlCommand objComm = null;
    IUow UowObj = null;

    public override void SetUow(IUow uow)
    {
        UowObj = uow;
        objConn = ((ADOUow)uow).Connection;
        objComm = new SqlCommand();
        objComm.Connection = objConn;
        objComm.Transaction = ((ADOUow)uow).Transaction;
    }

    private void OpenConnection()
    {
        if (objConn == null)
        {
            // objConn = new SqlConnection(ConnectionString); // when passing via a resolver in factory Dal
            objConn = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["Conn"].ConnectionString);
            objConn.Open();
            objComm = new SqlCommand();
            objComm.Connection = objConn;
        }
    }

    protected abstract void ExecuteCommand(AnyType obj); // we want child classes to define this method

    protected abstract List<AnyType> ExecuteCommand(); // for search

    private void CloseConnection()
    {
        if (UowObj == null)
        {
            objConn.Close();
        }
    }
    public void Execute(AnyType obj) // template method:  Fixed Sequence Insert
    {
        //fixed sequence
        OpenConnection();
        ExecuteCommand(obj);
        CloseConnection();

    }

    //for search as it returns list
    public List<AnyType> Execute() // Fixed Sequence select
    {
        //fixed sequence
        OpenConnection();
        List<AnyType> objTypes = ExecuteCommand();
        CloseConnection();
        return objTypes;

    }

    public override void Save()
    {
        foreach(AnyType o in AnyTypes)
        {
            Execute(o);
        }
    }   

    public override List<AnyType> Search()
    {
        return Execute();
    }
}
```

- Register the type in Factory Dal and in Form.cs use the same as below
```csharp
 private void btnUOW_Click(object sender, EventArgs e)
 {
     IUow uow = FactoryDal.FactoryUsingUnity<IUow>.CreateObject(DalLayer.Text == "ADODal" ? "AdoUow" : "EfUOW");
     try
     {
         BaseCustomer cust1 = FactoryCustomer.FactoryUsingUnity<BaseCustomer>.CreateObject("Lead");
         cust1.CustomerType = "Lead";
         cust1.CustomerName = "Cust1";
         cust1.BillDate = Convert.ToDateTime("12/12/2024");

         // Unit of work
         IRepository<BaseCustomer> dal = FactoryDal.FactoryUsingUnity<IRepository<BaseCustomer>>
                              .CreateObject(DalLayer.Text); // Unit
         dal.SetUow(uow);
         dal.Add(cust1); // In memory


         cust1 = FactoryCustomer.FactoryUsingUnity<BaseCustomer>.CreateObject("Lead");
         cust1.CustomerType = "Lead";
         cust1.CustomerName = "Cust2";
         cust1.CustomerAddress = "dzxcczxc";
         cust1.BillDate = Convert.ToDateTime("12/12/2024");
         IRepository<BaseCustomer> dal1 = FactoryDal.FactoryUsingUnity<IRepository<BaseCustomer>>
                              .CreateObject(DalLayer.Text); // Unit
         dal1.SetUow(uow);
         dal1.Add(cust1); // In memory

         uow.Commit();
     }
     catch (Exception ex) { 
         uow.Rollback();
         MessageBox.Show(ex.Message.ToString());
     }
 }
```

## Decorator Pattern
- Say we need validations like below

![image](https://github.com/user-attachments/assets/e51139b8-fd89-4889-a637-999d98fd8dd7)

- Since here Custome Name is base validation as it is compulsory for all types of customers
- for other validations we need to go on top of this base validation like
    - Customer Name + Phone Number will become Lead Validation
 
- decorator pattern: we creatte base functionality anf on top of that we add other functionality to create new functionality
- **Key Concepts**
- Base Validation
- Discrete Classes(Customer Validation, Phone Validation, Bill Validation)
- Link in all discrete classes

- **steps**
1. Define basde validation, Validation Linker and other discrete validation classes
```csharp
- Replace code ofValidation.cs
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
        if (obj.BillDate > DateTime.Now)
        {
            throw new Exception("Bill date  is not proper");
        }
    }
}
```

2. Inject the validation logic in FacvtoryCustomer
```csharp
IValidation<ICustomer> custValidate = new PhoneValidation(new CustomerBasicValidation());
objectsOfOurProjects.RegisterType<BaseCustomer, Lead>("Lead", new InjectionConstructor(custValidate));
```

3. Register validations and customers as
```csharp
public static AnyType CreateObject(string Type) // make it generic
{
    if (objectsOfOurProjects == null) {
        objectsOfOurProjects = new UnityContainer();

        //for Decorator Pattern
        IValidation<ICustomer> custValidate = new PhoneValidation(new CustomerBasicValidation());
        objectsOfOurProjects.RegisterType<BaseCustomer, OrgCustomer>("Lead", new InjectionConstructor(custValidate, "Lead"));

        custValidate = new CustomerBasicValidation();
        objectsOfOurProjects.RegisterType<BaseCustomer, OrgCustomer>("SelfService", new InjectionConstructor(custValidate, "Self Service"));

        custValidate = new CustomerAddressValidation(new CustomerBasicValidation());
        objectsOfOurProjects.RegisterType<BaseCustomer, OrgCustomer>("HomeDelivery", new InjectionConstructor(custValidate, "Home Delivery"));


        custValidate = new CustomerBillValidation(new CustomerBillValidation(new PhoneValidation(new CustomerBasicValidation())));
        objectsOfOurProjects.RegisterType<BaseCustomer, OrgCustomer>("Customer", new InjectionConstructor(custValidate, "Customer"));
    }


    //Design Pattern:- RIP Replace If with Polymorphism
    return objectsOfOurProjects.Resolve<AnyType>(Type);
}
```

- Decorator pattern can be made more dynamic using COR or builder pattern. 

## Iterator Pattern
- GOF pattern, cna be implemented automatically by using regualr APIs provided by .NET
- provides a ay to access the elements  of an object sequentially without exposing its underlying  representation
- Say we have a method that is returing a list. It is possible that user can manipulate that list and it migt result in wrong data. So here comes Iterator pattern which can only allow to iterate the list not manipulate.
- To implement iterator pattern, we have 2 avaiable interfaces: IEnumerable and iEnumerator

## Prototype and Mememto Pattern
- prototype is creating a copy of object
- **Type**
- Shallow Cloning: focus on breadth rather than depth, demonstrating a wide range of features at a high level without delving deeply into the specifics or functionalities of each feature.
- Deep Cloning: can be interpreted as a highly detailed and comprehensive prototype that closely mimics the final product

- **Step** create a clone
```csharp
_OldCopy = (ICustomer)this.MemberwiseClone();
```

- Momemto:
- is a behavioral design pattern that provides a way to capture and externalize an object's internal state so that the object can be restored to this state later, without violating encapsulation.
- It's useful for implementing undo mechanisms.
- **Key Components:**
- Originator: The object whose state needs to be saved and restored.
- Memento: The object that stores the state of the Originator. It provides no methods to modify the state.
- Caretaker: The object that holds and manages the Memento. It requests a save from the Originator, and restores the Originator's state from the Memento.
  
- **steps**
```csharp
// Design pattern :- memento pattern ( Revert old state)
private ICustomer _OldCopy = null;

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
```   

## Advantages of SOLIP principles over OOPs
- The SOLID principles complement Object-Oriented Programming (OOP) by providing a set of guidelines to create more robust, maintainable, and scalable software. While OOP offers a powerful foundation for software design, incorporating SOLID principles enhances the effectiveness of OOP. Here are the advantages of using SOLID principles over traditional OOP:

1. Single Responsibility Principle (SRP)
- OOP Limitation: Classes can become bloated and handle multiple responsibilities, making them harder to understand and maintain.
- SOLID Advantage: SRP ensures that each class has only one reason to change, promoting cleaner and more focused code. This makes the code easier to understand, test, and maintain.

2. Open/Closed Principle (OCP)
- OOP Limitation: Modifying existing classes to add new functionality can introduce bugs and require extensive testing.
- SOLID Advantage: OCP promotes extending the behavior of a system without modifying existing code. This reduces the risk of introducing bugs and makes the system more adaptable to change.

3. Liskov Substitution Principle (LSP)
- OOP Limitation: Improper use of inheritance can lead to subclasses that do not behave correctly when used in place of their base class.
- SOLID Advantage: LSP ensures that derived classes can be substituted for their base classes without altering the correctness of the program. This maintains the integrity of the class hierarchy and promotes polymorphism.

4. Interface Segregation Principle (ISP)
- OOP Limitation: Large, monolithic interfaces can force clients to depend on methods they do not use, leading to tight coupling and reduced flexibility.
- SOLID Advantage: ISP encourages the use of smaller, more specific interfaces. This reduces unnecessary dependencies and makes the system more modular and flexible.

5. Dependency Inversion Principle (DIP)
- OOP Limitation: High-level modules often depend on low-level modules, leading to tight coupling and difficulty in making changes or replacing components.
- SOLID Advantage: DIP promotes dependency on abstractions rather than concrete implementations. This decouples high-level and low-level modules, enhancing flexibility and making the system easier to modify and test.
