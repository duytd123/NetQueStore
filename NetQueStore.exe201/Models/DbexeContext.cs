//using System;
//using System.Collections.Generic;
//using Microsoft.EntityFrameworkCore;

//namespace NetQueStore.exe201.Models;

//public partial class DbexeContext : DbContext
//{
//    public DbexeContext()
//    {
//    }

//    public DbexeContext(DbContextOptions<DbexeContext> options)
//        : base(options)
//    {
//    }

//    public virtual DbSet<ActivityLog> ActivityLogs { get; set; }

//    public virtual DbSet<Bank> Banks { get; set; }

//    public virtual DbSet<BankAccount> BankAccounts { get; set; }

//    public virtual DbSet<BlogCategory> BlogCategories { get; set; }

//    public virtual DbSet<BlogPost> BlogPosts { get; set; }

//    public virtual DbSet<Cart> Carts { get; set; }

//    public virtual DbSet<CartItem> CartItems { get; set; }

//    public virtual DbSet<Category> Categories { get; set; }

//    public virtual DbSet<Coupon> Coupons { get; set; }

//    public virtual DbSet<CulturalEvent> CulturalEvents { get; set; }

//    public virtual DbSet<CustomerGroup> CustomerGroups { get; set; }

//    public virtual DbSet<District> Districts { get; set; }

//    public virtual DbSet<Faq> Faqs { get; set; }

//    public virtual DbSet<Food> Foods { get; set; }

//    public virtual DbSet<FoodEvent> FoodEvents { get; set; }

//    public virtual DbSet<FoodImage> FoodImages { get; set; }

//    public virtual DbSet<FoodProducer> FoodProducers { get; set; }

//    public virtual DbSet<GiftSet> GiftSets { get; set; }

//    public virtual DbSet<GiftSetItem> GiftSetItems { get; set; }

//    public virtual DbSet<GuestOrdersTracking> GuestOrdersTrackings { get; set; }

//    public virtual DbSet<Message> Messages { get; set; }

//    public virtual DbSet<MessageReply> MessageReplies { get; set; }

//    public virtual DbSet<NewsletterSubscription> NewsletterSubscriptions { get; set; }

//    public virtual DbSet<Notification> Notifications { get; set; }

//    public virtual DbSet<Order> Orders { get; set; }

//    public virtual DbSet<OrderItem> OrderItems { get; set; }

//    public virtual DbSet<OrderStatusHistory> OrderStatusHistories { get; set; }

//    public virtual DbSet<OutOfStockNotification> OutOfStockNotifications { get; set; }

//    public virtual DbSet<PasswordReset> PasswordResets { get; set; }

//    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

//    public virtual DbSet<Producer> Producers { get; set; }

//    public virtual DbSet<Province> Provinces { get; set; }

//    public virtual DbSet<Region> Regions { get; set; }

//    public virtual DbSet<Review> Reviews { get; set; }

//    public virtual DbSet<Setting> Settings { get; set; }

//    public virtual DbSet<ShippingAddress> ShippingAddresses { get; set; }

//    public virtual DbSet<ShippingFee> ShippingFees { get; set; }

//    public virtual DbSet<User> Users { get; set; }

//    public virtual DbSet<Ward> Wards { get; set; }

//    public virtual DbSet<Wishlist> Wishlists { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//    {
//        base.OnConfiguring(optionsBuilder);
//    }

//    protected override void OnModelCreating(ModelBuilder modelBuilder)
//    {
//        modelBuilder.Entity<ActivityLog>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__activity__3213E83F149A92E7");

//            entity.ToTable("activity_logs");

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.ActivityType)
//                .HasMaxLength(100)
//                .IsUnicode(false)
//                .HasColumnName("activity_type");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.Description)
//                .HasColumnType("text")
//                .HasColumnName("description");
//            entity.Property(e => e.IpAddress)
//                .HasMaxLength(45)
//                .IsUnicode(false)
//                .HasColumnName("ip_address");
//            entity.Property(e => e.UserAgent)
//                .HasColumnType("text")
//                .HasColumnName("user_agent");
//            entity.Property(e => e.UserId).HasColumnName("user_id");

//            entity.HasOne(d => d.User).WithMany(p => p.ActivityLogs)
//                .HasForeignKey(d => d.UserId)
//                .HasConstraintName("FK__activity___user___01D345B0");
//        });

//        modelBuilder.Entity<Bank>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__banks__3213E83FEB354EC0");

//            entity.ToTable("banks");

//            entity.HasIndex(e => e.Code, "UQ__banks__357D4CF9B17E012D").IsUnique();

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.Code)
//                .HasMaxLength(20)
//                .IsUnicode(false)
//                .HasColumnName("code");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.IsActive)
//                .HasDefaultValue(true)
//                .HasColumnName("is_active");
//            entity.Property(e => e.LogoFilename)
//                .HasMaxLength(255)
//                .IsUnicode(false)
//                .HasColumnName("logo_filename");
//            entity.Property(e => e.Name)
//                .HasMaxLength(200)
//                .IsUnicode(false)
//                .HasColumnName("name");
//            entity.Property(e => e.UpdatedAt)
//                .HasColumnType("datetime")
//                .HasColumnName("updated_at");
//        });

//        modelBuilder.Entity<BankAccount>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__bank_acc__3213E83F4865A412");

//            entity.ToTable("bank_accounts");

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.AccountName)
//                .HasMaxLength(200)
//                .IsUnicode(false)
//                .HasColumnName("account_name");
//            entity.Property(e => e.AccountNumber)
//                .HasMaxLength(50)
//                .IsUnicode(false)
//                .HasColumnName("account_number");
//            entity.Property(e => e.BankId).HasColumnName("bank_id");
//            entity.Property(e => e.Branch)
//                .HasMaxLength(200)
//                .IsUnicode(false)
//                .HasColumnName("branch");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.IsActive)
//                .HasDefaultValue(true)
//                .HasColumnName("is_active");
//            entity.Property(e => e.IsPrimary).HasColumnName("is_primary");
//            entity.Property(e => e.UpdatedAt)
//                .HasColumnType("datetime")
//                .HasColumnName("updated_at");

//            entity.HasOne(d => d.Bank).WithMany(p => p.BankAccounts)
//                .HasForeignKey(d => d.BankId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__bank_acco__bank___3B40CD36");
//        });

//        modelBuilder.Entity<BlogCategory>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__blog_cat__3213E83F157BCE51");

//            entity.ToTable("blog_categories");

