﻿using System;
namespace SD.Models.Responses.Failures
{
    public sealed class OperationFailure : IOperationFailure
    {
        public string PropertyName { get; set; }

        public string Description { get; set; }

        public string Code { get; set; }
    }
}

