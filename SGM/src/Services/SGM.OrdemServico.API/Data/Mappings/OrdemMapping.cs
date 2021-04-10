using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGM.OrdemServico.API.Models;

namespace SGM.OrdemServico.API.Data.Mappings
{
    public class OrdemMapping : IEntityTypeConfiguration<Ordem>
    {
        public void Configure(EntityTypeBuilder<Ordem> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.CodDepartamento)
                .IsRequired()
                .HasColumnType("varchar(100)");
            
            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasColumnType("varchar(500)");

            builder.Property(c => c.Solucao)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.ToTable("OrdemServico");
        }
    }
}