//            entity.HasIndex(e => e.Slug, "UQ__blog_cat__32DD1E4C9B350EBE").IsUnique();

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.Description)
//                .HasColumnType("text")
//                .HasColumnName("description");
//            entity.Property(e => e.Name)
//                .HasMaxLength(100)
//                .IsUnicode(false)
//                .HasColumnName("name");
//            entity.Property(e => e.Slug)
//                .HasMaxLength(100)
//                .IsUnicode(false)
//                .HasColumnName("slug");
//            entity.Property(e => e.UpdatedAt)
//                .HasColumnType("datetime")
//                .HasColumnName("updated_at");
//        });

//        modelBuilder.Entity<BlogPost>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__blog_pos__3213E83F4077145B");

//            entity.ToTable("blog_posts");

//            entity.HasIndex(e => e.Slug, "UQ__blog_pos__32DD1E4C339B7680").IsUnique();

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.AuthorId).HasColumnName("author_id");
//            entity.Property(e => e.Content)
//                .HasColumnType("text")
//                .HasColumnName("content");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.Excerpt)
//                .HasColumnType("text")
//                .HasColumnName("excerpt");
//            entity.Property(e => e.FeaturedImage)
//                .HasMaxLength(255)
//                .IsUnicode(false)
//                .HasColumnName("featured_image");
//            entity.Property(e => e.IsPublished).HasColumnName("is_published");
//            entity.Property(e => e.MetaDescription)
//                .HasColumnType("text")
//                .HasColumnName("meta_description");
//            entity.Property(e => e.MetaTitle)
//                .HasMaxLength(255)
//                .IsUnicode(false)
//                .HasColumnName("meta_title");
//            entity.Property(e => e.PublishedAt)
//                .HasColumnType("datetime")
//                .HasColumnName("published_at");
//            entity.Property(e => e.Slug)
//                .HasMaxLength(255)
//                .IsUnicode(false)
//                .HasColumnName("slug");
//            entity.Property(e => e.Title)
//                .HasMaxLength(255)
//                .IsUnicode(false)
//                .HasColumnName("title");
//            entity.Property(e => e.UpdatedAt)
//                .HasColumnType("datetime")
//                .HasColumnName("updated_at");

//            entity.HasOne(d => d.Author).WithMany(p => p.BlogPosts)
//                .HasForeignKey(d => d.AuthorId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__blog_post__autho__6BE40491");

//            entity.HasMany(d => d.BlogCategories).WithMany(p => p.BlogPosts)
//                .UsingEntity<Dictionary<string, object>>(
//                    "BlogPostCategory",
//                    r => r.HasOne<BlogCategory>().WithMany()
//                        .HasForeignKey("BlogCategoryId")
//                        .HasConstraintName("FK__blog_post__blog___73852659"),
//                    l => l.HasOne<BlogPost>().WithMany()
//                        .HasForeignKey("BlogPostId")
//                        .HasConstraintName("FK__blog_post__blog___72910220"),
//                    j =>
//                    {
//                        j.HasKey("BlogPostId", "BlogCategoryId").HasName("PK__blog_pos__C9D2F74ABFF98F2B");
//                        j.ToTable("blog_post_categories");
//                        j.IndexerProperty<int>("BlogPostId").HasColumnName("blog_post_id");
//                        j.IndexerProperty<int>("BlogCategoryId").HasColumnName("blog_category_id");
//                    });
//        });

//        modelBuilder.Entity<Cart>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__carts__3213E83FFE1D9AA9");

//            entity.ToTable("carts");

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.CustomerEmail)
//                .HasMaxLength(150)
//                .IsUnicode(false)
//                .HasColumnName("customer_email");
//            entity.Property(e => e.CustomerName)
//                .HasMaxLength(200)
//                .IsUnicode(false)
//                .HasColumnName("customer_name");
//            entity.Property(e => e.CustomerPhone)
//                .HasMaxLength(20)
//                .IsUnicode(false)
//                .HasColumnName("customer_phone");
//            entity.Property(e => e.SessionId)
//                .HasMaxLength(100)
//                .IsUnicode(false)
//                .HasColumnName("session_id");
//            entity.Property(e => e.UpdatedAt)
//                .HasColumnType("datetime")
//                .HasColumnName("updated_at");
//            entity.Property(e => e.UserId).HasColumnName("user_id");

//            entity.HasOne(d => d.User).WithMany(p => p.Carts)
//                .HasForeignKey(d => d.UserId)
//                .HasConstraintName("FK__carts__user_id__02FC7413");
//        });

//        modelBuilder.Entity<CartItem>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__cart_ite__3213E83F480E553F");

//            entity.ToTable("cart_items");

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.CartId).HasColumnName("cart_id");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.FoodId).HasColumnName("food_id");
//            entity.Property(e => e.Quantity)
//                .HasDefaultValue(1)
//                .HasColumnName("quantity");
//            entity.Property(e => e.UnitPrice)
//                .HasColumnType("decimal(16, 2)")
//                .HasColumnName("unit_price");
//            entity.Property(e => e.UpdatedAt)
//                .HasColumnType("datetime")
//                .HasColumnName("updated_at");

//            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems)
//                .HasForeignKey(d => d.CartId)
//                .HasConstraintName("FK__cart_item__cart___07C12930");

//            entity.HasOne(d => d.Food).WithMany(p => p.CartItems)
//                .HasForeignKey(d => d.FoodId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__cart_item__food___08B54D69");
//        });

//        modelBuilder.Entity<Category>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__categori__3213E83FE1ED7430");

//            entity.ToTable("categories");

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.Description)
//                .HasColumnType("text")
//                .HasColumnName("description");
//            entity.Property(e => e.DisplayOrder).HasColumnName("display_order");
//            entity.Property(e => e.ImageFilename)
//                .HasMaxLength(255)
//                .IsUnicode(false)
//                .HasColumnName("image_filename");
//            entity.Property(e => e.IsActive)
//                .HasDefaultValue(true)
//                .HasColumnName("is_active");
//            entity.Property(e => e.Name)
//                .HasMaxLength(100)
//                .IsUnicode(false)
//                .HasColumnName("name");
//            entity.Property(e => e.UpdatedAt)
//                .HasColumnType("datetime")
//                .HasColumnName("updated_at");
//        });

//        modelBuilder.Entity<Coupon>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__coupons__3213E83F6DE6EFF3");

//            entity.ToTable("coupons");

