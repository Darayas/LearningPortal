﻿using LearningPortal.Framework.Contracts;
using Microsoft.AspNetCore.Identity;

namespace LearningPortal.WebApp.Authentication
{
    public class CustomIdentityErrorDescriber : IdentityErrorDescriber
    {
        private readonly ILocalizer _Localizer;

        public CustomIdentityErrorDescriber(ILocalizer Localizer)
        {
            _Localizer = Localizer;
        }

        public override IdentityError DefaultError() { return new IdentityError { Code = nameof(DefaultError), Description = _Localizer["DefaultError"] }; }
        public override IdentityError ConcurrencyFailure() { return new IdentityError { Code = nameof(ConcurrencyFailure), Description = _Localizer["ConcurrencyFailure"] }; }
        public override IdentityError PasswordMismatch() { return new IdentityError { Code = nameof(PasswordMismatch), Description = _Localizer["PasswordMismatch"] }; }
        public override IdentityError InvalidToken() { return new IdentityError { Code = nameof(InvalidToken), Description = _Localizer["InvalidToken"] }; }
        public override IdentityError LoginAlreadyAssociated() { return new IdentityError { Code = nameof(LoginAlreadyAssociated), Description = _Localizer["LoginAlreadyAssociated"] }; }
        public override IdentityError InvalidUserName(string userName) { return new IdentityError { Code = nameof(InvalidUserName), Description = _Localizer["InvalidUserName"] }; }
        public override IdentityError InvalidEmail(string email) { return new IdentityError { Code = nameof(InvalidEmail), Description = _Localizer["InvalidEmail"] }; }
        public override IdentityError DuplicateUserName(string userName) { return new IdentityError { Code = nameof(DuplicateUserName), Description = _Localizer["DuplicateUserName"] }; }
        public override IdentityError DuplicateEmail(string email) { return new IdentityError { Code = nameof(DuplicateEmail), Description = _Localizer["DuplicateEmail"] }; }
        public override IdentityError InvalidRoleName(string role) { return new IdentityError { Code = nameof(InvalidRoleName), Description = _Localizer["InvalidRoleName"] }; }
        public override IdentityError DuplicateRoleName(string role) { return new IdentityError { Code = nameof(DuplicateRoleName), Description = _Localizer["DuplicateRoleName"] }; }
        public override IdentityError UserAlreadyHasPassword() { return new IdentityError { Code = nameof(UserAlreadyHasPassword), Description = _Localizer["UserAlreadyHasPassword"] }; }
        public override IdentityError UserLockoutNotEnabled() { return new IdentityError { Code = nameof(UserLockoutNotEnabled), Description = _Localizer["UserLockoutNotEnabled"] }; }
        public override IdentityError UserAlreadyInRole(string role) { return new IdentityError { Code = nameof(UserAlreadyInRole), Description = _Localizer["UserAlreadyInRole"] }; }
        public override IdentityError UserNotInRole(string role) { return new IdentityError { Code = nameof(UserNotInRole), Description = _Localizer["UserNotInRole"] }; }
        public override IdentityError PasswordTooShort(int length) { return new IdentityError { Code = nameof(PasswordTooShort), Description = _Localizer["PasswordTooShort", length] }; }
        public override IdentityError PasswordRequiresNonAlphanumeric() { return new IdentityError { Code = nameof(PasswordRequiresNonAlphanumeric), Description = _Localizer["PasswordRequiresNonAlphanumeric"] }; }
        public override IdentityError PasswordRequiresDigit() { return new IdentityError { Code = nameof(PasswordRequiresDigit), Description = _Localizer["PasswordRequiresDigit"] }; }
        public override IdentityError PasswordRequiresLower() { return new IdentityError { Code = nameof(PasswordRequiresLower), Description = _Localizer["PasswordRequiresLower"] }; }
        public override IdentityError PasswordRequiresUpper() { return new IdentityError { Code = nameof(PasswordRequiresUpper), Description = _Localizer["PasswordRequiresUpper"] }; }
        public override IdentityError RecoveryCodeRedemptionFailed() { return new IdentityError { Code = nameof(RecoveryCodeRedemptionFailed), Description = _Localizer["RecoveryCodeRedemptionFailed"] }; }
        public override IdentityError PasswordRequiresUniqueChars(int uniqueChars) { return new IdentityError { Code = nameof(PasswordRequiresUniqueChars), Description = _Localizer["PasswordRequiresUniqueChars"] }; }
    }
}
