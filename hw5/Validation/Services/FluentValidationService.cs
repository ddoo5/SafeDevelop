﻿using System;
using FluentValidation;
using FluentValidation.Results;
using SD.Models.Responses.Failures;

namespace SD
{
    public abstract class FluentValidationService<TEntity> : AbstractValidator<TEntity>, IValidationService<TEntity>
   where TEntity : class
    {
        public IReadOnlyList<IOperationFailure> ValidateEntity(TEntity item)
        {
            ValidationResult result = Validate(item);

            if (result is null || result.Errors.Count == 0)
            {
                return ArraySegment<IOperationFailure>.Empty;
            }

            List<IOperationFailure> failures = new List<IOperationFailure>(result.Errors.Count);

            foreach (ValidationFailure error in result.Errors)
            {
                OperationFailure failure = new OperationFailure(); failure.PropertyName = error.PropertyName; failure.Description = error.ErrorMessage; failure.Code = error.ErrorCode;
                failures.Add(failure);
            }
            return failures;
        }
    }
}

