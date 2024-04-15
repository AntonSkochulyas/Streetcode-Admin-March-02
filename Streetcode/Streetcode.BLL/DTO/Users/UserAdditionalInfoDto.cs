namespace Streetcode.BLL.Dto.Users
{
    public class UserAdditionalInfoDto
    {
        public int Id { get; set; }
        public string? Phone { get; set; }
        public string? FirstName { get; set; }

        public string? SecondName { get; set; }

        public string? ThirdName { get; set; }

        public string? Email { get; set; }

        public ushort Age { get; set; }
    }
}
