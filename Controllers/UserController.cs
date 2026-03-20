using ECommerce_API.Models;
using ECommerce_API.Repository;
using ECommerce_API.Service;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IRepository<User> _repository;
        private readonly UserService _userService;
        public UserController(IRepository<User> repository, UserService userService)
        {
            _repository = repository;
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _repository.GetAllAsync();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrUpdateUser(User user)
        {
            if (user.Id == 0)
            {
                await _repository.AddAsync(user);
                return Ok();
            }
            else
            {
                await _repository.Update(user);
                return Ok();
            }            
        }


        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            //var result = await _repository.Delete();
            return Ok();
        }

        [HttpPost]
        [Route("/UploadImportFile")]
        public async Task<IActionResult> UploadDataImport(IFormFile importFeed)
        {
            var result = await _userService.ImportUserAsync(importFeed);
            return Ok(result);
        }
    }
}
