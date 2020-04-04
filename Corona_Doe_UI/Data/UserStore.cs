using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Corona_Doe_UI.Data
{
    public class UserStore : IUserStore<User>,
        IUserPasswordStore<User>
    {
        public async Task<IdentityResult> CreateAsync(User user,
            CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
                return IdentityResult.Failed(new IdentityError { Description = "Operation was canceled." });
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "Argument was null." });

            try
            {
                await Task.Delay(0);
                //var result = await d.Admin.Insert(user);

                //if (result.Status != e.Status.Success)
                //    return IdentityResult.Failed(new IdentityError { Description = result.Msg });

                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = ex.Message });
            }
        }

        public async Task<IdentityResult> DeleteAsync(User user,
            CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
                return IdentityResult.Failed(new IdentityError { Description = "Operation was canceled." });
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "Argument was null." });

            try
            {
                await Task.Delay(0);
                //var result = await d.Admin.Delete(user.AmId);

                //if (result.Status != e.Status.Success)
                //    return IdentityResult.Failed(new IdentityError { Description = result.Msg });

                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = ex.Message });
            }
        }

        public async Task<User> FindByIdAsync(string userId,
            CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested || userId == null)
                return null;

            if (!Guid.TryParse(userId, out Guid idGuid))
                return null;

            await Task.Delay(0);
            //return await d.Admin.Get(idGuid);

            if (userId == "54dbd7d3-ed40-4908-9f1f-3735931a4a9f")
                return new User { login_id = "admin", pwd = "AQAAAAEAACcQAAAAEP3BlQw/Ei+Vh6yf8ZEOzDTCN4iUf2QIaR1AW0zm3Y26Bl7eK3kj+H75yqdsaE+vFA==" };
            else
                return null;
        }

        public async Task<User> FindByNameAsync(string userName,
            CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested || userName == null)
                return null;

            await Task.Delay(0);
            //return await d.Admin.Get(idGuid);

            if (userName.ToLower() == "admin")
                return new User { login_id = "admin", pwd = "AQAAAAEAACcQAAAAEP3BlQw/Ei+Vh6yf8ZEOzDTCN4iUf2QIaR1AW0zm3Y26Bl7eK3kj+H75yqdsaE+vFA==" };
            else
                return null;
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested || user == null)
                return null;

            return Task.FromResult(user.pwd);
        }

        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested || user == null)
                return null;

            return Task.FromResult(user.user_id.ToString());
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested || user == null)
                return null;

            return Task.FromResult(user.login_id);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.FromResult<object>(null);
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested || user == null || passwordHash == null)
                return null;

            user.pwd = passwordHash;
            return Task.FromResult<object>(null);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested || user == null || userName == null)
                return null;

            user.login_id = userName;
            return Task.FromResult<object>(null);
        }

        public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return IdentityResult.Failed(new IdentityError { Description = "Operation was canceled." });
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "Argument was null." });

            try
            {
                await Task.Delay(0);
                //var result = await d.Admin.Update(user);

                //if (result.Status != e.Status.Success)
                //    return IdentityResult.Failed(new IdentityError { Description = result.Msg });

                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = ex.Message });
            }
        }

        public void Dispose()
        {
        }
    }
}
