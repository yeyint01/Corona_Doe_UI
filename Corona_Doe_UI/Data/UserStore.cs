using Microsoft.AspNetCore.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using d = DataAccess;
using e = Entity;

namespace Corona_Doe_UI.Data
{
    public class UserStore : IUserStore<e.user_account>,
        IUserPasswordStore<e.user_account>
    {
        public async Task<IdentityResult> CreateAsync(e.user_account user,
            CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
                return IdentityResult.Failed(new IdentityError { Description = "Operation was canceled." });
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "Argument was null." });

            try
            {
                var result = await d.user_account.Insert(user);

                if (result.Status != e.shared.Status.Success)
                    return IdentityResult.Failed(new IdentityError { Description = result.Msg });

                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = ex.Message });
            }
        }

        public async Task<IdentityResult> DeleteAsync(e.user_account user,
            CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
                return IdentityResult.Failed(new IdentityError { Description = "Operation was canceled." });
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "Argument was null." });

            try
            {
                var result = await d.user_account.Delete(user.user_id);

                if (result.Status != e.shared.Status.Success)
                    return IdentityResult.Failed(new IdentityError { Description = result.Msg });

                return IdentityResult.Success;
            }
            catch (Exception ex)
            {
                return IdentityResult.Failed(new IdentityError { Description = ex.Message });
            }
        }

        public async Task<e.user_account> FindByIdAsync(string userId,
            CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested || userId == null)
                return null;

            if (!int.TryParse(userId, out int iduser))
                return null;

            return await d.user_account.Get(iduser);
        }

        public async Task<e.user_account> FindByNameAsync(string userName,
            CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested || userName == null)
                return null;

            return await d.user_account.Get(userName);
        }

        public Task<string> GetNormalizedUserNameAsync(e.user_account user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(e.user_account user, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested || user == null)
                return null;

            return Task.FromResult(user.password);
        }

        public Task<string> GetUserIdAsync(e.user_account user, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested || user == null)
                return null;

            return Task.FromResult(user.user_id.ToString());
        }

        public Task<string> GetUserNameAsync(e.user_account user, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested || user == null)
                return null;

            return Task.FromResult(user.user_name);
        }

        public Task<bool> HasPasswordAsync(e.user_account user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(e.user_account user, string normalizedName, CancellationToken cancellationToken)
        {
            return Task.FromResult<object>(null);
        }

        public Task SetPasswordHashAsync(e.user_account user, string passwordHash, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested || user == null || passwordHash == null)
                return null;

            user.password = passwordHash;
            return Task.FromResult<object>(null);
        }

        public Task SetUserNameAsync(e.user_account user, string userName, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested || user == null || userName == null)
                return null;

            user.user_name = userName;
            return Task.FromResult<object>(null);
        }

        public async Task<IdentityResult> UpdateAsync(e.user_account user, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
                return IdentityResult.Failed(new IdentityError { Description = "Operation was canceled." });
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "Argument was null." });

            try
            {
                var result = await d.user_account.Update(user);

                if (result.Status != e.shared.Status.Success)
                    return IdentityResult.Failed(new IdentityError { Description = result.Msg });

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
