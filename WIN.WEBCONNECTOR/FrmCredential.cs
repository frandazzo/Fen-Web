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
using System.Diagnostics;
using WIN.WEBCONNECTOR.corporatesFeneal;

namespace WIN.WEBCONNECTOR
{
    public partial class FrmCredential : Form
    {

 

        public FrmCredential()
        {
            InitializeComponent();
            LoadComboProvince();

            if (!string.IsNullOrEmpty(ProgramParameters.Instance.UserName))
                txtUserName.Text = ProgramParameters.Instance.UserName;
            if (!string.IsNullOrEmpty(ProgramParameters.Instance.Password))
                txtPwd.Text = ProgramParameters.Instance.Password;
            if (!string.IsNullOrEmpty(ProgramParameters.Instance.Province))
                cboProv.Text = ProgramParameters.Instance.Province;

        }

         private void LoadComboProvince()
         {
             cboProv.Items.Clear();
             IList province = GeoHandlerProvider.Instance.Geo.GetListaProvincie();
             foreach (string item in province)
             {
                 cboProv.Items.Add(item);
             }

             cboProv.Sorted = true;


             cboProv.SelectedIndex = 0;



             

         }


         private void LoadComboProvince(IList<string> territori)
         {
             cboProv.Items.Clear();
             foreach (string item in territori)
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
                 string username = txtUserName.Text;
                 string password = txtPwd.Text;
                 string prov = cboProv.Text;

                 CorporateCredentialMapper map = new CorporateCredentialMapper();
                 if (map.isCorporateAccount(username))
                 {
                     corporatesFeneal.Credential c = map.RetrieveRealCredentialsForCorporateAccount(username, password, prov);
                     if (c == null)
                     {
                         lblerror.Text = "Inserire le credenziali corrette!";
                         return;
                     }

                     //carico le credenziali corrette
                      username = c.RealUsername;
                      password = c.RealPassword;
                      prov = c.Province;
                 }


                
                 if (CredentialValidator.AreCredentialValid(username, password, prov))
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
             lblUrl.Text += " " + Properties.Settings.Default.WIN_WEBCONNECTOR_FenealgestServices_FenealgestwebServices  .ToString();
         }

         private void pictureBox1_Click(object sender, EventArgs e)
         {
             try 
            {
                //Process.Start("http://www.tecnoesis.it");
            }
            catch (Exception)
            {
	            //
            }
             
         }

         private void txtUserName_Leave(object sender, EventArgs e)
         {
             //if (!ProgramParameters.Instance.OpenedByHumanActor)
             //    return;


             //if (!String.IsNullOrEmpty(txtUserName.Text))
             //{
             //    corporatesFeneal.CorporateCredentialMapper m = new CorporateCredentialMapper();
             //    IList<string> tt = m.RetrieveCorporateAcconutTeritories(txtUserName.Text);
             //    if (tt.Count > 0)
             //        LoadComboProvince(tt);
             //    else
             //        LoadComboProvince();
             //}
             //else
             //{
             //    LoadComboProvince();
             //}
         }
    }
}
