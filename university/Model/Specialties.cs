using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace university.Model
{
    public class Specialties
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MinLength(3 , ErrorMessage = "Enter 3 Letters")]
        public string Name { get; set; }

        public DateTime? DatePublished { get; set; }

        public virtual TheSections TheSections { get; set; }

        public virtual ICollection<Books> Books { get; set; }

        public virtual ICollection<Teachers> Teachers { get; set; }

        public virtual ICollection<Students> Students { get; set; }
    }
}
