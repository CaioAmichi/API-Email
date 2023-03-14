
using EmailApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmailApi.Infrastructure.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(tab => tab.Id);


            builder.Property(tab => tab.NomeEmpresa)
                .IsRequired()
                .HasMaxLength(100);


            builder.Property(tab => tab.EmailRemetente)
                .IsRequired()
                .HasMaxLength(100);


            builder.Property(tab => tab.SenhaRemetente)
                .IsRequired()
                .HasMaxLength(100);


            builder.Property(tab => tab.Smtp)
                .IsRequired()
                .HasMaxLength(100);


            builder.ToTable("Clientes");
        }
    }
}
