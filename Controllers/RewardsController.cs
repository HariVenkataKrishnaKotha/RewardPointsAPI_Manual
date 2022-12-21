using Microsoft.AspNetCore.Mvc;
using RewardPointsAPI_Manual.Models;
using System.Linq;

namespace RewardPointsAPI_Manual.Controllers
{
    [Route("api/rewards")]
    public class RewardsController : Controller
    {
        [HttpGet]
        [ProducesResponseType(200)]
        [ApiExplorerSettings(IgnoreApi = false)]
        public IActionResult GetRewards([FromQuery] Customer customer)
        {
            // Calculate reward points for the customer
            var points = CalculateRewardPoints(customer);

            return Ok(new { customer.Name, Points = points });
        }

        public static int CalculateRewardPoints(Customer customer)
        {
            int points = 0;
            points += (customer.January - 50) > 0 ? (int)(customer.January - 50) : 0;
            points += (customer.January - 100) > 0 ? (int)(customer.January - 100) : 0;
            points += (customer.February - 50) > 0 ? (int)(customer.February - 50) : 0;
            points += (customer.February - 100) > 0 ? (int)(customer.February - 100) : 0;
            points += (customer.March - 50) > 0 ? (int)(customer.March - 50) : 0;
            points += (customer.March - 100) > 0 ? (int)(customer.March - 100) : 0;
            return points;
        }
    }
}
