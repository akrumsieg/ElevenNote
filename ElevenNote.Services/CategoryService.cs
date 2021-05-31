using ElevenNote.Data;
using ElevenNote.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Services
{
    public class CategoryService
    {
        //field
        private readonly Guid _userId;

        //constructor
        public CategoryService(Guid userId)
        {
            _userId = userId;
        }

        //methods
        //create
        public bool CreateCategory(CategoryCreate model)
        {
            var entity = new Category()
            {
                OwnerId = _userId,
                Name = model.Name
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Categories.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        //read - get all
        public IEnumerable<CategoryListItem> GetCategories()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx.Categories
                    .Where(e => e.OwnerId == _userId)
                    .Select(e => new CategoryListItem
                    {
                        CategoryId = e.CategoryId,
                        Name = e.Name,
                        NumOfNotes = e.Notes.Count()
                    }
                );
                return query.ToArray();
            }
        }

        //update
        public bool UpdateCategory(CategoryEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Categories.Single
                    (e => e.CategoryId == model.CategoryId && e.OwnerId == _userId);
                entity.Name = model.Name;

                return ctx.SaveChanges() == 1;
            }
        }

        //delete
        public bool DeleteCategory(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Categories.Single
                    (e => e.CategoryId == id && e.OwnerId == _userId);
                ctx.Categories.Remove(entity);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}
