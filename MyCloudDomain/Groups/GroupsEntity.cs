namespace MyCloudDomain.Groups
{
    public class GroupsEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int[] Users { get; set; }
        public DateTime CreateDate { get; set; }
        public double TotalSpace { get; set; }
    }
}
