﻿using FluentValidation.Internal;
using FluentValidation.Validators;
using Shouldly;
using VeeValidate.AspNetCore.Builders;
using VeeValidate.AspNetCore.FluentValidation.Adapters;
using Xunit;

namespace VeeValidate.AspNetCore.FluentValidation.Tests.Adapters
{
    public class LengthClientValidatorTests
    {
        private class TestObject
        {
            public string Length { get; set; }
        }

        [Fact]
        public void AddValidation_adds_max_and_min_rules()
        {
            // Arrange
            var property = PropertyRule.Create<TestObject, string>(x => x.Length);
            var adapter = new LengthClientValidator(property, new LengthValidator(2, 10));

            var context = new ClientModelValidationContextBuilder()
                .WithModelType<string>()
                .Build();

            // Act
            adapter.AddValidation(context);

            // Assert
            context.Attributes.Keys.ShouldContain("v-validate");
            context.Attributes["v-validate"].ShouldBe("{max:10,min:2}");
        }
    }
}
