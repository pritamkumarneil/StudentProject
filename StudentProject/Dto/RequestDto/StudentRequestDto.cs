namespace StudentProject.Dto.RequestDto
{
    public class StudentRequestDto
    {
        public string? Name { get; set; }
        public string EmailId { get; set; }=String.Empty;
        public string MobNo { get; set; }=string.Empty; 
        public int Age { get; set; }
        public string Branch { get; set; } = string.Empty;
        public DateTime StudentDateOfBirth { get; set; }
        public string StandardName { get; set; }
    }
}
