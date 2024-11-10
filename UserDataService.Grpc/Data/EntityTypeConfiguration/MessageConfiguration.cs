using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UserDataService.Grpc.Models;

namespace UserDataService.Grpc.Data.EntityTypeConfiguration
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(message => message.Id);
            builder.HasIndex(message => message.Id).IsUnique();
        }
    }
}
