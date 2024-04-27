using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.TestHelper;
using Microsoft.AspNetCore.Http;
using Streetcode.BLL.Dto.AdditionalContent;
using Streetcode.BLL.Dto.AdditionalContent.Coordinates.Types;
using Streetcode.BLL.Dto.AdditionalContent.Tag;
using Streetcode.BLL.Dto.Streetcode;
using Streetcode.BLL.MediatR.AdditionalContent.Coordinate.Create;
using Streetcode.BLL.MediatR.Streetcode.Streetcode;
using Streetcode.BLL.MediatR.Streetcode.Streetcode.Create;
using Streetcode.DAL.Entities.AdditionalContent;
using Xunit;

namespace Streetcode.XUnitTest.ValidationTests.Streetcode
{
    public class BaseStreetcodeValidatorTest
    {
        private const int MAXTEASERLENGTH = 650;
        private const int MAXTEASERLENGTHWITHNEWLINE = 550;
        private const int MAXURLLENGTH = 100;
        private const int MAXSHORTSECRIPTIONLENGTH = 33;
        private const int MAXDATESTRINGLENGTH = 50;
        private const int MAXALIASLENGTH = 50;
        private const int MAXTITLELENGTH = 100;
        private const int MAXNUMBEROFTAGS = 50;
        private const int MAXINSTAGRAMARLINKLENGTH = 255;
        private const int MAXINVOLVEDPEOPLELENGTH = 255;

        private readonly BaseStreetcodeCommandValidator _validator;

        public BaseStreetcodeValidatorTest()
        {
            _validator = new BaseStreetcodeCommandValidator();
        }

        [Theory]
        [InlineData(651)]
        [InlineData(MAXTEASERLENGTH + 100)]
        public void BaseStreetcode_TeaserLengthGraterThanAllowed_ShouldHaveErrors(int length)
        {
            // Arrange
            var dto = new BaseStreetcodeDto()
            {
                Teaser = CreateStringWithSpecificLength(length),
                ShortDescription = CreateStringWithSpecificLength(MAXSHORTSECRIPTIONLENGTH),
                TransliterationUrl = CreateStringWithSpecificLength(MAXURLLENGTH),
                DateString = CreateStringWithSpecificLength(MAXDATESTRINGLENGTH),
                Alias = CreateStringWithSpecificLength(MAXALIASLENGTH),
                Title = CreateStringWithSpecificLength(MAXTITLELENGTH),
                Tags = CreateCollectionWithSpecificLength(MAXNUMBEROFTAGS),
                InstagramARLink = CreateStringWithSpecificLength(MAXINSTAGRAMARLINKLENGTH),
                InvolvedPeople = CreateStringWithSpecificLength(MAXINVOLVEDPEOPLELENGTH)
            };
            var request = new CreateStreetcodeCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.Streetcode.Teaser);
        }

