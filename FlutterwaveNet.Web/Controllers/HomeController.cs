using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FlutterwaveNet.Web.Models;
using System.Web;
using Newtonsoft.Json;

namespace FlutterwaveNet.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {  
            Guid refId= Guid.NewGuid();
            FlutterWaveApi api = new FlutterWaveApi("Add Secret Key");
            TransactRequest request = new TransactRequest();
            request.currency = "NGN";
            request.amount = 5000;
            request.tx_ref = refId.ToString();
            request.redirect_url = "http://localhost:56933/home/Privacy";
            request.payment_options = "card";

            request.customer = new Customer()
            {
                email = "test@yahoo.com",
                name = "Ekene Duru",
                phonenumber = "08012345678"
            };
            request.customizations = new Customizations { title = "Test Payment", logo = "", description = "Middleout isn't free. Pay the price" };
            TransactionReponse resp= api.Initialize(request);
            if (resp.status == "success")
            {
                HttpContext.Response.Redirect(resp.data.link);

            }

            return View();
        }

        public IActionResult Privacy(string status, string tx_ref , string transaction_id)
        {
            var strRequest = Request.QueryString.ToString();
            var dict = HttpUtility.ParseQueryString(strRequest);
            var json = JsonConvert.SerializeObject(dict.AllKeys.ToDictionary(k => k, k => dict[k]));
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
