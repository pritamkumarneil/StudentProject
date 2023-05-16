namespace StudentProject.Models
{
    public class StudentAddress
    {
        //Properties of the class
        public int StudentAddressId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int PinCode { get; set; }

        // Navigational Properties
        public int StudentId { get; set; }
        public virtual Student student { get; set; } = null;
    }
}
