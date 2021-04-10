using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SGM.Solicitacao.API.Data.Mappings
{
    public class SolicitacaoMapping : IEntityTypeConfiguration<Models.Solicitacao>
    {
        public void Configure(EntityTypeBuilder<Models.Solicitacao> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CodDepartamento)
                .IsRequired()
                .HasColumnType("varchar(100)");
            
            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder.Property(c => c.Status)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.ToTable("Solicitacao");
        }
    }
}