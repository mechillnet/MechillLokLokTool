using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PNB.Domain.Models
{
    public partial class DbPhim77 : DbContext
    {
        public DbPhim77()
        {
        }

        public DbPhim77(DbContextOptions<DbPhim77> options)
            : base(options)
        {
        }

        public virtual DbSet<Ads> Ads { get; set; }
        public virtual DbSet<Calendar> Calendar { get; set; }
        public virtual DbSet<Cast> Cast { get; set; }
        public virtual DbSet<CastMapping> CastMapping { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<CategoryMapping> CategoryMapping { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Episode> Episode { get; set; }
        public virtual DbSet<EpisodeSource> EpisodeSource { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<Friend> Friend { get; set; }
        public virtual DbSet<GalleryImage> GalleryImage { get; set; }
        public virtual DbSet<HistoryLink> HistoryLink { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<LocalizedProperty> LocalizedProperty { get; set; }
        public virtual DbSet<Movie> Movie { get; set; }
        public virtual DbSet<MovieRate> MovieRate { get; set; }
        public virtual DbSet<MovieView> MovieView { get; set; }
        public virtual DbSet<MyContact> MyContact { get; set; }
        public virtual DbSet<PermissionCategory> PermissionCategory { get; set; }
        public virtual DbSet<PermissionRecord> PermissionRecord { get; set; }
        public virtual DbSet<PermissionRecordMapping> PermissionRecordMapping { get; set; }
        public virtual DbSet<Picture> Picture { get; set; }
        public virtual DbSet<PictureBinary> PictureBinary { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<SelectOption> SelectOption { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }
        public virtual DbSet<Store> Store { get; set; }
        public virtual DbSet<TCategory> TCategory { get; set; }
        public virtual DbSet<TPriority> TPriority { get; set; }
        public virtual DbSet<TStatus> TStatus { get; set; }
        public virtual DbSet<TTicket> TTicket { get; set; }
        public virtual DbSet<TType> TType { get; set; }
        public virtual DbSet<Topic> Topic { get; set; }
        public virtual DbSet<UrlRecord> UrlRecord { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.; Database=DbPhim77;User Id=sa;Password=Bau123!@#;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ads>(entity =>
            {
                entity.Property(e => e.ExpriesDate).HasColumnType("datetime");

                entity.Property(e => e.FacebookVideoId).HasMaxLength(50);
            });

            modelBuilder.Entity<Calendar>(entity =>
            {
                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Cast>(entity =>
            {
                entity.Property(e => e.Avatar).HasMaxLength(1000);

                entity.Property(e => e.AvatarThumb).HasMaxLength(1000);

                entity.Property(e => e.BirthDay).HasColumnType("datetime");

                entity.Property(e => e.Country).HasMaxLength(200);

                entity.Property(e => e.CreateOn).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Link)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.SearchText).HasMaxLength(500);

                entity.Property(e => e.SeoDescription).HasMaxLength(500);

                entity.Property(e => e.SeoTitle).HasMaxLength(200);

                entity.Property(e => e.UpdateOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<CastMapping>(entity =>
            {
                entity.HasOne(d => d.Cast)
                    .WithMany(p => p.CastMapping)
                    .HasForeignKey(d => d.CastId)
                    .HasConstraintName("FK_Movie_CastMapping_Movie_Cast");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CastMapping)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Movie_CastMapping_Movie_Product");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Link)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.SeoDescription).HasMaxLength(500);

                entity.Property(e => e.SeoTitle).HasMaxLength(200);
            });

            modelBuilder.Entity<CategoryMapping>(entity =>
            {
                entity.HasOne(d => d.Category)
                    .WithMany(p => p.CategoryMapping)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Movie_CategoryProductMapping_Movie_Category");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.CategoryMapping)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Movie_CategoryProductMapping_Movie_Product");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Link)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.SeoDescription).HasMaxLength(500);

                entity.Property(e => e.SeoTitle).HasMaxLength(200);
            });

            modelBuilder.Entity<Episode>(entity =>
            {
                entity.Property(e => e.CreateOn).HasColumnType("datetime");

                entity.Property(e => e.FullLink)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Link)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.UpdateOn).HasColumnType("datetime");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Episode)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movie_ProductEpisode_Movie_Product");
            });

            modelBuilder.Entity<EpisodeSource>(entity =>
            {
                entity.Property(e => e.CreateOn).HasColumnType("datetime");

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");

                entity.Property(e => e.SubLink).HasMaxLength(500);

                entity.Property(e => e.UpdateOn).HasColumnType("datetime");

                entity.Property(e => e.VideoId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.ProductEpisode)
                    .WithMany(p => p.EpisodeSource)
                    .HasForeignKey(d => d.ProductEpisodeId)
                    .HasConstraintName("FK_Movie_ProductSource_Movie_ProductEpisode");
            });

            modelBuilder.Entity<Files>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Filename)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<Friend>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(1000);

                entity.Property(e => e.Avatar).HasMaxLength(1000);

                entity.Property(e => e.FacebookLink).HasMaxLength(1000);

                entity.Property(e => e.Name).HasMaxLength(1000);

                entity.Property(e => e.Phone).HasMaxLength(1000);

                entity.Property(e => e.Zalo).HasMaxLength(1000);
            });

            modelBuilder.Entity<GalleryImage>(entity =>
            {
                entity.Property(e => e.Alt).HasMaxLength(1000);

                entity.Property(e => e.Url).HasMaxLength(1000);
            });

            modelBuilder.Entity<HistoryLink>(entity =>
            {
                entity.Property(e => e.CreateAt).HasColumnType("datetime");

                entity.Property(e => e.Link)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.Property(e => e.Currency)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FlagImageFileName).HasMaxLength(500);

                entity.Property(e => e.LanguageCulture)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(500);
            });

            modelBuilder.Entity<LocalizedProperty>(entity =>
            {
                entity.Property(e => e.LocaleKey)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(e => e.LocaleKeyGroup)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(e => e.LocaleValue).IsRequired();
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.Property(e => e.Avatar).HasMaxLength(1000);

                entity.Property(e => e.Cdnavatar).HasColumnName("CDNAvatar");

                entity.Property(e => e.Cdnbanner).HasColumnName("CDNBanner");

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Director).HasMaxLength(100);

                entity.Property(e => e.Language).HasMaxLength(100);

                entity.Property(e => e.Link)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.OriginalLink)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.OtherName).HasMaxLength(200);

                entity.Property(e => e.Panner).HasMaxLength(1000);

                entity.Property(e => e.SearchText).HasMaxLength(500);

                entity.Property(e => e.SeoDescription).HasMaxLength(500);

                entity.Property(e => e.SeoKeywords).HasMaxLength(500);

                entity.Property(e => e.SeoTitle).HasMaxLength(200);

                entity.Property(e => e.ShowTimes).HasMaxLength(200);

                entity.Property(e => e.StatusTitle).HasMaxLength(500);

                entity.Property(e => e.Time).HasMaxLength(50);

                entity.Property(e => e.Trailer).HasMaxLength(1000);

                entity.Property(e => e.UpdateOn).HasColumnType("datetime");

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.Movie)
                    .HasForeignKey(d => d.CountryId)
                    .HasConstraintName("FK_Movie_Product_Movie_Country");
            });

            modelBuilder.Entity<MovieRate>(entity =>
            {
                entity.Property(e => e.CreateOn).HasColumnType("datetime");

                entity.Property(e => e.Ip)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.MovieRate)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Movie_ProductRate_Movie_Product");
            });

            modelBuilder.Entity<MovieView>(entity =>
            {
                entity.Property(e => e.ViewAt).HasColumnType("datetime");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.MovieView)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_Movie_ViewProduct_Movie_Product");
            });

            modelBuilder.Entity<MyContact>(entity =>
            {
                entity.Property(e => e.Avatar).HasMaxLength(500);

                entity.Property(e => e.Email).HasMaxLength(500);

                entity.Property(e => e.Facebook).HasMaxLength(500);

                entity.Property(e => e.IsUser).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Phone).HasMaxLength(500);
            });

            modelBuilder.Entity<PermissionCategory>(entity =>
            {
                entity.Property(e => e.CategoryCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryName).HasMaxLength(500);
            });

            modelBuilder.Entity<PermissionRecord>(entity =>
            {
                entity.Property(e => e.Category).HasMaxLength(1000);

                entity.Property(e => e.SystemName).HasMaxLength(1000);

                entity.Property(e => e.Url)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PermissionRecordMapping>(entity =>
            {
                entity.HasKey(e => new { e.PermissionRecordId, e.CustomerRoleId })
                    .HasName("PK_PermissionRecord_Mapping_1");

                entity.ToTable("PermissionRecord_Mapping");

                entity.HasIndex(e => e.PermissionRecordId)
                    .HasName("IX_PermissionRecord_Mapping");

                entity.Property(e => e.PermissionRecordId).HasColumnName("PermissionRecord_Id");

                entity.Property(e => e.CustomerRoleId).HasColumnName("CustomerRole_Id");

                entity.HasOne(d => d.CustomerRole)
                    .WithMany(p => p.PermissionRecordMapping)
                    .HasForeignKey(d => d.CustomerRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PermissionRecord_Mapping_Role");

                entity.HasOne(d => d.PermissionRecord)
                    .WithMany(p => p.PermissionRecordMapping)
                    .HasForeignKey(d => d.PermissionRecordId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PermissionRecord_Mapping_PermissionRecord");
            });

            modelBuilder.Entity<Picture>(entity =>
            {
                entity.Property(e => e.MimeType).HasMaxLength(40);

                entity.Property(e => e.SeoFilename).HasMaxLength(300);
            });

            modelBuilder.Entity<PictureBinary>(entity =>
            {
                entity.HasIndex(e => e.PictureId)
                    .HasName("UQ__PictureB__8C2866D92D075738")
                    .IsUnique();

                entity.HasOne(d => d.Picture)
                    .WithOne(p => p.PictureBinary)
                    .HasForeignKey<PictureBinary>(d => d.PictureId)
                    .HasConstraintName("FK_PictureBinary_Picture");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<SelectOption>(entity =>
            {
                entity.Property(e => e.AttributesValue).HasMaxLength(500);

                entity.Property(e => e.AttributesValue1).HasMaxLength(500);

                entity.Property(e => e.ClassificationCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Settings>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.Property(e => e.Key)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.Property(e => e.StoreId).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.DomainName).HasMaxLength(250);

                entity.Property(e => e.Icon).HasMaxLength(250);

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.Noticafition).HasMaxLength(500);

                entity.Property(e => e.Title).HasMaxLength(250);
            });

            modelBuilder.Entity<TCategory>(entity =>
            {
                entity.ToTable("T_Category");

                entity.Property(e => e.ColorHex).HasMaxLength(50);

                entity.Property(e => e.CreateOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.UpdateOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<TPriority>(entity =>
            {
                entity.ToTable("T_Priority");

                entity.Property(e => e.ColorHex).HasMaxLength(50);

                entity.Property(e => e.CreateOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.UpdateOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<TStatus>(entity =>
            {
                entity.ToTable("T_Status");

                entity.Property(e => e.ColorHex).HasMaxLength(50);

                entity.Property(e => e.CreateOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.UpdateOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<TTicket>(entity =>
            {
                entity.ToTable("T_Ticket");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateOn).HasColumnType("datetime");

                entity.Property(e => e.DateClose).HasColumnType("datetime");

                entity.Property(e => e.DateOpen).HasColumnType("datetime");

                entity.Property(e => e.DeadLine).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(1000);

                entity.Property(e => e.UpdateOn).HasColumnType("datetime");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TTicket)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_T_Ticket_T_Category");

                entity.HasOne(d => d.Priority)
                    .WithMany(p => p.TTicket)
                    .HasForeignKey(d => d.PriorityId)
                    .HasConstraintName("FK_T_Ticket_T_Priority");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TTicket)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK_T_Ticket_T_Status");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.TTicket)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_T_Ticket_T_Type");
            });

            modelBuilder.Entity<TType>(entity =>
            {
                entity.ToTable("T_Type");

                entity.Property(e => e.ColorHex).HasMaxLength(50);

                entity.Property(e => e.CreateOn).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.UpdateOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");
            });

            modelBuilder.Entity<UrlRecord>(entity =>
            {
                entity.Property(e => e.EntityName)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(e => e.Slug)
                    .IsRequired()
                    .HasMaxLength(400);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(1000);

                entity.Property(e => e.Address1).HasMaxLength(1000);

                entity.Property(e => e.Avatar).HasMaxLength(1000);

                entity.Property(e => e.CannotLoginUntilDate).HasColumnType("datetime");

                entity.Property(e => e.Company).HasMaxLength(1000);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(1000);

                entity.Property(e => e.LastIp).HasMaxLength(1000);

                entity.Property(e => e.Money)
                    .HasColumnType("decimal(18, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Name).HasMaxLength(1000);

                entity.Property(e => e.Password).HasMaxLength(1000);

                entity.Property(e => e.PasswordSalt).HasMaxLength(1000);

                entity.Property(e => e.Phone).HasMaxLength(1000);

                entity.Property(e => e.RoleId)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.TaxNumber).HasMaxLength(1000);

                entity.Property(e => e.TokenLogin)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.TotalMoney)
                    .HasColumnType("decimal(18, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedOn).HasColumnType("datetime");

                entity.Property(e => e.Username).HasMaxLength(1000);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
