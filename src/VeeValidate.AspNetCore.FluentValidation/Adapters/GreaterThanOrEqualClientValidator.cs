﻿using System;
using FluentValidation.Internal;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.FluentValidation.Adapters
{
    public class GreaterThanOrEqualClientValidator : VeeValidateClientValidatorBase
    {
        private readonly VeeValidateOptions _options;

        public GreaterThanOrEqualClientValidator(PropertyRule rule, IPropertyValidator validator, VeeValidateOptions options) : base(rule, validator)
        {
            _options = options;
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            var rangeValidator = (GreaterThanOrEqualValidator)Validator;

            if (rangeValidator.ValueToCompare != null)
            {
                MergeAttribute(context.Attributes, "data-vv-as", context.ModelMetadata.GetDisplayName());

                if (context.ModelMetadata.UnderlyingOrModelType == typeof(DateTime))
                {
                    var normalisedDateFormat = _options.Dates.Format.Replace('D', 'd').Replace('Y', 'y');

                    MergeValidationAttribute(context.Attributes, "date_format", $"'{_options.Dates.Format}'");

                    if (DateTime.TryParse(rangeValidator.ValueToCompare.ToString(), out var maxDate))
                    {
                        MergeValidationAttribute(context.Attributes, "after", $"['{maxDate.ToString(normalisedDateFormat)}',true]");
                    }
                }
                else
                {
                    MergeValidationAttribute(context.Attributes, "min_value", rangeValidator.ValueToCompare);
                }
            }
        }
    }
}