using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UpdatePanalTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Label1.Text = DateTime.Now.ToString();
    }
   
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        Label4.Text = Calendar1.SelectedDate.ToString();
        TextBox1.Text = Calendar1.SelectedDate.ToString();
    }

   
}