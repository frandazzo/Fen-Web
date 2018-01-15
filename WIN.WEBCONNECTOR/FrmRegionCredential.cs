using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WIN.BASEREUSE;
using System.Collections;
using WIN.WEBCONNECTOR.GeoElements;
using WIN.TECHNICAL.MIDDLEWARE.Internet;
using WIN.WEBCONNECTOR.Credential;

namespace WIN.WEBCONNECTOR
{
    public partial class FrmRegionCredential : Form
    {

 

        public FrmRegionCredential()
        {
            InitializeComponent();
            LoadComboRegioni();

            if (!string.IsNullOrEmpty(ProgramParameters.Instance.UserName))
                txtUserName.Text = ProgramParameters.Instance.UserName;
            if (!string.IsNullOrEmpty(ProgramParameters.Instance.Password))
                txtPwd.Text = ProgramParameters.Instance.Password;
            if (!string.IsNullOrEmpty(ProgramParameters.Instance.Province))
                cboProv.Text = ProgramParameters.Instance.Province;

        }

         private void LoadComboRegioni()
         {
             IList province = GeoHandlerProvider.Instance.Geo.GetListaRegioni();
             foreach (string item in province)
             {
                 cboProv.Items.Add(item);
             }

             cboProv.Sorted = true;


             cboProv.SelectedIndex = 0;



             

         }

         private void cmdOk_Click(object sender, EventArgs e)
         {

             try
             {
                 WIN.GUI.UTILITY.Helper.ShowWaitBox("Attendere verifica credenziali...", Properties.Resources.Waiting3);
                 if (CredentialValidator.AreRegionalCredentialValid(txtUserName.Text, txtPwd.Text, cboProv.Text))
                 {
                     this.DialogResult = DialogResult.OK;
                     this.Close();
                     return;
                 }
                //this.DialogResult = DialogResult.Cancel;
                 else
                 {
                     lblerror.Text = "Inserire le credenziali corrette!";
                 }
             }
             catch (Exception ex)
             {
                 this.DialogResult = DialogResult.Cancel;
                 MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }
             finally
             {
                 WIN.GUI.UTILITY.Helper.HideWaitBox();
             }

            
             


         }

      
         private void cmdClose_Click(object sender, EventArgs e)
         {
             this.DialogResult = DialogResult.Cancel;
             this.Close();
         }

         private void FrmCredential_Load(object sender, EventArgs e)
         {
             lblerror.Text = "";
             lblUrl.Text += " " + Properties.Settings.Default.WIN_WEBCONNECTOR_FenealgestServices_FenealgestwebServices .ToString();
         }
    }
}
