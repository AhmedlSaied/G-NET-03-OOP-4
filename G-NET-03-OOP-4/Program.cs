using System;

namespace OOPAssignment04
{

    #region PART 01: THEORETICAL QUESTIONS ANSWERS

    /*
     * Q1 : Difference between static binding and dynamic binding:
     * - Static Binding (Early Binding): Resolved at compile time. Method calls are bound directly
     *   to the object's declared type (e.g., non-virtual methods, overloaded methods).
     * - Dynamic Binding (Late Binding): Resolved at runtime. Method calls are resolved based on
     *   the actual underlying object type using the vtable (e.g., overridden virtual/abstract methods).
     * 
     * Q2 : Difference between method overloading and method overriding:
     * - Overloading: Multiple methods in the same class sharing the same name but different signatures
     *   (parameter count, types, or order). Represents compile-time polymorphism.
     * - Overriding: Redefining a base class virtual/abstract method in a derived class using the same
     *   signature. Represents runtime polymorphism.
     * 
     * Q3 : Keywords used for Method Overriding:
     * - virtual: Declared in the base class to allow derived classes to override the method execution.
     * - override: Declared in derived classes to replace/extend the base class implementation.
     * - base: Used inside derived classes to invoke the base class implementation explicitly (e.g., base.PrintTicket()).
     */

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
    #region PART 02 - QUESTION 3: CINEMA CLASS

    public class Cinema
    {
        public string CinemaName { get; set; }
        private readonly Projector projector;
        private readonly Ticket[] tickets = new Ticket[20];

        public Cinema(string cinemaName)
        {
            CinemaName = cinemaName;
            projector = new Projector();
        }

        public bool AddTicket(Ticket t)
        {
            for (int i = 0; i < tickets.Length; i++)
            {
                if (tickets[i] == null)
                {
                    tickets[i] = t;
                    return true;
                }
            }
            return false;
        }

        public void PrintAllTickets()
        {
            Console.WriteLine("========== All Tickets ==========");
            for (int i = 0; i < tickets.Length; i++)
            {
                if (tickets[i] != null)
                {
                    tickets[i].PrintTicket();
                }
            }
        }

        public void OpenCinema()
        {
            Console.WriteLine("========== Cinema Opened ==========");
            projector.Start();
        }

        public void CloseCinema()
        {
            Console.WriteLine("\n========== Cinema Closed ==========");
            projector.Stop();
            #endregion
        }
    }
}
#endregion