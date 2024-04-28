using FluentValidation.TestHelper;
using Streetcode.BLL.Dto.Team;
using Streetcode.BLL.Dto.Timeline;
using Streetcode.BLL.MediatR.Team.TeamMembersLinks.Create;
using Streetcode.BLL.MediatR.Timeline.TimelineItem.Create;
using Streetcode.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Streetcode.XUnitTest.ValidationTests.Timeline.TimelineItem
{
    public class TimelineItemValidatorTest
    {
        private readonly CreateTimelineItemCommandValidator _validator;

        public TimelineItemValidatorTest()
        {
            _validator = new CreateTimelineItemCommandValidator();
        }

        [Fact]
        public void Date_Is_Required_Should_Pass()
        {
            // Arrange
            var dto = new TimelineItemCreateDto() { Date = DateTime.Now };
            var request = new CreateTimelineItemCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.TimelineItem.Date);
        }

        [Fact]
        public void Date_Is_Required_Should_Not_Pass()
        {
            // Arrange
            var dto = new TimelineItemCreateDto() { };
            var request = new CreateTimelineItemCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.TimelineItem.Date);
        }

        [Fact]
        public void DateViewPattern_Is_Required_Should_Not_Pass()
        {
            // Arrange
            var dto = new TimelineItemCreateDto() { };
            var request = new CreateTimelineItemCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.TimelineItem.DateViewPattern);
        }

        [Fact]
        public void DateViewPattern_Is_Required_Should_Pass()
        {
            // Arrange
            var dto = new TimelineItemCreateDto() { DateViewPattern = DateViewPattern.Year };
            var request = new CreateTimelineItemCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.TimelineItem.DateViewPattern);
        }

        [Fact]
        public void Title_Is_Required_Should_Not_Pass()
        {
            // Arrange
            var dto = new TimelineItemCreateDto() { };
            var request = new CreateTimelineItemCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.TimelineItem.Title);
        }

        [Fact]
        public void Title_Is_Required_Should_Pass()
        {
            // Arrange
            var dto = new TimelineItemCreateDto() { Title = "A" };
            var request = new CreateTimelineItemCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.TimelineItem.Title);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(99)]
        [InlineData(49)]
        public void Title_Max_Length_Should_Pass(int titleLength)
        {
            // Arrange
            string title = string.Empty;

            for (int i = 0; i < titleLength; i++)
            {
                title += "A";
            }

            var dto = new TimelineItemCreateDto() { Title = title };
            var request = new CreateTimelineItemCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.TimelineItem.Title);
        }

        [Theory]
        [InlineData(254)]
        [InlineData(101)]
        [InlineData(333)]
        public void Title_Max_Length_Should_Not_Pass(int titleLength)
        {
            // Arrange
            string title = string.Empty;

            for (int i = 0; i < titleLength; i++)
            {
                title += "A";
            }

            var dto = new TimelineItemCreateDto() { Title = title };
            var request = new CreateTimelineItemCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.TimelineItem.Title);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(300)]
        [InlineData(599)]
        public void Description_Max_Length_Should_Pass(int descriptionLength)
        {
            // Arrange
            string description = string.Empty;

            for (int i = 0; i < descriptionLength; i++)
            {
                description += "A";
            }

            var dto = new TimelineItemCreateDto() { Description = description };
            var request = new CreateTimelineItemCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.TimelineItem.Description);
        }

        [Theory]
        [InlineData(601)]
        [InlineData(999)]
        [InlineData(1235)]
        public void Description_Max_Length_Should_Not_Pass(int descriptionLength)
        {
            // Arrange
            string description = string.Empty;

            for (int i = 0; i < descriptionLength; i++)
            {
                description += "A";
            }

            var dto = new TimelineItemCreateDto() { Description = description };
            var request = new CreateTimelineItemCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.TimelineItem.Description);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(25)]
        [InlineData(29)]
        public void Context_Max_Length_Should_Pass(int contextLength)
        {
            // Arrange
            string context = string.Empty;

            for (int i = 0; i < contextLength; i++)
            {
                context += "A";
            }

            var dto = new TimelineItemCreateDto() { Context = context };
            var request = new CreateTimelineItemCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.TimelineItem.Context);
        }

        [Theory]
        [InlineData(51)]
        [InlineData(333)]
        [InlineData(256)]
        public void Context_Max_Length_Should_Not_Pass(int contextLength)
        {
            // Arrange
            string context = string.Empty;

            for (int i = 0; i < contextLength; i++)
            {
                context += "A";
            }

            var dto = new TimelineItemCreateDto() { Context = context };
            var request = new CreateTimelineItemCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.TimelineItem.Context);
        }
    }
}