//            entity.HasIndex(e => e.Code, "UQ__coupons__357D4CF90F9B41D0").IsUnique();

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.Code)
//                .HasMaxLength(50)
//                .IsUnicode(false)
//                .HasColumnName("code");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.Description)
//                .HasColumnType("text")
//                .HasColumnName("description");
//            entity.Property(e => e.DiscountType)
//                .HasMaxLength(20)
//                .IsUnicode(false)
//                .HasColumnName("discount_type");
//            entity.Property(e => e.DiscountValue)
//                .HasColumnType("decimal(16, 2)")
//                .HasColumnName("discount_value");
//            entity.Property(e => e.ExpiresAt)
//                .HasColumnType("datetime")
//                .HasColumnName("expires_at");
//            entity.Property(e => e.IsActive)
//                .HasDefaultValue(true)
//                .HasColumnName("is_active");
//            entity.Property(e => e.MaxDiscount)
//                .HasColumnType("decimal(16, 2)")
//                .HasColumnName("max_discount");
//            entity.Property(e => e.MinPurchase)
//                .HasColumnType("decimal(16, 2)")
//                .HasColumnName("min_purchase");
//            entity.Property(e => e.StartsAt)
//                .HasColumnType("datetime")
//                .HasColumnName("starts_at");
//            entity.Property(e => e.UpdatedAt)
//                .HasColumnType("datetime")
//                .HasColumnName("updated_at");
//            entity.Property(e => e.UsageLimit).HasColumnName("usage_limit");
//            entity.Property(e => e.UsedCount).HasColumnName("used_count");
//        });

//        modelBuilder.Entity<CulturalEvent>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__cultural__3213E83FE1E86F9A");

//            entity.ToTable("cultural_events");

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.Description)
//                .HasColumnType("text")
//                .HasColumnName("description");
//            entity.Property(e => e.EndDate).HasColumnName("end_date");
//            entity.Property(e => e.ImageFilename)
//                .HasMaxLength(255)
//                .IsUnicode(false)
//                .HasColumnName("image_filename");
//            entity.Property(e => e.IsActive)
//                .HasDefaultValue(true)
//                .HasColumnName("is_active");
//            entity.Property(e => e.Name)
//                .HasMaxLength(200)
//                .IsUnicode(false)
//                .HasColumnName("name");
//            entity.Property(e => e.ProvinceId).HasColumnName("province_id");
//            entity.Property(e => e.StartDate).HasColumnName("start_date");
//            entity.Property(e => e.UpdatedAt)
//                .HasColumnType("datetime")
//                .HasColumnName("updated_at");

//            entity.HasOne(d => d.Province).WithMany(p => p.CulturalEvents)
//                .HasForeignKey(d => d.ProvinceId)
//                .HasConstraintName("FK__cultural___provi__7B5B524B");
//        });

//        modelBuilder.Entity<CustomerGroup>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__customer__3213E83FF9310F21");

//            entity.ToTable("customer_groups");

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.Description)
//                .HasColumnType("text")
//                .HasColumnName("description");
//            entity.Property(e => e.Name)
//                .HasMaxLength(100)
//                .IsUnicode(false)
//                .HasColumnName("name");
//            entity.Property(e => e.UpdatedAt)
//                .HasColumnType("datetime")
//                .HasColumnName("updated_at");
//        });

//        modelBuilder.Entity<District>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__district__3213E83F26D4E03E");

//            entity.ToTable("districts");

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.Name)
//                .HasMaxLength(100)
//                .IsUnicode(false)
//                .HasColumnName("name");
//            entity.Property(e => e.ProvinceId).HasColumnName("province_id");

//            entity.HasOne(d => d.Province).WithMany(p => p.Districts)
//                .HasForeignKey(d => d.ProvinceId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__districts__provi__49C3F6B7");
//        });

//        modelBuilder.Entity<Faq>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__faqs__3213E83F26445A93");

//            entity.ToTable("faqs");

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.Answer)
//                .HasColumnType("text")
//                .HasColumnName("answer");
//            entity.Property(e => e.Category)
//                .HasMaxLength(100)
//                .IsUnicode(false)
//                .HasColumnName("category");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.DisplayOrder).HasColumnName("display_order");
//            entity.Property(e => e.IsActive)
//                .HasDefaultValue(true)
//                .HasColumnName("is_active");
//            entity.Property(e => e.Question)
//                .HasMaxLength(255)
//                .IsUnicode(false)
//                .HasColumnName("question");
//            entity.Property(e => e.UpdatedAt)
//                .HasColumnType("datetime")
//                .HasColumnName("updated_at");
//        });

//        modelBuilder.Entity<Food>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__foods__3213E83F8307AEFF");

//            entity.ToTable("foods");

//            entity.HasIndex(e => e.Slug, "UQ__foods__32DD1E4C85A90E5D").IsUnique();

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.Allergens)
//                .HasColumnType("text")
//                .HasColumnName("allergens");
//            entity.Property(e => e.CategoryId).HasColumnName("category_id");
//            entity.Property(e => e.Certification)
//                .HasColumnType("text")
//                .HasColumnName("certification");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.CulturalSignificance)
//                .HasColumnType("text")
//                .HasColumnName("cultural_significance");
//            entity.Property(e => e.Description)
//                .HasColumnType("text")
//                .HasColumnName("description");
//            entity.Property(e => e.Ingredients)
//                .HasColumnType("text")
//                .HasColumnName("ingredients");
//            entity.Property(e => e.IsActive)
//                .HasDefaultValue(true)
//                .HasColumnName("is_active");
//            entity.Property(e => e.IsFeatured).HasColumnName("is_featured");
//            entity.Property(e => e.IsSpecial).HasColumnName("is_special");
//            entity.Property(e => e.IsVegetarian).HasColumnName("is_vegetarian");
//            entity.Property(e => e.Name)
//                .HasMaxLength(100)
//                .IsUnicode(false)
//                .HasColumnName("name");
//            entity.Property(e => e.PreparationMethod)
//                .HasColumnType("text")
//                .HasColumnName("preparation_method");
//            entity.Property(e => e.Price)
//                .HasColumnType("decimal(16, 2)")
//                .HasColumnName("price");
//            entity.Property(e => e.ProvinceId).HasColumnName("province_id");
//            entity.Property(e => e.RegionId).HasColumnName("region_id");
//            entity.Property(e => e.SalePrice)
//                .HasColumnType("decimal(16, 2)")
//                .HasColumnName("sale_price");
//            entity.Property(e => e.ServingSuggestion)
//                .HasColumnType("text")
//                .HasColumnName("serving_suggestion");
//            entity.Property(e => e.ShelfLife)
//                .HasMaxLength(100)
//                .IsUnicode(false)
//                .HasColumnName("shelf_life");
//            entity.Property(e => e.Slug)
//                .HasMaxLength(150)
//                .IsUnicode(false)
//                .HasColumnName("slug");
//            entity.Property(e => e.StockQuantity).HasColumnName("stock_quantity");
//            entity.Property(e => e.StorageInstructions)
//                .HasColumnType("text")
//                .HasColumnName("storage_instructions");
//            entity.Property(e => e.Unit)
//                .HasMaxLength(50)
//                .IsUnicode(false)
//                .HasColumnName("unit");
//            entity.Property(e => e.UpdatedAt)
//                .HasColumnType("datetime")
//                .HasColumnName("updated_at");
//            entity.Property(e => e.Weight)
//                .HasColumnType("decimal(10, 2)")
//                .HasColumnName("weight");

