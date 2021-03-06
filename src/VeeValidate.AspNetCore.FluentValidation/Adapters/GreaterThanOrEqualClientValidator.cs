﻿using System;
using FluentValidation.Internal;
using FluentValidation.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.FluentValidation.Adapters
{
    public class GreaterThanOrEqualClientValidator : VeeValidateClientValidatorBase
    {
        private readonly Func<HttpContext, string> _dateFormatProvider;
        
        public GreaterThanOrEqualClientValidator(PropertyRule rule, IPropertyValidator validator, Func<HttpContext, string> dateFormatProvider) : base(rule, validator)
        {
            _dateFormatProvider = dateFormatProvider;
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            var rangeValidator = (GreaterThanOrEqualValidator)Validator;

            if (rangeValidator.ValueToCompare != null)
            {
                MergeAttribute(context.Attributes, "data-vv-as", context.ModelMetadata.GetDisplayName());

                if (context.ModelMetadata.UnderlyingOrModelType == typeof(DateTime))
                {
                    var dateFormat = _dateFormatProvider(context.ActionContext.HttpContext);
                    var normalisedDateFormat = dateFormat.Replace('D', 'd').Replace('Y', 'y');

                    MergeValidationAttribute(context.Attributes, "date_format", $"'{dateFormat}'");

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
