﻿using Microsoft.AspNetCore.Mvc;

namespace University.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
