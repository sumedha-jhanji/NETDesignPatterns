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
            //for update : it will happen automatically because objects are referred ByRef
            foreach (AnyType type in AnyTypes) {
                if (obj.Equals(type))
                {
                    return;
                }
            }
            AnyTypes.Add(obj);
        }

        public IEnumerable<AnyType> GetData()
        {
            return AnyTypes;
        }

        public virtual AnyType GetData(int Index)
        {
            return AnyTypes[Index];
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
