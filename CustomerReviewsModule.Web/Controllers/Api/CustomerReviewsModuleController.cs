using System.Net;
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
        private readonly ICustomerReviewService _customerReviewService;

        public CustomerReviewsModuleController(
            ICustomerReviewSearchService customerReviewSearchService,
            ICustomerReviewService customerReviewService)
        {
            _customerReviewSearchService = customerReviewSearchService;
            _customerReviewService = customerReviewService;
        }


        /// <summary>
        /// Return product Customer review search results
        /// </summary>
        [HttpPost]
        [Route("search")]
        [ResponseType(typeof(GenericSearchResult<CustomerReview>))]
        [CheckPermission(Permission = Core.ModuleConstants.Security.Permissions.Read)]
        public IHttpActionResult SearchCustomerReviews(CustomerReviewSearchCriteria criteria)
        {
            var result = _customerReviewSearchService.SearchCustomerReviews(criteria);
            return Ok(result);
        }

        /// <summary>
        ///  Create new or update existing customer review
        /// </summary>
        /// <param name="customerReviews">Customer reviews</param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(void))]
        [CheckPermission(Permission = Core.ModuleConstants.Security.Permissions.Update)]
        public IHttpActionResult Update(CustomerReview[] customerReviews)
        {
            _customerReviewService.SaveCustomerReviews(customerReviews);
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Delete Customer Reviews by IDs
        /// </summary>
        /// <param name="ids">IDs</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("")]
        [ResponseType(typeof(void))]
        [CheckPermission(Permission = Core.ModuleConstants.Security.Permissions.Delete)]
        public IHttpActionResult Delete([FromUri] string[] ids)
        {
            _customerReviewService.DeleteCustomerReviews(ids);
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Return average rating by product identitier
        /// </summary>
        [HttpGet]
        [Route("products/{productId}/averageRating")]
        [ResponseType(typeof(int))]
        [CheckPermission(Permission = Core.ModuleConstants.Security.Permissions.Read)]
        public IHttpActionResult GetAverageRating(string productId)
        {
            var averageRating = _customerReviewService.GetAverageRating(productId);
            return Ok(averageRating);
        }
    }
}
