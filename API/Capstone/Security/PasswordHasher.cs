using Capstone.Security.Models;
using System;
using System.Security.Cryptography;

namespace Capstone.Security
{
    /// <summary>
    /// The hash provider provides functionality to hash a plain text password and validate
    /// an existing password against its hash.
    /// </summary>
    public class PasswordHasher : IPasswordHasher
    {
        private const int WorkFactor = 10000;

        /// <summary>
        /// Hashes a plain text password.
        /// </summary>
        /// <param name="plainTextPassword"></param>
        /// <returns></returns>
        public PasswordHash ComputeHash(string plainTextPassword)
        {
            //Create the hashing provider
            Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(plainTextPassword, 8, WorkFactor);

            //Get the Hashed Password
            byte[] hash = rfc.GetBytes(20);

            //Set the SaltValue
            string salt = Convert.ToBase64String(rfc.Salt);

            //Return the Hashed Password
            return new PasswordHash(Convert.ToBase64String(hash), salt);
        }

        /// <summary>
        /// Verifies if an existing hashed password matches a plaintext password (+salt).
        /// </summary>
        /// <param name="existingHashedPassword">The password we are comparing to.</param>
        /// <param name="plainTextPassword">The plaintext password being validated.</param>
        /// <param name="salt">The salt used to get the existing hashed password.</param>
        /// <returns></returns>
        public bool VerifyHashMatch(string existingHashedPassword, string plainTextPassword, string salt)
        {
            byte[] saltArray = Convert.FromBase64String(salt);      //gets us the byte[] array representation

            //Create the hashing provider
            Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(plainTextPassword, saltArray, WorkFactor);

            //Get the hashed password
            byte[] hash = rfc.GetBytes(20);

            //Compare the hashed password values
            string newHashedPassword = Convert.ToBase64String(hash);

            return (existingHashedPassword == newHashedPassword);
        }
    }
}
