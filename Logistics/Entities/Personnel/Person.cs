using Logistics.Entities.Contact;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Logistics.Entities.Personnel
{
    [Table("Person")]
    public class Person
    {
        public long Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FullName
        {
            get
            {
                string fullName = this.FirstName ?? string.Empty;
                if (!string.IsNullOrEmpty(this.LastName))
                    fullName = $"{LastName}, {fullName}";
                if (!string.IsNullOrEmpty(this.MiddleName))
                    fullName = $"{fullName} {MiddleName}";
                return fullName;
            }
        }

        public DateTime? Dob { get; set; } = null;
        public int Age
        {
            get 
            {
                int age = 0;
                if (this.Dob.HasValue)
                {                    
                    age = DateTime.Now.Year - this.Dob.Value.Year;
                    if (DateTime.Now.DayOfYear < this.Dob.Value.DayOfYear)
                        age = age - 1;
                }                
                return age;
            }
        }

        public virtual List<Address> Addresses { get; set; } = new List<Address>();
        public virtual List<Phone> Phones { get; set; } = new List<Phone>();
        public virtual List<EmailAddress> Emails { get; set; } = new List<EmailAddress>();

        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}