using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BREWCITY.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public string ? Topic { get; set; }
    }
}
