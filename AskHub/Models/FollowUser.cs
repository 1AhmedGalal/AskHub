namespace AskHub.Models
{
    public class FollowUser
    {
        public int Id { get; set; }

        public string FollowerUsername { get; set; } = null!;
        //public AppUser Follower { get; set; } = null!;

        public string FollowingUsername { get; set; } = null!;
        //public AppUser Following { get; set; } = null!;
    }
}
