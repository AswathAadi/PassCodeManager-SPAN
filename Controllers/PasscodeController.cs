using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PassCodeManager.DTO.RequestObjects;
using PassCodeManager.Services.Abstract;

namespace PassCodeManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PasscodeController : ControllerBase
    {
        private readonly IPassCodeService _securityService;
        public PasscodeController(IPassCodeService securityService)
        {
            _securityService = securityService;
        }

        [HttpPost("AddPasscode")]
        public async Task<ApiResponse> AddPassCode([FromBody]AddPasscodeObject passcodeObject)
        {
            try
            {
                var result = await _securityService.AddPasscode(passcodeObject);
                return new ApiResponse("Passcode Saved Successfully!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpGet("GetPasscode/{Mobile}")]
        public async Task<ApiResponse> GetPassCodesByMobile([FromRoute]string Mobile)
        {
            try
            {
                var result = await _securityService.GetPassCodesByMobile(Mobile);
                return new ApiResponse(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpPut("UpdatePasscode")]
        public async Task<ApiResponse> UpdatePassCode([FromBody]UpdatePasscodeObject request)
        {
            try
            {
                var result = await _securityService.UpdatePassCode(request);
                return new ApiResponse(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        [HttpDelete("DeletePasscode/{Id}")]
        public async Task<ApiResponse> DeletePassCode([FromBody]string Id)
        {
            try
            {
                var result = await _securityService.DeletePasscode(Id);

                return new ApiResponse("Passcode Delted Successfully.", 200);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}