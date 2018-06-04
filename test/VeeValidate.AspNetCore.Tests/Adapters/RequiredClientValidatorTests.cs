﻿using Shouldly;
using System.ComponentModel.DataAnnotations;
using VeeValidate.AspNetCore.Adapters;
using VeeValidate.AspNetCore.Tests.Builders;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class RequiredClientValidatorTests
    {
        [Fact]
        public void AddValidation_adds_required_rule()
        {
            // Arrange
            var attribute = new RequiredAttribute();
            var adapter = new RequiredClientValidator(attribute);
            
            var context = new ClientModelValidationContextBuilder()
                .WithModel(attribute)
                .Build();

            // Act
            adapter.AddValidation(context);

            // Assert
            context.Attributes.Keys.ShouldContain("v-validate");
            context.Attributes["v-validate"].ShouldBe("{required:true}");
        }
    }
}