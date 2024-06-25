using System.Security.Claims;
using Honey.Domain.Dto.User;
using Honey.Domain.Dto.User.Update;
using Honey.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Honey.Controllers
{
    /// <summary>
    /// Контроллер для взаимодействия с пользователем
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("/api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        /// <summary>
        /// Коструктор
        /// </summary>
        /// <param name="userService"></param>
        public UserController
        (
            IUserService userService
        )
        {
            _userService = userService;
        }

        /// <summary>
        /// Редактировать информацию о пользователе
        /// </summary>
        /// <param name="user"></param>
        /// <response code="200">Return user Id and token</response>
        /// <response code="400">If new UserName is already exists or age is not valid</response>
        /// <response code="404">If user with UserName doesn't exist</response>
        [HttpPut("[action]")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserUpdateResponseDto>> Edit(UserUpdateRequestDto user)
        {
            var result = await _userService.Edit(user);

            return Ok(result);
        }


        /// <summary>
        /// Получить пользователя по ID
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Return user</response>
        /// <response code="404">If user with Id doesn't exist</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserModelResponseDto>> GetById(Guid id)
        {
            var result = await _userService.GetById(id);

            return Ok(result);
        }

        /// <summary>
        /// Найти пользователя по никнейму
        /// </summary>
        /// <param name="name"></param>
        /// <response code="200">Return user</response>
        /// <response code="404">If user with UserName doesn't exist</response>
        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserModelResponseDto>> GetByUserName(string name)
        {
            var result = await _userService.GetByUserName(name);

            return Ok(result);
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200">Return user</response>
        /// <response code="404">If user doesn't exist</response>
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<UserModelResponseDto>> Delete(Guid id)
        {
            var userId = GetCurrentUserId();

            if (userId != id)
            {
                return BadRequest("Вам недоступна эта операция");
            }
            
            var result = await _userService.Delete(id);

            return Ok(result);
        }
        
        private Guid GetCurrentUserId()
        {
            var test = HttpContext.User.FindFirst(p => p.Type == ClaimTypes.Name)?.Value;

            Guid.TryParse(test, out var userId);

            return userId;
        }
        
    }
}