using InvoiceAPI.BP;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using InvoiceAPI.BP.Interface;
using InvoiceAPI.DataAccess.Models;

namespace InvoiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public ActionResult<List<Category>> GetAll()
        {
            return Ok(_categoryService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Category> GetCategory(int id)
        {
            var category = _categoryService.Get(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        public ActionResult AddCategory([FromBody]Category category)
        {
            _categoryService.Add(category);
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCategory(int id, [FromBody]Category category)
        {
            if (_categoryService.Get(id) == null)
                return NotFound();

            _categoryService.Update(id, category);
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCategory(int id)
        {
            if (_categoryService.Get(id) == null)
                return NotFound();

            _categoryService.Delete(id);
            return Ok();
        }

    }
}
