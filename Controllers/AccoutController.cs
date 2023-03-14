using System.Net;
using FindMyPet.Business;
using FindMyPet.Models;
using FindMyPet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FindMyPet.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    private readonly AccoutBusiness _accoutBusiness;
    private readonly MessageRequest _returnHttp;


    public AccountController(AccoutBusiness accoutBusiness, MessageRequest ob)
    {
        _accoutBusiness = accoutBusiness;
        _returnHttp = ob;
    }

    [HttpPost("v1/authenticate")]
    public async Task<IActionResult> Login([FromServices] TokenService tokenService, [FromBody] UserData credentials)
    {
        try
        {
            UserAuthenticate authenticate = await _accoutBusiness.Authentication(credentials.user, credentials.pass);

            if (authenticate != null)
            {
                TokenBody token = await _accoutBusiness.GenerateToken(tokenService, authenticate);

                return Ok(token);
            }
            else
            {
                ReturnSanitize result = await _returnHttp.GetStatus("Credenciais inválidas!", 401);

                return BadRequest(result);
            }
        }
        catch (Exception)
        {
            ReturnSanitize result = await _returnHttp.GetStatus("Ops.. aconteceu algum problema, tente novamente mais tarde!", 400);

            return BadRequest(result);
        }
    }

    [Authorize(Roles = "admin")]
    [HttpPost("v1/create")]
    public async Task<IActionResult> Create(CreatedUser user)
    {
        try
        {
            var result = await _accoutBusiness.Created(user);

            return CreatedAtAction("Usuário criado com sucesso!", 201);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }

    }
}