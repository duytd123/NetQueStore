using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NetQueStore.exe201.Models;

public partial class Exe2Context : DbContext
{
    public Exe2Context()
    {
    }

    public Exe2Context(DbContextOptions<Exe2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<ActivityLog> ActivityLogs { get; set; }

    public virtual DbSet<Bank> Banks { get; set; }

    public virtual DbSet<BankAccount> BankAccounts { get; set; }

    public virtual DbSet<BlogCategory> BlogCategories { get; set; }

    public virtual DbSet<BlogPost> BlogPosts { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Coupon> Coupons { get; set; }

    public virtual DbSet<CulturalEvent> CulturalEvents { get; set; }

    public virtual DbSet<District> Districts { get; set; }

    public virtual DbSet<Faq> Faqs { get; set; }

    public virtual DbSet<Food> Foods { get; set; }

    public virtual DbSet<FoodEvent> FoodEvents { get; set; }

    public virtual DbSet<FoodImage> FoodImages { get; set; }

    public virtual DbSet<FoodProducer> FoodProducers { get; set; }

    public virtual DbSet<GiftSet> GiftSets { get; set; }

    public virtual DbSet<GiftSetItem> GiftSetItems { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<MessageReply> MessageReplies { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<OrderStatusHistory> OrderStatusHistories { get; set; }

    public virtual DbSet<PasswordReset> PasswordResets { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<Producer> Producers { get; set; }

    public virtual DbSet<Province> Provinces { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Setting> Settings { get; set; }

    public virtual DbSet<ShippingAddress> ShippingAddresses { get; set; }

    public virtual DbSet<ShippingFee> ShippingFees { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Ward> Wards { get; set; }

    public virtual DbSet<Wishlist> Wishlists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server =DESKTOP-2713COR\\TRANDUY; database= exe2;uid=sa;pwd=123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityLog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__activity__3213E83FD614E658");

            entity.ToTable("activity_logs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActivityType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("activity_type");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("ip_address");
            entity.Property(e => e.UserAgent)
                .HasColumnType("text")
                .HasColumnName("user_agent");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.ActivityLogs)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__activity___user___7D0E9093");
        });

        modelBuilder.Entity<Bank>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__banks__3213E83F728AFE73");

            entity.ToTable("banks");

            entity.HasIndex(e => e.Code, "UQ__banks__357D4CF9F1E95B74").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.LogoFilename)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("logo_filename");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<BankAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__bank_acc__3213E83F5CC0B5DA");

            entity.ToTable("bank_accounts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccountName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("account_name");
            entity.Property(e => e.AccountNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("account_number");
            entity.Property(e => e.BankId).HasColumnName("bank_id");
            entity.Property(e => e.Branch)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("branch");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.IsPrimary).HasColumnName("is_primary");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Bank).WithMany(p => p.BankAccounts)
                .HasForeignKey(d => d.BankId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__bank_acco__bank___3B40CD36");
        });

        modelBuilder.Entity<BlogCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__blog_cat__3213E83FB9703857");

            entity.ToTable("blog_categories");

            entity.HasIndex(e => e.Slug, "UQ__blog_cat__32DD1E4CF375256E").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Slug)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("slug");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<BlogPost>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__blog_pos__3213E83F89038E53");

            entity.ToTable("blog_posts");

            entity.HasIndex(e => e.Slug, "UQ__blog_pos__32DD1E4CBC2A3078").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AuthorId).HasColumnName("author_id");
            entity.Property(e => e.Content)
                .HasColumnType("text")
                .HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Excerpt)
                .HasColumnType("text")
                .HasColumnName("excerpt");
            entity.Property(e => e.FeaturedImage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("featured_image");
            entity.Property(e => e.IsPublished).HasColumnName("is_published");
            entity.Property(e => e.MetaDescription)
                .HasColumnType("text")
                .HasColumnName("meta_description");
            entity.Property(e => e.MetaTitle)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("meta_title");
            entity.Property(e => e.PublishedAt)
                .HasColumnType("datetime")
                .HasColumnName("published_at");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("slug");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Author).WithMany(p => p.BlogPosts)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__blog_post__autho__671F4F74");