//            entity.HasOne(d => d.Category).WithMany(p => p.Foods)
//                .HasForeignKey(d => d.CategoryId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__foods__category___5BE2A6F2");

//            entity.HasOne(d => d.Province).WithMany(p => p.Foods)
//                .HasForeignKey(d => d.ProvinceId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__foods__province___5DCAEF64");

//            entity.HasOne(d => d.Region).WithMany(p => p.Foods)
//                .HasForeignKey(d => d.RegionId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__foods__region_id__5CD6CB2B");
//        });

//        modelBuilder.Entity<FoodEvent>(entity =>
//        {
//            entity.HasKey(e => new { e.FoodId, e.EventId }).HasName("PK__food_eve__4D7B42AA876D3B3C");

//            entity.ToTable("food_events");

//            entity.Property(e => e.FoodId).HasColumnName("food_id");
//            entity.Property(e => e.EventId).HasColumnName("event_id");
//            entity.Property(e => e.Description)
//                .HasColumnType("text")
//                .HasColumnName("description");

//            entity.HasOne(d => d.Event).WithMany(p => p.FoodEvents)
//                .HasForeignKey(d => d.EventId)
//                .HasConstraintName("FK__food_even__event__7F2BE32F");

//            entity.HasOne(d => d.Food).WithMany(p => p.FoodEvents)
//                .HasForeignKey(d => d.FoodId)
//                .HasConstraintName("FK__food_even__food___7E37BEF6");
//        });

//        modelBuilder.Entity<FoodImage>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__food_ima__3213E83FDB3F200E");

//            entity.ToTable("food_images");

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.DisplayOrder).HasColumnName("display_order");
//            entity.Property(e => e.Filename)
//                .HasMaxLength(255)
//                .IsUnicode(false)
//                .HasColumnName("filename");
//            entity.Property(e => e.FoodId).HasColumnName("food_id");
//            entity.Property(e => e.IsPrimary).HasColumnName("is_primary");

//            entity.HasOne(d => d.Food).WithMany(p => p.FoodImages)
//                .HasForeignKey(d => d.FoodId)
//                .HasConstraintName("FK__food_imag__food___6383C8BA");
//        });

//        modelBuilder.Entity<FoodProducer>(entity =>
//        {
//            entity.HasKey(e => new { e.FoodId, e.ProducerId }).HasName("PK__food_pro__B1EBBED4ABE9FB82");

//            entity.ToTable("food_producers");

//            entity.Property(e => e.FoodId).HasColumnName("food_id");
//            entity.Property(e => e.ProducerId).HasColumnName("producer_id");
//            entity.Property(e => e.IsPrimary).HasColumnName("is_primary");

//            entity.HasOne(d => d.Food).WithMany(p => p.FoodProducers)
//                .HasForeignKey(d => d.FoodId)
//                .HasConstraintName("FK__food_prod__food___75A278F5");

//            entity.HasOne(d => d.Producer).WithMany(p => p.FoodProducers)
//                .HasForeignKey(d => d.ProducerId)
//                .HasConstraintName("FK__food_prod__produ__76969D2E");
//        });

//        modelBuilder.Entity<GiftSet>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__gift_set__3213E83FA2D0BDC5");

//            entity.ToTable("gift_sets");

//            entity.HasIndex(e => e.Slug, "UQ__gift_set__32DD1E4CE53F6537").IsUnique();

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.Description)
//                .HasColumnType("text")
//                .HasColumnName("description");
//            entity.Property(e => e.ImageFilename)
//                .HasMaxLength(255)
//                .IsUnicode(false)
//                .HasColumnName("image_filename");
//            entity.Property(e => e.IsActive)
//                .HasDefaultValue(true)
//                .HasColumnName("is_active");
//            entity.Property(e => e.IsFeatured).HasColumnName("is_featured");
//            entity.Property(e => e.Name)
//                .HasMaxLength(200)
//                .IsUnicode(false)
//                .HasColumnName("name");
//            entity.Property(e => e.Occasion)
//                .HasMaxLength(100)
//                .IsUnicode(false)
//                .HasColumnName("occasion");
//            entity.Property(e => e.PackagingType)
//                .HasMaxLength(100)
//                .IsUnicode(false)
//                .HasColumnName("packaging_type");
//            entity.Property(e => e.Price)
//                .HasColumnType("decimal(16, 2)")
//                .HasColumnName("price");
//            entity.Property(e => e.SalePrice)
//                .HasColumnType("decimal(16, 2)")
//                .HasColumnName("sale_price");
//            entity.Property(e => e.Slug)
//                .HasMaxLength(255)
//                .IsUnicode(false)
//                .HasColumnName("slug");
//            entity.Property(e => e.UpdatedAt)
//                .HasColumnType("datetime")
//                .HasColumnName("updated_at");
//        });

//        modelBuilder.Entity<GiftSetItem>(entity =>
//        {
//            entity.HasKey(e => new { e.GiftSetId, e.FoodId }).HasName("PK__gift_set__346B45099E6DF5C3");

//            entity.ToTable("gift_set_items");

//            entity.Property(e => e.GiftSetId).HasColumnName("gift_set_id");
//            entity.Property(e => e.FoodId).HasColumnName("food_id");
//            entity.Property(e => e.Quantity)
//                .HasDefaultValue(1)
//                .HasColumnName("quantity");

//            entity.HasOne(d => d.Food).WithMany(p => p.GiftSetItems)
//                .HasForeignKey(d => d.FoodId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__gift_set___food___18EBB532");

//            entity.HasOne(d => d.GiftSet).WithMany(p => p.GiftSetItems)
//                .HasForeignKey(d => d.GiftSetId)
//                .HasConstraintName("FK__gift_set___gift___17F790F9");
//        });

//        modelBuilder.Entity<GuestOrdersTracking>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__guest_or__3213E83FA10541E0");

//            entity.ToTable("guest_orders_tracking");

//            entity.HasIndex(e => e.TrackingCode, "UQ__guest_or__FF2B077A9DABD4EC").IsUnique();

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.ExpiresAt)
//                .HasColumnType("datetime")
//                .HasColumnName("expires_at");
//            entity.Property(e => e.GuestEmail)
//                .HasMaxLength(150)
//                .IsUnicode(false)
//                .HasColumnName("guest_email");
//            entity.Property(e => e.OrderId).HasColumnName("order_id");
//            entity.Property(e => e.TrackingCode)
//                .HasMaxLength(50)
//                .IsUnicode(false)
//                .HasColumnName("tracking_code");