        [Theory]
        [InlineData(MAXTEASERLENGTHWITHNEWLINE + 1)]
        [InlineData(MAXTEASERLENGTHWITHNEWLINE + 100)]
        public void BaseStreetcode_TeaserWithNewLineLengthGraterThanAllowed_ShouldHaveErrors(int length)
        {
            // Arrange
            var dto = new BaseStreetcodeDto()
            {
                Teaser = $"{CreateStringWithSpecificLength(length / 2)}\n{CreateStringWithSpecificLength(length / 2)}",
                ShortDescription = CreateStringWithSpecificLength(MAXSHORTSECRIPTIONLENGTH),
                TransliterationUrl = CreateStringWithSpecificLength(MAXURLLENGTH),
                DateString = CreateStringWithSpecificLength(MAXDATESTRINGLENGTH),
                Alias = CreateStringWithSpecificLength(MAXALIASLENGTH),
                Title = CreateStringWithSpecificLength(MAXTITLELENGTH),
                Tags = CreateCollectionWithSpecificLength(MAXNUMBEROFTAGS),
                InstagramARLink = CreateStringWithSpecificLength(MAXINSTAGRAMARLINKLENGTH),
                InvolvedPeople = CreateStringWithSpecificLength(MAXINVOLVEDPEOPLELENGTH)
            };
            var request = new CreateStreetcodeCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.Streetcode.Teaser);
        }

        [Theory]
        [InlineData(MAXSHORTSECRIPTIONLENGTH + 1)]
        [InlineData(MAXSHORTSECRIPTIONLENGTH + 100)]
        public void BaseStreetcode_ShortDescriptionLengthGraterThanAllowed_ShouldHaveErrors(int length)
        {
            // Arrange
            var dto = new BaseStreetcodeDto()
            {
                Teaser = CreateStringWithSpecificLength(MAXTEASERLENGTH),
                ShortDescription = CreateStringWithSpecificLength(length),
                TransliterationUrl = CreateStringWithSpecificLength(MAXURLLENGTH),
                DateString = CreateStringWithSpecificLength(MAXDATESTRINGLENGTH),
                Alias = CreateStringWithSpecificLength(MAXALIASLENGTH),
                Title = CreateStringWithSpecificLength(MAXTITLELENGTH),
                Tags = CreateCollectionWithSpecificLength(MAXNUMBEROFTAGS),
                InstagramARLink = CreateStringWithSpecificLength(MAXINSTAGRAMARLINKLENGTH),
                InvolvedPeople = CreateStringWithSpecificLength(MAXINVOLVEDPEOPLELENGTH)
            };
            var request = new CreateStreetcodeCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.Streetcode.ShortDescription);
        }

        [Theory]
        [InlineData(MAXURLLENGTH + 1)]
        [InlineData(MAXURLLENGTH + 100)]
        public void BaseStreetcode_UrlLengthGraterThanAllowed_ShouldHaveErrors(int length)
        {
            // Arrange
            var dto = new BaseStreetcodeDto()
            {
                Teaser = CreateStringWithSpecificLength(MAXTEASERLENGTH),
                ShortDescription = CreateStringWithSpecificLength(MAXSHORTSECRIPTIONLENGTH),
                TransliterationUrl = CreateStringWithSpecificLength(length),
                DateString = CreateStringWithSpecificLength(MAXDATESTRINGLENGTH),
                Alias = CreateStringWithSpecificLength(MAXALIASLENGTH),
                Title = CreateStringWithSpecificLength(MAXTITLELENGTH),
                Tags = CreateCollectionWithSpecificLength(MAXNUMBEROFTAGS),
                InstagramARLink = CreateStringWithSpecificLength(MAXINSTAGRAMARLINKLENGTH),
                InvolvedPeople = CreateStringWithSpecificLength(MAXINVOLVEDPEOPLELENGTH)
            };
            var request = new CreateStreetcodeCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.Streetcode.TransliterationUrl);
        }

        [Theory]
        [InlineData(MAXDATESTRINGLENGTH + 1)]
        [InlineData(MAXDATESTRINGLENGTH + 100)]
        public void BaseStreetcode_DateStringLengthGraterThanAllowed_ShouldHaveErrors(int length)
        {
            // Arrange
            var dto = new BaseStreetcodeDto()
            {
                Teaser = CreateStringWithSpecificLength(MAXTEASERLENGTH),
                ShortDescription = CreateStringWithSpecificLength(MAXSHORTSECRIPTIONLENGTH),
                TransliterationUrl = CreateStringWithSpecificLength(MAXURLLENGTH),
                DateString = CreateStringWithSpecificLength(length),
                Alias = CreateStringWithSpecificLength(MAXALIASLENGTH),
                Title = CreateStringWithSpecificLength(MAXTITLELENGTH),
                Tags = CreateCollectionWithSpecificLength(MAXNUMBEROFTAGS),
                InstagramARLink = CreateStringWithSpecificLength(MAXINSTAGRAMARLINKLENGTH),
                InvolvedPeople = CreateStringWithSpecificLength(MAXINVOLVEDPEOPLELENGTH)
            };
            var request = new CreateStreetcodeCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.Streetcode.DateString);
        }

        [Theory]
        [InlineData(MAXALIASLENGTH + 1)]
        [InlineData(MAXALIASLENGTH + 100)]
        public void BaseStreetcode_AliasLengthGraterThanAllowed_ShouldHaveErrors(int length)
        {
            // Arrange
            var dto = new BaseStreetcodeDto()
            {
                Teaser = CreateStringWithSpecificLength(MAXTEASERLENGTH),
                ShortDescription = CreateStringWithSpecificLength(MAXSHORTSECRIPTIONLENGTH),
                TransliterationUrl = CreateStringWithSpecificLength(MAXURLLENGTH),
                DateString = CreateStringWithSpecificLength(MAXDATESTRINGLENGTH),
                Alias = CreateStringWithSpecificLength(length),
                Title = CreateStringWithSpecificLength(MAXTITLELENGTH),
                Tags = CreateCollectionWithSpecificLength(MAXNUMBEROFTAGS),
                InstagramARLink = CreateStringWithSpecificLength(MAXINSTAGRAMARLINKLENGTH),
                InvolvedPeople = CreateStringWithSpecificLength(MAXINVOLVEDPEOPLELENGTH)
            };
            var request = new CreateStreetcodeCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.Streetcode.Alias);
        }

        [Theory]
        [InlineData(MAXTITLELENGTH + 1)]
        [InlineData(MAXTITLELENGTH + 100)]
        public void BaseStreetcode_TitleLengthGraterThanAllowed_ShouldHaveErrors(int length)
        {
            // Arrange
            var dto = new BaseStreetcodeDto()
            {
                Teaser = CreateStringWithSpecificLength(MAXTEASERLENGTH),
                ShortDescription = CreateStringWithSpecificLength(MAXSHORTSECRIPTIONLENGTH),
                TransliterationUrl = CreateStringWithSpecificLength(MAXURLLENGTH),
                DateString = CreateStringWithSpecificLength(MAXDATESTRINGLENGTH),
                Alias = CreateStringWithSpecificLength(MAXALIASLENGTH),
                Title = CreateStringWithSpecificLength(length),
                Tags = CreateCollectionWithSpecificLength(MAXNUMBEROFTAGS),
                InstagramARLink = CreateStringWithSpecificLength(MAXINSTAGRAMARLINKLENGTH),
                InvolvedPeople = CreateStringWithSpecificLength(MAXINVOLVEDPEOPLELENGTH)
            };
            var request = new CreateStreetcodeCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.Streetcode.Title);
        }

        [Theory]
        [InlineData(MAXNUMBEROFTAGS + 1)]
        [InlineData(MAXNUMBEROFTAGS + 10)]
        public void BaseStreetcode_TagsCollectionLengthGraterThanAllowed_ShouldHaveErrors(int length)
        {
            // Arrange
            var dto = new BaseStreetcodeDto()
            {
                Teaser = CreateStringWithSpecificLength(MAXTEASERLENGTH),
                ShortDescription = CreateStringWithSpecificLength(MAXSHORTSECRIPTIONLENGTH),
                TransliterationUrl = CreateStringWithSpecificLength(MAXURLLENGTH),
                DateString = CreateStringWithSpecificLength(MAXDATESTRINGLENGTH),
                Alias = CreateStringWithSpecificLength(MAXALIASLENGTH),
                Title = CreateStringWithSpecificLength(MAXTITLELENGTH),
                Tags = CreateCollectionWithSpecificLength(length),
                InstagramARLink = CreateStringWithSpecificLength(MAXINSTAGRAMARLINKLENGTH),
                InvolvedPeople = CreateStringWithSpecificLength(MAXINVOLVEDPEOPLELENGTH)
            };
            var request = new CreateStreetcodeCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.Streetcode.Tags);
        }

        [Theory]
        [InlineData(MAXINSTAGRAMARLINKLENGTH + 1)]
        [InlineData(MAXINSTAGRAMARLINKLENGTH + 100)]
        public void BaseStreetcode_InstagramARLinkLengthGraterThanAllowed_ShouldHaveErrors(int length)
        {
            // Arrange
            var dto = new BaseStreetcodeDto()
            {
                Teaser = CreateStringWithSpecificLength(MAXTEASERLENGTH),
                ShortDescription = CreateStringWithSpecificLength(MAXSHORTSECRIPTIONLENGTH),
                TransliterationUrl = CreateStringWithSpecificLength(MAXURLLENGTH),
                DateString = CreateStringWithSpecificLength(MAXDATESTRINGLENGTH),
                Alias = CreateStringWithSpecificLength(MAXALIASLENGTH),
                Title = CreateStringWithSpecificLength(MAXTITLELENGTH),
                Tags = CreateCollectionWithSpecificLength(MAXNUMBEROFTAGS),
                InstagramARLink = CreateStringWithSpecificLength(length),
                InvolvedPeople = CreateStringWithSpecificLength(MAXINVOLVEDPEOPLELENGTH)
            };
            var request = new CreateStreetcodeCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.Streetcode.InstagramARLink);
        }

        [Theory]
        [InlineData(MAXINVOLVEDPEOPLELENGTH + 1)]
        [InlineData(MAXINVOLVEDPEOPLELENGTH + 100)]
        public void BaseStreetcode_InvolvedPeopleLengthGraterThanAllowed_ShouldHaveErrors(int length)
        {
            // Arrange
            var dto = new BaseStreetcodeDto()
            {
                Teaser = CreateStringWithSpecificLength(MAXTEASERLENGTH),
                ShortDescription = CreateStringWithSpecificLength(MAXSHORTSECRIPTIONLENGTH),
                TransliterationUrl = CreateStringWithSpecificLength(MAXURLLENGTH),
                DateString = CreateStringWithSpecificLength(MAXDATESTRINGLENGTH),
                Alias = CreateStringWithSpecificLength(MAXALIASLENGTH),
                Title = CreateStringWithSpecificLength(MAXTITLELENGTH),
                Tags = CreateCollectionWithSpecificLength(MAXNUMBEROFTAGS),
                InstagramARLink = CreateStringWithSpecificLength(MAXINSTAGRAMARLINKLENGTH),
                InvolvedPeople = CreateStringWithSpecificLength(length)
            };
            var request = new CreateStreetcodeCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldHaveValidationErrorFor(x => x.Streetcode.InvolvedPeople);
        }

        [Fact]
        public void BaseStreetcode_ValidDto_ShouldNotHaveErrors()
        {
            // Arrange
            var dto = new BaseStreetcodeDto()
            {
                Teaser = CreateStringWithSpecificLength(MAXTEASERLENGTH),
                ShortDescription = CreateStringWithSpecificLength(MAXSHORTSECRIPTIONLENGTH),
                TransliterationUrl = CreateStringWithSpecificLength(MAXURLLENGTH),
                DateString = CreateStringWithSpecificLength(MAXDATESTRINGLENGTH),
                Alias = CreateStringWithSpecificLength(MAXALIASLENGTH),
                Title = CreateStringWithSpecificLength(MAXTITLELENGTH),
                Tags = CreateCollectionWithSpecificLength(MAXNUMBEROFTAGS),
                InstagramARLink = CreateStringWithSpecificLength(MAXINSTAGRAMARLINKLENGTH),
                InvolvedPeople = CreateStringWithSpecificLength(MAXINVOLVEDPEOPLELENGTH)
            };
            var request = new CreateStreetcodeCommand(dto);

            // Act
            var validationResult = _validator.TestValidate(request);

            // Assert
            validationResult.ShouldNotHaveValidationErrorFor(x => x.Streetcode.Teaser);
            validationResult.ShouldNotHaveValidationErrorFor(x => x.Streetcode.ShortDescription);
            validationResult.ShouldNotHaveValidationErrorFor(x => x.Streetcode.TransliterationUrl);
            validationResult.ShouldNotHaveValidationErrorFor(x => x.Streetcode.DateString);
            validationResult.ShouldNotHaveValidationErrorFor(x => x.Streetcode.Alias);
            validationResult.ShouldNotHaveValidationErrorFor(x => x.Streetcode.Title);
            validationResult.ShouldNotHaveValidationErrorFor(x => x.Streetcode.Tags);
            validationResult.ShouldNotHaveValidationErrorFor(x => x.Streetcode.InstagramARLink);
            validationResult.ShouldNotHaveValidationErrorFor(x => x.Streetcode.InvolvedPeople);
        }

        private string CreateStringWithSpecificLength(int length)
        {
            string character = "A";
            var result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(character);
            }

            return result.ToString();
        }

        private IEnumerable<StreetcodeTagDto> CreateCollectionWithSpecificLength(int length)
        {
            var list = new List<StreetcodeTagDto>(length);

            for (int i = 1; i <= length; i++)
            {
                list.Add(new StreetcodeTagDto { Id = i });
            }

            return list;
        }
    }
}
