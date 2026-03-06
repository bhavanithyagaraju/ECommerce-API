using ECommerce_API.Models;
using ECommerce_API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IRepository<User> _repository;
        public UserController(IRepository<User> repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }
    }
}
