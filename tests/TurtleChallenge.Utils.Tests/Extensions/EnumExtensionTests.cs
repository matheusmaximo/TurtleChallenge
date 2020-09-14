using System;
using System.ComponentModel;
using TurtleChallenge.TestUtilities.xUnit.Settings;
using TurtleChallenge.Utils.Extensions;
using Xunit;

namespace TurtleChallenge.Utils.Tests.Extensions
{
    [Trait(Traits.Category, Categories.Unit)]
    public static class EnumExtensionTests
    {
        [Fact]
        public static void GetDescription_GivenValueWithDescription_ShouldReturnValueDescription()
        {
            var value = Test.FirstOption;
            var expected = "Description";

            var result = EnumExtensions.GetDescription(value);

            Assert.Equal(expected, result);
        }

        [Fact]
        public static void GetDescription_GivenValueWithoutDescription_ShouldReturnValueName()
        {
            var value = Test.SecondOption;
            var expected = Enum.GetName(typeof(Test), value);

            var result = EnumExtensions.GetDescription(value);

            Assert.Equal(expected, result);
        }

        [Fact]
        public static void GetDescription_GivenInvalidValue_ShouldReturnEmpty()
        {
            var value = (Test)2;
            var expected = string.Empty;

            var result = EnumExtensions.GetDescription(value);

            Assert.Equal(expected, result);
        }
    }

    public enum Test
    {
        [Description("Description")]
        FirstOption,

        SecondOption
    }
}
