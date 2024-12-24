using Microsoft.AspNetCore.Mvc.Filters;

namespace RestaurantReservation.Filters
{
    public class ResultFilterImplemented:ResultFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine("Result filter excuting...");
            base.OnResultExecuting(context);
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine("Result filter executed...");
            base.OnResultExecuted(context);
        }
    }
}
