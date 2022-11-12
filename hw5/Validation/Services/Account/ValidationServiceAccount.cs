using System;
using FluentValidation;
using FluentValidation.Results;
using SD.Models.Request;
using SD.Services.Interfaces;

namespace SD.Validation.Services
{
    internal sealed class ValidationServiceAccount : FluentValidationService<AccountRequest>, IValidationServiceAccount
    {
        public ValidationServiceAccount()
        {

            ///check first name on valid
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Имя не должно быть пустым")
                .WithErrorCode("EXP-1.1");

            RuleFor(x => x.Name)
                .MinimumLength(3)
                .MaximumLength(20)
                .WithMessage("Имя должно быть в рамках допустимой длинны")
                .WithErrorCode("EXP-1.1.2");

            RuleFor(x => x.Name).Custom((s, context) =>
            {
                if (s.Contains("1234567890-'^*~@/!#$%&") == true)
                {
                    context.AddFailure(new ValidationFailure(nameof(AccountRequest.Name), "Имя не должно включать числа или спец.символы")
                    {
                        ErrorCode = "EXP-1.1.3"
                    });
                }
            });


            ///check surname on valid
            RuleFor(x => x.Surname)
                .NotEmpty()
                .WithMessage("Фамилия не должна быть пустой")
                .WithErrorCode("EXP-1.2");

            RuleFor(x => x.Surname)
                .MinimumLength(3)
                .MaximumLength(25)
                .WithMessage("Фамилия должна быть в рамках допустимой длинны")
                .WithErrorCode("EXP-1.2.2");

            RuleFor(x => x.Surname).Custom((s, context) =>
            {
                if (s.Contains("1234567890-'^*~@/!#$%&") == true)
                {
                    context.AddFailure(new ValidationFailure(nameof(AccountRequest.Surname), "Фамилия не должна включать числа или спец.символы")
                    {
                        ErrorCode = "EXP-1.2.3"
                    });
                }
            });


            ///check email on valid
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Почта не должна быть пустой")
                .WithErrorCode("EXP-1.4");

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Почта должна соответствовать стандартам")
                .WithErrorCode("EXP-1.4.2");


            ///check password on valid
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Пароль не должен быть пустым")
                .WithErrorCode("EXP-1.4");   //todo: change number of exception

            RuleFor(x => x.Password)
                .MinimumLength(8)
                .MaximumLength(20)
                .WithMessage("Пароль не должен быть длиннее 20 символов и короче 8")
                .WithErrorCode("EXP-1.4.2");  //todo: change number of exception

            RuleFor(x => x.Password).Custom((s, context) =>
            {
                if (s.Contains(" ") == true)
                {
                    context.AddFailure(new ValidationFailure(nameof(AccountRequest.Password), "Пароль не должен включать в себя пробел")
                    {
                        ErrorCode = "EXP-1.2.3"   //todo: change number of exception
                    });
                }
            });
        }
    }
}

