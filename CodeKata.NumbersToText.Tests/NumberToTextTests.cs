using Xunit;

namespace CodeKata.NumbersToText.Tests
{
    public class NumberToTextTests
    {
        [Fact]
        public void InputSingleNumberZero_CorrectOutput()
        {
            Assert.Equal("null", NumberUtil.ToText(0));
        }

        [Fact]
        public void InputSingleNumberOne_CorrectOutput()
        {
            Assert.Equal("eins", NumberUtil.ToText(1));
        }

        [Fact]
        public void InputSingleNumberNine_CorrectOutput()
        {
            Assert.Equal("neun", NumberUtil.ToText(9));
        }

        [Fact]
        public void InputSingleNumberSieben_CorrectOutput()
        {
            Assert.Equal("sieben", NumberUtil.ToText(7));
        }

        [Fact]
        public void InputTwelve_CorrectOutput()
        {
            Assert.Equal("zwˆlf", NumberUtil.ToText(12));
        }

        [Fact]
        public void InputTwenty_CorrectOutput()
        {
            Assert.Equal("zwanzig", NumberUtil.ToText(20));
        }

        [Fact]
        public void InputThirty_CorrectOutput()
        {
            Assert.Equal("dreiﬂig", NumberUtil.ToText(30));
        }

        [Fact]
        public void InputEigthty_CorrectOutput()
        {
            Assert.Equal("achtzig", NumberUtil.ToText(80));
        }

        [Fact]
        public void InputForty_CorrectOutput()
        {
            Assert.Equal("vierzig", NumberUtil.ToText(40));
        }

        [Fact]
        public void InputEigthtyTwo_CorrectOutput()
        {
            Assert.Equal("zweiundachtzig", NumberUtil.ToText(82));
        }

        [Fact]
        public void InputEigthtyOne_CorrectOutput()
        {
            Assert.Equal("einundachtzig", NumberUtil.ToText(81));
        }

        [Fact]
        public void InputEigthtyFour_CorrectOutput()
        {
            Assert.Equal("vierundachtzig", NumberUtil.ToText(84));
        }

        [Fact]
        public void InputEigthtySeven_CorrectOutput()
        {
            Assert.Equal("siebenundachtzig", NumberUtil.ToText(87));
        }

        [Fact]
        public void InputEleven_CorrectOutput()
        {
            Assert.Equal("elf", NumberUtil.ToText(11));
        }

        [Fact]
        public void InputTen_CorrectOutput()
        {
            Assert.Equal("zehn", NumberUtil.ToText(10));
        }

        [Fact]
        public void InputEighteen_CorrectOutput()
        {
            Assert.Equal("achtzehn", NumberUtil.ToText(18));
        }

        [Fact]
        public void InputSixteen_CorrectOutput()
        {
            Assert.Equal("sechzehn", NumberUtil.ToText(16));
        }

        [Fact]
        public void InputSeventeen_CorrectOutput()
        {
            Assert.Equal("siebzehn", NumberUtil.ToText(17));
        }

        [Fact]
        public void InputOneHundredEightyOne_CorrectOutput()
        {
            Assert.Equal("einhunderteinundachtzig", NumberUtil.ToText(181));
        }

        [Fact]
        public void InputSevenHundredThirtyThree_CorrectOutput()
        {
            Assert.Equal("siebenhundertdreiunddreiﬂig", NumberUtil.ToText(733));
        }

        [Fact]
        public void InputSevenHundredThirty_CorrectOutput()
        {
            Assert.Equal("siebenhundertunddreiﬂig", NumberUtil.ToText(730));
        }

        [Fact]
        public void InputThousands_CorrectOutput()
        {
            Assert.Equal("eintausend", NumberUtil.ToText(1000));
        }

        [Fact]
        public void InputThousands2_CorrectOutput()
        {
            Assert.Equal("eintausenddreihundertvierundsiebzig", NumberUtil.ToText(1374));
        }

        [Fact]
        public void InputOneHundredAndEleven_CorrectOutput()
        {
            Assert.Equal("einhundertundelf", NumberUtil.ToText(111));
        }

        [Fact]
        public void InputTwoHundredAndEleven_CorrectOutput()
        {
            Assert.Equal("zweihundertundelf", NumberUtil.ToText(211));
        }

        [Fact]
        public void InputTwoHundredAndOne_CorrectOutput()
        {
            Assert.Equal("zweihundertundeins", NumberUtil.ToText(201));
        }

        [Fact]
        public void InputTwoHundredAndTen_CorrectOutput()
        {
            Assert.Equal("zweihundertundzehn", NumberUtil.ToText(210));
        }
    }
}
