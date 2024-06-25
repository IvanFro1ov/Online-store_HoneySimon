namespace Honey.BL.Authentication.Hashing
{
    /// <summary>
    /// Интерфейс для взаимодействия с паролями
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// Захешировать пароль
        /// </summary>
        /// <param name="password">Пароль пользователя</param>
        /// <returns></returns>
        string HashPassword(string password);
        
        /// <summary>
        /// Сравнить пароль
        /// </summary>
        /// <param name="providedPassword">Пароль для сравнения</param>
        /// <param name="passwordHash">Захешированный пароль</param>
        /// <returns></returns>
        bool PasswordMatches(string providedPassword, string passwordHash);
    }
}