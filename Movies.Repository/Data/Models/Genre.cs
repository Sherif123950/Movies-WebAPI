using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Repository.Data.Models
{
	public class Genre
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }
		[MaxLength(100,ErrorMessage ="Name Maximum Length is 100 Charachter")]
        public string Name { get; set; }
    }
}
