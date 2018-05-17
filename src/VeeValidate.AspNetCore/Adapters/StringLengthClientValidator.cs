﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace VeeValidate.AspNetCore.Adapters
{
    public class StringLengthClientValidator : VeeAttributeAdapter<StringLengthAttribute>
    {
        public StringLengthClientValidator(StringLengthAttribute attribute) : base(attribute)
        {
        }

        public override void AddValidationRules(ClientModelValidationContext context)
        {
            if (Attribute.MaximumLength != int.MaxValue)
            {
                MergeValidationAttribute(context.Attributes, $"max:{Attribute.MaximumLength}");
            }

            if (Attribute.MinimumLength != 0)
            {
                MergeValidationAttribute(context.Attributes, $"min:{Attribute.MinimumLength}");                
            }
        }
    }
}
