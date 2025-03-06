using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AIS_MyWebApi.Models
{
    public class CreateUserModelRequest
    {
       public int UserId { get; set; }
       public string FirstName { get; set; }
       public string LastName { get; set; }
       public string Email { get; set; }
       public string Mobile { get; set; }
       public bool IsActive { get; set; }
    }

     public class CreateUserModelResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}