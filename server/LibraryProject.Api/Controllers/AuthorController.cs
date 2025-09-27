using LibraryProject.Api.DTOs;
using LibraryProject.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryProject.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorController : ControllerBase
{
    private readonly AuthorService _service;

    public AuthorController(AuthorService service) => _service = service;

    [HttpGet]
    [Produces("application/json")]
    public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAll()
    {
        var authors = await _service.GetAllAsync();
        return Ok(authors);
    }

    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(AuthorDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthorDto>> Create(CreateAuthorDto dto)
    {
        try
        {
            var id = await _service.CreateAsync(dto);
            var author = await _service.GetByIdAsync(id);
            if (author is null)
            {
                return Problem(detail: "Author could not be retrieved after creation.", statusCode: StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction(nameof(GetById), new { id }, author);
        }
        catch (ArgumentException ex)
        {
            return ValidationProblem(ex.Message);
        }
    }

    [HttpGet("{id}")]
    [Produces("application/json")]
    public async Task<ActionResult<AuthorDto>> GetById(string id)
    {
        var author = await _service.GetByIdAsync(id);
        if (author is null)
        {
            return NotFound();
        }

        return Ok(author);
    }

    [HttpPut("{id}")]
    [Produces("application/json")]
    public async Task<ActionResult<AuthorDto>> Update(string id, CreateAuthorDto dto)
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
