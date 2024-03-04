namespace BarLib
{
    public class Beer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double? Abv { get; set; }

        public override string ToString()
        {
            return $"{Id} | Name: {Name} - {Abv}%";
        }

        // Validates Name
        // Name must not be null
        // Name must be at least 3 characters
        public void ValidateName() 
        {
            // Checks if Name is null
            if ( Name == null)
            {
                throw new ArgumentNullException("Name cannot be null");
            }

            // Checks if Name consists of at least 3 characters
            if (Name.Length < 3)
            {
                throw new ArgumentException("Name must be at least 3 characters");
            }
        }

        // Validates Abv
        // Abv must not be Null
        // Abv must be 0 < Abv > 67
        public void ValidateAbv() 
        {
            // Checks if Abv is Null
            if (!Abv.HasValue)
            {
                throw new ArgumentNullException("Abv must be set");
            }

            // Checks if Abv is in range
            if (Abv < 0 || Abv > 67)
            {
                throw new ArgumentOutOfRangeException("Abv must be between 0% and 67%");
            }
        }

        // Validates Name and Abv
        public void Validate()
        {
            ValidateName();
            ValidateAbv();
        }
    }
}
