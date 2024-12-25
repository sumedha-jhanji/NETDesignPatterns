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
