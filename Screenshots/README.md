## Design Pattern
- like signleton where we know how to and where to write things. We know source code

## Architectural pattern
- u know the block diagram like MVC, MVP

## Architectural style
- divide project into smaller ones
- example REST, SOA, Microservice

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
