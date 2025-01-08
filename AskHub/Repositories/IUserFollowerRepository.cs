namespace AskHub.Repositories
{
    public interface IUserFollowerRepository
    {
        public void AddFollow(string followerUsername, string followingUsername);

        public void RemoveFollow(string followerUsername, string followingUsername);

        public int CountFollowers(string username);

        public bool IsFollowing(string followerUsername, string followingUsername);

        public List<string> GetFollowersUsernames(string username);

        public List<string> GetFollowingsUsernames(string username);
    }
}
