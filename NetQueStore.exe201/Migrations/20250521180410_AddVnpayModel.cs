using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetQueStore.exe201.Migrations
{
    /// <inheritdoc />
    public partial class AddVnpayModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "banks",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    code = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    logo_filename = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__banks__3213E83F728AFE73", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "blog_categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    slug = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__blog_cat__3213E83FB9703857", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    image_filename = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    display_order = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__categori__3213E83F167E741E", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "coupons",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    discount_type = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    discount_value = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    min_purchase = table.Column<decimal>(type: "decimal(16,2)", nullable: true),
                    max_discount = table.Column<decimal>(type: "decimal(16,2)", nullable: true),
                    starts_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    expires_at = table.Column<DateTime>(type: "datetime", nullable: false),
                    usage_limit = table.Column<int>(type: "int", nullable: true),
                    used_count = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__coupons__3213E83FE50262EF", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "faqs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    question = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    answer = table.Column<string>(type: "text", nullable: false),
                    category = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    display_order = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__faqs__3213E83F9FE3AD34", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "gift_sets",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    slug = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    price = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    sale_price = table.Column<decimal>(type: "decimal(16,2)", nullable: true),
                    image_filename = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    is_featured = table.Column<bool>(type: "bit", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    occasion = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    packaging_type = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__gift_set__3213E83FF942961F", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "password_resets",
                columns: table => new
                {
                    email = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    token = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    expires_at = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__password__AB6E6165E86D432F", x => x.email);
                });

            migrationBuilder.CreateTable(
                name: "payment_methods",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    code = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    instructions = table.Column<string>(type: "text", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    display_order = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__payment___3213E83F7BFB1929", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "regions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    image_filename = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__regions__3213E83FBA92FACE", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "settings",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    setting_key = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    setting_value = table.Column<string>(type: "text", nullable: true),
                    setting_group = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false, defaultValue: "general"),
                    is_public = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__settings__3213E83F4B26399D", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstname = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    lastname = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    role = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    profile_image = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    email_verified_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    verification_token = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__users__3213E83F2694C156", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "VnInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VnPayResponseCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VnInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "bank_accounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bank_id = table.Column<int>(type: "int", nullable: false),
                    account_name = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    account_number = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    branch = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    is_primary = table.Column<bool>(type: "bit", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__bank_acc__3213E83F5CC0B5DA", x => x.id);
                    table.ForeignKey(
                        name: "FK__bank_acco__bank___3B40CD36",
                        column: x => x.bank_id,
                        principalTable: "banks",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "provinces",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    region_id = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__province__3213E83F80D41B17", x => x.id);
                    table.ForeignKey(
                        name: "FK__provinces__regio__45F365D3",
                        column: x => x.region_id,
                        principalTable: "regions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "activity_logs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    activity_type = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    ip_address = table.Column<string>(type: "varchar(45)", unicode: false, maxLength: 45, nullable: true),
                    user_agent = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__activity__3213E83FD614E658", x => x.id);
                    table.ForeignKey(
                        name: "FK__activity___user___7D0E9093",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "blog_posts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    slug = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    excerpt = table.Column<string>(type: "text", nullable: true),
                    content = table.Column<string>(type: "text", nullable: false),
                    featured_image = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    author_id = table.Column<int>(type: "int", nullable: false),
                    is_published = table.Column<bool>(type: "bit", nullable: false),
                    published_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    meta_title = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    meta_description = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__blog_pos__3213E83F89038E53", x => x.id);
                    table.ForeignKey(
                        name: "FK__blog_post__autho__671F4F74",
                        column: x => x.author_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "carts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    session_id = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__carts__3213E83F959D5878", x => x.id);
                    table.ForeignKey(
                        name: "FK__carts__user_id__7B5B524B",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstname = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    lastname = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    subject = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    message = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, defaultValue: "unread"),
                    is_resolved = table.Column<bool>(type: "bit", nullable: false),
                    assigned_to = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__messages__3213E83F00FFF349", x => x.id);
                    table.ForeignKey(
                        name: "FK__messages__assign__5CA1C101",
                        column: x => x.assigned_to,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "cultural_events",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    province_id = table.Column<int>(type: "int", nullable: true),
                    start_date = table.Column<DateOnly>(type: "date", nullable: true),
                    end_date = table.Column<DateOnly>(type: "date", nullable: true),
                    image_filename = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__cultural__3213E83F16137903", x => x.id);
                    table.ForeignKey(
                        name: "FK__cultural___provi__73BA3083",
                        column: x => x.province_id,
                        principalTable: "provinces",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "districts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    province_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__district__3213E83F13B274AD", x => x.id);
                    table.ForeignKey(
                        name: "FK__districts__provi__1BC821DD",
                        column: x => x.province_id,
                        principalTable: "provinces",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "foods",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    slug = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    category_id = table.Column<int>(type: "int", nullable: false),
                    region_id = table.Column<int>(type: "int", nullable: false),
                    province_id = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    sale_price = table.Column<decimal>(type: "decimal(16,2)", nullable: true),
                    stock_quantity = table.Column<int>(type: "int", nullable: false),
                    unit = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    weight = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    shelf_life = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    storage_instructions = table.Column<string>(type: "text", nullable: true),
                    is_featured = table.Column<bool>(type: "bit", nullable: false),
                    is_special = table.Column<bool>(type: "bit", nullable: false),
                    is_vegetarian = table.Column<bool>(type: "bit", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ingredients = table.Column<string>(type: "text", nullable: true),
                    preparation_method = table.Column<string>(type: "text", nullable: true),
                    serving_suggestion = table.Column<string>(type: "text", nullable: true),
                    cultural_significance = table.Column<string>(type: "text", nullable: true),
                    allergens = table.Column<string>(type: "text", nullable: true),
                    certification = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__foods__3213E83F4D0792E5", x => x.id);
                    table.ForeignKey(
                        name: "FK__foods__category___5441852A",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__foods__province___5629CD9C",
                        column: x => x.province_id,
                        principalTable: "provinces",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__foods__region_id__5535A963",
                        column: x => x.region_id,
                        principalTable: "regions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "producers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    slug = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    province_id = table.Column<int>(type: "int", nullable: false),
                    address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    website = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    established_year = table.Column<int>(type: "int", nullable: true),
                    image_filename = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    featured = table.Column<bool>(type: "bit", nullable: false),
                    is_active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__producer__3213E83FB2DC83A1", x => x.id);
                    table.ForeignKey(
                        name: "FK__producers__provi__6A30C649",
                        column: x => x.province_id,
                        principalTable: "provinces",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "shipping_fees",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    province_id = table.Column<int>(type: "int", nullable: false),
                    fee = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    min_order_value = table.Column<decimal>(type: "decimal(16,2)", nullable: true),
                    free_shipping_min = table.Column<decimal>(type: "decimal(16,2)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__shipping__3213E83F25692D7F", x => x.id);
                    table.ForeignKey(
                        name: "FK__shipping___provi__2B0A656D",
                        column: x => x.province_id,
                        principalTable: "provinces",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "blog_post_categories",
                columns: table => new
                {
                    blog_post_id = table.Column<int>(type: "int", nullable: false),
                    blog_category_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__blog_pos__C9D2F74A0ED78BD8", x => new { x.blog_post_id, x.blog_category_id });
                    table.ForeignKey(
                        name: "FK__blog_post__blog___6DCC4D03",
                        column: x => x.blog_post_id,
                        principalTable: "blog_posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__blog_post__blog___6EC0713C",
                        column: x => x.blog_category_id,
                        principalTable: "blog_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "message_replies",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    message_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    reply = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__message___3213E83F71A482A4", x => x.id);
                    table.ForeignKey(
                        name: "FK__message_r__messa__607251E5",
                        column: x => x.message_id,
                        principalTable: "messages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__message_r__user___6166761E",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "wards",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    district_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__wards__3213E83F40B5ACA6", x => x.id);
                    table.ForeignKey(
                        name: "FK__wards__district___1F98B2C1",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "cart_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cart_id = table.Column<int>(type: "int", nullable: false),
                    food_id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    unit_price = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__cart_ite__3213E83F0294E126", x => x.id);
                    table.ForeignKey(
                        name: "FK__cart_item__cart___00200768",
                        column: x => x.cart_id,
                        principalTable: "carts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__cart_item__food___01142BA1",
                        column: x => x.food_id,
                        principalTable: "foods",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "food_events",
                columns: table => new
                {
                    food_id = table.Column<int>(type: "int", nullable: false),
                    event_id = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__food_eve__4D7B42AAB31326E4", x => new { x.food_id, x.event_id });
                    table.ForeignKey(
                        name: "FK__food_even__event__778AC167",
                        column: x => x.event_id,
                        principalTable: "cultural_events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__food_even__food___76969D2E",
                        column: x => x.food_id,
                        principalTable: "foods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "food_images",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    food_id = table.Column<int>(type: "int", nullable: false),
                    filename = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    is_primary = table.Column<bool>(type: "bit", nullable: false),
                    display_order = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__food_ima__3213E83FC4C3D115", x => x.id);
                    table.ForeignKey(
                        name: "FK__food_imag__food___5BE2A6F2",
                        column: x => x.food_id,
                        principalTable: "foods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "gift_set_items",
                columns: table => new
                {
                    gift_set_id = table.Column<int>(type: "int", nullable: false),
                    food_id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false, defaultValue: 1)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__gift_set__346B450952D07B4D", x => new { x.gift_set_id, x.food_id });
                    table.ForeignKey(
                        name: "FK__gift_set___food___114A936A",
                        column: x => x.food_id,
                        principalTable: "foods",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__gift_set___gift___10566F31",
                        column: x => x.gift_set_id,
                        principalTable: "gift_sets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    food_id = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    rating = table.Column<byte>(type: "tinyint", nullable: false),
                    comment = table.Column<string>(type: "text", nullable: true),
                    is_verified_purchase = table.Column<bool>(type: "bit", nullable: false),
                    is_approved = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__reviews__3213E83F4C635CE3", x => x.id);
                    table.ForeignKey(
                        name: "FK__reviews__food_id__628FA481",
                        column: x => x.food_id,
                        principalTable: "foods",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__reviews__user_id__6383C8BA",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "wishlists",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    food_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__wishlist__3213E83F54D768DB", x => x.id);
                    table.ForeignKey(
                        name: "FK__wishlists__food___06CD04F7",
                        column: x => x.food_id,
                        principalTable: "foods",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__wishlists__user___05D8E0BE",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "food_producers",
                columns: table => new
                {
                    food_id = table.Column<int>(type: "int", nullable: false),
                    producer_id = table.Column<int>(type: "int", nullable: false),
                    is_primary = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__food_pro__B1EBBED4BB2AE233", x => new { x.food_id, x.producer_id });
                    table.ForeignKey(
                        name: "FK__food_prod__food___6E01572D",
                        column: x => x.food_id,
                        principalTable: "foods",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__food_prod__produ__6EF57B66",
                        column: x => x.producer_id,
                        principalTable: "producers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_number = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    session_id = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    guest_email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    order_date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    subtotal = table.Column<decimal>(type: "decimal(16,2)", nullable: true),
                    tax = table.Column<decimal>(type: "decimal(16,2)", nullable: true),
                    shipping_fee = table.Column<decimal>(type: "decimal(16,2)", nullable: true),
                    discount = table.Column<decimal>(type: "decimal(16,2)", nullable: true),
                    coupon_id = table.Column<int>(type: "int", nullable: true),
                    gift_wrapping_fee = table.Column<decimal>(type: "decimal(16,2)", nullable: true),
                    total_amount = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    recipient_name = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    recipient_phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    recipient_email = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true),
                    province_id = table.Column<int>(type: "int", nullable: true),
                    district_id = table.Column<int>(type: "int", nullable: true),
                    ward_id = table.Column<int>(type: "int", nullable: true),
                    delivery_address = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    delivery_notes = table.Column<string>(type: "text", nullable: true),
                    payment_method_id = table.Column<int>(type: "int", nullable: true),
                    bank_id = table.Column<int>(type: "int", nullable: true),
                    transaction_id = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    payment_status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    order_status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    shipping_provider = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    shipping_method = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    tracking_number = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    estimated_delivery = table.Column<DateTime>(type: "datetime", nullable: true),
                    actual_delivery = table.Column<DateTime>(type: "datetime", nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true),
                    admin_notes = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__orders__3213E83F66FA6F3E", x => x.id);
                    table.ForeignKey(
                        name: "FK__orders__bank_id__498EEC8D",
                        column: x => x.bank_id,
                        principalTable: "banks",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__orders__coupon_i__47A6A41B",
                        column: x => x.coupon_id,
                        principalTable: "coupons",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__orders__district__4B7734FF",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__orders__payment___489AC854",
                        column: x => x.payment_method_id,
                        principalTable: "payment_methods",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__orders__province__4A8310C6",
                        column: x => x.province_id,
                        principalTable: "provinces",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__orders__user_id__46B27FE2",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__orders__ward_id__4C6B5938",
                        column: x => x.ward_id,
                        principalTable: "wards",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "shipping_addresses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    recipient_name = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    province_id = table.Column<int>(type: "int", nullable: false),
                    district_id = table.Column<int>(type: "int", nullable: false),
                    ward_id = table.Column<int>(type: "int", nullable: false),
                    address_detail = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    is_default = table.Column<bool>(type: "bit", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    updated_at = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__shipping__3213E83F3D6C9CEC", x => x.id);
                    table.ForeignKey(
                        name: "FK__shipping___distr__2645B050",
                        column: x => x.district_id,
                        principalTable: "districts",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__shipping___provi__25518C17",
                        column: x => x.province_id,
                        principalTable: "provinces",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__shipping___user___245D67DE",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK__shipping___ward___2739D489",
                        column: x => x.ward_id,
                        principalTable: "wards",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "order_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_id = table.Column<int>(type: "int", nullable: false),
                    food_id = table.Column<int>(type: "int", nullable: true),
                    product_name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    unit_price = table.Column<decimal>(type: "decimal(16,2)", nullable: false),
                    subtotal = table.Column<decimal>(type: "decimal(16,2)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__order_it__3213E83F858D6C91", x => x.id);
                    table.ForeignKey(
                        name: "FK__order_ite__food___51300E55",
                        column: x => x.food_id,
                        principalTable: "foods",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__order_ite__order__503BEA1C",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_status_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_id = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    comment = table.Column<string>(type: "text", nullable: true),
                    changed_by = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__order_st__3213E83FEB3FC26E", x => x.id);
                    table.ForeignKey(
                        name: "FK__order_sta__chang__55F4C372",
                        column: x => x.changed_by,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK__order_sta__order__55009F39",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_activity_logs_user_id",
                table: "activity_logs",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_bank_accounts_bank_id",
                table: "bank_accounts",
                column: "bank_id");

            migrationBuilder.CreateIndex(
                name: "UQ__banks__357D4CF9F1E95B74",
                table: "banks",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__blog_cat__32DD1E4CF375256E",
                table: "blog_categories",
                column: "slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_blog_post_categories_blog_category_id",
                table: "blog_post_categories",
                column: "blog_category_id");

            migrationBuilder.CreateIndex(
                name: "IX_blog_posts_author_id",
                table: "blog_posts",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "UQ__blog_pos__32DD1E4CBC2A3078",
                table: "blog_posts",
                column: "slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cart_items_cart_id",
                table: "cart_items",
                column: "cart_id");

            migrationBuilder.CreateIndex(
                name: "IX_cart_items_food_id",
                table: "cart_items",
                column: "food_id");

            migrationBuilder.CreateIndex(
                name: "IX_carts_user_id",
                table: "carts",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "UQ__coupons__357D4CF997A546B8",
                table: "coupons",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cultural_events_province_id",
                table: "cultural_events",
                column: "province_id");

            migrationBuilder.CreateIndex(
                name: "IX_districts_province_id",
                table: "districts",
                column: "province_id");

            migrationBuilder.CreateIndex(
                name: "IX_food_events_event_id",
                table: "food_events",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "IX_food_images_food_id",
                table: "food_images",
                column: "food_id");

            migrationBuilder.CreateIndex(
                name: "IX_food_producers_producer_id",
                table: "food_producers",
                column: "producer_id");

            migrationBuilder.CreateIndex(
                name: "IX_foods_category_id",
                table: "foods",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_foods_province_id",
                table: "foods",
                column: "province_id");

            migrationBuilder.CreateIndex(
                name: "IX_foods_region_id",
                table: "foods",
                column: "region_id");

            migrationBuilder.CreateIndex(
                name: "UQ__foods__32DD1E4C239DB539",
                table: "foods",
                column: "slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_gift_set_items_food_id",
                table: "gift_set_items",
                column: "food_id");

            migrationBuilder.CreateIndex(
                name: "UQ__gift_set__32DD1E4CE6DDA26A",
                table: "gift_sets",
                column: "slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_message_replies_message_id",
                table: "message_replies",
                column: "message_id");

            migrationBuilder.CreateIndex(
                name: "IX_message_replies_user_id",
                table: "message_replies",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_messages_assigned_to",
                table: "messages",
                column: "assigned_to");

            migrationBuilder.CreateIndex(
                name: "IX_order_items_food_id",
                table: "order_items",
                column: "food_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_items_order_id",
                table: "order_items",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_status_history_changed_by",
                table: "order_status_history",
                column: "changed_by");

            migrationBuilder.CreateIndex(
                name: "IX_order_status_history_order_id",
                table: "order_status_history",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_bank_id",
                table: "orders",
                column: "bank_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_coupon_id",
                table: "orders",
                column: "coupon_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_district_id",
                table: "orders",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_payment_method_id",
                table: "orders",
                column: "payment_method_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_province_id",
                table: "orders",
                column: "province_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_user_id",
                table: "orders",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_ward_id",
                table: "orders",
                column: "ward_id");

            migrationBuilder.CreateIndex(
                name: "UQ__orders__730E34DFC66C0A0B",
                table: "orders",
                column: "order_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ__payment___357D4CF96AAFA371",
                table: "payment_methods",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_producers_province_id",
                table: "producers",
                column: "province_id");

            migrationBuilder.CreateIndex(
                name: "UQ__producer__32DD1E4C46CDC3F3",
                table: "producers",
                column: "slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_provinces_region_id",
                table: "provinces",
                column: "region_id");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_food_id",
                table: "reviews",
                column: "food_id");

            migrationBuilder.CreateIndex(
                name: "IX_reviews_user_id",
                table: "reviews",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "UQ__settings__0DFAC4272EB17829",
                table: "settings",
                column: "setting_key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_shipping_addresses_district_id",
                table: "shipping_addresses",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_shipping_addresses_province_id",
                table: "shipping_addresses",
                column: "province_id");

            migrationBuilder.CreateIndex(
                name: "IX_shipping_addresses_user_id",
                table: "shipping_addresses",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_shipping_addresses_ward_id",
                table: "shipping_addresses",
                column: "ward_id");

            migrationBuilder.CreateIndex(
                name: "IX_shipping_fees_province_id",
                table: "shipping_fees",
                column: "province_id");

            migrationBuilder.CreateIndex(
                name: "UQ__users__AB6E61644254E430",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_wards_district_id",
                table: "wards",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_wishlists_food_id",
                table: "wishlists",
                column: "food_id");

            migrationBuilder.CreateIndex(
                name: "UQ_wishlist_user_food",
                table: "wishlists",
                columns: new[] { "user_id", "food_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "activity_logs");

            migrationBuilder.DropTable(
                name: "bank_accounts");

            migrationBuilder.DropTable(
                name: "blog_post_categories");

            migrationBuilder.DropTable(
                name: "cart_items");

            migrationBuilder.DropTable(
                name: "faqs");

            migrationBuilder.DropTable(
                name: "food_events");

            migrationBuilder.DropTable(
                name: "food_images");

            migrationBuilder.DropTable(
                name: "food_producers");

            migrationBuilder.DropTable(
                name: "gift_set_items");

            migrationBuilder.DropTable(
                name: "message_replies");

            migrationBuilder.DropTable(
                name: "order_items");

            migrationBuilder.DropTable(
                name: "order_status_history");

            migrationBuilder.DropTable(
                name: "password_resets");

            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.DropTable(
                name: "settings");

            migrationBuilder.DropTable(
                name: "shipping_addresses");

            migrationBuilder.DropTable(
                name: "shipping_fees");

            migrationBuilder.DropTable(
                name: "VnInfos");

            migrationBuilder.DropTable(
                name: "wishlists");

            migrationBuilder.DropTable(
                name: "blog_posts");

            migrationBuilder.DropTable(
                name: "blog_categories");

            migrationBuilder.DropTable(
                name: "carts");

            migrationBuilder.DropTable(
                name: "cultural_events");

            migrationBuilder.DropTable(
                name: "producers");

            migrationBuilder.DropTable(
                name: "gift_sets");

            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "foods");

            migrationBuilder.DropTable(
                name: "banks");

            migrationBuilder.DropTable(
                name: "coupons");

            migrationBuilder.DropTable(
                name: "payment_methods");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "wards");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "districts");

            migrationBuilder.DropTable(
                name: "provinces");

            migrationBuilder.DropTable(
                name: "regions");
        }
    }
}
