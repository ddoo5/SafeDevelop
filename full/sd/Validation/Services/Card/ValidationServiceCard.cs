using System;
using FluentValidation;
using FluentValidation.Results;
using SD.Models.Request;
using SD.Services.Interfaces;

namespace SD.Validation.Services
{
    internal sealed class ValidationServiceCard : FluentValidationService<CardRequest>, IValidationServiceCard
    {
        public ValidationServiceCard()
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
                    context.AddFailure(new ValidationFailure(nameof(CardRequest.Name), "Имя не должно включать числа или спец.символы")
                    {
                        ErrorCode = "EXP-1.1.3"
                    });
                }
            });


            ///check card number on valid
            RuleFor(x => x.CardNumber)
                .NotEmpty()
                .WithMessage("Номер карты не должен быть пустым")
                .WithErrorCode("EXP-1.2"); //todo: change number of exception

            RuleFor(x => x.CardNumber)
                .MinimumLength(19)
                .MaximumLength(19)
                .WithMessage("Номер карты должен соответствовать 16 символам")
                .WithErrorCode("EXP-1.2.2"); //todo: change number of exception

            RuleFor(x => x.CardNumber).Custom((s, context) =>
            {
                if (s.Contains("qwertyuioplkjhgfdsazxcvbnmйцукенгшщздлорпавыфячсмитьбю-'^*~@/!#$%&") == true)
                {
                    context.AddFailure(new ValidationFailure(nameof(CardRequest.CardNumber), "Номер карты не должен включать в себя иные символы, кроме цифр")
                    {
                        ErrorCode = "EXP-1.2.3"  //todo: change number of exception
                    });
                }
            });


            ///check cvv on valid
            RuleFor(x => x.CVV)
                .NotEmpty()
                .WithMessage("CVV не должен быть пустым")
                .WithErrorCode("EXP-1.4");   //todo: change number of exception

            RuleFor(x => x.CVV.ToString())
                .MinimumLength(3)
                .MaximumLength(3)
                .WithMessage("CVV должен включать в себя строго 3 символа")
                .WithErrorCode("EXP-1.4.2");   //todo: change number of exception

            RuleFor(x => x.CVV.ToString()).Custom((s, context) =>
            {
                if (s.Contains("qwertyuioplkjhgfdsazxcvbnmйцукенгшщздлорпавыфячсмитьбю-'^*~@/!#$%&") == true)
                {
                    context.AddFailure(new ValidationFailure(nameof(CardRequest.CVV), "CVV не должен включать в себя иные символы, кроме цифр")
                    {
                        ErrorCode = "EXP-1.2.3"  //todo: change number of exception
                    });
                }
            });
        }
    }
}

