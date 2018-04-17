using Dashboard.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Dashboard.Data.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

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

            builder.Entity<Blog>()
                .HasMany<Post>(b => b.Posts)
                .WithOne(p => p.Blog);

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

            builder.Entity<Job>(m =>
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

                m.HasMany(p => p.StaticPipelines);
                m.HasMany(p => p.DynamicPipelines);
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
                model.HasOne(p => p.Project);

                //model.Property(p => p.Project)
                //    .HasField("_project")
                //    .UsePropertyAccessMode(PropertyAccessMode.Field);

                model.OwnsOne(p => p.Position);
            });
            builder.Entity<MemePanel>(model =>
            {
                model.HasBaseType<Panel>();
            });
            builder.Entity<StaticBranchPanel>(model =>
            {
                model.HasBaseType<Panel>();
            });
            builder.Entity<DynamicPipelinesPanel>(model =>
            {
                model.HasBaseType<Panel>();
            });
            #endregion
        }
    }
}
