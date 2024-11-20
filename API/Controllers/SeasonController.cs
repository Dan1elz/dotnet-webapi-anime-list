using Microsoft.AspNetCore.Mvc;

namespace dotnet_anime_list.API.Controllers
{
    public class SeasonController : Controller
    {
        public IActionResult Index()
        {
            return View();


            //rotas para criar uma nova temporada, atualizar essa temporada, deletar essa temporada.

        }
    }
}
