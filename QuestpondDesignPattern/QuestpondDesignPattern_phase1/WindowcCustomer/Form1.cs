using MiddleLayer;
using FactoryCustomer;
using InterfaceCustomer;
using InterfaceDal;
using FactoryDal;

namespace WindowcCustomer
{
    public partial class Form1 : Form
    {
        private BaseCustomer _customer = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void cmbCustomerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //_customer = Factory.CreateCustomer(cmbCustomerType.Text);
            _customer = FactoryCustomer.FactoryUsingUnity<BaseCustomer>.CreateObject(cmbCustomerType.Text);
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            try
            {
                SetCustomer();
                _customer.Validate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void SetCustomer()
        {
            _customer.CustomerName = txtCustomerName.Text;
            _customer.PhoneNumber = txtPhoneNumber.Text;
            _customer.BillDate = !string.IsNullOrEmpty(txtBillDate.Text) ? Convert.ToDateTime(txtBillDate.Text) : DateTime.Now;
            _customer.BillAmount = !string.IsNullOrEmpty(txtBillAmount.Text) ? Convert.ToInt32(txtBillAmount.Text) : 0;
            _customer.CustomerAddress = txtAddress.Text;
        }

        private void addCustomer_Click(object sender, EventArgs e)
        {
            SetCustomer();
            IRepository<BaseCustomer> dal = FactoryDal.FactoryUsingUnity<IRepository<BaseCustomer>>.CreateObject(DalLayer.Text);
            dal.Add(_customer); // in-memory
            dal.Save();//physical commit
            LoadGrid();
            ClearCustomer();
        }

        private void ClearCustomer()
        {
            txtCustomerName.Text = "";
            txtPhoneNumber.Text = "";
            txtBillDate.Text = DateTime.Now.Date.ToString();
            txtBillAmount.Text = "";
            txtAddress.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DalLayer.Items.Add("ADODal");
            DalLayer.Items.Add("EFDal");
            DalLayer.SelectedIndex = 0;
            LoadGrid();

        }
        private void LoadGrid()
        {
            IRepository<BaseCustomer> Idal = FactoryDal.FactoryUsingUnity<IRepository<BaseCustomer>>.CreateObject(DalLayer.Text);
            List<BaseCustomer> custs = Idal.Search();
            dtgGridCustomer.DataSource = custs;

        }

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
    }
}
