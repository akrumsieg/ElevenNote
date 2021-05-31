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
    public class CategoryController : ApiController
    {

        //A controller talks to our API

        private CategoryService CreateCategoryService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId()); //STUDY!
            var service = new CategoryService(userId); //creating a category service for a particular user
            return service;
        }

        //create
        public IHttpActionResult Post(CategoryCreate model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateCategoryService();
            if (!service.CreateCategory(model)) return InternalServerError();
            return Ok("Category was successfully created!");
        }


        //read - get all method
        public IHttpActionResult Get()
        {
            var service = CreateCategoryService();
            var categories = service.GetCategories();
            return Ok(categories);
        }

        //update
        public IHttpActionResult Put(CategoryEdit model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateCategoryService();
            if (!service.UpdateCategory(model)) return InternalServerError();
            return Ok("Category was successfully updated!");
        }

        //delete
        public IHttpActionResult Delete(int id)
        {
            var service = CreateCategoryService();
            if (!service.DeleteCategory(id)) return InternalServerError();
            return Ok("Category was successfully deleted!");
        }
    }
}
