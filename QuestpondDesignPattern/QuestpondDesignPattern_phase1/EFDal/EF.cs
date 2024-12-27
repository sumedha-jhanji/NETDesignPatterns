using InterfaceCustomer;
using InterfaceDal;
using MiddleLayer;
using System.Data.Entity;

namespace EFDal
{
    //Design Pattern:- Class Adapter Pattern. We are using save() from IDal which allows clients to use common name
    public class EFDalAbstract<AnyType> : DbContext, IDal<AnyType> where AnyType : class
    {
        public EFDalAbstract():base("name=conn")
        {
            
        }
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
}
