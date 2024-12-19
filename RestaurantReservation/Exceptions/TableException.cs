namespace RestaurantReservation.Exceptions
{
    public class TableException:Exception
    {
        public TableException()
        {

        }
        public TableException(string message) : base(message) { }

        public TableException(string message, Exception inner) : base(message, inner) { }
       
    }
}
