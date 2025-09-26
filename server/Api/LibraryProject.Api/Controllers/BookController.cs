using LibraryProject.Api.DTOs;
using LibraryProject.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace LibraryProject.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly BookService _service;
    public BookController(BookService service) => _service = service;
    
    [HttpGet]
    [Produces("application/json")]
    public async Task<ActionResult<List<BookDTO>>> GetAll()
        => await _service.GetAllAsync();

    [HttpPost]
    [Produces("application/json")] 
    [ProducesResponseType(typeof(BookDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BookDTO>> Create(CreateBookDTO dto)
    {
        var book = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
    }

    [HttpGet("{id}")]
    [Produces("application/json")]
    public async Task<ActionResult<BookDTO>> GetById(string id)
    {
        var book = await _service.GetByIdAsync(id);
        if (book == null) return NotFound();
        return Ok(book);
    }
    
    [HttpPut("{id}")]
    [Produces("application/json")]
    public async Task<ActionResult<BookDTO>> Update(string id, CreateBookDTO dto)
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