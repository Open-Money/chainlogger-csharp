using System;
using System.Security.Cryptography;
using System.Text;

namespace Om.Chainlogger
{
    /// <summary>
    /// A class to hash the values, add salt and verify with given values.
    /// ...
    ///    Attributes
    ///    ----------
    ///    salt : str
    ///        a salt value to randomize the hash
    ///    Methods
    ///    -------
    ///    create(string)
    ///    add0x(string)
    ///    hash_with_salt(string)
    ///    verify_input(string, string)
    /// </summary>
    class Hasher
    {
        private string salt;

        /// <summary>
        /// Initialize the given class
        /// </summary>
        /// <param name="salt"></param>
        public Hasher(string salt)
        {
            this.salt = salt;
        }

        /// <summary>
        /// Returns the initialized class.
        /// </summary>
        /// <param name="salt"></param>
        /// <returns>Hasher (class): A hasher class</returns>
        public static Hasher Create(string salt)
        {
            return new Hasher(salt);
        }

        /// <summary>
        /// Returns the "0x" added version of the input.
        /// </summary>
        /// <param name="data"></param>
        /// <returns>value (string): "0x" merged with a data</returns>
        public static string Add0x(string data)
        {
            return "0x" + data;
        }

        /// <summary>
        /// Returns the hashed with sha256 version of the data.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>value (string): hashed and salted version of the data</returns>
        public string HashWithSalt(string input)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                //From String to byte array
                byte[] sourceBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256Hash.ComputeHash(sourceBytes);
                string hash = BitConverter.ToString(hashBytes).Replace("-", String.Empty);

                return hash;
            }
        }

        /// <summary>
        /// Takes the raw input and hashed_input, and makes a comparison.
        /// </summary>
        /// <param name="hashedInput"></param>
        /// <param name="rawData"></param>
        /// <returns>value (boolean): are they the same or not</returns>
        public bool VerifyInput(string hashedInput, object rawData)
        {
            if (hashedInput == Hasher.Add0x(HashWithSalt(Parser.JsonEncode(rawData))))
            {
                return true;
            }
            return false;
        }
    }
}
