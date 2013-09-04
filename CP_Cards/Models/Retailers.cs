using CP_Cards.infasctructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CP_Cards.Models
{
    public class Retailers
    {
        [Required]
        [Display(Name="Territory")]
        public string  Territory { get; set; }
        public string Name { get; set; }
        public string  Address { get; set; }
        public string City { get; set; }
        public string  State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Name2 { get; set; }
        public string Address2 { get; set; }
        public string City2 { get; set; }
        public string State2 { get; set; }
        public string Zip2 { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public string faxPhone { get; set; }
        public string WorkPhone { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name="Remember on this Computer")]
        public bool RememberMe { get; set; }
        public string RetailerID { get; set; }

        public bool IsValid(string _username, string _password)
        {

            DataService ds = new DataService();
            string exist = ds.Login(_username, Encryption.Encode(_password));
            if (exist != null)
                return true;
            else
                return false;
        }
    }
}