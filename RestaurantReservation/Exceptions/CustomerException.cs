namespace RestaurantReservation.Exceptions
{
    public class CustomerException : Exception
    {
        public CustomerException()
        {
            
        }
        public CustomerException(string message) : base(message) { }

        public CustomerException(string message, Exception inner) : base(message, inner) { }
        public static CustomerException EmailAlreadyExists(string message) => new CustomerException(message);
    }
}