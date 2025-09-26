using LibraryProject.Api.DTOs;
using LibraryProject.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace LibraryProject.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
    private readonly AuthorService _service;
    public AuthorController(AuthorService service) => _service = service;
    
    [HttpGet]
    [Produces("application/json")]
    public async Task<ActionResult<List<AuthorDTO>>> GetAll()
        => await _service.GetAllAsync();

    [HttpPost]
    [Produces("application/json")] 
    [ProducesResponseType(typeof(AuthorDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthorDTO>> Create(CreateAuthorDTO dto)
    {
        var author = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = author.Id }, author);
    }

    [HttpGet("{id}")]
    [Produces("application/json")]
    public async Task<ActionResult<AuthorDTO>> GetById(string id)
    {
        var author = await _service.GetByIdAsync(id);
        if (author == null) return NotFound();
        return Ok(author);
    }
    
    [HttpPut("{id}")]
    [Produces("application/json")]
    public async Task<ActionResult<AuthorDTO>> Update(string id, CreateAuthorDTO dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

}