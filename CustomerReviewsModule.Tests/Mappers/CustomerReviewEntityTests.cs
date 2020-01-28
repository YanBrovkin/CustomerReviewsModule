using System;
using AutoFixture;
using CustomerReviewsModule.Core.Models;
using CustomerReviewsModule.Data.Models;
using FluentAssertions;
using VirtoCommerce.Platform.Core.Common;
using Xunit;

namespace CustomerReviewsModule.Tests.Mappers
{
    public class CustomerReviewEntityTests
    {
        private readonly Fixture randomizer;

        public CustomerReviewEntityTests()
        {
            randomizer = new Fixture();
        }

        [Fact]
        public void ToModel_ShouldThrowArgumentNullException_IfInputModelIsNull()
        {
            //arrange
            var entity = randomizer.Create<CustomerReviewEntity>();
            CustomerReview inputModel = null;

            //act
            Action act = () => entity.ToModel(inputModel);

            //assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ToModel_ShouldReturnCustomerReview_IfInputModelIsProper()
        {
            //arrange
            var entity = randomizer.Create<CustomerReviewEntity>();
            var inputModel = randomizer.Create<CustomerReview>();

            //act
            var result = entity.ToModel(inputModel);

            //assert
            result.Should().BeEquivalentTo(
                new CustomerReview
                {
                    Id = entity.Id,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    ModifiedBy = entity.ModifiedBy,
                    ModifiedDate = entity.ModifiedDate,
                    AuthorNickname = entity.AuthorNickname,
                    Content = entity.Content,
                    IsActive = entity.IsActive,
                    ProductId = entity.ProductId,
                    Rating = entity.Rating
                });
        }

        [Fact]
        public void FromModel_ShouldThrowArgumentNullException_IfInputModelIsNull()
        {
            //arrange
            var entity = randomizer.Create<CustomerReviewEntity>();
            CustomerReview inputModel = null;
            var map = randomizer.Create<PrimaryKeyResolvingMap>();

            //act
            Action act = () => entity.FromModel(inputModel, map);

            //assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void FromModel_ShouldReturnCustomerReviewEntity_IfInputModelIsProper()
        {
            //arrange
            var entity = randomizer.Create<CustomerReviewEntity>();
            var inputModel = randomizer.Create<CustomerReview>();
            var map = randomizer.Create<PrimaryKeyResolvingMap>();

            //act
            var result = entity.FromModel(inputModel, map);

            //assert
            result.Should().BeEquivalentTo(
                new CustomerReviewEntity
                {
                    Id = entity.Id,
                    CreatedBy = entity.CreatedBy,
                    CreatedDate = entity.CreatedDate,
                    ModifiedBy = entity.ModifiedBy,
                    ModifiedDate = entity.ModifiedDate,
                    AuthorNickname = entity.AuthorNickname,
                    Content = entity.Content,
                    IsActive = entity.IsActive,
                    ProductId = entity.ProductId,
                    Rating = entity.Rating
                });
        }

        [Fact]
        public void Patch_ShouldThrowArgumentNullException_IfInputModelIsNull()
        {
            //arrange
            var sourceEntity = randomizer.Create<CustomerReviewEntity>();
            CustomerReviewEntity destinationEntity = null;

            //act
            Action act = () => sourceEntity.Patch(destinationEntity);

            //assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Patch_ShouldProperlyUpdateTargetEntity_IfDataIsProper()
        {
            //arrange
            var sourceEntity = randomizer.Create<CustomerReviewEntity>();
            var destinationEntity = randomizer.Create<CustomerReviewEntity>();

            //act
            sourceEntity.Patch(destinationEntity);

            //assert
            destinationEntity.Should().BeEquivalentTo(
                new CustomerReviewEntity
                {
                    Id = destinationEntity.Id,
                    CreatedBy = destinationEntity.CreatedBy,
                    CreatedDate = destinationEntity.CreatedDate,
                    ModifiedBy = destinationEntity.ModifiedBy,
                    ModifiedDate = destinationEntity.ModifiedDate,
                    AuthorNickname = sourceEntity.AuthorNickname,
                    Content = sourceEntity.Content,
                    IsActive = sourceEntity.IsActive,
                    ProductId = sourceEntity.ProductId,
                    Rating = sourceEntity.Rating
                });
        }
    }
}
