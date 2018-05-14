using System;
using System.Web;

namespace Frapid.Account.ViewModels
{
    public class Reset
    {
        public Reset()
        {
            string token = Guid.NewGuid().ToString();
            HttpContext.Current.Session["Token"] = token;
            this.Token = token;
        }

        public string Token { get; set; }
    }
}