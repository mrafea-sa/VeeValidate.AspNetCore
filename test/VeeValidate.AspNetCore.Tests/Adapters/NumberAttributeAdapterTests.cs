﻿using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Shouldly;
using VeeValidate.AspNetCore.Adapters;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class NumberAttributeAdapterTests
    {
        [Theory]
        [InlineData(typeof(short))]
        [InlineData(typeof(int))]
        [InlineData(typeof(long))]
        public void AddValidation_adds_numeric_validation_rule(Type modelType)
        {
            // Arrange
            var adapter = new NumberAttributeAdapter();
            var metadata = new EmptyModelMetadataProvider().GetMetadataForType(modelType);

            // Act
            var result = adapter.GetVeeValidateRule("true", metadata);

            // Assert            
            result.ShouldBe("numeric:true"); 
        }

        [Theory]
        [InlineData(typeof(double))]
        [InlineData(typeof(float))]
        [InlineData(typeof(decimal))]
        public void AddValidation_adds_decimal_validation_rule(Type modelType)
        {
            // Arrange
            var adapter = new NumberAttributeAdapter();
            var metadata = new EmptyModelMetadataProvider().GetMetadataForType(modelType);

            // Act
            var result = adapter.GetVeeValidateRule("true", metadata);

            // Assert  
            result.ShouldBe("decimal:true");
        }
    }
}