using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Models
{
    public class CategoryCreate
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Category names must be 50 characters or fewer.")]
        public string Name { get; set; }
    }
}