//            entity.HasOne(d => d.Order).WithMany(p => p.GuestOrdersTrackings)
//                .HasForeignKey(d => d.OrderId)
//                .HasConstraintName("FK__guest_ord__order__51300E55");
//        });

//        modelBuilder.Entity<Message>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__messages__3213E83F38E38727");

//            entity.ToTable("messages");

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.AssignedTo).HasColumnName("assigned_to");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.Email)
//                .HasMaxLength(150)
//                .IsUnicode(false)
//                .HasColumnName("email");
//            entity.Property(e => e.Firstname)
//                .HasMaxLength(100)
//                .IsUnicode(false)
//                .HasColumnName("firstname");
//            entity.Property(e => e.IsResolved).HasColumnName("is_resolved");
//            entity.Property(e => e.Lastname)
//                .HasMaxLength(100)
//                .IsUnicode(false)
//                .HasColumnName("lastname");
//            entity.Property(e => e.Message1)
//                .HasColumnType("text")
//                .HasColumnName("message");
//            entity.Property(e => e.Phone)
//                .HasMaxLength(20)
//                .IsUnicode(false)
//                .HasColumnName("phone");
//            entity.Property(e => e.Status)
//                .HasMaxLength(20)
//                .IsUnicode(false)
//                .HasDefaultValue("unread")
//                .HasColumnName("status");
//            entity.Property(e => e.Subject)
//                .HasMaxLength(255)
//                .IsUnicode(false)
//                .HasColumnName("subject");
//            entity.Property(e => e.UpdatedAt)
//                .HasColumnType("datetime")
//                .HasColumnName("updated_at");

//            entity.HasOne(d => d.AssignedToNavigation).WithMany(p => p.Messages)
//                .HasForeignKey(d => d.AssignedTo)
//                .HasConstraintName("FK__messages__assign__6166761E");
//        });

//        modelBuilder.Entity<MessageReply>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__message___3213E83F80D432F3");

//            entity.ToTable("message_replies");

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.MessageId).HasColumnName("message_id");
//            entity.Property(e => e.Reply)
//                .HasColumnType("text")
//                .HasColumnName("reply");
//            entity.Property(e => e.UserId).HasColumnName("user_id");

//            entity.HasOne(d => d.Message).WithMany(p => p.MessageReplies)
//                .HasForeignKey(d => d.MessageId)
//                .HasConstraintName("FK__message_r__messa__65370702");

//            entity.HasOne(d => d.User).WithMany(p => p.MessageReplies)
//                .HasForeignKey(d => d.UserId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__message_r__user___662B2B3B");
//        });

//        modelBuilder.Entity<NewsletterSubscription>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__newslett__3213E83F395E0C5B");

//            entity.ToTable("newsletter_subscriptions");

//            entity.HasIndex(e => e.Email, "UQ__newslett__AB6E616420C2469C").IsUnique();

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.Email)
//                .HasMaxLength(150)
//                .IsUnicode(false)
//                .HasColumnName("email");
//            entity.Property(e => e.IsActive)
//                .HasDefaultValue(true)
//                .HasColumnName("is_active");
//            entity.Property(e => e.SubscribedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("subscribed_at");
//            entity.Property(e => e.UnsubscribedAt)
//                .HasColumnType("datetime")
//                .HasColumnName("unsubscribed_at");
//        });

//        modelBuilder.Entity<Notification>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__notifica__3213E83F26CEB331");

//            entity.ToTable("notifications");

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.Content)
//                .HasColumnType("text")
//                .HasColumnName("content");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.IsRead).HasColumnName("is_read");
//            entity.Property(e => e.Link)
//                .HasMaxLength(255)
//                .IsUnicode(false)
//                .HasColumnName("link");
//            entity.Property(e => e.Title)
//                .HasMaxLength(255)
//                .IsUnicode(false)
//                .HasColumnName("title");
//            entity.Property(e => e.Type)
//                .HasMaxLength(50)
//                .IsUnicode(false)
//                .HasColumnName("type");
//            entity.Property(e => e.UserId).HasColumnName("user_id");

//            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
//                .HasForeignKey(d => d.UserId)
//                .OnDelete(DeleteBehavior.Cascade)
//                .HasConstraintName("FK__notificat__user___0697FACD");
//        });

//        modelBuilder.Entity<Order>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__orders__3213E83FC6A56658");

//            entity.ToTable("orders");

//            entity.HasIndex(e => e.OrderNumber, "UQ__orders__730E34DF3F9BF08C").IsUnique();

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.ActualDelivery)
//                .HasColumnType("datetime")
//                .HasColumnName("actual_delivery");
//            entity.Property(e => e.AdminNotes)
//                .HasColumnType("text")
//                .HasColumnName("admin_notes");
//            entity.Property(e => e.BankId).HasColumnName("bank_id");
//            entity.Property(e => e.CouponId).HasColumnName("coupon_id");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.DeliveryAddress)
//                .HasMaxLength(255)
//                .IsUnicode(false)
//                .HasColumnName("delivery_address");
//            entity.Property(e => e.DeliveryNotes)
//                .HasColumnType("text")
//                .HasColumnName("delivery_notes");
//            entity.Property(e => e.Discount)
//                .HasColumnType("decimal(16, 2)")
//                .HasColumnName("discount");
//            entity.Property(e => e.DistrictId).HasColumnName("district_id");
//            entity.Property(e => e.EstimatedDelivery)
//                .HasColumnType("datetime")
//                .HasColumnName("estimated_delivery");
//            entity.Property(e => e.GiftWrappingFee)
//                .HasColumnType("decimal(16, 2)")
//                .HasColumnName("gift_wrapping_fee");
//            entity.Property(e => e.GuestEmail)
//                .HasMaxLength(150)
//                .IsUnicode(false)
//                .HasColumnName("guest_email");
//            entity.Property(e => e.Notes)
//                .HasColumnType("text")
//                .HasColumnName("notes");
//            entity.Property(e => e.OrderDate)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("order_date");
//            entity.Property(e => e.OrderNumber)
//                .HasMaxLength(50)
//                .IsUnicode(false)
//                .HasColumnName("order_number");
//            entity.Property(e => e.OrderStatus)
//                .HasMaxLength(20)
//                .IsUnicode(false)
//                .HasColumnName("order_status");
//            entity.Property(e => e.PaymentMethodId).HasColumnName("payment_method_id");
//            entity.Property(e => e.PaymentStatus)
//                .HasMaxLength(20)
//                .IsUnicode(false)
//                .HasColumnName("payment_status");
//            entity.Property(e => e.ProvinceId).HasColumnName("province_id");
//            entity.Property(e => e.RecipientEmail)
//                .HasMaxLength(150)
//                .IsUnicode(false)
//                .HasColumnName("recipient_email");
//            entity.Property(e => e.RecipientName)
//                .HasMaxLength(200)
//                .IsUnicode(false)
//                .HasColumnName("recipient_name");
//            entity.Property(e => e.RecipientPhone)
//                .HasMaxLength(20)
//                .IsUnicode(false)
//                .HasColumnName("recipient_phone");
//            entity.Property(e => e.SessionId)
//                .HasMaxLength(100)
//                .IsUnicode(false)
//                .HasColumnName("session_id");
//            entity.Property(e => e.ShippingFee)
//                .HasColumnType("decimal(16, 2)")
//                .HasColumnName("shipping_fee");
//            entity.Property(e => e.ShippingMethod)
//                .HasMaxLength(100)
//                .IsUnicode(false)
//                .HasColumnName("shipping_method");
//            entity.Property(e => e.ShippingProvider)
//                .HasMaxLength(100)
//                .IsUnicode(false)
//                .HasColumnName("shipping_provider");
//            entity.Property(e => e.Subtotal)
//                .HasColumnType("decimal(16, 2)")
//                .HasColumnName("subtotal");
//            entity.Property(e => e.Tax)
//                .HasColumnType("decimal(16, 2)")
//                .HasColumnName("tax");
//            entity.Property(e => e.TotalAmount)
//                .HasColumnType("decimal(16, 2)")
//                .HasColumnName("total_amount");
//            entity.Property(e => e.TrackingNumber)
//                .HasMaxLength(100)
//                .IsUnicode(false)
//                .HasColumnName("tracking_number");
//            entity.Property(e => e.TransactionId)
//                .HasMaxLength(100)
//                .IsUnicode(false)
//                .HasColumnName("transaction_id");
//            entity.Property(e => e.UpdatedAt)
//                .HasColumnType("datetime")
//                .HasColumnName("updated_at");
//            entity.Property(e => e.UserId).HasColumnName("user_id");
//            entity.Property(e => e.WardId).HasColumnName("ward_id");          

