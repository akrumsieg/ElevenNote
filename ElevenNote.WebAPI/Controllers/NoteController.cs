using ElevenNote.Models;
using ElevenNote.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ElevenNote.WebAPI.Controllers
{
    [Authorize]
    public class NoteController : ApiController
    {
        private NoteService CreateNoteService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var noteService = new NoteService(userId);
            return noteService;
        }

        //get all method
        public IHttpActionResult Get()
        {
            NoteService noteService = CreateNoteService();
            var notes = noteService.GetNotes();
            return Ok(notes);
        }

        //get by id
        public IHttpActionResult Get(int id)
        {
            NoteService service = CreateNoteService();
            var note = service.GetNoteById(id);
            return Ok(note);
        }

        //get starred or not-starred
        [Route("api/Note/{starredOrNot:bool}")]
        public IHttpActionResult Get(bool starredOrNot)
        {
            NoteService service = CreateNoteService();
            var notes = service.GetStarredNotes(starredOrNot);
            return Ok(notes);
        }

        //create
        public IHttpActionResult Post(NoteCreate note)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateNoteService();
            if (!service.CreateNote(note)) return InternalServerError();
            return Ok("Note creation successful!");
        }

        //update
        public IHttpActionResult Put(NoteEdit note)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateNoteService();
            if (!service.UpdateNote(note)) return InternalServerError();
            return Ok("Update successful!");
        }

        public IHttpActionResult PutToggleStar(int id)
        {
            NoteService service = CreateNoteService();
            if (!service.ToggleStar(id)) return InternalServerError();
            return Ok("Star toggled!");
        }

        //delete
        public IHttpActionResult Delete(int id)
        {
            var service = CreateNoteService();
            if (!service.DeleteNote(id)) return InternalServerError();
            return Ok("Delete successful!");
        }
    }
}
