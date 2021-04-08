using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BREWCITY.Models
{
    public class TransactionHistory
    {
        [Key]
        public int Id { get; set; }
    }
}
