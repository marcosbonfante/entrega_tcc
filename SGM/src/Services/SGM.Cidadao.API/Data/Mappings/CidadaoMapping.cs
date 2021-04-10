using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGM.Core.DomainObjects;

namespace SGM.Cidadao.API.Data.Mappings
{
    public class CidadaoMapping : IEntityTypeConfiguration<Models.Cidadao>
    {
        public void Configure(EntityTypeBuilder<Models.Cidadao> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.OwnsOne(c => c.Cpf, tf =>
            {
                tf.Property(c => c.Numero)
                    .IsRequired()
                    .HasMaxLength(Cpf.CpfMaxLength)
                    .HasColumnName("Cpf")
                    .HasColumnType($"varchar({Cpf.CpfMaxLength})");
            });

            builder.OwnsOne(c => c.Email, tf =>
            {
                tf.Property(c => c.Endereco)
                    .IsRequired()
                    .HasColumnName("Email")
                    .HasColumnType($"varchar({Email.EnderecoMaxLength})");
            });

            // 1 : 1 => Aluno : Endereco
            builder.HasOne(c => c.Endereco)
                .WithOne(c => c.Cidadao);

            builder.ToTable("Cidadao");
        }
    }
}