using Microsoft.EntityFrameworkCore;
using E_Commerce_APIs.Domain.Entities;

namespace E_Commerce_APIs.Infrastructure.Persistence.Context;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Auditlog> Auditlogs { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<CartSession> CartSessions { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Inventory> Inventories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<OrderShippingAddress> OrderShippingAddresses { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PaymentStatus> PaymentStatuses { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<ReviewUserStatus> ReviewUserStatuses { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAddress> UserAddresses { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    public virtual DbSet<VendorOffer> VendorOffers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pgcrypto");

        modelBuilder.Entity<Auditlog>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("auditlog_pkey");

            entity.ToTable("auditlog");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Action)
                .HasMaxLength(50)
                .HasColumnName("action");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_at");
            entity.Property(e => e.EntityId)
                .HasMaxLength(50)
                .HasColumnName("entity_id");
            entity.Property(e => e.EntityName)
                .HasMaxLength(100)
                .HasColumnName("entity_name");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("brand_pkey");

            entity.ToTable("brand");

            entity.HasIndex(e => e.Name, "brand_name_key").IsUnique();

            entity.HasIndex(e => e.Slug, "brand_slug_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("creation_date");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_at");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("modified_date");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .HasColumnName("slug");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cart_item_pkey");

            entity.ToTable("cart_item");

            entity.HasIndex(e => new { e.CartSessionId, e.VendorOfferId }, "cart_item_cart_session_id_vendor_offer_id_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CartSessionId).HasColumnName("cart_session_id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("creation_date");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("modified_date");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(1)
                .HasColumnName("quantity");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .HasColumnName("slug");
            entity.Property(e => e.VendorOfferId).HasColumnName("vendor_offer_id");

            entity.HasOne(d => d.CartSession).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.CartSessionId)
                .HasConstraintName("cart_item_cart_session_id_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("cart_item_product_id_fkey");

            entity.HasOne(d => d.VendorOffer).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.VendorOfferId)
                .HasConstraintName("cart_item_vendor_offer_id_fkey");
        });

        modelBuilder.Entity<CartSession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cart_session_pkey");

            entity.ToTable("cart_session");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("creation_date");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("modified_date");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .HasColumnName("slug");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.CartSessions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("cart_session_user_id_fkey");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("category_pkey");

            entity.ToTable("category");

            entity.HasIndex(e => e.Slug, "category_slug_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("creation_date");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DisplayText)
                .HasMaxLength(255)
                .HasColumnName("display_text");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("modified_date");
            entity.Property(e => e.ParentId).HasColumnName("parent_id");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .HasColumnName("slug");

            entity.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_category_parent");
        });

        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("inventory_pkey");

            entity.ToTable("inventory");

            entity.HasIndex(e => e.VendorOfferId, "inventory_vendor_offer_id_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("creation_date");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("modified_date");
            entity.Property(e => e.Quantity)
                .HasDefaultValue(0)
                .HasColumnName("quantity");
            entity.Property(e => e.ReservedQuantity)
                .HasDefaultValue(0)
                .HasColumnName("reserved_quantity");
            entity.Property(e => e.VendorOfferId).HasColumnName("vendor_offer_id");

            entity.HasOne(d => d.VendorOffer).WithOne(p => p.Inventory)
                .HasForeignKey<Inventory>(d => d.VendorOfferId)
                .HasConstraintName("inventory_vendor_offer_id_fkey");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Order_pkey");

            entity.ToTable("Order");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("creation_date");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("modified_date");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .HasColumnName("slug");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.Total)
                .HasPrecision(10, 2)
                .HasColumnName("total");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Order_status_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("Order_user_id_fkey");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("order_item_pkey");

