using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotesAPI.Contexts;
using NotesAPI.Dtos;
using NotesAPI.Models;

namespace NotesAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class NotesController
    {
        private readonly AppDbContext _context;

        public NotesController(AppDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Note>>> GetAllNotes()
        {
            return await _context.Notes.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Note>> CreateNote([FromBody] CreateNoteDto createNote)
        {
            Note note = new Note
            {
                Body = createNote.Body,
                Title = createNote.Title,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };
            
            _context.Notes.Add(note);

            await _context.SaveChangesAsync();

            return note;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Note>> UpdateNote(int id, [FromBody] CreateNoteDto createNote)
        {
            Note note = await _context.Notes.FindAsync(id);

            if (note == null)
                return new NotFoundResult();

            note.Body = createNote.Body;
            note.Title = createNote.Title;
            note.ModifiedAt = DateTime.Now;

            _context.Notes.Update(note);

            await _context.SaveChangesAsync();

            return note;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteNote(int id)
        {
            Note note = await _context.Notes.FindAsync(id);

            if (note == null)
                return new NotFoundResult();

            _context.Notes.Remove(note);

            await _context.SaveChangesAsync();

            return new OkResult();
        }
    }
}