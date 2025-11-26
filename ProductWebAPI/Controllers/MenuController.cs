using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductWebAPI.Data;
using ProductWebAPI.DTOs;
using ProductWebAPI.Models;
using ProductWebAPI.Services;
using System.Security.Claims;

namespace ProductWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly StudentDbContext _context;
        private readonly IMenuService _service;

        public MenuController(StudentDbContext context, IMenuService service)
        {
            _context = context;
            _service = service;
        }

        [HttpGet("get-list-menus")]
        public async Task<IActionResult> GetAllMenus()
        {
            var menus = await _service.GetMenus().ToListAsync();
            return Ok(menus);
        }

        [HttpPost("create-menu")]
        public async Task<IActionResult> AddMenu([FromBody] Menu menu)
        {
            var newMenu = await _service.CreateMenuAsync(menu);
            if (newMenu == null)
                return Conflict("Failed to create menu");
            return Ok(new { success = true ,message = "Menu created successfully", data = newMenu });
        }

        // ADMIN: Create Menu
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateMenu(MenuDTO dto)
        {
            var menu = new Menu
            {
                Title = dto.Title,
                Url = dto.Url,
                Icon = dto.Icon,
                Order = dto.Order,
                AllowedRoles = string.Join(",", dto.Roles)
            };

            _context.Menus.Add(menu);
            await _context.SaveChangesAsync();
            return Ok(menu);
        }

        // PUBLIC: Get Menus by Role
        [HttpGet("{role}")]
        public async Task<IActionResult> GetMenusByRole(string role)
        {
            var menus = await _context.Menus
                .Where(m => m.AllowedRoles.Contains(role))
                .OrderBy(m => m.Order)
                .ToListAsync();

            return Ok(menus);
        }

        // Get by user
        [HttpGet("get-by-user")]
        public async Task<IActionResult> GetMenusForUser()
        {
            var role = User.FindFirst(ClaimTypes.Role)?.Value;
            if (string.IsNullOrEmpty(role)) return Unauthorized(new { message = "Role not found in token" });
            // filter menus by allowed roles
            var menus = await _context.Menus
                .Where(m => m.AllowedRoles.Contains(role))
                .OrderBy(m => m.Order)
                .ToListAsync();
            return Ok(menus);
        }
    }
}
