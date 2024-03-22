
using Microsoft.AspNetCore.Mvc;

namespace UR.CoursePlannerBFF.UserService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController
    {
        private readonly IUserApiService _userApiService;

        public UserController(IUserApiService userApiService)
        {
            _userApiService = userApiService;
        }

        [HttpPost]
        public async Task<ActionResult<UserController>> CreateUser([FromBody]UserController user)
        {
            throw new NotImplementedException("Not Implemented");
        }

    }
}
