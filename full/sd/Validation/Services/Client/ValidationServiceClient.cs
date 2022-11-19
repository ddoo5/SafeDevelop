using System;
using FluentValidation;
using FluentValidation.Results;
using SD.Models.Request;
using SD.Models.Requests;
using SD.Services.Interfaces;

namespace SD.Validation.Services
{
    internal sealed class ValidationServiceClient : FluentValidationService<ClientRequest>, IValidationServiceClient
    {

        public ValidationServiceClient()
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
                    context.AddFailure(new ValidationFailure(nameof(ClientRequest.Name), "Имя не должно включать числа или спец.символы")
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


            ///check patronymic on valid
            RuleFor(x => x.Patronymic)
                .NotEmpty()
                .WithMessage("Отчество не должно быть пустым")
                .WithErrorCode("EXP-1.2");  //todo: change number of exception

            RuleFor(x => x.Patronymic)
                .MinimumLength(3)
                .MaximumLength(25)
                .WithMessage("Отчество должно быть в рамках допустимой длинны")
                .WithErrorCode("EXP-1.2.2");  //todo: change number of exception

            RuleFor(x => x.Patronymic).Custom((s, context) =>
            {
                if (s.Contains("1234567890-'^*~@/!#$%&") == true)
                {
                    context.AddFailure(new ValidationFailure(nameof(AccountRequest.Surname), "Отчество не должно включать числа или спец.символы")
                    {
                        ErrorCode = "EXP-1.2.3"  //todo: change number of exception
                    });
                }
            });
        }
    }
}

