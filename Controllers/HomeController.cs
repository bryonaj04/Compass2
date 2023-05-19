using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Compass.Models;

namespace Compass.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly PersonController _personController;
    private readonly RoomController _roomController;


    public HomeController(ILogger<HomeController> logger, PersonController personController, RoomController roomController)
    {
        _logger = logger;
        _personController = personController;
        _roomController = roomController;
    }

    public async Task<IActionResult> Index()
    {
        var people = await _personController.GetPeople();
        var rooms = await _roomController.GetRooms();

        var viewModel = new SchedulerViewModel
        {
            People = people,
            Rooms = rooms
        };

        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}