using System.Net.Http.Json;
using Chattoo.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Chattoo.GraphQL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICurrentUserIdService _currentUserIdService;
            
        public HomeController(ICurrentUserIdService currentUserIdService)
        {
            _currentUserIdService = currentUserIdService;
        }

        public ActionResult Index()
        {
            var x = this.Request;
            var id = _currentUserIdService.UserId;
            return new OkResult();
        }
    }
}