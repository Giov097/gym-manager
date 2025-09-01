using System.Security.Cryptography;
using System.Text;
using GymManager_BE;
using GymManager_BLL.Exceptions;
using GymManager_MPP;

namespace GymManager_BLL.Impl;

public class UserService : IUserService
{
    private readonly UserMapper _mapper = new();

    private const string Key = "f41lc7epz45q15n9";

    public Task<User> Login(string email, string password)
    {
        return _mapper.GetByEmail(email).ContinueWith(user =>
                    user.Result ?? throw new UserNotFoundException())
                .ContinueWith(user =>
                    !EncryptString(password).Equals(user.Result.Password)
                        ? throw new InvalidCredentialsException("Usuario o contraseña inválidos")
                        : user.Result)
            ;
    }

    public Task<User> GetUserByEmail(string email)
    {
        return _mapper.GetByEmail(email).ContinueWith(user =>
            user.Result ?? throw new UserNotFoundException());
    }

    public Task<User> CreateUser(User user)
    {
        var encryptedPassword = EncryptString(user.Password);
        user.Password = encryptedPassword;
        return _mapper.Create(user);
    }

    public Task<User> GetUserById(long id)
    {
        return _mapper.GetById(id)
            .ContinueWith(user =>
                user.Result ?? throw new UserNotFoundException());
    }

    public Task<List<User>> GetUsers()
    {
        return _mapper.GetAll();
    }

    public Task UpdateUser(long id, User user)
    {
        return _mapper.GetById(id).ContinueWith(existingUser =>
        {
            if (existingUser.Result == null)
            {
                throw new UserNotFoundException();
            }

            existingUser.Result.Email = user.Email;
            existingUser.Result.FirstName = user.FirstName;
            existingUser.Result.LastName = user.LastName;
            if (!user.Password.Equals(existingUser.Result.Password))
            {
                existingUser.Result.Password = EncryptString(user.Password);
            }

            return _mapper.Update(existingUser.Result);
        });
    }

    private static string EncryptString(string plainText)
    {
        var iv = new byte[16];
        byte[] array;

        using (var aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(Key);
            aes.IV = iv;

            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using MemoryStream memoryStream = new();
            using CryptoStream cryptoStream =
                new(memoryStream, encryptor, CryptoStreamMode.Write);
            using (StreamWriter streamWriter = new(cryptoStream))
            {
                streamWriter.Write(plainText);
            }

            array = memoryStream.ToArray();
        }

        return Convert.ToBase64String(array);
    }

    // No se usa, pero lo dejé como referencia
    private static string DecryptString(string cipherText)
    {
        var iv = new byte[16];
        var buffer = Convert.FromBase64String(cipherText);

        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(Key);
        aes.IV = iv;
        var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using MemoryStream memoryStream = new(buffer);
        using CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Read);
        using StreamReader streamReader = new(cryptoStream);
        return streamReader.ReadToEnd();
    }
}