using CoffeePointOfSale.Configuration;
using CoffeePointOfSale.Services.FormFactory;
using CreditCardValidator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CoffeePointOfSale.Forms
{
    public partial class FormPayment : Base.FormNoCloseBase
    {
        private IAppSettings _appSettings;
        public FormPayment(IAppSettings appSettings) : this()
        {
            _appSettings = appSettings;
        }

        public FormPayment()
        {
            InitializeComponent();
        }

        private void OnLoadFormPayment(object sender, EventArgs e)
        {
            
        }
        private void OnClickBtnClose(object sender, EventArgs e)
        {
            Close(); //closes this form
            FormFactory.Get<FormMain>().Show(); //re-opens the main form
        }

        private void FormPayment_Load(object sender, EventArgs e)
        {
            if (Globals.IsAnonymous)
            {
                labIsAnonymous.Text = "Anonymous";
                btnRedeem.Enabled = false;
                btnRedeem.Hide();
            }
            else
            {
                btnRedeem.Enabled = true;
                btnRedeem.Show();
            }
            labTotal.Text = labTotal.Text +" "+  Globals.Total;
            labSubtotal.Text = labSubtotal.Text + " " + Globals.SubTotal;
            labTax.Text = labTax.Text + " " + Globals.Tax;

        }
        static private bool CreditChanged = false;
        private void txtBoxCreditCard_TextChanged(object sender, EventArgs e)
        {
            CreditChanged= true;
        }
        private void BtnCreditCard_Click(object sender, EventArgs e)
        {
            if ((CreditChanged) && (txtBoxCreditCard.Text != string.Empty))
            {
                CreditCardDetector detector = new CreditCardDetector(txtBoxCreditCard.Text);
                if (detector.IsValid())
                {
                    //Change to Reciept screen here
                    Globals.CreditCard = txtBoxCreditCard.Text;
                    Globals.PayMethod = "credit";
                    Close(); //closes this form
                    FormFactory.Get<FormReceipt>().Show(); //re-opens the main form
                }
                else
                {
                    //Write to an error Label that is normally hidden from view
                    labError.Text = "Please enter a valid credit card.";
                }
            }
        }
        private void btnRedeem_Click(object sender, EventArgs e)
        {
            if (Globals.IsAnonymous)
            {

            }
            else
            {
                Globals.PayMethod = "point";
                Close(); //closes this form
                FormFactory.Get<FormReceipt>().Show(); //re-opens the main form
            }
        }

        private void labRedeemableP_Click(object sender, EventArgs e)
        {

        }
    }
}
