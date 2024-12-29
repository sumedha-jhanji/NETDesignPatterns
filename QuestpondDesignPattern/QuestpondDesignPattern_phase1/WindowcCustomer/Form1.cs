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
        private IRepository<BaseCustomer> Idal;

        public Form1()
        {
            InitializeComponent();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            DalLayer.Items.Add("ADODal");
            DalLayer.Items.Add("EFDal");
            DalLayer.SelectedIndex = 0;
            Idal = FactoryDal.FactoryUsingUnity<IRepository<BaseCustomer>>.CreateObject(DalLayer.Text);
            LoadGrid();
        }

        //in-memory
        private void LoadGridInMemory()
        {
            dtgGridCustomer.DataSource = null;
            IEnumerable<BaseCustomer> custs = Idal.GetData();
            dtgGridCustomer.DataSource = custs;
        }

        //physical data load
        private void LoadGrid()
        {
            dtgGridCustomer.DataSource = null;
            IEnumerable<BaseCustomer> custs = Idal.Search();
            dtgGridCustomer.DataSource = custs;
        }

        private void cmbCustomerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //_customer = Factory.CreateCustomer(cmbCustomerType.Text);
            _customer = FactoryCustomer.FactoryUsingUnity<BaseCustomer>.CreateObject(cmbCustomerType.Text);
        }

        private void SetCustomer()
        {
            _customer.CustomerName = txtCustomerName.Text;
            _customer.PhoneNumber = txtPhoneNumber.Text;
            _customer.BillDate = Convert.ToDateTime(txtBillDate.Text);
            _customer.BillAmount = Convert.ToDecimal(!String.IsNullOrEmpty(txtBillAmount.Text) ? txtBillAmount.Text: "0");
            _customer.CustomerAddress = txtAddress.Text;
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
                MessageBox.Show(ex.Message.ToString());
            }

        }

        //in-memory
        private void addCustomer_Click(object sender, EventArgs e)
        {
            try
            {
                SetCustomer();
                Idal.Add(_customer); // in-memory + Validate
                LoadGridInMemory();
                ClearCustomer();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        //physical Commit
        private void btnSavecustomer_Click(object sender, EventArgs e)
        {
            Idal.Save();
            ClearCustomer();
            LoadGrid();
        }

        private void ClearCustomer()
        {
            txtCustomerName.Text = "";
            txtPhoneNumber.Text = "";
            txtBillDate.Text = DateTime.Now.Date.ToString();
            txtBillAmount.Text = "";
            txtAddress.Text = "";
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
            catch (Exception ex)
            {
                uow.Rollback();
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void DalLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
         //   LoadGrid();
        }
    }
}
