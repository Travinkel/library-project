using LibraryProject.Api.DTOs;
using LibraryProject.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        try
        {
            var genre = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = genre.Id }, genre);
        }
        catch (ArgumentException ex)
        {
            return ValidationProblem(ex.Message);
        }
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
        try
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (updated is null)
            {
                return NotFound();
            }

            return Ok(updated);
        }
        catch (ArgumentException ex)
        {
            return ValidationProblem(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
