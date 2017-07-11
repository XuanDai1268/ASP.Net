using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class test2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie objCookie = new HttpCookie("myCookie", "Hello,Cookie!");
        Response.Cookies.Add(objCookie); 
    }
   
}