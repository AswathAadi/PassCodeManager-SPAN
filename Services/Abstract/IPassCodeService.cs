﻿using PassCodeManager.DTO.RequestObjects;
using PassCodeManager.DTO.ResponseObjects;

namespace PassCodeManager.Services.Abstract
{
    public interface IPassCodeService
    {
        Task<SecurityResponseObject> AddPasscode(AddPasscodeObject passcodeObject);
        Task<Dictionary<string, string>> GetPassCodesByMobile(string mobile);
        Task<SecurityResponseObject> UpdatePassCode(UpdatePasscodeObject request);
        Task<bool> DeletePasscode(string passCodeId);
    }
}