//            entity.HasOne(d => d.Coupon).WithMany(p => p.Orders)
//                .HasForeignKey(d => d.CouponId)
//                .HasConstraintName("FK__orders__coupon_i__47A6A41B");

//            entity.HasOne(d => d.District).WithMany(p => p.Orders)
//                .HasForeignKey(d => d.DistrictId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__orders__district__4B7734FF");

//            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Orders)
//                .HasForeignKey(d => d.PaymentMethodId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__orders__payment___489AC854");

//            entity.HasOne(d => d.Province).WithMany(p => p.Orders)
//                .HasForeignKey(d => d.ProvinceId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__orders__province__4A8310C6");

//            entity.HasOne(d => d.User).WithMany(p => p.Orders)
//                .HasForeignKey(d => d.UserId)
//                .HasConstraintName("FK__orders__user_id__46B27FE2");

//            entity.HasOne(d => d.Ward).WithMany(p => p.Orders)
//                .HasForeignKey(d => d.WardId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__orders__ward_id__4C6B5938");
//        });

//        modelBuilder.Entity<OrderItem>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__order_it__3213E83F9A74B703");

//            entity.ToTable("order_items");

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.FoodId).HasColumnName("food_id");
//            entity.Property(e => e.OrderId).HasColumnName("order_id");
//            entity.Property(e => e.ProductName)
//                .HasMaxLength(100)
//                .IsUnicode(false)
//                .HasColumnName("product_name");
//            entity.Property(e => e.Quantity).HasColumnName("quantity");
//            entity.Property(e => e.Subtotal)
//                .HasColumnType("decimal(16, 2)")
//                .HasColumnName("subtotal");
//            entity.Property(e => e.UnitPrice)
//                .HasColumnType("decimal(16, 2)")
//                .HasColumnName("unit_price");

//            entity.HasOne(d => d.Food).WithMany(p => p.OrderItems)
//                .HasForeignKey(d => d.FoodId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__order_ite__food___55F4C372");

//            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
//                .HasForeignKey(d => d.OrderId)
//                .HasConstraintName("FK__order_ite__order__55009F39");
//        });

//        modelBuilder.Entity<OrderStatusHistory>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__order_st__3213E83F777DD625");

//            entity.ToTable("order_status_history");

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.ChangedBy).HasColumnName("changed_by");
//            entity.Property(e => e.Comment)
//                .HasColumnType("text")
//                .HasColumnName("comment");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.OrderId).HasColumnName("order_id");
//            entity.Property(e => e.Status)
//                .HasMaxLength(20)
//                .IsUnicode(false)
//                .HasColumnName("status");

//            entity.HasOne(d => d.ChangedByNavigation).WithMany(p => p.OrderStatusHistories)
//                .HasForeignKey(d => d.ChangedBy)
//                .HasConstraintName("FK__order_sta__chang__5AB9788F");

//            entity.HasOne(d => d.Order).WithMany(p => p.OrderStatusHistories)
//                .HasForeignKey(d => d.OrderId)
//                .HasConstraintName("FK__order_sta__order__59C55456");
//        });

//        modelBuilder.Entity<OutOfStockNotification>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__out_of_s__3213E83FEA83C16C");

//            entity.ToTable("out_of_stock_notifications");

//            entity.HasIndex(e => new { e.FoodId, e.Email }, "UQ_out_of_stock_food_email").IsUnique();

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.Email)
//                .HasMaxLength(150)
//                .IsUnicode(false)
//                .HasColumnName("email");
//            entity.Property(e => e.FoodId).HasColumnName("food_id");
//            entity.Property(e => e.IsNotified).HasColumnName("is_notified");
//            entity.Property(e => e.NotifiedAt)
//                .HasColumnType("datetime")
//                .HasColumnName("notified_at");

//            entity.HasOne(d => d.Food).WithMany(p => p.OutOfStockNotifications)
//                .HasForeignKey(d => d.FoodId)
//                .HasConstraintName("FK__out_of_st__food___11158940");
//        });

//        modelBuilder.Entity<PasswordReset>(entity =>
//        {
//            entity.HasKey(e => e.Email).HasName("PK__password__AB6E616546AE65A7");

//            entity.ToTable("password_resets");

//            entity.Property(e => e.Email)
//                .HasMaxLength(150)
//                .IsUnicode(false)
//                .HasColumnName("email");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.ExpiresAt)
//                .HasColumnType("datetime")
//                .HasColumnName("expires_at");
//            entity.Property(e => e.Token)
//                .HasMaxLength(255)
//                .IsUnicode(false)
//                .HasColumnName("token");
//        });

//        modelBuilder.Entity<PaymentMethod>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__payment___3213E83F3B045FFD");

//            entity.ToTable("payment_methods");

