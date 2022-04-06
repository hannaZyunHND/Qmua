using Microsoft.EntityFrameworkCore;
using MI.Entity.Models;
using Utils.Settings;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System;
using MI.Entity.Interfaces;

namespace MI.Dal.IDbContext
{
    public class IDbContext : DbContext
    {

        //Scaffold-DbContext "Server=118.70.185.130;Database=enterbuy_merged;Trusted_Connection=False;User Id=ndev;password=asd123!@##@" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -f
        public virtual DbSet<ActionInFunctions> ActionInFunctions { get; set; }
        public virtual DbSet<Actions> Actions { get; set; }
        public virtual DbSet<Ads> Ads { get; set; }
        public virtual DbSet<AggregatedCounter> AggregatedCounter { get; set; }
        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<ArticleInLanguage> ArticleInLanguage { get; set; }
        public virtual DbSet<ArticlesInZone> ArticlesInZone { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<BankInstallment> BankInstallment { get; set; }
        public virtual DbSet<BannerAds> BannerAds { get; set; }
        public virtual DbSet<Colors> Colors { get; set; }
        public virtual DbSet<Comment> Comment { get; set; }
        public virtual DbSet<Config> Config { get; set; }
        public virtual DbSet<ConfigInLanguage> ConfigInLanguage { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<Coupon> Coupon { get; set; }
        public virtual DbSet<CouponChild> CouponChild { get; set; }
        public virtual DbSet<CouponInProduct> CouponInProduct { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<CustomLucky> CustomLucky { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<DepartmentInLanguage> DepartmentInLanguage { get; set; }
        public virtual DbSet<FileUpload> FileUpload { get; set; }
        public virtual DbSet<FlashSale> FlashSale { get; set; }
        public virtual DbSet<Functions> Functions { get; set; }
        public virtual DbSet<Hash> Hash { get; set; }
        public virtual DbSet<Job> Job { get; set; }
        public virtual DbSet<JobParameter> JobParameter { get; set; }
        public virtual DbSet<JobQueue> JobQueue { get; set; }
        public virtual DbSet<Language> Language { get; set; }
        public virtual DbSet<List> List { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<LocationInLanguage> LocationInLanguage { get; set; }
        public virtual DbSet<LuckySpin> LuckySpin { get; set; }
        public virtual DbSet<MaintainSpectificatinInProduct> MaintainSpectificatinInProduct { get; set; }
        public virtual DbSet<MaintainSpectificationInLanguage> MaintainSpectificationInLanguage { get; set; }
        public virtual DbSet<MaintainSpectifications> MaintainSpectifications { get; set; }
        public virtual DbSet<MaintainSpectificationTemplate> MaintainSpectificationTemplate { get; set; }
        public virtual DbSet<Manufacturer> Manufacturer { get; set; }
        public virtual DbSet<ManufacturerInLanguage> ManufacturerInLanguage { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<OrderPromotionDetail> OrderPromotionDetail { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductInArticle> ProductInArticle { get; set; }
        public virtual DbSet<ProductInFlashSale> ProductInFlashSale { get; set; }
        public virtual DbSet<ProductInLanguage> ProductInLanguage { get; set; }
        public virtual DbSet<ProductInProduct> ProductInProduct { get; set; }
        public virtual DbSet<ProductInPromotion> ProductInPromotion { get; set; }
        public virtual DbSet<ProductInRegion> ProductInRegion { get; set; }
        public virtual DbSet<ProductInZone> ProductInZone { get; set; }
        public virtual DbSet<ProductPriceInLocation> ProductPriceInLocation { get; set; }
        public virtual DbSet<ProductSpecifications> ProductSpecifications { get; set; }
        public virtual DbSet<ProductSpecificationTemplate> ProductSpecificationTemplate { get; set; }
        public virtual DbSet<ProductSpecificationTemplateInLanguage> ProductSpecificationTemplateInLanguage { get; set; }
        public virtual DbSet<Promotion> Promotion { get; set; }
        public virtual DbSet<PromotionInLanguage> PromotionInLanguage { get; set; }
        public virtual DbSet<Property> Property { get; set; }
        public virtual DbSet<PropertyLanguage> PropertyLanguage { get; set; }
        public virtual DbSet<Rating> Rating { get; set; }
        public virtual DbSet<Redirect> Redirect { get; set; }
        public virtual DbSet<Schema> Schema { get; set; }
        public virtual DbSet<Server> Server { get; set; }
        public virtual DbSet<Set> Set { get; set; }
        public virtual DbSet<State> State { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<TagInProductLanguage> TagInProductLanguage { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserPermission> UserPermission { get; set; }
        public virtual DbSet<ViewCount> ViewCount { get; set; }
        public virtual DbSet<Voucher> Voucher { get; set; }
        public virtual DbSet<Zone> Zone { get; set; }
        public virtual DbSet<ZoneInLanguage> ZoneInLanguage { get; set; }
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ViewCount>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
            modelBuilder.Entity<ActionInFunctions>(entity =>
            {
                entity.HasKey(e => new { e.FunctionId, e.ActionId });

                entity.Property(e => e.FunctionId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ActionId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Action)
                    .WithMany(p => p.ActionInFunctions)
                    .HasForeignKey(d => d.ActionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActionInFunctions_Actions");

                entity.HasOne(d => d.Function)
                    .WithMany(p => p.ActionInFunctions)
                    .HasForeignKey(d => d.FunctionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ActionInFunctions_Functions");
            });

            modelBuilder.Entity<Actions>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Ads>(entity =>
            {
                entity.Property(e => e.Content).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Thumb).HasMaxLength(255);

                entity.Property(e => e.Url).HasMaxLength(255);
            });

            modelBuilder.Entity<AggregatedCounter>(entity =>
            {
                entity.HasKey(e => e.Key);

                entity.ToTable("AggregatedCounter", "HangFire");

                entity.Property(e => e.Key)
                    .HasMaxLength(100)
                    .ValueGeneratedNever();

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<Article>(entity =>
            {
                entity.Property(e => e.Avatar).HasMaxLength(500);

                entity.Property(e => e.CreatedBy)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DistributionDate).HasColumnType("datetime");

                entity.Property(e => e.LastModifiedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LastModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.PublishedBy)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Url).HasMaxLength(500);

                entity.Property(e => e.ViewCount).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<ArticleInLanguage>(entity =>
            {
                entity.HasKey(e => new { e.LanguageCode, e.ArticleId });

                entity.Property(e => e.LanguageCode).HasMaxLength(10);

                entity.Property(e => e.Author).HasMaxLength(500);

                entity.Property(e => e.Body)
                    .IsRequired()
                    .HasColumnType("ntext");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.IsAllowComment).HasDefaultValueSql("((1))");

                entity.Property(e => e.MetaCanonical).HasMaxLength(500);

                entity.Property(e => e.MetaDescription).HasMaxLength(255);

                entity.Property(e => e.MetaKeyword).HasMaxLength(255);

                entity.Property(e => e.MetaNoIndex).HasMaxLength(500);

                entity.Property(e => e.MetaTitle).HasMaxLength(255);

                entity.Property(e => e.SocialDescription).HasMaxLength(500);

                entity.Property(e => e.SocialImage).HasMaxLength(255);

                entity.Property(e => e.SocialTitle).HasMaxLength(255);
                //Lavista entity
                entity.Property(e => e.ThiTruong).HasMaxLength(255);
                
                entity.Property(e => e.MoHinh).HasMaxLength(255);
                
                entity.Property(e => e.DuAn).HasMaxLength(255);
                
                entity.Property(e => e.MucDichDauTu).HasMaxLength(255);
                //End lavista entity
                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Url).HasMaxLength(500);

                entity.Property(e => e.ViewCount).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ArticleInLanguage)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArticleInLanguage_Article");

                entity.HasOne(d => d.LanguageCodeNavigation)
                    .WithMany(p => p.ArticleInLanguage)
                    .HasForeignKey(d => d.LanguageCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArticleInLanguage_Language");
            });

            modelBuilder.Entity<ArticlesInZone>(entity =>
            {
                entity.HasKey(e => new { e.ArticleId, e.ZoneId });

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ArticlesInZone)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArticlesInZone_Article");

                entity.HasOne(d => d.Zone)
                    .WithMany(p => p.ArticlesInZone)
                    .HasForeignKey(d => d.ZoneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ArticlesInZone_Zone");
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.Avatar)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FullName).HasMaxLength(250);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<BankInstallment>(entity =>
            {
                entity.Property(e => e.Avatar)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BannerAds>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.LanguageCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Colors>(entity =>
            {
                entity.HasKey(e => new { e.Code, e.LanguageCode });

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LanguageCode).HasMaxLength(10);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Show).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.Content).HasDefaultValueSql("('')");

                entity.Property(e => e.CreatedByAdminId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.CreatedByCustomerId).HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LanguageCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.ObjectId).HasDefaultValueSql("((0))");

                entity.Property(e => e.ObjectType)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ParentId).HasDefaultValueSql("((0))");

                entity.Property(e => e.PhoneOrMail).HasMaxLength(200);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.Type)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Config>(entity =>
            {
                entity.HasKey(e => e.ConfigName);

                entity.Property(e => e.ConfigName)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.ConfigGroupKey)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ConfigLabel).HasMaxLength(200);

                entity.Property(e => e.Page).HasMaxLength(100);
            });

            modelBuilder.Entity<ConfigInLanguage>(entity =>
            {
                entity.HasKey(e => new { e.LanguageCode, e.ConfigName });

                entity.Property(e => e.LanguageCode).HasMaxLength(10);

                entity.Property(e => e.ConfigName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.ConfigNameNavigation)
                    .WithMany(p => p.ConfigInLanguage)
                    .HasForeignKey(d => d.ConfigName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ConfigInLanguage_Config");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('male')");

                entity.Property(e => e.ModifiedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(300);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Source)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.Title).HasMaxLength(200);

                entity.Property(e => e.Type).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.Code)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Locked).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.ValueDiscount).HasColumnType("text");
            });

            modelBuilder.Entity<CouponChild>(entity =>
            {
                entity.HasKey(e => e.Ma);

                entity.Property(e => e.Ma)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreateBy).HasMaxLength(200);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ExprireDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.NumberUseCode).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<CouponInProduct>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.CouponId });