            entity.ToTable("order_item");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("creation_date");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("modified_date");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .HasColumnName("product_name");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .HasColumnName("slug");
            entity.Property(e => e.TotalPrice)
                .HasPrecision(10, 2)
                .HasColumnName("total_price");
            entity.Property(e => e.UnitPrice)
                .HasPrecision(10, 2)
                .HasColumnName("unit_price");
            entity.Property(e => e.VendorOfferId).HasColumnName("vendor_offer_id");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("order_item_order_id_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("order_item_product_id_fkey");

            entity.HasOne(d => d.VendorOffer).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.VendorOfferId)
                .HasConstraintName("order_item_vendor_offer_id_fkey");
        });

        modelBuilder.Entity<OrderShippingAddress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("order_shipping_address_pkey");

            entity.ToTable("order_shipping_address");

            entity.HasIndex(e => e.OrderId, "order_shipping_address_order_id_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("creation_date");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .HasColumnName("location");
            entity.Property(e => e.Mobile)
                .HasMaxLength(20)
                .HasColumnName("mobile");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("modified_date");
            entity.Property(e => e.OrderId).HasColumnName("order_id");

            entity.HasOne(d => d.Order).WithOne(p => p.OrderShippingAddress)
                .HasForeignKey<OrderShippingAddress>(d => d.OrderId)
                .HasConstraintName("order_shipping_address_order_id_fkey");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("order_status_pkey");

            entity.ToTable("order_status");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("payment_pkey");

            entity.ToTable("payment");

            entity.HasIndex(e => e.OrderId, "payment_order_id_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Amount)
                .HasPrecision(10, 2)
                .HasColumnName("amount");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("creation_date");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("modified_date");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Provider)
                .HasMaxLength(100)
                .HasColumnName("provider");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .HasColumnName("slug");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Order).WithOne(p => p.Payment)
                .HasForeignKey<Payment>(d => d.OrderId)
                .HasConstraintName("payment_order_id_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Payments)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("payment_status_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("payment_user_id_fkey");
        });

        modelBuilder.Entity<PaymentStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("payment_status_pkey");

            entity.ToTable("payment_status");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Status)
                .HasMaxLength(255)
                .HasColumnName("status");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_pkey");

            entity.ToTable("product");

            entity.HasIndex(e => e.Slug, "product_slug_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.BrandId).HasColumnName("brand_id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("creation_date");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Feature)
                .HasColumnType("jsonb")
                .HasColumnName("feature");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("modified_date");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.ReviewsCount)
                .HasDefaultValue(0)
                .HasColumnName("reviews_count");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .HasColumnName("slug");
            entity.Property(e => e.SmallImage)
                .HasMaxLength(500)
                .HasColumnName("small_image");

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("product_brand_id_fkey");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_category_pkey");

            entity.ToTable("product_category");

            entity.HasIndex(e => new { e.ProductId, e.CategoryId }, "product_category_product_id_category_id_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.ProductId).HasColumnName("product_id");

            entity.HasOne(d => d.Category).WithMany(p => p.ProductCategories)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("product_category_category_id_fkey");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductCategories)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("product_category_product_id_fkey");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("product_images_pkey");

            entity.ToTable("product_images");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Alt)
                .HasMaxLength(255)
                .HasColumnName("alt");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("creation_date");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Image)
                .HasMaxLength(500)
                .HasColumnName("image");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("modified_date");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .HasColumnName("slug");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("product_images_product_id_fkey");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Refresh_Token_pkey");

            entity.ToTable("Refresh_Token");

            entity.HasIndex(e => e.Token, "Refresh_Token_token_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.ExpiresAt).HasColumnName("expires_at");
            entity.Property(e => e.IpAddress)
                .HasMaxLength(45)
                .HasColumnName("ip_address");
            entity.Property(e => e.RevokedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("revoked_at");
            entity.Property(e => e.Token).HasColumnName("token");
            entity.Property(e => e.UserAgent).HasColumnName("user_agent");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("Refresh_Token_user_id_fkey");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("review_pkey");

            entity.ToTable("review");

            entity.HasIndex(e => new { e.UserId, e.OrderItemId }, "review_user_id_order_item_id_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("creation_date");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("modified_date");
            entity.Property(e => e.OrderItemId).HasColumnName("order_item_id");
            entity.Property(e => e.Rate).HasColumnName("rate");
            entity.Property(e => e.Reported)
                .HasDefaultValue(false)
                .HasColumnName("reported");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .HasColumnName("slug");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.UserStatusId).HasColumnName("user_status_id");

            entity.HasOne(d => d.OrderItem).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.OrderItemId)
                .HasConstraintName("review_order_item_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("review_user_id_fkey");

            entity.HasOne(d => d.UserStatus).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.UserStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("review_user_status_id_fkey");
        });

        modelBuilder.Entity<ReviewUserStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("review_user_status_pkey");

            entity.ToTable("review_user_status");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("creation_date");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("modified_date");
            entity.Property(e => e.Status)
                .HasMaxLength(250)
                .HasColumnName("status");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Role_pkey");

            entity.ToTable("Role");

            entity.HasIndex(e => e.Name, "Role_name_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "User_email_key").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "User_phone_number_key").IsUnique();

            entity.HasIndex(e => e.Slug, "User_slug_key").IsUnique();

            entity.HasIndex(e => e.UserName, "User_user_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.IsVerified)
                .HasDefaultValue(false)
                .HasColumnName("is_verified");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("modified_date");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .HasColumnName("slug");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("user_name");
        });

        modelBuilder.Entity<UserAddress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_Address_pkey");

            entity.ToTable("User_Address");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .HasColumnName("country");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("creation_date");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .HasColumnName("location");
            entity.Property(e => e.Mobile)
                .HasMaxLength(20)
                .HasColumnName("mobile");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("modified_date");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(20)
                .HasColumnName("postal_code");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .HasColumnName("slug");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserAddresses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("User_Address_user_id_fkey");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_Role_pkey");

            entity.ToTable("User_Role");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.AssignedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("assigned_at");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("User_Role_role_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("User_Role_user_id_fkey");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("vendor_pkey");

            entity.ToTable("vendor");

            entity.HasIndex(e => e.Email, "vendor_email_key").IsUnique();

            entity.HasIndex(e => e.Name, "vendor_name_key").IsUnique();

            entity.HasIndex(e => e.PhoneNumber, "vendor_phone_number_key").IsUnique();

            entity.HasIndex(e => e.Slug, "vendor_slug_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.AverageRate)
                .HasDefaultValueSql("0.0")
                .HasColumnName("average_rate");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("creation_date");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("modified_date");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .HasColumnName("slug");
            entity.Property(e => e.WebsiteUrl)
                .HasMaxLength(500)
                .HasColumnName("website_url");
        });

        modelBuilder.Entity<VendorOffer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("vendor_offer_pkey");

            entity.ToTable("vendor_offer");

            entity.HasIndex(e => new { e.ProductId, e.VendorId }, "vendor_offer_product_id_vendor_id_key").IsUnique();

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("creation_date");
            entity.Property(e => e.DeletedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deleted_at");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .HasColumnName("is_deleted");
            entity.Property(e => e.ModifiedDate)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("modified_date");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Slug)
                .HasMaxLength(255)
                .HasColumnName("slug");
            entity.Property(e => e.VendorId).HasColumnName("vendor_id");

            entity.HasOne(d => d.Product).WithMany(p => p.VendorOffers)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("vendor_offer_product_id_fkey");

            entity.HasOne(d => d.Vendor).WithMany(p => p.VendorOffers)
                .HasForeignKey(d => d.VendorId)
                .HasConstraintName("vendor_offer_vendor_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
