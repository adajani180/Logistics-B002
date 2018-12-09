using Logistics.Areas.Config.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logistics.Entities.Access
{
    [Table("SystemUser")]
    public class SystemUser
    {
        public long Id { get; set; } = 0;

        //[Required(ErrorMessage = "The User Name is required.")]
        //[StringLength(20)]        
        public string UserName { get; set; }
        public string PasswordHash { get; set; }

        //[Required(ErrorMessage = "The Status is required.")]
        public long? StatusLookupId { get; set; }
        [ForeignKey("StatusLookupId")]
        public virtual ConfigLookup StatusLookup { get; set; }
    }
}