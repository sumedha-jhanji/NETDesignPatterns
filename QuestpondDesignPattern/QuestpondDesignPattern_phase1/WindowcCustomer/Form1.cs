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
            IDal<BaseCustomer> dal = FactoryDal.FactoryUsingUnity<IDal<BaseCustomer>>.CreateObject(DalLayer.Text);
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
            IDal<BaseCustomer> Idal =  FactoryDal.FactoryUsingUnity<IDal<BaseCustomer>>.CreateObject(DalLayer.Text);
            List<BaseCustomer> custs = Idal.Search();
            dtgGridCustomer.DataSource = custs;

        }
    }
}