            entity.HasMany(d => d.BlogCategories).WithMany(p => p.BlogPosts)
                .UsingEntity<Dictionary<string, object>>(
                    "BlogPostCategory",
                    r => r.HasOne<BlogCategory>().WithMany()
                        .HasForeignKey("BlogCategoryId")
                        .HasConstraintName("FK__blog_post__blog___6EC0713C"),
                    l => l.HasOne<BlogPost>().WithMany()
                        .HasForeignKey("BlogPostId")
                        .HasConstraintName("FK__blog_post__blog___6DCC4D03"),
                    j =>
                    {
                        j.HasKey("BlogPostId", "BlogCategoryId").HasName("PK__blog_pos__C9D2F74A0ED78BD8");
                        j.ToTable("blog_post_categories");
                        j.IndexerProperty<int>("BlogPostId").HasColumnName("blog_post_id");
                        j.IndexerProperty<int>("BlogCategoryId").HasColumnName("blog_category_id");
                    });
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__carts__3213E83F959D5878");

            entity.ToTable("carts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.SessionId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("session_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__carts__user_id__7B5B524B");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__cart_ite__3213E83F0294E126");

            entity.ToTable("cart_items");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CartId).HasColumnName("cart_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.FoodId).HasColumnName("food_id");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasColumnName("quantity");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("unit_price");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.CartId)
                .HasConstraintName("FK__cart_item__cart___00200768");

            entity.HasOne(d => d.Food).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.FoodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__cart_item__food___01142BA1");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__categori__3213E83F167E741E");

            entity.ToTable("categories");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.DisplayOrder).HasColumnName("display_order");
            entity.Property(e => e.ImageFilename)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("image_filename");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__coupons__3213E83FE50262EF");

            entity.ToTable("coupons");

            entity.HasIndex(e => e.Code, "UQ__coupons__357D4CF997A546B8").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.DiscountType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("discount_type");
            entity.Property(e => e.DiscountValue)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("discount_value");
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("datetime")
                .HasColumnName("expires_at");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.MaxDiscount)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("max_discount");
            entity.Property(e => e.MinPurchase)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("min_purchase");
            entity.Property(e => e.StartsAt)
                .HasColumnType("datetime")
                .HasColumnName("starts_at");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UsageLimit).HasColumnName("usage_limit");
            entity.Property(e => e.UsedCount).HasColumnName("used_count");
        });

        modelBuilder.Entity<CulturalEvent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__cultural__3213E83F16137903");

            entity.ToTable("cultural_events");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.ImageFilename)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("image_filename");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.ProvinceId).HasColumnName("province_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Province).WithMany(p => p.CulturalEvents)
                .HasForeignKey(d => d.ProvinceId)
                .HasConstraintName("FK__cultural___provi__73BA3083");
        });

        modelBuilder.Entity<District>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__district__3213E83F13B274AD");

            entity.ToTable("districts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.ProvinceId).HasColumnName("province_id");

            entity.HasOne(d => d.Province).WithMany(p => p.Districts)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__districts__provi__1BC821DD");
        });

        modelBuilder.Entity<Faq>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__faqs__3213E83F9FE3AD34");

            entity.ToTable("faqs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Answer)
                .HasColumnType("text")
                .HasColumnName("answer");
            entity.Property(e => e.Category)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("category");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DisplayOrder).HasColumnName("display_order");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Question)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("question");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Food>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__foods__3213E83F4D0792E5");

            entity.ToTable("foods");

            entity.HasIndex(e => e.Slug, "UQ__foods__32DD1E4C239DB539").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Allergens)
                .HasColumnType("text")
                .HasColumnName("allergens");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Certification)
                .HasColumnType("text")
                .HasColumnName("certification");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CulturalSignificance)
                .HasColumnType("text")
                .HasColumnName("cultural_significance");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Ingredients)
                .HasColumnType("text")
                .HasColumnName("ingredients");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.IsFeatured).HasColumnName("is_featured");
            entity.Property(e => e.IsSpecial).HasColumnName("is_special");
            entity.Property(e => e.IsVegetarian).HasColumnName("is_vegetarian");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PreparationMethod)
                .HasColumnType("text")
                .HasColumnName("preparation_method");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("price");
            entity.Property(e => e.ProvinceId).HasColumnName("province_id");
            entity.Property(e => e.RegionId).HasColumnName("region_id");
            entity.Property(e => e.SalePrice)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("sale_price");
            entity.Property(e => e.ServingSuggestion)
                .HasColumnType("text")
                .HasColumnName("serving_suggestion");
            entity.Property(e => e.ShelfLife)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("shelf_life");
            entity.Property(e => e.Slug)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("slug");
            entity.Property(e => e.StockQuantity).HasColumnName("stock_quantity");
            entity.Property(e => e.StorageInstructions)
                .HasColumnType("text")
                .HasColumnName("storage_instructions");
            entity.Property(e => e.Unit)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("unit");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.Weight)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("weight");

            entity.HasOne(d => d.Category).WithMany(p => p.Foods)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__foods__category___5441852A");

            entity.HasOne(d => d.Province).WithMany(p => p.Foods)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__foods__province___5629CD9C");

            entity.HasOne(d => d.Region).WithMany(p => p.Foods)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__foods__region_id__5535A963");
        });

        modelBuilder.Entity<FoodEvent>(entity =>
        {
            entity.HasKey(e => new { e.FoodId, e.EventId }).HasName("PK__food_eve__4D7B42AAB31326E4");

            entity.ToTable("food_events");

            entity.Property(e => e.FoodId).HasColumnName("food_id");
            entity.Property(e => e.EventId).HasColumnName("event_id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");

            entity.HasOne(d => d.Event).WithMany(p => p.FoodEvents)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__food_even__event__778AC167");

            entity.HasOne(d => d.Food).WithMany(p => p.FoodEvents)
                .HasForeignKey(d => d.FoodId)
                .HasConstraintName("FK__food_even__food___76969D2E");
        });

        modelBuilder.Entity<FoodImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__food_ima__3213E83FC4C3D115");

            entity.ToTable("food_images");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DisplayOrder).HasColumnName("display_order");
            entity.Property(e => e.Filename)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("filename");
            entity.Property(e => e.FoodId).HasColumnName("food_id");
            entity.Property(e => e.IsPrimary).HasColumnName("is_primary");

            entity.HasOne(d => d.Food).WithMany(p => p.FoodImages)
                .HasForeignKey(d => d.FoodId)
                .HasConstraintName("FK__food_imag__food___5BE2A6F2");
        });

        modelBuilder.Entity<FoodProducer>(entity =>
        {
            entity.HasKey(e => new { e.FoodId, e.ProducerId }).HasName("PK__food_pro__B1EBBED4BB2AE233");

            entity.ToTable("food_producers");

            entity.Property(e => e.FoodId).HasColumnName("food_id");
            entity.Property(e => e.ProducerId).HasColumnName("producer_id");
            entity.Property(e => e.IsPrimary).HasColumnName("is_primary");

            entity.HasOne(d => d.Food).WithMany(p => p.FoodProducers)
                .HasForeignKey(d => d.FoodId)
                .HasConstraintName("FK__food_prod__food___6E01572D");

            entity.HasOne(d => d.Producer).WithMany(p => p.FoodProducers)
                .HasForeignKey(d => d.ProducerId)
                .HasConstraintName("FK__food_prod__produ__6EF57B66");
        });

        modelBuilder.Entity<GiftSet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__gift_set__3213E83FF942961F");

            entity.ToTable("gift_sets");

            entity.HasIndex(e => e.Slug, "UQ__gift_set__32DD1E4CE6DDA26A").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.ImageFilename)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("image_filename");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.IsFeatured).HasColumnName("is_featured");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Occasion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("occasion");
            entity.Property(e => e.PackagingType)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("packaging_type");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("price");
            entity.Property(e => e.SalePrice)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("sale_price");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("slug");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<GiftSetItem>(entity =>
        {
            entity.HasKey(e => new { e.GiftSetId, e.FoodId }).HasName("PK__gift_set__346B450952D07B4D");

            entity.ToTable("gift_set_items");

            entity.Property(e => e.GiftSetId).HasColumnName("gift_set_id");
            entity.Property(e => e.FoodId).HasColumnName("food_id");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasColumnName("quantity");

            entity.HasOne(d => d.Food).WithMany(p => p.GiftSetItems)
                .HasForeignKey(d => d.FoodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__gift_set___food___114A936A");

            entity.HasOne(d => d.GiftSet).WithMany(p => p.GiftSetItems)
                .HasForeignKey(d => d.GiftSetId)
                .HasConstraintName("FK__gift_set___gift___10566F31");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__messages__3213E83F00FFF349");

            entity.ToTable("messages");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AssignedTo).HasColumnName("assigned_to");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("firstname");
            entity.Property(e => e.IsResolved).HasColumnName("is_resolved");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("lastname");
            entity.Property(e => e.Message1)
                .HasColumnType("text")
                .HasColumnName("message");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("unread")
                .HasColumnName("status");
            entity.Property(e => e.Subject)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("subject");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.Messages)
                .HasForeignKey(d => d.AssignedTo)
                .HasConstraintName("FK__messages__assign__5CA1C101");
        });

        modelBuilder.Entity<MessageReply>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__message___3213E83F71A482A4");

            entity.ToTable("message_replies");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.MessageId).HasColumnName("message_id");
            entity.Property(e => e.Reply)
                .HasColumnType("text")
                .HasColumnName("reply");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Message).WithMany(p => p.MessageReplies)
                .HasForeignKey(d => d.MessageId)
                .HasConstraintName("FK__message_r__messa__607251E5");

            entity.HasOne(d => d.User).WithMany(p => p.MessageReplies)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__message_r__user___6166761E");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__orders__3213E83F66FA6F3E");

            entity.ToTable("orders");

            entity.HasIndex(e => e.OrderNumber, "UQ__orders__730E34DFC66C0A0B").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ActualDelivery)
                .HasColumnType("datetime")
                .HasColumnName("actual_delivery");
            entity.Property(e => e.AdminNotes)
                .HasColumnType("text")
                .HasColumnName("admin_notes");
            entity.Property(e => e.BankId).HasColumnName("bank_id");
            entity.Property(e => e.CouponId).HasColumnName("coupon_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DeliveryAddress)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("delivery_address");
            entity.Property(e => e.DeliveryNotes)
                .HasColumnType("text")
                .HasColumnName("delivery_notes");
            entity.Property(e => e.Discount)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("discount");
            entity.Property(e => e.DistrictId).HasColumnName("district_id");
            entity.Property(e => e.EstimatedDelivery)
                .HasColumnType("datetime")
                .HasColumnName("estimated_delivery");
            entity.Property(e => e.GiftWrappingFee)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("gift_wrapping_fee");
            entity.Property(e => e.Notes)
                .HasColumnType("text")
                .HasColumnName("notes");
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("order_date");
            entity.Property(e => e.OrderNumber)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("order_number");
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("order_status");
            entity.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id");
            entity.Property(e => e.PaymentStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("payment_status");
            entity.Property(e => e.ProvinceId).HasColumnName("province_id");
            entity.Property(e => e.RecipientEmail)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("recipient_email");
            entity.Property(e => e.RecipientName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("recipient_name");
            entity.Property(e => e.RecipientPhone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("recipient_phone");
            entity.Property(e => e.ShippingFee)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("shipping_fee");
            entity.Property(e => e.ShippingMethod)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("shipping_method");
            entity.Property(e => e.ShippingProvider)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("shipping_provider");
            entity.Property(e => e.Subtotal)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("subtotal");
            entity.Property(e => e.Tax)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("tax");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("total_amount");
            entity.Property(e => e.TrackingNumber)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("tracking_number");
            entity.Property(e => e.TransactionId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("transaction_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.WardId).HasColumnName("ward_id");

            entity.HasOne(d => d.Bank).WithMany(p => p.Orders)
                .HasForeignKey(d => d.BankId)
                .HasConstraintName("FK__orders__bank_id__498EEC8D");

            entity.HasOne(d => d.Coupon).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CouponId)
                .HasConstraintName("FK__orders__coupon_i__47A6A41B");

            entity.HasOne(d => d.District).WithMany(p => p.Orders)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__orders__district__4B7734FF");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__orders__payment___489AC854");

            entity.HasOne(d => d.Province).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__orders__province__4A8310C6");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__orders__user_id__46B27FE2");

            entity.HasOne(d => d.Ward).WithMany(p => p.Orders)
                .HasForeignKey(d => d.WardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__orders__ward_id__4C6B5938");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__order_it__3213E83F858D6C91");

            entity.ToTable("order_items");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.FoodId).HasColumnName("food_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ProductName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("product_name");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Subtotal)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("subtotal");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("unit_price");

            entity.HasOne(d => d.Food).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.FoodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__order_ite__food___51300E55");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__order_ite__order__503BEA1C");
        });

        modelBuilder.Entity<OrderStatusHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__order_st__3213E83FEB3FC26E");

            entity.ToTable("order_status_history");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ChangedBy).HasColumnName("changed_by");
            entity.Property(e => e.Comment)
                .HasColumnType("text")
                .HasColumnName("comment");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("status");

            entity.HasOne(d => d.ChangedByNavigation).WithMany(p => p.OrderStatusHistories)
                .HasForeignKey(d => d.ChangedBy)
                .HasConstraintName("FK__order_sta__chang__55F4C372");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderStatusHistories)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__order_sta__order__55009F39");
        });

        modelBuilder.Entity<PasswordReset>(entity =>
        {
            entity.HasKey(e => e.Email).HasName("PK__password__AB6E6165E86D432F");

            entity.ToTable("password_resets");

            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("datetime")
                .HasColumnName("expires_at");
            entity.Property(e => e.Token)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("token");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__payment___3213E83F7BFB1929");

            entity.ToTable("payment_methods");

            entity.HasIndex(e => e.Code, "UQ__payment___357D4CF96AAFA371").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.DisplayOrder).HasColumnName("display_order");
            entity.Property(e => e.Instructions)
                .HasColumnType("text")
                .HasColumnName("instructions");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Producer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__producer__3213E83FB2DC83A1");

            entity.ToTable("producers");

            entity.HasIndex(e => e.Slug, "UQ__producer__32DD1E4C46CDC3F3").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.EstablishedYear).HasColumnName("established_year");
            entity.Property(e => e.Featured).HasColumnName("featured");
            entity.Property(e => e.ImageFilename)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("image_filename");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.ProvinceId).HasColumnName("province_id");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("slug");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.Website)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("website");

            entity.HasOne(d => d.Province).WithMany(p => p.Producers)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__producers__provi__6A30C649");
        });

        modelBuilder.Entity<Province>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__province__3213E83F80D41B17");

            entity.ToTable("provinces");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.RegionId).HasColumnName("region_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Region).WithMany(p => p.Provinces)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__provinces__regio__45F365D3");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__regions__3213E83FBA92FACE");

            entity.ToTable("regions");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.ImageFilename)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("image_filename");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__reviews__3213E83F4C635CE3");

            entity.ToTable("reviews");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comment)
                .HasColumnType("text")
                .HasColumnName("comment");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.FoodId).HasColumnName("food_id");
            entity.Property(e => e.IsApproved).HasColumnName("is_approved");
            entity.Property(e => e.IsVerifiedPurchase).HasColumnName("is_verified_purchase");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Food).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.FoodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__reviews__food_id__628FA481");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__reviews__user_id__6383C8BA");
        });

        modelBuilder.Entity<Setting>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__settings__3213E83F4B26399D");

            entity.ToTable("settings");

            entity.HasIndex(e => e.SettingKey, "UQ__settings__0DFAC4272EB17829").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.IsPublic)
                .HasDefaultValue(true)
                .HasColumnName("is_public");
            entity.Property(e => e.SettingGroup)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValue("general")
                .HasColumnName("setting_group");
            entity.Property(e => e.SettingKey)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("setting_key");
            entity.Property(e => e.SettingValue)
                .HasColumnType("text")
                .HasColumnName("setting_value");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
        });

        modelBuilder.Entity<ShippingAddress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__shipping__3213E83F3D6C9CEC");

            entity.ToTable("shipping_addresses");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AddressDetail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address_detail");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DistrictId).HasColumnName("district_id");
            entity.Property(e => e.IsDefault).HasColumnName("is_default");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.ProvinceId).HasColumnName("province_id");
            entity.Property(e => e.RecipientName)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("recipient_name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.WardId).HasColumnName("ward_id");

            entity.HasOne(d => d.District).WithMany(p => p.ShippingAddresses)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__shipping___distr__2645B050");

            entity.HasOne(d => d.Province).WithMany(p => p.ShippingAddresses)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__shipping___provi__25518C17");

            entity.HasOne(d => d.User).WithMany(p => p.ShippingAddresses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__shipping___user___245D67DE");

            entity.HasOne(d => d.Ward).WithMany(p => p.ShippingAddresses)
                .HasForeignKey(d => d.WardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__shipping___ward___2739D489");
        });

        modelBuilder.Entity<ShippingFee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__shipping__3213E83F25692D7F");

            entity.ToTable("shipping_fees");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Fee)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("fee");
            entity.Property(e => e.FreeShippingMin)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("free_shipping_min");
            entity.Property(e => e.MinOrderValue)
                .HasColumnType("decimal(16, 2)")
                .HasColumnName("min_order_value");
            entity.Property(e => e.ProvinceId).HasColumnName("province_id");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Province).WithMany(p => p.ShippingFees)
                .HasForeignKey(d => d.ProvinceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__shipping___provi__2B0A656D");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83F2694C156");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UQ__users__AB6E61644254E430").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.EmailVerifiedAt)
                .HasColumnType("datetime")
                .HasColumnName("email_verified_at");
            entity.Property(e => e.Firstname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("firstname");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("lastname");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.ProfileImage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("profile_image");
            entity.Property(e => e.Role)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("role");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updated_at");
            entity.Property(e => e.VerificationToken)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("verification_token");
        });

        modelBuilder.Entity<Ward>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__wards__3213E83F40B5ACA6");

            entity.ToTable("wards");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.DistrictId).HasColumnName("district_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("name");

            entity.HasOne(d => d.District).WithMany(p => p.Wards)
                .HasForeignKey(d => d.DistrictId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__wards__district___1F98B2C1");
        });

        modelBuilder.Entity<Wishlist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__wishlist__3213E83F54D768DB");

            entity.ToTable("wishlists");

            entity.HasIndex(e => new { e.UserId, e.FoodId }, "UQ_wishlist_user_food").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.FoodId).HasColumnName("food_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Food).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.FoodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__wishlists__food___06CD04F7");

            entity.HasOne(d => d.User).WithMany(p => p.Wishlists)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__wishlists__user___05D8E0BE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
