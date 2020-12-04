namespace Capstone.Security.Models
{
    /// <summary>
    /// Represents a hashed password.
    /// </summary>
    public class PasswordHash
    {
        /// <summary>
        /// Creates a new hashed password.
        /// </summary>
        /// <param name="password">The hashed password</param>
        /// <param name="salt">The salt used to get the hashed password.</param>
        public PasswordHash(string password, string salt)
        {
            this.Password = password;
            this.Salt = salt;
        }

        /// <summary>
        /// The hashed password
        /// </summary>
        public string Password { get; }

        /// <summary>
        /// The salt used to get the hashed password.
        /// </summary>
        public string Salt { get; }

    }
}
