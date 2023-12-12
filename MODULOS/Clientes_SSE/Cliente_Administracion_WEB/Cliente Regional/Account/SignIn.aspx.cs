using System;
using DevExpress.Web;
using Cliente_Regional.Model;

    public partial class SignInModule : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
        //Caso contrario, si ya tiene iniciada sesión, lo redireccionamos al Default (Pagina de carga por defecto)
        if (AuthHelper.IsAuthenticated())
        {
            Response.Redirect("~/");
        }
    }

    protected void SignInButton_Click(object sender, EventArgs e) {
        FormLayout.FindItemOrGroupByName("GeneralError").Visible = false;
        if (ASPxEdit.ValidateEditorsInContainer(this))
        {
            // DXCOMMENT: You Authentication logic
            if (!AuthHelper.SignIn(UserNameTextBox.Text, PasswordButtonEdit.Text))
            {
                GeneralErrorDiv.InnerText = "Datos de sesión invalidos";
                FormLayout.FindItemOrGroupByName("GeneralError").Visible = true;
            }
            else
                Response.Redirect("~/");
        }

    }
}