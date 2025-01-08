using AskHub.Models;
using Microsoft.AspNetCore.Identity;

namespace AskHub.Repositories
{
    public class UserFollowerSqlRepository : IUserFollowerRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _appDbContext;

        public UserFollowerSqlRepository(UserManager<AppUser> userManager, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _appDbContext = appDbContext;
        }

        public void AddFollow(string followerUsername, string followingUsername)
        {
            var existingFollow = _appDbContext.FollowUsers
                                      .FirstOrDefault(uf => uf.FollowerUsername == followerUsername && uf.FollowingUsername == followingUsername);

            AppUser? follower = _userManager.Users.FirstOrDefault(u => u.UserName == followerUsername);
            AppUser? following = _userManager.Users.FirstOrDefault(u => u.UserName == followingUsername);

            if (existingFollow == null && follower != null && following != null)
            {
                var follow = new FollowUser
                {
                    FollowerUsername = followerUsername,
                    FollowingUsername = followingUsername,
                    //Follower = follower,
                    //Following = following
                };

                _appDbContext.FollowUsers.Add(follow);
                _appDbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Follow Already Exists");
            }
        }

        public void RemoveFollow(string followerUsername, string followingUsername)
        {
            var follow = _appDbContext.FollowUsers
                                      .FirstOrDefault(uf => uf.FollowerUsername == followerUsername && uf.FollowingUsername == followingUsername);

            if (follow != null)
            {
                _appDbContext.FollowUsers.Remove(follow);
                _appDbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Follow Doesn't Exist");
            }
        }

        public int CountFollowers(string username)
        {
            return _appDbContext.FollowUsers
                                .Count(uf => uf.FollowingUsername == username);
        }

        public List<string> GetFollowersUsernames(string username)
        {
            var followers = _appDbContext.FollowUsers
                                         .Where(uf => uf.FollowingUsername == username)
                                         .Select(uf => uf.FollowerUsername) // Select the Follower AppUser
                                         .ToList();

            
                return followers;
            
            
        }

        public List<string> GetFollowingsUsernames(string username)
        {
            var followings = _appDbContext.FollowUsers
                                          .Where(uf => uf.FollowerUsername == username)
                                          .Select(uf => uf.FollowingUsername) // Select the Following AppUser
                                          .ToList();

            
              return followings;
            
        }

        public bool IsFollowing(string followerUsername, string followingUsername)
        {
            var result = _appDbContext.FollowUsers.FirstOrDefault(uf => uf.FollowerUsername == followerUsername
                                                                        && uf.FollowingUsername == followingUsername);

            return result != null;
        }
    }
}
