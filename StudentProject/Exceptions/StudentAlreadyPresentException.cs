namespace StudentProject.Exceptions
{
    public class StudentAlreadyPresentException : Exception
    {
        public StudentAlreadyPresentException(string message) : base(message) { }
    }
}
