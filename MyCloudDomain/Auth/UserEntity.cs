namespace MyCloudDomain.Auth
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname{ get; set; }
        public string Email { get; set; }
        public double AllocatedSpace { get; set; }
        public double AvailableSpace { get; set; }
        public DateTime DateAdd { get; set; }
        public string Company { get; set; }
        public DateTime ContratDate { get; set; }
        public string Function { get; set; }
        public byte[] UserImage { get; set; }
    }
}
