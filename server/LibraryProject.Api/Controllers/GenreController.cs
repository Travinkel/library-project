using LibraryProject.Api.DTOs;
using LibraryProject.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace LibraryProject.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GenreController : ControllerBase
{
    private readonly GenreService _service;
    public GenreController(GenreService service) => _service = service;
    
    [HttpGet]
    [Produces("application/json")]
    public async Task<ActionResult<List<GenreDto>>> GetAll()
        => await _service.GetAllAsync();

    [HttpPost]
    [Produces("application/json")] 
    [ProducesResponseType(typeof(GenreDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<GenreDto>> Create(CreateGenreDto dto)
    {
        var genre = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = genre.Id }, genre);
    }

    [HttpGet("{id}")]
    [Produces("application/json")]
    public async Task<ActionResult<GenreDto>> GetById(string id)
    {
        var genre = await _service.GetByIdAsync(id);
        if (genre == null) return NotFound();
        return Ok(genre);
    }
    
    [HttpPut("{id}")]
    [Produces("application/json")]
    public async Task<ActionResult<GenreDto>> Update(string id, CreateGenreDto dto)
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