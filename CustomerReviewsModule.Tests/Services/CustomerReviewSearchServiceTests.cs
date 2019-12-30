using System;
using System.Linq;
using AutoFixture;
using CustomerReviewsModule.Core.Models;
using CustomerReviewsModule.Core.Services;
using CustomerReviewsModule.Data.Models;
using CustomerReviewsModule.Data.Repositories;
using CustomerReviewsModule.Data.Services;
using FluentAssertions;
using Moq;
using VirtoCommerce.Domain.Commerce.Model.Search;
using Xunit;

namespace CustomerReviewsModule.Tests.Services
{
    public class CustomerReviewSearchServiceTests
    {
        private Fixture randomizer;
        private Mock<ICustomerReviewRepository> repository;
        private Mock<ICustomerReviewService> customerReviewService;
        private CustomerReviewSearchService service;

        public CustomerReviewSearchServiceTests()
        {
            randomizer = new Fixture();
            repository = new Mock<ICustomerReviewRepository>();
            customerReviewService = new Mock<ICustomerReviewService>();
            service = new CustomerReviewSearchService(() => repository.Object, customerReviewService.Object);
        }

        [Fact]
        public void SearchCustomerReviews_ShouldThrowArgumentException_IfCriteriaIsNull()
        {
            //arrange
            CustomerReviewSearchCriteria criteria = null;

            //act
            Action act = () => service.SearchCustomerReviews(criteria);

            //assert
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void SearchCustomerReviews_ShouldReturnProductsByIds_IfProductIdsIsSet()
        {
            //arrange
            var productIds = randomizer.CreateMany<string>(2).ToArray();
            var criteria = new CustomerReviewSearchCriteria
            {
                ProductIds = productIds,
                Take = 2
            };

            var customerReviewEntities = randomizer.CreateMany<CustomerReviewEntity>(2);
            customerReviewEntities.ElementAt(0).ProductId = productIds[0];
            repository.Setup(p => p.CustomerReviews).Returns(customerReviewEntities.AsQueryable());

            var customerReviews = randomizer.CreateMany<CustomerReview>(1);
            customerReviews.ElementAt(0).Id = customerReviewEntities.ElementAt(0).Id;
            customerReviewService
                .Setup(p => p.GetByIds(It.IsAny<string[]>()))
                .Returns(customerReviews.ToArray());

            //act
            var result = service.SearchCustomerReviews(criteria);

            //assert
            result.Should().BeEquivalentTo(new GenericSearchResult<CustomerReview>
            {
                TotalCount = 1,
                Results = new[]
                {
                    new CustomerReview
                    {
                        Id = customerReviews.ElementAt(0).Id,
                        CreatedDate = customerReviews.ElementAt(0).CreatedDate,
                        ModifiedDate = customerReviews.ElementAt(0).ModifiedDate,
                        CreatedBy = customerReviews.ElementAt(0).CreatedBy,
                        ModifiedBy = customerReviews.ElementAt(0).ModifiedBy,
                        AuthorNickname = customerReviews.ElementAt(0).AuthorNickname,
                        Content = customerReviews.ElementAt(0).Content,
                        IsActive = customerReviews.ElementAt(0).IsActive,
                        ProductId = productIds[0]
                    }
                }
            });
        }

        [Fact]
        public void SearchCustomerReviews_ShouldReturnActiveProducts_IfIsActiveIsSet()
        {
            //arrange
            //var productIds = randomizer.CreateMany<string>(2).ToArray();
            var criteria = new CustomerReviewSearchCriteria
            {

                Take = 2
            };

            var customerReviewEntities = randomizer.CreateMany<CustomerReviewEntity>(2);
            //customerReviewEntities.ElementAt(0).ProductId = productIds[0];
            repository.Setup(p => p.CustomerReviews).Returns(customerReviewEntities.AsQueryable());

            var customerReviews = randomizer.CreateMany<CustomerReview>(1);
            customerReviews.ElementAt(0).Id = customerReviewEntities.ElementAt(0).Id;
            //customerReviewService
            //    .Setup(p => p.GetByIds(It.IsAny<string[]>()))
            //    .Returns(p => customerReviews.Where(r => p.Contains(r.Id)).ToArray());

            //act
            var result = service.SearchCustomerReviews(criteria);

            //assert
            result.Should().BeEquivalentTo(new GenericSearchResult<CustomerReview>
            {
                TotalCount = 1,
                Results = new[]
                {
                    new CustomerReview
                    {
                        Id = customerReviews.ElementAt(0).Id,
                        CreatedDate = customerReviews.ElementAt(0).CreatedDate,
                        ModifiedDate = customerReviews.ElementAt(0).ModifiedDate,
                        CreatedBy = customerReviews.ElementAt(0).CreatedBy,
                        ModifiedBy = customerReviews.ElementAt(0).ModifiedBy,
                        AuthorNickname = customerReviews.ElementAt(0).AuthorNickname,
                        Content = customerReviews.ElementAt(0).Content,
                        IsActive = customerReviews.ElementAt(0).IsActive,
                        //ProductId = productIds[0]
                    }
                }
            });
        }
    }
}
