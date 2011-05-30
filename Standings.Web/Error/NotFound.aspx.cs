using System;
using System.Net;

namespace Standings.Web.Error
{
    public partial class NotFound : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.StatusCode = (int)HttpStatusCode.NotFound;
        }
    }
}