//            entity.HasIndex(e => e.Code, "UQ__payment___357D4CF9008432F2").IsUnique();

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.Code)
//                .HasMaxLength(50)
//                .IsUnicode(false)
//                .HasColumnName("code");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.Description)
//                .HasColumnType("text")
//                .HasColumnName("description");
//            entity.Property(e => e.DisplayOrder).HasColumnName("display_order");
//            entity.Property(e => e.Instructions)
//                .HasColumnType("text")
//                .HasColumnName("instructions");
//            entity.Property(e => e.IsActive)
//                .HasDefaultValue(true)
//                .HasColumnName("is_active");
//            entity.Property(e => e.Name)
//                .HasMaxLength(100)
//                .IsUnicode(false)
//                .HasColumnName("name");
//            entity.Property(e => e.UpdatedAt)
//                .HasColumnType("datetime")
//                .HasColumnName("updated_at");
//        });

//        modelBuilder.Entity<Producer>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__producer__3213E83FCC5CBACC");

//            entity.ToTable("producers");

//            entity.HasIndex(e => e.Slug, "UQ__producer__32DD1E4C4C57B5CE").IsUnique();

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.Address)
//                .HasMaxLength(255)
//                .IsUnicode(false)
//                .HasColumnName("address");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.Description)
//                .HasColumnType("text")
//                .HasColumnName("description");
//            entity.Property(e => e.Email)
//                .HasMaxLength(150)
//                .IsUnicode(false)
//                .HasColumnName("email");
//            entity.Property(e => e.EstablishedYear).HasColumnName("established_year");
//            entity.Property(e => e.Featured).HasColumnName("featured");
//            entity.Property(e => e.ImageFilename)
//                .HasMaxLength(255)
//                .IsUnicode(false)
//                .HasColumnName("image_filename");
//            entity.Property(e => e.IsActive)
//                .HasDefaultValue(true)
//                .HasColumnName("is_active");
//            entity.Property(e => e.Name)
//                .HasMaxLength(200)
//                .IsUnicode(false)
//                .HasColumnName("name");
//            entity.Property(e => e.Phone)
//                .HasMaxLength(20)
//                .IsUnicode(false)
//                .HasColumnName("phone");
//            entity.Property(e => e.ProvinceId).HasColumnName("province_id");
//            entity.Property(e => e.Slug)
//                .HasMaxLength(255)
//                .IsUnicode(false)
//                .HasColumnName("slug");
//            entity.Property(e => e.UpdatedAt)
//                .HasColumnType("datetime")
//                .HasColumnName("updated_at");
//            entity.Property(e => e.Website)
//                .HasMaxLength(255)
//                .IsUnicode(false)
//                .HasColumnName("website");

//            entity.HasOne(d => d.Province).WithMany(p => p.Producers)
//                .HasForeignKey(d => d.ProvinceId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__producers__provi__71D1E811");
//        });

//        modelBuilder.Entity<Province>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__province__3213E83F3CE0AEC1");

//            entity.ToTable("provinces");

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.IsActive)
//                .HasDefaultValue(true)
//                .HasColumnName("is_active");
//            entity.Property(e => e.Name)
//                .HasMaxLength(100)
//                .IsUnicode(false)
//                .HasColumnName("name");
//            entity.Property(e => e.RegionId).HasColumnName("region_id");
//            entity.Property(e => e.UpdatedAt)
//                .HasColumnType("datetime")
//                .HasColumnName("updated_at");

//            entity.HasOne(d => d.Region).WithMany(p => p.Provinces)
//                .HasForeignKey(d => d.RegionId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__provinces__regio__45F365D3");
//        });

//        modelBuilder.Entity<Region>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__regions__3213E83FA129CDDB");

//            entity.ToTable("regions");

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.Description)
//                .HasColumnType("text")
//                .HasColumnName("description");
//            entity.Property(e => e.ImageFilename)
//                .HasMaxLength(255)
//                .IsUnicode(false)
//                .HasColumnName("image_filename");
//            entity.Property(e => e.IsActive)
//                .HasDefaultValue(true)
//                .HasColumnName("is_active");
//            entity.Property(e => e.Name)
//                .HasMaxLength(100)
//                .IsUnicode(false)
//                .HasColumnName("name");
//            entity.Property(e => e.UpdatedAt)
//                .HasColumnType("datetime")
//                .HasColumnName("updated_at");
//        });

//        modelBuilder.Entity<Review>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__reviews__3213E83FD00163E1");

//            entity.ToTable("reviews");

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.Comment)
//                .HasColumnType("text")
//                .HasColumnName("comment");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.FoodId).HasColumnName("food_id");
//            entity.Property(e => e.IsApproved).HasColumnName("is_approved");
//            entity.Property(e => e.IsVerifiedPurchase).HasColumnName("is_verified_purchase");
//            entity.Property(e => e.Rating).HasColumnName("rating");
//            entity.Property(e => e.UpdatedAt)
//                .HasColumnType("datetime")
//                .HasColumnName("updated_at");
//            entity.Property(e => e.UserId).HasColumnName("user_id");

//            entity.HasOne(d => d.Food).WithMany(p => p.Reviews)
//                .HasForeignKey(d => d.FoodId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__reviews__food_id__6A30C649");

//            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
//                .HasForeignKey(d => d.UserId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__reviews__user_id__6B24EA82");
//        });

//        modelBuilder.Entity<Setting>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__settings__3213E83FFD0454A8");

//            entity.ToTable("settings");

//            entity.HasIndex(e => e.SettingKey, "UQ__settings__0DFAC427FEC30F37").IsUnique();

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.IsPublic)
//                .HasDefaultValue(true)
//                .HasColumnName("is_public");
//            entity.Property(e => e.SettingGroup)
//                .HasMaxLength(100)
//                .IsUnicode(false)
//                .HasDefaultValue("general")
//                .HasColumnName("setting_group");
//            entity.Property(e => e.SettingKey)
//                .HasMaxLength(100)
//                .IsUnicode(false)
//                .HasColumnName("setting_key");
//            entity.Property(e => e.SettingValue)
//                .HasColumnType("text")
//                .HasColumnName("setting_value");
//            entity.Property(e => e.UpdatedAt)
//                .HasColumnType("datetime")
//                .HasColumnName("updated_at");
//        });

//        modelBuilder.Entity<ShippingAddress>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__shipping__3213E83F12474BB2");

