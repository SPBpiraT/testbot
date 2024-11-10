using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UserDataService.Grpc.Models;

namespace UserDataService.Grpc.Data.EntityTypeConfiguration
{
    public class TgUserConfiguration : IEntityTypeConfiguration<TgUser>
    {
        public void Configure(EntityTypeBuilder<TgUser> builder)
        {
            builder.HasKey(user => user.Id);
            builder.HasIndex(user => user.Id).IsUnique();
        }
    }
}
