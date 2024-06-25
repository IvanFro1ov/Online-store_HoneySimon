using Honey.Common.Base;

namespace Honey.DB.Entities
{
    /// <summary>
    /// Модель кода восстановления в БД
    /// </summary>
    public class RestoringCodeEntity : BaseModel
    {
        /// <summary>
        /// Код восстановления
        /// </summary>
        public string Code { get; set; }
        
        /// <summary>
        /// Время истечения
        /// </summary>
        public long Expiration { get; set; }
        
        /// <summary>
        /// Электронная почта, по которой отправлен код
        /// </summary>
        public string Email { get; set; }
        
        /// <summary>
        /// Использован ли
        /// </summary>
        public bool IsUsed { get; set; }
    }
}