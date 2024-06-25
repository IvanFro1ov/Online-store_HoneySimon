using Honey.Common.Base;
using Honey.Common.Enums.Roles;

namespace Honey.DB.Entities
{
    /// <summary>
    /// Модель Пользователя в БД
    /// </summary>
    public class UserEntity : BaseModel
    {
        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string Firstname { get; set; }
        
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Отчество пользователя
        /// </summary>
        public string Lastname { get; set; }
        
        /// <summary>
        /// Возраст пользователя
        /// </summary>
        public int Age { get; set; }
        
        /// <summary>
        /// Электронная почта пользователя
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Пароль пользователя в виде Хеш-кода
        /// </summary>
        public string Password { get; set; }
        
        /// <summary>
        /// Никнейм-пользователя
        /// </summary>
        public string UserName { get; set; }
        
        /// <summary>
        /// Роль пользователя в системе
        /// </summary>
        public UserRoles Role { get; set; }
    }
}