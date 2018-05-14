using System;
using System.Collections.Generic;
using Frapid.WebsiteBuilder.DTO;

namespace Frapid.WebsiteBuilder.ViewModels
{
    public class ContactUs : IWebsitePage
    {
        public ContactUs()
        {
            this.Token = Guid.NewGuid().ToString();
        }

        public string Title { get; set; }
        public string LayoutPath { get; set; }
        public string Layout { get; set; }
        public IEnumerable<Contact> Contacts { get; set; } 
        public string Token { get; set; }
    }
}