using System.ComponentModel.DataAnnotations;

namespace MVCUmbraco.Models
{
    public class Contact
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
       
        [Required]
        public string Message { get; set; }
    }
}