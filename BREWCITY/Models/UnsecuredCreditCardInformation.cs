using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BREWCITY.Models
{
    public class UnsecuredCreditCardInformation
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        [Display(Name = "Enter the name of your company as it appears on your credit card.")]
        public string CompanyNameOnCreditCardRequired { get; set; }
        [Display(Name = "Credit card number:")]
        public string CreditCardNumber { get; set; }
        [Display(Name = "Expiration Month:")]
        public string Month { get; set; }
        [Display(Name = "Expiration Year:")]
        public int Year { get; set; }
        [Display(Name = "cvc")]
        public int CVC { get; set; }

    }
}
