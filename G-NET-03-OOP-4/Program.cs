using System;

namespace OOPAssignment04
{
    #region SUPPORTING CLASSES

    public class Projector
    {
        public void Start() => Console.WriteLine("Projector started.");
        public void Stop() => Console.WriteLine("Projector stopped.");
    }

    #endregion

    #region PART 02 - QUESTION 1: BASE TICKET CLASS

    public class Ticket
    {
        private static int totalTickets = 0;

        private string movieName = "Unknown";
        private decimal price = 1m;

        public int TicketId { get; }

        public string MovieName
        {
            get => movieName;
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    movieName = value;
                }
            }
        }

        public decimal Price
        {
            get => price;
            set
            {
                if (value > 0)
                {
                    price = value;
                }
            }
        }

        public decimal PriceAfterTax => Price * 1.14m;

        public Ticket(string movieName, decimal price)
        {
            TicketId = ++totalTickets;
            MovieName = movieName;
            Price = price;
        }

        public static int GetTotalTickets() => totalTickets;

        #region Q1.b - SetPrice Overloading

        public void SetPrice(decimal newPrice)
        {
            Price = newPrice;
        }

        public void SetPrice(decimal basePrice, decimal multiplier)
        {
            Price = basePrice * multiplier;
        }

        #endregion
    }
    #endregion
}

