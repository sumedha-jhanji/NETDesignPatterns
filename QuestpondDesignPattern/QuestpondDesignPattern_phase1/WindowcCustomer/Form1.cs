using MiddleLayer;
using FactoryCustomer;
using InterfaceCustomer;

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
            _customer = FactoryUsingUnity.CreateCustomer(cmbCustomerType.Text);
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            try
            {
                SetCustomer();
                _customer.Validate();
            }
            catch (Exception ex) {                
               MessageBox.Show(ex.Message);
            }
            
        }

        private void SetCustomer()
        {
            _customer.CustomerName = txtCustomerName.Text;
            _customer.PhoneNumber = txtPhoneNumber.Text;
            _customer.BillDate =  !string.IsNullOrEmpty(txtBillDate.Text) ? Convert.ToDateTime(txtBillDate.Text) : DateTime.Now;
            _customer.BillAmount = !string.IsNullOrEmpty(txtBillAmount.Text) ? Convert.ToInt32(txtBillAmount.Text) : 0 ;
            _customer.CustomerAddress = txtAddress.Text;
        }
    }
}
