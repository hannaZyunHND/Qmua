using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MI.Entity.Models
{
    public partial class enterbuy_mergedContext : DbContext
    {
        public enterbuy_mergedContext()
        {
        }

        public enterbuy_mergedContext(DbContextOptions<enterbuy_mergedContext> options)
            : base(options)
        {
        }

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

        // Unable to generate entity type for table 'HangFire.Counter'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.AspNetUserRoles'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.Permissions'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=118.70.185.130;Database=enterbuy_merged;Trusted_Connection=False;User Id=ndev;password=asd123!@##@");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
        }
    }
}
