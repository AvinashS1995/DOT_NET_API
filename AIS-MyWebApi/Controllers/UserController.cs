using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AIS_MyWebApi.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace AIS_MyWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public readonly IUserService _userService;
        public readonly ILogger<UserController> _logger;
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        } 
    }
}