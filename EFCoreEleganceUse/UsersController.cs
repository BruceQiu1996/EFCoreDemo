using EFCoreEleganceUse.Domain.Entities;
using EFCoreEleganceUse.Domain.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EFCoreEleganceUse
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        //生产中可以在应用层下创建service层,聚合各种实体仓储
        private readonly IAsyncRepository<User, string> _userAsyncRepository;

        public UsersController(ILogger<UsersController> logger, IAsyncRepository<User, string> userAsyncRepository)
        {
            _logger = logger;
            _userAsyncRepository = userAsyncRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Get()
        {
            return Ok(await _userAsyncRepository.All().ToListAsync());
        }
    }
}
