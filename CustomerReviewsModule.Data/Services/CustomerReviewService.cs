﻿using System;
using System.Linq;
using CustomerReviewsModule.Core.Models;
using CustomerReviewsModule.Core.Services;
using CustomerReviewsModule.Data.Models;
using CustomerReviewsModule.Data.Repositories;
using VirtoCommerce.Platform.Core.Common;
using VirtoCommerce.Platform.Data.Infrastructure;

namespace CustomerReviewsModule.Data.Services
{
    public class CustomerReviewService : ServiceBase, ICustomerReviewService
    {
        private readonly Func<ICustomerReviewRepository> _repositoryFactory;

        public CustomerReviewService(Func<ICustomerReviewRepository> repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public CustomerReview[] GetByIds(string[] ids)
        {
            using (var repository = _repositoryFactory())
            {
                return repository
                    .GetByIds(ids)
                    .Select(x => x.ToModel(AbstractTypeFactory<CustomerReview>.TryCreateInstance()))
                    .ToArray();
            }
        }

        public void SaveCustomerReviews(CustomerReview[] items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            var pkMap = new PrimaryKeyResolvingMap();
            using (var repository = _repositoryFactory())
            {
                using (var changeTracker = GetChangeTracker(repository))
                {
                    var alreadyExistEntities = repository
                        .GetByIds(items.Where(m => !m.IsTransient())
                        .Select(x => x.Id)
                        .ToArray());
                    foreach (var derivativeContract in items)
                    {
                        var sourceEntity = AbstractTypeFactory<CustomerReviewEntity>.TryCreateInstance().FromModel(derivativeContract, pkMap);
                        var targetEntity = alreadyExistEntities.FirstOrDefault(x => x.Id == sourceEntity.Id);
                        if (targetEntity == null)
                        {
                            repository.Add(sourceEntity);
                            continue;
                        }
                        changeTracker.Attach(targetEntity);
                        sourceEntity.Patch(targetEntity);
                    }

                    CommitChanges(repository);
                    pkMap.ResolvePrimaryKeys();
                }
            }
        }

        public void DeleteCustomerReviews(string[] ids)
        {
            var items = GetByIds(ids);

            using (var repository = _repositoryFactory())
            {
                repository.RemoveByIds(items.Select(x => x.Id).ToArray());
                CommitChanges(repository);
            }
        }

        public int GetAverageRating(string productId)
        {
            if (productId.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(productId));
            using (var repository = _repositoryFactory())
            {
                return Convert.ToInt32(repository.CustomerReviews.Where(r => r.ProductId == productId).Average(r => (int?)r.Rating) ?? default);
            }
        }
    }
}
