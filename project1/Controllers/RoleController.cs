using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using project1.Data;

namespace project1.Controllers
{
    public class RoleController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(ApplicationDbContext context,RoleManager<IdentityRole> roleManager)
        {
            this._context = context;
            this._roleManager = roleManager;
        }

        

        public IActionResult Index()
        {
            return View();
        }
    }
}
