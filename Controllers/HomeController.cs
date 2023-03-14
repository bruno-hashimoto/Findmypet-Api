using FindMyPet.Business;
using FindMyPet.Models;
using FindMyPet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FindMyPet.Controllers;

[ApiController]
public class HomeController : ControllerBase
{
 
    private readonly ApplicationBusiness _applicationBusiness;
    private readonly MessageRequest _returnHttp;

    public HomeController(ApplicationBusiness app, MessageRequest ob)
    {
        _applicationBusiness = app;
        _returnHttp = ob;
    }

    [Authorize(Roles = "admin")]
    [HttpPost("v1/send")]
    public async Task<IActionResult> Post([FromBody] Post newPost)
    {

        bool save = await _applicationBusiness.newPost(newPost);

        if(save)
        {
            ReturnSanitize message = await _returnHttp.GetStatus("Post adicionado com sucesso.", 200);
            return Ok(message);
        }
        else
        {
            ReturnSanitize message = await _returnHttp.GetStatus("Não foi possível criar o post", 400);
            return BadRequest(message);
        }
    }

    [Authorize(Roles = "admin")]
    [HttpGet("v1/feed/{latitude}/{longitude}")]
    public async Task<IActionResult> Feed(double latitude, double longitude)
    {
        return Ok(await _applicationBusiness.requestFeed(latitude, longitude));
    }
}