﻿using ElevenNote.Models;
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
            var categoryService = new CategoryService(userId); //creating a category service for a particular user
            return categoryService;
        }

        //create
        public IHttpActionResult Post(CategoryCreate category)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var service = CreateCategoryService();
            if (!service.CreateCategory(category)) return InternalServerError();
            return Ok("Category was successfully created!");
        }


        //get all method
        //public IHttpActionResult Get()
        //{
        //    CategoryService categoryService = CreateCategoryService();
        //    var categories = categoryService.GetCategories();
        //    return Ok(categories);
        //}
    }
}