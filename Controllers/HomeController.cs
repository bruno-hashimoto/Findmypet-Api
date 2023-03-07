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

    public HomeController(ApplicationBusiness app)
    {
        _applicationBusiness = app;
    }

    [Authorize(Roles = "admin")]
    [HttpPost("v1/send")]
    public async Task<IActionResult> Post([FromBody] Post newPost)
    {

        bool save = await _applicationBusiness.newPost(newPost);

        if(save)
        {
            return Ok("Post adicionado com sucesso.");
        }
        else
        {
            return BadRequest("Não foi possível criar o post");
        }
    }

    [Authorize(Roles = "admin")]
    [HttpGet("v1/feed/{latitude}/{longitude}")]
    public async Task<IActionResult> Feed(double latitude, double longitude)
    {
        return Ok(await _applicationBusiness.requestFeed(latitude, longitude));
    }
}