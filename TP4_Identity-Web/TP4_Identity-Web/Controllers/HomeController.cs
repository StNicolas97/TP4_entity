using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using TP4_Identity_Web.Data;
using TP4_Identity_Web.Models;
using TP4_Identity_Web.ViewModels.Home;

namespace TP4_Identity_Web.Controllers
{
    public class HomeController(UserManager<ApplicationUser> userManager) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var vm = new AccueilVM
            {
                EstConnecte = User.Identity?.IsAuthenticated ?? false
            };

            if (vm.EstConnecte)
            {
                var user = await userManager.GetUserAsync(User);
                vm.Surnom = user?.Surnom;

                // Récupérer le claim "Peuple"
                vm.Peuple = User.FindFirstValue("Peuple");

                // Récupérer le rôle
                if (User.IsInRole("Roi")) vm.Role = "Roi";
                else if (User.IsInRole("Capitaine")) vm.Role = "Capitaine";
                else if (User.IsInRole("Habitant")) vm.Role = "Habitant";
            }

            return View(vm);
        }
    }
}
