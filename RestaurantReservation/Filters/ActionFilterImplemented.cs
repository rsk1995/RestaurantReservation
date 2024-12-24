using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace RestaurantReservation.Filters
{
    public class ActionFilterImplemented : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine("Action is executing...");
            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine("Action is executed...");
            base.OnActionExecuted(context);
        }

    }
}
