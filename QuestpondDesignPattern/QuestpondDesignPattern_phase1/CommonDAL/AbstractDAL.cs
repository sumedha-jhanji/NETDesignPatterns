using InterfaceDal;

namespace CommonDAL
{
    public abstract class AbstractDAL<AnyType> : IRepository<AnyType> // half defined class : common class for all DAL classes (in-memory store)
    {
        protected List<AnyType> AnyTypes = new List<AnyType>();
       // protected string ConnectionString = "";

        //public AbstractDAL(string _connectionString)
        //{
        //    ConnectionString = _connectionString;
        //}

        public virtual void Add(AnyType obj) // we want to provide opportunity to other classes to overeride this methid and can use there own in-memory storing methods
        {
            AnyTypes.Add(obj);
        }

        public IEnumerable<AnyType> GetData()
        {
            return AnyTypes;
        }

        public virtual void Save()
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<AnyType> Search()
        {
            throw new NotImplementedException();
        }

        public virtual void SetUow(IUow uow)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(AnyType obj)
        {
            throw new NotImplementedException();
        }
    }
}
