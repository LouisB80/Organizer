using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Organizer.Models
{
    public class Broker
    {
        public int BrokerID { get; set; }
        [MaxLength(50), Required]
        [Display(Name = "Prénom")]
        public string Firstname { get; set; }
        [MaxLength(50), Required]
        [Display(Name = "Nom")]
        public string Lastname { get; set; }
        [MaxLength(150), Required]
        [EmailAddress]
        public string Mail { get; set; }
        [MaxLength(10), Required]
        [Display(Name = "Numéro de téléphone")]
        [RegularExpression(@"^0[0-9]{9}$")]
        public string PhoneNumber { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                return Firstname + " " + Lastname;
            }
        }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}