using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLIENT.Controllers
{
    public class EducationsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
