using Microsoft.AspNetCore.Mvc;

namespace OdeToFood.Controllers
{
    // Attribute based routing
    // generic controller and action mean you must specify the controller and action names
    [Route("[controller]/[action]")]

    // Could also use to specify url further
    // [Route("company/[controller]/[action")] 

    public class AboutController : Controller
    {
        ///This means you can get to phone by default SO LONG AS some other method is explicit
        ///unnecessary when /action is in Route on Controller
        //[Route("")]
        public string Phone()
        {
            return "1+555-555-5555";
        }

        ///This means you MUST have /address in the url to get this
        ///unnecessary when /action is in Route on Controller
        //[Route("address")]
        public string Address()
        {
            return "USA";
        }
    }
}
