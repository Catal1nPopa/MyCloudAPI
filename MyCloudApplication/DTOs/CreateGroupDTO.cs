namespace MyCloudApplication.DTOs
{
    public class CreateGroupDTO
    {
        public string Name { get; set; }
        public int[] Users { get; set; }
        public DateTime CreateDate { get; set; }
        public double TotalSpace { get; set; }
    }
}
