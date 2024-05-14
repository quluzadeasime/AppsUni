using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Permissions;

namespace MaximApp.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ServiceController : Controller
	{
		AppDbContext _context;
		private readonly IWebHostEnvironment _environment;

        public ServiceController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult Index()
		{
			var services = _context.Services.ToList();
			return View(services);
		}
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(Service service)
		{
			if (!service.ImgFile.ContentType.Contains("image/"))
			{
				ModelState.AddModelError("ImgFile", "Invalid input");
				return View();
			}
			if (service.ImgFile.Length > 2097152)
			{
				ModelState.AddModelError("ImgFile", "Max uzunluq 2 mb ola biler!");
				return View();
			}
			string path = @"C:\Users\II novbe\Desktop\Apps\MaximApp\MaximApp\wwwroot\upload\";
			//string filename = Guid.NewGuid() + service.ImgFile.FileName;
			string filename = service.ImgFile.FileName;
			using(FileStream stream = new FileStream(path + filename, FileMode.Create))
			{
				service.ImgFile.CopyTo(stream);
			}
			if(!ModelState.IsValid)
			{
				return View();
			}
			Service service1 = new Service()
			{
				Title = service.Title,
				ImgFile = service.ImgFile,
				ImgUrl = filename,
				Description = service.Description,
			};
			 _context.Services.Add(service1);
			 _context.SaveChanges();
			return RedirectToAction("Index");
		}

		public IActionResult Delete(int id)
		{
			var service=_context.Services.FirstOrDefault(x => x.Id == id);	
			if(service != null)
			{
				string path = _environment.WebRootPath + @"\upload\" + service.ImgUrl;
				FileInfo fileInfo = new FileInfo(path);
				if (fileInfo.Exists)
				{
					fileInfo.Delete();
				}
				_context.Services.Remove(service);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
			return RedirectToAction(nameof(Index));
        }
		public IActionResult Update(int id)
		{
			var service = _context.Services.FirstOrDefault(x=>x.Id == id);
			Service newService = new Service()
			{
				Id= service.Id,
				Title = service.Title,
				ImgFile = service.ImgFile,
				Description = service.Description,
				ImgUrl = service.ImgUrl

			};
			if(service != null)
			{

				return View(newService);
            }
			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		public IActionResult Update(Service service)
		{
			if(!ModelState.IsValid)
			{
				return View();
			}
			var oldservice = _context.Services.FirstOrDefault(x => x.Id == service.Id);
			if(oldservice != null) { return RedirectToAction("Index"); }
			oldservice.Title = service.Title;
			oldservice.Description = service.Description;
			oldservice.ImgUrl = service.ImgUrl;
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
		
	}
}
