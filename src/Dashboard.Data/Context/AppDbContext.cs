using Dashboard.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Dashboard.Data.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Pipeline> Pipelines { get; set; }
        public DbSet<Project> Projects { get; set; }

        public DbSet<Panel> Panels { get; set; }
        public DbSet<MemePanel> MemePanels { get; set; }
        public DbSet<StaticBranchPanel> StaticBranchPanels { get; set; }
        public DbSet<DynamicPipelinesPanel> DynamicPipelinesPanels { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Fluent API
            builder.Entity<Pipeline>(m =>
            {
                m.HasKey(p => p.Id);
                m.HasMany(p => p.Stages);
            });

            builder.Entity<Stage>(m =>
            {
                m.HasKey(p => p.Id);
            });

            builder.Entity<Project>(m =>
            {
                m.HasKey(p => p.Id);
                m.Property(p => p.Id).ValueGeneratedOnAdd();

                m.Property(p => p.ApiProjectId).IsRequired();
                m.Property(p => p.ApiAuthenticationToken).IsRequired();
                m.Property(p => p.ApiHostUrl).IsRequired();
                m.Property(p => p.DataProviderName).IsRequired();
                m.Property(p => p.PipelinesNumber).HasDefaultValue(100);

                m.HasMany(p => p.Pipelines);

                //m.HasMany(p => p.StaticPipelines);
                //m.HasMany(p => p.DynamicPipelines);
            });

            builder.Entity<BranchName>(m =>
            {
                m.HasKey(p => p.Id);
            });

            #region Panels
            builder.Entity<Panel>(model =>
            {
                model.HasKey(p => p.Id);
                model.Property(p => p.Id).ValueGeneratedOnAdd();

                model.HasOne(p => p.Project)
                    .WithMany()
                    .HasForeignKey(p => p.ProjectId);

                model.OwnsOne(p => p.Position);

                model.HasDiscriminator<string>("PanelType")
                    .HasValue<MemePanel>(nameof(MemePanel))
                    .HasValue<StaticBranchPanel>(nameof(StaticBranchPanel))
                    .HasValue<DynamicPipelinesPanel>(nameof(DynamicPipelinesPanel));
            });
            builder.Entity<DynamicPipelinesPanel>(model =>
            {
                model.Property(p => p.PanelRegex).HasDefaultValue(".*");
            });
            #endregion
        }
    }
}