                entity.Property(e => e.Type).HasDefaultValueSql("((1))");

                entity.Property(e => e.Value).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Ip)
                    .HasColumnName("IP")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MetaData).HasColumnType("ntext");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.Os)
                    .HasColumnName("OS")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Pcname)
                    .HasColumnName("PCName")
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Source).HasDefaultValueSql("((1))");

                entity.Property(e => e.Type).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<CustomLucky>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LuckySpinName).HasMaxLength(255);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Hotline)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DepartmentInLanguage>(entity =>
            {
                entity.HasKey(e => new { e.LanguageCode, e.DepartmentId });

                entity.Property(e => e.LanguageCode).HasMaxLength(10);

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Url).HasMaxLength(255);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.DepartmentInLanguage)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DepartmentInLanguage_Department");
            });

            modelBuilder.Entity<FileUpload>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(2000);

                entity.Property(e => e.Dimensions)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FileDownloadPath)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FileExt)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.FilePath)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.FileSize).HasDefaultValueSql("((0))");

                entity.Property(e => e.LastModifiedBy).HasMaxLength(50);

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.Type).HasDefaultValueSql("((1))");

                entity.Property(e => e.UploadedBy).HasMaxLength(50);

                entity.Property(e => e.UploadedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<FlashSale>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Functions>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CssClass).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ParentId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Url).HasMaxLength(50);
            });

            modelBuilder.Entity<Hash>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Field });

                entity.ToTable("Hash", "HangFire");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Field).HasMaxLength(100);
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("Job", "HangFire");

                entity.Property(e => e.Arguments).IsRequired();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.InvocationData).IsRequired();

                entity.Property(e => e.StateName).HasMaxLength(20);
            });

            modelBuilder.Entity<JobParameter>(entity =>
            {
                entity.HasKey(e => new { e.JobId, e.Name });

                entity.ToTable("JobParameter", "HangFire");

                entity.Property(e => e.Name).HasMaxLength(40);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobParameter)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_HangFire_JobParameter_Job");
            });

            modelBuilder.Entity<JobQueue>(entity =>
            {
                entity.HasKey(e => new { e.Queue, e.Id });

                entity.ToTable("JobQueue", "HangFire");

                entity.Property(e => e.Queue).HasMaxLength(50);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.FetchedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<Language>(entity =>
            {
                entity.HasKey(e => e.LanguageCode);

                entity.Property(e => e.LanguageCode)
                    .HasMaxLength(10)
                    .ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.SetDefault).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<List>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Id });

                entity.ToTable("List", "HangFire");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.Code).HasMaxLength(10);

                entity.Property(e => e.LanguageCode).HasMaxLength(10);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Note).HasMaxLength(255);
            });

            modelBuilder.Entity<LocationInLanguage>(entity =>
            {
                entity.HasKey(e => new { e.LanguageCode, e.LocationId });

                entity.Property(e => e.LanguageCode).HasMaxLength(10);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.LocationInLanguage)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LocationInLanguage_Location");
            });

            modelBuilder.Entity<LuckySpin>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
                entity.Property(e => e.Ratio).HasColumnType("int");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<MaintainSpectificatinInProduct>(entity =>
            {
                entity.Property(e => e.LanguageCode)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MaintainSpectificationInLanguage>(entity =>
            {
                entity.Property(e => e.LanguageCode).HasMaxLength(10);
            });

            modelBuilder.Entity<MaintainSpectificationTemplate>(entity =>
            {
                entity.Property(e => e.SpectificationId).HasDefaultValueSql("((0))");

                entity.Property(e => e.ZoneId).HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Manufacturer>(entity =>
            {
                entity.Property(e => e.Avatar).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(255);
            });

            modelBuilder.Entity<ManufacturerInLanguage>(entity =>
            {
                entity.HasKey(e => new { e.LanguageCode, e.ManufacturerId });

                entity.Property(e => e.LanguageCode).HasMaxLength(10);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.Url).HasMaxLength(255);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.Path)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.Property(e => e.LogPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Quantity).HasColumnType("decimal(18, 1)");

                entity.Property(e => e.Vat).HasDefaultValueSql("((0))");

                entity.Property(e => e.VatPrice).HasDefaultValueSql("((0))");

                entity.Property(e => e.Voucher).HasMaxLength(500);

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderDetail_Orders");
            });

            modelBuilder.Entity<OrderPromotionDetail>(entity =>
            {
                entity.Property(e => e.LogName).HasMaxLength(500);

                entity.Property(e => e.LogType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LogValue).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.OrderDetail)
                    .WithMany(p => p.OrderPromotionDetail)
                    .HasForeignKey(d => d.OrderDetailId)
                    .HasConstraintName("FK_OrderPromotionDetail_OrderDetail");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.Property(e => e.CreatedBy).HasMaxLength(500);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OrderCode)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.OrderSourceType).HasDefaultValueSql("((1))");

                entity.Property(e => e.Status)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Vat).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Orders_Customer");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Color).HasMaxLength(500);

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ExprirePromotion)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Guarantee).HasMaxLength(500);

                entity.Property(e => e.IsInstallment)
                    .HasColumnName("isInstallment")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifyBy)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ModifyDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.PropertyId).HasMaxLength(500);

                entity.Property(e => e.Status).HasDefaultValueSql("((2))");

                entity.Property(e => e.Unit).HasMaxLength(200);

                entity.Property(e => e.Url)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Vat).HasDefaultValueSql("((0))");

                entity.Property(e => e.ViewCount).HasDefaultValueSql("((0))");

                entity.Property(e => e.Voucher).HasMaxLength(100);

                entity.Property(e => e.Warranty).HasMaxLength(50);
                entity.Property(e => e.ArticleId).HasMaxLength(50);
                entity.Property(e => e.ProductComboParentId);

            });

            modelBuilder.Entity<ProductInArticle>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.ArticleId });

                entity.HasOne(d => d.Article)
                    .WithMany(p => p.ProductInArticle)
                    .HasForeignKey(d => d.ArticleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductInArticle_Article");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductInArticle)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductInArticle_Product");
            });

            modelBuilder.Entity<ProductInFlashSale>(entity =>
            {
                entity.Property(e => e.ProductPriceInFlashSale).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<ProductInLanguage>(entity =>
            {
                entity.HasKey(e => new { e.LanguageCode, e.ProductId });

                entity.Property(e => e.LanguageCode).HasMaxLength(10);

                entity.Property(e => e.Catalog).HasMaxLength(250);

                entity.Property(e => e.Content).HasColumnType("ntext");

                entity.Property(e => e.Description).HasColumnType("ntext");

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.MetaCanonical).HasMaxLength(500);

                entity.Property(e => e.MetaDescription).HasMaxLength(255);

                entity.Property(e => e.MetaKeyword).HasMaxLength(255);

                entity.Property(e => e.MetaNoIndex).HasMaxLength(500);

                entity.Property(e => e.MetaTitle).HasMaxLength(255);

                entity.Property(e => e.PromotionInfo).HasMaxLength(500);

                entity.Property(e => e.SocialDescription).HasMaxLength(500);

                entity.Property(e => e.SocialImage).HasMaxLength(255);

                entity.Property(e => e.SocialTitle).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(225);

                entity.Property(e => e.Url)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ViewCount).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductInLanguage)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductInLanguage_Product");
            });

            modelBuilder.Entity<ProductInProduct>(entity =>
            {
                entity.Property(e => e.Type)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.ConfigPrice).HasDefaultValue(0);
                entity.Property(e => e.ConfigNote).HasDefaultValue("");
            });

            modelBuilder.Entity<ProductInPromotion>(entity =>
            {
                entity.Property(e => e.LocationId).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductInPromotion)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_ProductInPromotion_Product");
            });

            modelBuilder.Entity<ProductInRegion>(entity =>
            {
                entity.HasKey(e => new { e.ZoneId, e.ProductId, e.IsPrimary });

                entity.Property(e => e.BigThumb)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductInRegion)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductInRegion_Product");

                entity.HasOne(d => d.Zone)
                    .WithMany(p => p.ProductInRegion)
                    .HasForeignKey(d => d.ZoneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductInRegion_Zone");
            });

            modelBuilder.Entity<ProductInZone>(entity =>
            {
                entity.HasKey(e => new { e.ZoneId, e.ProductId, e.IsPrimary });

                entity.Property(e => e.BigThumb)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductInZone)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductInZone_Product");

                entity.HasOne(d => d.Zone)
                    .WithMany(p => p.ProductInZone)
                    .HasForeignKey(d => d.ZoneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductInZone_Zone");
            });

            modelBuilder.Entity<ProductPriceInLocation>(entity =>
            {
                entity.HasKey(e => new { e.LocationId, e.ProductId });

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<ProductSpecifications>(entity =>
            {
                entity.Property(e => e.IsShowLayout).HasColumnName("isShowLayout");

                entity.Property(e => e.Value).HasMaxLength(255);
            });

            modelBuilder.Entity<ProductSpecificationTemplate>(entity =>
            {
                entity.Property(e => e.Value).HasMaxLength(255);
            });

            modelBuilder.Entity<ProductSpecificationTemplateInLanguage>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .ForSqlServerIsClustered(false);

                entity.HasIndex(e => new { e.LanguageCode, e.ProductSpecificationTemplateId })
                    .HasName("PK_ProductSpecificationTemplateInLanguage")
                    .IsUnique()
                    .ForSqlServerIsClustered();

                entity.Property(e => e.LanguageCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.HasOne(d => d.ProductSpecificationTemplate)
                    .WithMany(p => p.ProductSpecificationTemplateInLanguage)
                    .HasForeignKey(d => d.ProductSpecificationTemplateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductSpecificationTemplateInLanguage_ProductSpecificationTemplate");
            });

            modelBuilder.Entity<Promotion>(entity =>
            {
                entity.Property(e => e.IsDiscountPrice).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('default')");

                entity.Property(e => e.Value)
                    .HasColumnType("decimal(18, 0)")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<PromotionInLanguage>(entity =>
            {
                entity.HasKey(e => new { e.PromotionId, e.LanguageCode });

                entity.Property(e => e.LanguageCode).HasMaxLength(10);

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.Url)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Property>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Position)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Thumb).HasMaxLength(500);
            });

            modelBuilder.Entity<PropertyLanguage>(entity =>
            {
                entity.HasKey(e => new { e.PropertyId, e.LanguageCode });

                entity.Property(e => e.LanguageCode).HasMaxLength(10);

                entity.Property(e => e.Content).HasMaxLength(500);

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.PropertyLanguage)
                    .HasForeignKey(d => d.PropertyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PropertyLanguage_Property");
            });

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Rate).HasColumnType("decimal(18, 1)");
            });

            modelBuilder.Entity<Redirect>(entity =>
            {
                entity.HasKey(e => e.UrlOld);

                entity.Property(e => e.UrlOld)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.UrlNew)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Schema>(entity =>
            {
                entity.HasKey(e => e.Version);

                entity.ToTable("Schema", "HangFire");

                entity.Property(e => e.Version).ValueGeneratedNever();
            });

            modelBuilder.Entity<Server>(entity =>
            {
                entity.ToTable("Server", "HangFire");

                entity.Property(e => e.Id)
                    .HasMaxLength(100)
                    .ValueGeneratedNever();

                entity.Property(e => e.LastHeartbeat).HasColumnType("datetime");
            });

            modelBuilder.Entity<Set>(entity =>
            {
                entity.HasKey(e => new { e.Key, e.Value });

                entity.ToTable("Set", "HangFire");

                entity.Property(e => e.Key).HasMaxLength(100);

                entity.Property(e => e.Value).HasMaxLength(256);

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.HasKey(e => new { e.JobId, e.Id });

                entity.ToTable("State", "HangFire");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Reason).HasMaxLength(100);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.State)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_HangFire_State_Job");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.EditedBy)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LanguageCode).HasMaxLength(10);

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.ParentId).HasDefaultValueSql("((0))");

                entity.Property(e => e.Url)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TagInProductLanguage>(entity =>
            {
                entity.HasKey(e => new { e.TagId, e.ProductInLanguageId });

                entity.Property(e => e.ProductInLanguageId)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ActiveCode)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.Avatar)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FullName)
                    .HasMaxLength(255)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.IsSystem).HasDefaultValueSql("((0))");

                entity.Property(e => e.LastChangePass).HasColumnType("datetime");

                entity.Property(e => e.LastLogined).HasColumnType("datetime");

                entity.Property(e => e.LoginType).HasDefaultValueSql("((0))");

                entity.Property(e => e.Mobile)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.OtpSecretKey)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SocialId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserPermission>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.PermissionId, e.ZoneId });
            });

            modelBuilder.Entity<ViewCount>(entity =>
            {
                entity.Property(e => e.ViewDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Voucher>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.Property(e => e.Code)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.LanguageCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Value).HasColumnType("decimal(18, 0)");
            });

            modelBuilder.Entity<Zone>(entity =>
            {
                entity.Property(e => e.Avatar).HasMaxLength(500);

                entity.Property(e => e.Background).HasMaxLength(200);

                entity.Property(e => e.Banner).HasMaxLength(200);

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Icon).HasMaxLength(255);

                entity.Property(e => e.IsShowHome).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedBy).HasMaxLength(50);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.SortOrder).HasDefaultValueSql("((2))");
                entity.Property(e => e.ZoneSearchType).HasDefaultValueSql("((0))");

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ZoneInLanguage>(entity =>
            {
                entity.HasKey(e => new { e.LanguageCode, e.ZoneId });

                entity.Property(e => e.LanguageCode).HasMaxLength(10);

                entity.Property(e => e.BannerLink).HasMaxLength(200);

                entity.Property(e => e.BreadcrumUrl)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.BreadcrumbFirst).HasMaxLength(200);

                entity.Property(e => e.Content).HasColumnType("ntext");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.MetaCanonical).HasMaxLength(500);

                entity.Property(e => e.MetaDescription).HasMaxLength(255);

                entity.Property(e => e.MetaKeyword).HasMaxLength(255);

                entity.Property(e => e.MetaNoIndex).HasMaxLength(500);

                entity.Property(e => e.MetaTitle).HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.LanguageCodeNavigation)
                    .WithMany(p => p.ZoneInLanguage)
                    .HasForeignKey(d => d.LanguageCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ZoneInLanguage_Language");

                entity.HasOne(d => d.Zone)
                    .WithMany(p => p.ZoneInLanguage)
                    .HasForeignKey(d => d.ZoneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ZoneInLanguage_Zone");
            });
        }

        public override int SaveChanges()
        {
            var modified = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);

            foreach (EntityEntry item in modified)
            {
                var changedOrAddedItem = item.Entity as IDateTracking;
                if (changedOrAddedItem != null)
                {
                    if (item.State == EntityState.Added)
                    {
                        changedOrAddedItem.DateCreated = DateTime.Now;
                    }

                    changedOrAddedItem.DateModified = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer(AppSettings.ConnectionString);
            }
        }
    }
}