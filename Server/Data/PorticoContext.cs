using Microsoft.EntityFrameworkCore;
using Server.Entities;

namespace Server.Data
{
    public partial class PorticoContext : DbContext
    {
        public PorticoContext()
        {
        }

        public PorticoContext(DbContextOptions<PorticoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbEformSessionForm> TbEformSessionForms { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("MPIDOM\\IT_DEV_CLM_SQL");

            modelBuilder.Entity<TbEformSessionForm>(entity =>
            {
                entity.HasKey(e => e.EFormSessionFormId);

                entity.ToTable("tbEFormSessionForms", "dbo");

                entity.Property(e => e.EFormSessionFormId)
                    .HasColumnName("eFormSessionFormId")
                    .HasComment("Primary key");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())")
                    .HasComment("Timestamp of when the record was created");

                entity.Property(e => e.EFormId)
                    .HasColumnName("eFormId")
                    .HasComment("eForm id");

                entity.Property(e => e.EFormSessionId)
                    .HasColumnName("eFormSessionId")
                    .HasComment("eForm session id");

                entity.Property(e => e.IsSubmitted).HasComment("Determines whether the doument is submitted");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasComment("Timestamp of when the record was last modified");

                entity.Property(e => e.LogInfo)
                    .HasMaxLength(4000)
                    .HasComment("Log the eform submit Informations");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
