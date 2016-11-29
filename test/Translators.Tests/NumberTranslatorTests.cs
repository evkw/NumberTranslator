using System;
using Xunit;
using Shouldly;

namespace Translators.Tests
{
    public class NumberTranslatorTests
    {
        private readonly NumberTranslator _numberTranslater;
        public NumberTranslatorTests()
        {
            _numberTranslater = new NumberTranslator();
        }

        [Fact]
        public void Translate_IntEntered_StringReturned()
        {
            var result = _numberTranslater.Translate("6");
            result.ShouldBe("6");
        }

        [Fact]
        public void Translate_InvalidStringEtnered_ArgumentExceptionThrown()
        {
            Assert.Throws<ArgumentException>(() => _numberTranslater.Translate("Hello"));
        }

        [Theory]
        [InlineData(new char[] { '1' }, new char[] { '0', '0', '1' })]
        [InlineData(new char[] { '1', '2' }, new char[] { '0', '1', '2' })]
        [InlineData(new char[] { '1', '2', '3' }, new char[] { '1', '2', '3' })]
        [InlineData(new char[] { '1', '2', '3', '4' }, new char[] { '0', '0', '1', '2', '3', '4' })]
        [InlineData(new char[] { '1', '2', '3', '4', '5' }, new char[] { '0', '1', '2', '3', '4', '5' })]
        [InlineData(new char[] { '9', '9', '9', '9', '9'}, new char[] { '0', '9', '9', '9', '9', '9'})]
        public void FillInArray_DoesWhat_YouExpect(char[] entered, char[] expected)
        {
            var result = _numberTranslater.FillInArray(entered);
            result.ShouldBe(expected);
        }

        [Theory]
        [InlineData(new char[] { '0', '1' }, "One")]
        [InlineData(new char[] { '0', '8' }, "Eight")]
        [InlineData(new char[] { '1', '2' }, "Twelve")]
        [InlineData(new char[] { '2', '3' }, "Twenty Three")]
        [InlineData(new char[] { '3', '4' }, "Thirty Four")]
        [InlineData(new char[] { '4', '5' }, "Fourty Five")]
        public void DoMagic_DoesMagic(char[] entered, string expected)
        {
            var result = _numberTranslater.GetTens(entered);
            result.ShouldBe(expected);
        }

        [Theory]
        [InlineData(new char[] { '0', '1', '2' }, "Twelve")]
        [InlineData(new char[] { '2', '3', '0' }, "Two Hundred and Thirty")]
        [InlineData(new char[] { '3', '1', '4' }, "Three Hundred and Fourteen")]
        [InlineData(new char[] { '4', '5', '7' }, "Four Hundred and Fifty Seven")]
        [InlineData(new char[] { '9', '9', '9' }, "Nine Hundred and Ninety Nine")]
        public void HandleDigitGrouping_WorksProperly(char[] entered, string expected)
        {
            var result = _numberTranslater.HandleDigitGrouping(entered);
            result.ShouldBe(expected);
        }


        [Theory]
        [InlineData("12", "Twelve")]
        [InlineData("230", "Two Hundred and Thirty")]
        [InlineData("314", "Three Hundred and Fourteen")]
        [InlineData("457", "Four Hundred and Fifty Seven")]
        [InlineData("9999", "Nine Thousand Nine Hundred and Ninety Nine")]
        [InlineData("99999", "Ninety Nine Thousand Nine Hundred and Ninety Nine")]
        [InlineData("999999", "Nine Million Nine Hundred and Ninety Nine Thousand Nine Hundred and Ninety Nine")]
        public void Translate_Works(string entered, string expected)
        {
            var result = _numberTranslater.Translate(entered);
            result.ShouldBe(expected);
        }

    }
}
