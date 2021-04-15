using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BREWCITY.Models
{
    public class DeliveryDetails
    {
        [Key]
        public int Id { get; set; }
        //or transaction id
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        [Display(Name = "Enter the name of the responsible party receiving the shipment.")]

        public string ResponsiblePartyName { get; set; }
        [Display(Name = "Enter the best phone number for the delivery contact:")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Delivery street number and address:")]
        public string Street { get; set; }
        [Display(Name = "Delivery City:")]
        public string City { get; set; }
        [Display(Name = "Delivery State")]
        public string State { get; set; }
        [Display(Name = "Enter any required state alcohol licence numbers for your state. Delivery may be delayed if information is not provided.")]
        public string StateAlcoholLicenceNumber { get; set; }
    }
}
