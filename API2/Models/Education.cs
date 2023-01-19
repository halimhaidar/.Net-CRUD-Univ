using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace API2.Models
{
    public class Education
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Degree { get; set; }
        [Required]
        public string GPA { get; set; }
        [Required]
        [ForeignKey("University")]
        public int University_Id { get; set; }
        [JsonIgnore]
        public virtual Profilling Profilling { get; set; }
      
        public virtual University University { get; set; }


    }
}
