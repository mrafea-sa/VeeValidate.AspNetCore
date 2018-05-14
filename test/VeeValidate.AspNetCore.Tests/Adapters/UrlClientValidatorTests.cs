﻿using Shouldly;
using System.ComponentModel.DataAnnotations;
using VeeValidate.AspNetCore.Adapters;
using VeeValidate.AspNetCore.Tests.Builders;
using Xunit;

namespace VeeValidate.AspNetCore.Tests.Adapters
{
    public class UrlClientValidatorTests
    {
        [Fact]
        public void AddValidation_adds_validation_rule()
        {
            // Arrange     
            var attribute = new UrlAttribute();
            var adapter = new UrlClientValidator(attribute);

            var context = new ClientModelValidationContextBuilder()
                .WithModel(attribute)                
                .Build();

            // Act
            adapter.AddValidation(context);

            // Assert
            context.Attributes.ShouldContainKey("data-vv-as");
            context.Attributes.ShouldContainKey("v-validate");
            context.Attributes["v-validate"].ShouldBe("{url:[true,true]}");
        }
    }
}