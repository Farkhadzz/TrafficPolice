using System.Net;
using System.Runtime.CompilerServices;

namespace TrafficPolice.Controllers.Base;

public class ControllerBase
{
    public HttpListenerContext HttpContext;

    public string View([CallerMemberName] string MethodName = "") {
        var controllerName = this.GetType().Name[..(this.GetType().Name.LastIndexOf("Controller"))];

        return File.ReadAllText($"{controllerName}/{MethodName}.html");
    }
}