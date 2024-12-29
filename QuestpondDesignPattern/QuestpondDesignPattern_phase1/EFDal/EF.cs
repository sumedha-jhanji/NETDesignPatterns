using InterfaceCustomer;
using InterfaceDal;
using MiddleLayer;
using System.Data.Entity;

namespace EFDal
{
    //Design Pattern:- Class Adapter Pattern. We are using save() from IDal which allows clients to use common name
    public class EFDalAbstract<AnyType> : DbContext, IRepository<AnyType> where AnyType : class
    {
        DbContext dbcont = null;
        //public EFDalAbstract():base("name=conn")
        //{

        //}
        public EFDalAbstract()
        {
            dbcont = new EUow(); // Self contained transaction
        }

        public void Add(AnyType obj) //in-memory
        {
            dbcont.Set<AnyType>().Add(obj);
        }

        public IEnumerable<AnyType> GetData()
        {
            throw new NotImplementedException();
        }

        public void Save() 
        {
            dbcont.SaveChanges();// physical Commit
        }

        public IEnumerable<AnyType> Search()
        {
            return dbcont.Set<AnyType>().AsQueryable<AnyType>().ToList<AnyType>();
        }

        public void SetUow(IUow uow)
        {
            dbcont = ((EUow)uow); // Global transaction UOW
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
            //when we have separate classes for each type of customer
            //modelBuilder.Entity<BaseCustomer>().Map<Customer>(m => m.Requires("CustomerType").HasValue("Customer"));
            //modelBuilder.Entity<BaseCustomer>().Map<Lead>(m => m.Requires("CustomerType").HasValue("Lead"));
            modelBuilder.Entity<BaseCustomer>().Ignore(t => t.CustomerType);
        }
    }

    public class EUow : DbContext, IUow
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BaseCustomer>()
                       .ToTable("tblCustomer");
            //when we have separate classes for each type of customer
            //modelBuilder.Entity<BaseCustomer>().Map<Customer>(m => m.Requires("CustomerType").HasValue("Customer"));
            //modelBuilder.Entity<BaseCustomer>().Map<Lead>(m => m.Requires("CustomerType").HasValue("Lead"));
            modelBuilder.Entity<BaseCustomer>().Ignore(t => t.CustomerType);
        }
        public EUow() : base("name=ConnEf")
        {

        }
        public void Commit()
        {
            SaveChanges();
        }

        public void Rollback()
        {
            Dispose();
        }
    }
}
