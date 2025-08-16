using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_commerce_Core.DTO.AccountDTO
{
    public class addRoleDto
    {
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }
    public class responsAllUserDTO
    {
        public string id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

    }
    public class getUserByIdDTO
    {
        public string id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

    }
    public class getuserroleDTO
    {
        [Required]
        public string Email { get; set; }
      
    }
    public class UpdateUserDTO
    {
        public string id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