//            entity.ToTable("shipping_addresses");

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.AddressDetail)
//                .HasMaxLength(255)
//                .IsUnicode(false)
//                .HasColumnName("address_detail");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.DistrictId).HasColumnName("district_id");
//            entity.Property(e => e.IsDefault).HasColumnName("is_default");
//            entity.Property(e => e.Phone)
//                .HasMaxLength(20)
//                .IsUnicode(false)
//                .HasColumnName("phone");
//            entity.Property(e => e.ProvinceId).HasColumnName("province_id");
//            entity.Property(e => e.RecipientName)
//                .HasMaxLength(200)
//                .IsUnicode(false)
//                .HasColumnName("recipient_name");
//            entity.Property(e => e.UpdatedAt)
//                .HasColumnType("datetime")
//                .HasColumnName("updated_at");
//            entity.Property(e => e.UserId).HasColumnName("user_id");
//            entity.Property(e => e.WardId).HasColumnName("ward_id");

//            entity.HasOne(d => d.District).WithMany(p => p.ShippingAddresses)
//                .HasForeignKey(d => d.DistrictId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__shipping___distr__2645B050");

//            entity.HasOne(d => d.Province).WithMany(p => p.ShippingAddresses)
//                .HasForeignKey(d => d.ProvinceId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__shipping___provi__25518C17");

//            entity.HasOne(d => d.User).WithMany(p => p.ShippingAddresses)
//                .HasForeignKey(d => d.UserId)
//                .HasConstraintName("FK__shipping___user___245D67DE");

//            entity.HasOne(d => d.Ward).WithMany(p => p.ShippingAddresses)
//                .HasForeignKey(d => d.WardId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__shipping___ward___2739D489");
//        });

//        modelBuilder.Entity<ShippingFee>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__shipping__3213E83FB12D5370");

//            entity.ToTable("shipping_fees");

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.Fee)
//                .HasColumnType("decimal(16, 2)")
//                .HasColumnName("fee");
//            entity.Property(e => e.FreeShippingMin)
//                .HasColumnType("decimal(16, 2)")
//                .HasColumnName("free_shipping_min");
//            entity.Property(e => e.MinOrderValue)
//                .HasColumnType("decimal(16, 2)")
//                .HasColumnName("min_order_value");
//            entity.Property(e => e.ProvinceId).HasColumnName("province_id");
//            entity.Property(e => e.UpdatedAt)
//                .HasColumnType("datetime")
//                .HasColumnName("updated_at");

//            entity.HasOne(d => d.Province).WithMany(p => p.ShippingFees)
//                .HasForeignKey(d => d.ProvinceId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__shipping___provi__2B0A656D");
//        });

//        modelBuilder.Entity<User>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__users__3213E83FC7640B40");

//            entity.ToTable("users");

//            entity.HasIndex(e => e.Email, "UQ__users__AB6E6164CBE37EF3").IsUnique();

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.Address)
//                .HasMaxLength(255)
//                .IsUnicode(false)
//                .HasColumnName("address");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.Email)
//                .HasMaxLength(150)
//                .IsUnicode(false)
//                .HasColumnName("email");
//            entity.Property(e => e.EmailVerifiedAt)
//                .HasColumnType("datetime")
//                .HasColumnName("email_verified_at");
//            entity.Property(e => e.Firstname)
//                .HasMaxLength(100)
//                .IsUnicode(false)
//                .HasColumnName("firstname");
//            entity.Property(e => e.IsActive)
//                .HasDefaultValue(true)
//                .HasColumnName("is_active");
//            entity.Property(e => e.Lastname)
//                .HasMaxLength(100)
//                .IsUnicode(false)
//                .HasColumnName("lastname");
//            entity.Property(e => e.Password)
//                .HasMaxLength(255)
//                .IsUnicode(false)
//                .HasColumnName("password");
//            entity.Property(e => e.Phone)
//                .HasMaxLength(20)
//                .IsUnicode(false)
//                .HasColumnName("phone");
//            entity.Property(e => e.ProfileImage)
//                .HasMaxLength(255)
//                .IsUnicode(false)
//                .HasColumnName("profile_image");
//            entity.Property(e => e.Role)
//                .HasMaxLength(10)
//                .IsUnicode(false)
//                .HasColumnName("role");
//            entity.Property(e => e.UpdatedAt)
//                .HasColumnType("datetime")
//                .HasColumnName("updated_at");
//            entity.Property(e => e.VerificationToken)
//                .HasMaxLength(100)
//                .IsUnicode(false)
//                .HasColumnName("verification_token");

//            entity.HasMany(d => d.Groups).WithMany(p => p.Users)
//                .UsingEntity<Dictionary<string, object>>(
//                    "UserCustomerGroup",
//                    r => r.HasOne<CustomerGroup>().WithMany()
//                        .HasForeignKey("GroupId")
//                        .HasConstraintName("FK__user_cust__group__17C286CF"),
//                    l => l.HasOne<User>().WithMany()
//                        .HasForeignKey("UserId")
//                        .HasConstraintName("FK__user_cust__user___16CE6296"),
//                    j =>
//                    {
//                        j.HasKey("UserId", "GroupId").HasName("PK__user_cus__A4E94E55F910C991");
//                        j.ToTable("user_customer_groups");
//                        j.IndexerProperty<int>("UserId").HasColumnName("user_id");
//                        j.IndexerProperty<int>("GroupId").HasColumnName("group_id");
//                    });
//        });

//        modelBuilder.Entity<Ward>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__wards__3213E83F4B5E3B1D");

//            entity.ToTable("wards");

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.DistrictId).HasColumnName("district_id");
//            entity.Property(e => e.Name)
//                .HasMaxLength(100)
//                .IsUnicode(false)
//                .HasColumnName("name");

//            entity.HasOne(d => d.District).WithMany(p => p.Wards)
//                .HasForeignKey(d => d.DistrictId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__wards__district___4D94879B");
//        });

//        modelBuilder.Entity<Wishlist>(entity =>
//        {
//            entity.HasKey(e => e.Id).HasName("PK__wishlist__3213E83F63C1F4E7");

//            entity.ToTable("wishlists");

//            entity.HasIndex(e => new { e.UserId, e.FoodId }, "UQ_wishlist_user_food").IsUnique();

//            entity.Property(e => e.Id).HasColumnName("id");
//            entity.Property(e => e.CreatedAt)
//                .HasDefaultValueSql("(getdate())")
//                .HasColumnType("datetime")
//                .HasColumnName("created_at");
//            entity.Property(e => e.FoodId).HasColumnName("food_id");
//            entity.Property(e => e.UserId).HasColumnName("user_id");

//            entity.HasOne(d => d.Food).WithMany(p => p.Wishlists)
//                .HasForeignKey(d => d.FoodId)
//                .OnDelete(DeleteBehavior.ClientSetNull)
//                .HasConstraintName("FK__wishlists__food___0E6E26BF");

//            entity.HasOne(d => d.User).WithMany(p => p.Wishlists)
//                .HasForeignKey(d => d.UserId)
//                .HasConstraintName("FK__wishlists__user___0D7A0286");
//        });

//        OnModelCreatingPartial(modelBuilder);
//    }

//    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
//}
