namespace Capstone.Security
{
    public interface ITokenGenerator
    {
        /// <summary>
        /// Generates a new authentication token.
        /// </summary>
        /// <param name="username">The user's username</param>
        /// <returns></returns>
        string GenerateToken(int userId, string username);

        /// <summary>
        /// Generates a new authentication token.
        /// </summary>
        /// <param name="username">The user's username</param>
        /// <param name="role">The user's role</param>
        /// <returns></returns>
        string GenerateToken(int userId, string username, string role);
    }
}
