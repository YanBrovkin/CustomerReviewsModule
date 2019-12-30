using System.Web.Http;
using System.Web.Http.Description;
using CustomerReviewsModule.Core.Models;
using CustomerReviewsModule.Core.Services;
using VirtoCommerce.Domain.Commerce.Model.Search;
using VirtoCommerce.Platform.Core.Web.Security;

namespace CustomerReviewsModule.Web.Controllers.Api
{
    [RoutePrefix("api/customerReviews")]
    public class CustomerReviewsModuleController : ApiController
    {
        private readonly ICustomerReviewSearchService _customerReviewSearchService;

        public CustomerReviewsModuleController(ICustomerReviewSearchService customerReviewSearchService)
        {
            _customerReviewSearchService = customerReviewSearchService;
        }

        [HttpPost]
        [Route("search")]
        [ResponseType(typeof(GenericSearchResult<CustomerReview>))]
        [CheckPermission(Permission = Core.ModuleConstants.Security.Permissions.Read)]
        public IHttpActionResult SearchCustomerReviews(CustomerReviewSearchCriteria criteria)
        {
            var result = _customerReviewSearchService.SearchCustomerReviews(criteria);
            return Ok(result);
        }
    }
}
