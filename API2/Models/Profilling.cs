using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace API2.Models
{
    public class Profilling
    {
        [Key]
        public string NIK { get; set; }
        [Required]
        public int Education_Id { get; set; }
        
        public virtual Account Account { get; set; }
        
        public virtual Education Education { get; set; }
    }
}
