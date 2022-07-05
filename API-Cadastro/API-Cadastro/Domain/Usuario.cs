using System;
using System.ComponentModel.DataAnnotations;

namespace API_Cadastro.Domain
{
    public class Usuario
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string Surname { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public DateTime CreationDate { get; set; }

    }
}
