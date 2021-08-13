using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiCRUDDemo.DTO
{
    public class UserForLoginDTO
    {
        [Required]
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
