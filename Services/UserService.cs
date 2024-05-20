using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PassCodeManager.Classified.DBcontext;
using PassCodeManager.Classified.Entities;
using PassCodeManager.DTO.Enums;
using PassCodeManager.DTO.RequestObjects;
using PassCodeManager.Services.Abstract;

namespace PassCodeManager.Services
{
    public class UserService : IUserService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public UserService(Context context, IMapper mapper)
        {
            _context = context
                ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper
                ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> RegisterUserAsync(RegisterUserObject registerUser)
        {
            try
            {
                var existingEnty = await _context.Users.FirstOrDefaultAsync(x => x.Mobile == registerUser.Mobile || x.Email == registerUser.Email);

                if (existingEnty != null)
                {
                    if (!string.IsNullOrWhiteSpace(existingEnty.Mobile)) throw new Exception("Mobile Already Exist");
                    else throw new Exception("Email Already Exist");
                }

                // Implement Email.js intergration for the User Verification.

                //  At Present Users Will be Verified Automatically.
                // Passwords Need to be Ashed.

                var UserEntity = _mapper.Map<TblUsers>(registerUser);
                UserEntity.IsLockedOut = false;
                UserEntity.IsActive = true;
                UserEntity.RegistrationDate = DateTime.Now;
                UserEntity.VerifiedMediums = (int)VerifiedMediumsEnum.Email;

                await _context.AddAsync(UserEntity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public async Task<bool> ValidateUser(string UserName, string password)
        {
            try
            {
                bool isValid = await _context.Users.AnyAsync(x => x.UserName == UserName && x.Password == password); // At present Directly password is getting compared. Need to Make the code changes for the Ashing thing.
                return isValid;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
