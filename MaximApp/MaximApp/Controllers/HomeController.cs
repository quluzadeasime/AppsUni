
namespace MaximApp.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext _dbContext;

		public HomeController(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public IActionResult Index()
        {
            var service = _dbContext.Services.ToList();
            return View(service);
        }

    }
}
