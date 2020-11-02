using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Lab01ASP.Models;
using Microsoft.AspNet.Identity;

namespace Lab01ASP
{
    public partial class SiteMaster : MasterPage
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //protected void Unnamed_LoggingOut(object sender, LoginCancelEventArgs e)
        //{
        //    Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        //}
        public IQueryable<Category> GetCategories()
        {
            var _db = new Lab01ASP.Models.ProductContext();
            IQueryable<Category> query = _db.Categories;
            return query;
        }
    }

}