using Microsoft.AspNetCore.Mvc; // For building controllers and handling HTTP requests

namespace API.Controllers;

// BaseApiController acts as the base class for other API controllers
// [ApiController] attribute indicates that this is an API controller
[ApiController]

// [Route("api/[controller]")] sets the base route for derived controllers
// [controller] is replaced by the name of the derived controller (e.g., AccountController -> api/account)
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
  // This class does not contain any methods or properties
  // It serves as a base class to centralize common behaviors or attributes
  // All controllers inheriting from this class will share the route and attributes defined here
}

