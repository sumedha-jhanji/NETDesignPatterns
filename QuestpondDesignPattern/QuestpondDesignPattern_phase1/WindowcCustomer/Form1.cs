using MiddleLayer;
using FactoryCustomer;
using InterfaceCustomer;
using InterfaceDal;
using FactoryDal;

namespace WindowcCustomer
{
    public partial class Form1 : Form
    {
        private ICustomer _customer;

        public Form1()
        {
            InitializeComponent();
        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbCustomerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //_customer = Factory.CreateCustomer(cmbCustomerType.Text);
            _customer = FactoryCustomer.FactoryUsingUnity<ICustomer>.CreateObject(cmbCustomerType.Text);
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
            IDal<ICustomer> dal = FactoryDal.FactoryUsingUnity<IDal<ICustomer>>.CreateObject("ADODal");
            dal.Add(_customer); // in-memory
            dal.Save();//physical commit
            LoadGrid();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DalLayer.Items.Add("ADODal");
            DalLayer.Items.Add("EFDal");
            LoadGrid();

        }
        private void LoadGrid()
        {
            IDal<ICustomer> Idal = FactoryDal.FactoryUsingUnity<IDal<ICustomer>>.CreateObject("ADODal");
            List<ICustomer> custs = Idal.Search();
            dtgGridCustomer.DataSource = custs;

        }
    }
}
