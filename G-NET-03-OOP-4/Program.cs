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

    #region Q1.a - Virtual PrintTicket Method

        public virtual void PrintTicket()
        {
            Console.Write($"Ticket #{TicketId} | {MovieName} | Price: {Price:F0} EGP | After Tax: {PriceAfterTax:F2} EGP");
        }

        #endregion
    }
    #endregion
    #region PART 02 - QUESTION 2: CHILD TICKET CLASSES

    public class StandardTicket : Ticket
    {
        public string SeatNumber { get; set; }

        public StandardTicket(string movieName, decimal price, string seatNumber)
            : base(movieName, price)
        {
            SeatNumber = seatNumber;
        }

        public override void PrintTicket()
        {
            base.PrintTicket();
            Console.WriteLine();
            Console.WriteLine($"  Seat: {SeatNumber}");
        }
    }

    public class VIPTicket : Ticket
    {
        public bool LoungeAccess { get; set; }
        public decimal ServiceFee { get; } = 50m;

        public VIPTicket(string movieName, decimal price, bool loungeAccess)
            : base(movieName, price)
        {
            LoungeAccess = loungeAccess;
        }

        public override void PrintTicket()
        {
            base.PrintTicket();
            Console.WriteLine();
            string loungeStr = LoungeAccess ? "Yes" : "No";
            Console.WriteLine($"  Lounge: {loungeStr} | Service Fee: {ServiceFee:F0} EGP");
        }
    }

    public class IMAXTicket : Ticket
    {
        public bool Is3D { get; set; }

        public IMAXTicket(string movieName, decimal price, bool is3D)
            : base(movieName, price + (is3D ? 30m : 0m))
        {
            Is3D = is3D;
        }

        public override void PrintTicket()
        {
            base.PrintTicket();
            Console.WriteLine();
            string is3DStr = Is3D ? "Yes" : "No";
            Console.WriteLine($"  IMAX 3D: {is3DStr}");
        }
    }

    #endregion
}

