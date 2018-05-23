﻿using System;
using Shouldly;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using VeeValidate.AspNetCore.Adapters;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class RangeMaxAttributeAdapterTests
    {
        private readonly VeeValidateOptions _options = new VeeValidateOptions
        {
            Dates = new DateValidationOptions
            {
                Format = "DD/MM/YYYY"
            }
        };

        [Fact]
        public void AddValidation_adds_max_value_validation_rule()
        {
            // Arrange
            var adapter = new RangeMaxAttributeAdapter(_options);
            var metadata = new EmptyModelMetadataProvider().GetMetadataForType(typeof(int));

            // Act
            var result = adapter.GetVeeValidateRule("10", metadata);

            // Assert
            result.ShouldBe("max_value:'10'");
        }

        [Theory]
        [InlineData("2016-03-01")]
        [InlineData("03/01/2016")]
        [InlineData("Mar 01 2016")]
        public void AddValidation_adds_before_validation_rule(string date)
        {
            // Arrange
            var adapter = new RangeMaxAttributeAdapter(_options);
            var metadata = new EmptyModelMetadataProvider().GetMetadataForType(typeof(DateTime));
            
            // Act
            var result = adapter.GetVeeValidateRule(date, metadata);

            // Assert
            result.ShouldBe("before:['01/03/2016',true]");
        }
    }
}
