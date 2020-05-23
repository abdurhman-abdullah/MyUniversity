﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace university.Model
{
    public class Students
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Enter 3 Letters")]
        public string FirstName { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Enter 3 Letters")]
        public string LastName { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Enter 3 Letters")]
        public string NameFamily { get; set; }

        public DateTime? DatePublished { get; set; }

        public virtual Specialties specialtie { get; set; }

        public virtual Supervisor supervisor { get; set; }

        public virtual ICollection<BookTeacherStudent> BookTeacherStudents { get; set; }

    }
}
