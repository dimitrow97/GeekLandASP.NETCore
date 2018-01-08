using PCStore.Common.Mapping;
using PCStore.Data.Models;

namespace PCStore.Services.Admin.Models
{

    public class AdminUserListingServiceModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }
    }
}
