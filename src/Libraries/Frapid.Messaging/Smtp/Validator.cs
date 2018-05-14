using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Frapid.Messaging.Smtp
{
    internal class Validator
    {
        public Validator(string emailAddress)
        {
            this.EmailAddress = emailAddress;
        }

        public bool IsValid { get; private set; }
        public string EmailAddress { get; set; }

        public void Validate()
        {
            this.IsValid = false;

            if (string.IsNullOrWhiteSpace(this.EmailAddress))
            {
                return;
            }

            string emailAddress = this.EmailAddress;

            emailAddress = Regex.Replace(emailAddress, @"(@)(.+)$", this.DomainMapper);

            // Return true if address is in valid e-mail format.
            this.IsValid = Regex.IsMatch
                (
                    emailAddress,
                    @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
                    RegexOptions.IgnoreCase);
        }

        private string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            var idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                this.IsValid = true;
            }
            return match.Groups[1].Value + domainName;
        }
    }
}