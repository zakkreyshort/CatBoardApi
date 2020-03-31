using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CatBoardApi.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace CatBoardApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AuthenticationController : ControllerBase
  {

    private readonly IAuthenticateService _authService;

    public AuthenticationController(IAuthenticateService authService)
    {
      _authService = authService;
    }

    [AllowAnonymous]
    [HttpPost]
    public ActionResult RequestToken([FromBody] TokenRequest request)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest("Invalid Request");
      }

      string token;
      
      if (_authService.IsAuthenticated(request, out token))
      {
        return Ok(token);
      }

      return BadRequest("Invalid Request");

    }
  }

}