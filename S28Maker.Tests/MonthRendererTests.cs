
using S28Maker.Services;
using Xunit;

namespace S28Maker.Tests
{
    public class MonthRendererTests
    {
        [Fact]
        public void MonthNameShouldDetectedFromMonthNumberDecember()
        {
            var monthPos = MonthRenderer.GetMonthPos(12);

            var monthName = MonthRenderer.MonthNames[monthPos];

            Assert.Equal("дек/июнь", monthName);
        }

        [Fact]
        public void MonthNameShouldDetectedFromMonthNumberJanuary()
        {
            var monthPos = MonthRenderer.GetMonthPos(1);

            var monthName = MonthRenderer.MonthNames[monthPos];

            Assert.Equal("янв/июль", monthName);
        }

        [Fact]
        public void MonthNameShouldDetectedFromMonthNumberJune()
        {
            var monthPos = MonthRenderer.GetMonthPos(7);

            var monthName = MonthRenderer.MonthNames[monthPos];

            Assert.Equal("янв/июль", monthName);
        }

        [Fact]
        public void MonthNameShouldDetectedFromMonthNumberFebruary()
        {
            var monthPos = MonthRenderer.GetMonthPos(2);

            var monthName = MonthRenderer.MonthNames[monthPos];

            Assert.Equal("февр/авг", monthName);
        }

        [Fact]
        public void MonthNameShouldDetectedFromMonthNumberAugust()
        {
            var monthPos = MonthRenderer.GetMonthPos(8);

            var monthName = MonthRenderer.MonthNames[monthPos];

            Assert.Equal("февр/авг", monthName);
        }
    }
}
