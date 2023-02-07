using Grpc.Core;
using IdentityService.BusinessLogic.Exceptions;
using IdentityService.BusinessLogic.Services.Abstarct;

namespace IdentityService.BusinessLogic.Services
{
    public class CheckUserService : CheckUser.CheckUserBase
    {
        private readonly IUserService _userService;

        public CheckUserService(IUserService userService)
        {
            _userService = userService;
        }

        public override async Task<ResponseMessage> Check(RequestEmail request, ServerCallContext context)
        {
            try
            {
                var user = await _userService.GetAsync(request.Email);

                return await Task.FromResult(new ResponseMessage()
                {
                    IsExists = true,
                });
            }
            catch (Exception ex)
            {
                if (ex is NotFoundException)
                {
                    return await Task.FromResult(new ResponseMessage()
                    {
                        IsExists = false,
                    });
                }

                throw;
            }
        }
    }
}
