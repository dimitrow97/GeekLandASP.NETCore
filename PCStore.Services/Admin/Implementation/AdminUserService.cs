namespace PCStore.Services.Admin.Implementations
{
    using AutoMapper.QueryableExtensions;
    using PCStore.Services.Admin.Models;
    using Microsoft.EntityFrameworkCore;
    using PCStore.Data;
    using PCStore.Services.Admin;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class AdminUserService : IAdminUserService
    {
        private readonly PCStoreDbContext db;

        public AdminUserService(PCStoreDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminUserListingServiceModel>> AllAsync()
            => await this.db
                .Users
                .ProjectTo<AdminUserListingServiceModel>()
                .ToListAsync();
    }
}
