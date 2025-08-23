using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.DTO.AddresDtos
{
    public class CreateAddressDto
    {

        [Required(ErrorMessage = "User Name is required.")]

        public string UserName { get; set; } = string.Empty;

        [Required (ErrorMessage ="Adress line 1 is required")]

        [MinLength(8, ErrorMessage = "Must be at lest 8 char "), MaxLength(100)]
        public string AddressLine1 { get; set; }

        [Required(ErrorMessage = "Adress line 2 is required")]
        [MinLength(8, ErrorMessage = "Must be at lest 8 char "),MaxLength(100)]

        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "City is required")]

        [MinLength(3, ErrorMessage = "Must be at lest 3 char "),MaxLength(100)]

        public string City { get; set; }
        [Required(ErrorMessage = "State is required")]
        [MinLength(3, ErrorMessage = "Must be at lest 3 char "),MaxLength(100)]
        public string State { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [MinLength(3, ErrorMessage = "Must be at lest 3 char "), MaxLength(100)]
        public string Country { get; set; }

        [Required(ErrorMessage = "Postal code is required")]
        [MinLength(5, ErrorMessage = "Must be at lest 5 char "), MaxLength(20)]
        public string PostalCode { get; set; }


    }
}
