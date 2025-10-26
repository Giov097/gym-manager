using GymManager_BE;
using GymManager_BLL.Exceptions;
using GymManager_MPP;

namespace GymManager_BLL.Impl;

public class XmlUserService : IUserService
{
    private readonly XmlUserMapper _mapper = new("data.xml");

    public Task<User> Login(User userInput)
    {
        return _mapper.GetByEmail(userInput.Email).ContinueWith(user =>
                    user.Result ?? throw new UserNotFoundException())
                .ContinueWith(user =>
                    !userInput.Password!.Equals(user.Result.Password)
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
            existingUser.Result.UserRoles = user.UserRoles;
            if (!user.Password!.Equals(existingUser.Result.Password))
            {
                existingUser.Result.Password = user.Password;
            }

            return _mapper.Update(existingUser.Result);
        });
    }

    public Task<User> GetUserByFeeId(long feeId)
    {
        return _mapper.GetByFeeId(feeId).ContinueWith(user =>
            user.Result ?? throw new UserNotFoundException());
    }
}