﻿using FluentValidation.Results;

namespace Catalog.Application.Exceptions;

public sealed class ValidationException : ApplicationException
{
    public IDictionary<string, string[]> Errors { get; }

    private ValidationException()
        : base("One or more validation failures have occured")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures
            .GroupBy(f => f.PropertyName, f => f.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }
}