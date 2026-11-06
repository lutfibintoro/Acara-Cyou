using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebServer.Infrastructures.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    row_version = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    normalized_name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    concurrency_stamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_roles", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    row_version = table.Column<int>(type: "int", nullable: false),
                    user_name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    normalized_user_name = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    normalized_email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email_confirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    password_hash = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    security_stamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    concurrency_stamp = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone_number = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone_number_confirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    two_factor_enabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    lockout_end = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    lockout_enabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    access_failed_count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_users", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "bank_code",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    issuer = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_bifast_support = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bank_code", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "payment_gateway",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payment_gateway", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    row_version = table.Column<int>(type: "int", nullable: false),
                    role_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    claim_type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    claim_value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_role_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_role_claims_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "AspNetRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    row_version = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    claim_type = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    claim_value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_asp_net_user_claims_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    login_provider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    provider_key = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    row_version = table.Column<int>(type: "int", nullable: false),
                    provider_display_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    user_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_logins", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "fk_asp_net_user_logins_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    role_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    row_version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_asp_net_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "AspNetRoles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_asp_net_user_roles_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    login_provider = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    expired_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    row_version = table.Column<int>(type: "int", nullable: false),
                    value = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_asp_net_user_tokens", x => new { x.user_id, x.login_provider, x.name });
                    table.ForeignKey(
                        name: "fk_asp_net_user_tokens_asp_net_users_user_id",
                        column: x => x.user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "event",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    after_buy_description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    form_template = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    form_custom_url = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_registration_open = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    open_registration_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    close_registration_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    bank_account_number = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    bank_account_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    row_version = table.Column<int>(type: "int", nullable: false),
                    acara_cyou_user_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_event", x => x.id);
                    table.ForeignKey(
                        name: "fk_event_acara_cyou_user_acara_cyou_user_id",
                        column: x => x.acara_cyou_user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    total_item_price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    discount_amount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    application_fee = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    grand_total = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    form_template_value = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_checked_in = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    row_version = table.Column<int>(type: "int", nullable: false),
                    acara_cyou_user_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_acara_cyou_user_acara_cyou_user_id",
                        column: x => x.acara_cyou_user_id,
                        principalTable: "AspNetUsers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "payment_method",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    flat_fee = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    percentage_fee = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    admin_fee_bearer = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    payment_gateway_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payment_method", x => x.id);
                    table.ForeignKey(
                        name: "fk_payment_method_payment_gateway_payment_gateway_id",
                        column: x => x.payment_gateway_id,
                        principalTable: "payment_gateway",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "disbursement_detail",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    disbursement_account_number = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    disbursement_account_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    disbursement_amount = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    disbursement_fee = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    disbursementbank_code = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    disbursementbank_issuer = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    row_version = table.Column<int>(type: "int", nullable: false),
                    event_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_disbursement_detail", x => x.id);
                    table.ForeignKey(
                        name: "fk_disbursement_detail_event_event_id",
                        column: x => x.event_id,
                        principalTable: "event",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "poster",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    full_path_url = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    mime_type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    byte_length = table.Column<ulong>(type: "bigint unsigned", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    row_version = table.Column<int>(type: "int", nullable: false),
                    event_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_poster", x => x.id);
                    table.ForeignKey(
                        name: "fk_poster_event_event_id",
                        column: x => x.event_id,
                        principalTable: "event",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ticket_category",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    category = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    participant_notice = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    price_one = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    quota = table.Column<int>(type: "int", nullable: false),
                    booked_quota = table.Column<int>(type: "int", nullable: false),
                    is_sales_open = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    open_sales_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    close_sales_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    is_deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    row_version = table.Column<int>(type: "int", nullable: false),
                    event_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ticket_category", x => x.id);
                    table.ForeignKey(
                        name: "fk_ticket_category_event_event_id",
                        column: x => x.event_id,
                        principalTable: "event",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "invoice",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    invoice_number = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    amount_due = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    due_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    row_version = table.Column<int>(type: "int", nullable: false),
                    order_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_invoice", x => x.id);
                    table.ForeignKey(
                        name: "fk_invoice_order_order_id",
                        column: x => x.order_id,
                        principalTable: "order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "order_item",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    price_one = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    row_version = table.Column<int>(type: "int", nullable: false),
                    order_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    ticket_category_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_order_item", x => x.id);
                    table.ForeignKey(
                        name: "fk_order_item_order_order_id",
                        column: x => x.order_id,
                        principalTable: "order",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_order_item_ticket_category_ticket_category_id",
                        column: x => x.ticket_category_id,
                        principalTable: "ticket_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "payment_detail",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    pg_provider = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    pg_transaction_id = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    pg_reference = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    pg_method = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    pg_fee = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    paid_total = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    va = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    qr_string = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    raw_response = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    callback_at = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    row_version = table.Column<int>(type: "int", nullable: false),
                    invoice_id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payment_detail", x => x.id);
                    table.ForeignKey(
                        name: "fk_payment_detail_invoice_invoice_id",
                        column: x => x.invoice_id,
                        principalTable: "invoice",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "bank_code",
                columns: new[] { "id", "code", "is_bifast_support", "issuer" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "002", true, "Bank BRI" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "008", true, "Bank Mandiri" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "009", true, "Bank BNI" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "011", true, "Bank Danamon" },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "013", true, "Bank Permata" },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "014", true, "Bank Central Asia" },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "016", true, "Bank Maybank Indonesia" },
                    { new Guid("00000000-0000-0000-0000-000000000008"), "019", true, "Bank Panin" },
                    { new Guid("00000000-0000-0000-0000-000000000009"), "022", true, "CIMB Niaga" },
                    { new Guid("00000000-0000-0000-0000-00000000000a"), "023", true, "Bank UOB Indonesia" },
                    { new Guid("00000000-0000-0000-0000-00000000000b"), "028", true, "Bank OCBC NISP" },
                    { new Guid("00000000-0000-0000-0000-00000000000c"), "031", true, "Citi Bank" },
                    { new Guid("00000000-0000-0000-0000-00000000000d"), "036", true, "Bank CCB (Ex-Bank Windu Kentjana)" },
                    { new Guid("00000000-0000-0000-0000-00000000000e"), "037", true, "Bank Artha Graha" },
                    { new Guid("00000000-0000-0000-0000-00000000000f"), "042", true, "MUFG Bank" },
                    { new Guid("00000000-0000-0000-0000-000000000010"), "046", true, "Bank DBS" },
                    { new Guid("00000000-0000-0000-0000-000000000011"), "050", true, "Standard Chartered Bank" },
                    { new Guid("00000000-0000-0000-0000-000000000012"), "054", true, "Bank Capital" },
                    { new Guid("00000000-0000-0000-0000-000000000013"), "061", true, "ANZ Indonesia" },
                    { new Guid("00000000-0000-0000-0000-000000000014"), "069", true, "Bank Of China Indonesia" },
                    { new Guid("00000000-0000-0000-0000-000000000015"), "076", true, "Bank Bumi Arta" },
                    { new Guid("00000000-0000-0000-0000-000000000016"), "087", true, "Bank HSBC Indonesia" },
                    { new Guid("00000000-0000-0000-0000-000000000017"), "095", true, "Bank JTrust Indonesia" },
                    { new Guid("00000000-0000-0000-0000-000000000018"), "097", true, "Bank Mayapada" },
                    { new Guid("00000000-0000-0000-0000-000000000019"), "110", true, "Bank BJB" },
                    { new Guid("00000000-0000-0000-0000-00000000001a"), "111", true, "Bank DKI" },
                    { new Guid("00000000-0000-0000-0000-00000000001b"), "112", true, "Bank BPD DIY" },
                    { new Guid("00000000-0000-0000-0000-00000000001c"), "113", true, "Bank Jateng" },
                    { new Guid("00000000-0000-0000-0000-00000000001d"), "115", true, "Bank Jambi" },
                    { new Guid("00000000-0000-0000-0000-00000000001e"), "116", true, "Bank Aceh" },
                    { new Guid("00000000-0000-0000-0000-00000000001f"), "117", true, "Bank Sumut" },
                    { new Guid("00000000-0000-0000-0000-000000000020"), "118", true, "Bank Nagari" },
                    { new Guid("00000000-0000-0000-0000-000000000021"), "119", true, "Bank Riau Kepri" },
                    { new Guid("00000000-0000-0000-0000-000000000022"), "120", true, "Bank Sumsel Babel" },
                    { new Guid("00000000-0000-0000-0000-000000000023"), "121", true, "Bank Lampung" },
                    { new Guid("00000000-0000-0000-0000-000000000024"), "122", true, "Bank Kalsel" },
                    { new Guid("00000000-0000-0000-0000-000000000025"), "123", true, "Bank Kalbar" },
                    { new Guid("00000000-0000-0000-0000-000000000026"), "124", true, "Bank Kaltimtara" },
                    { new Guid("00000000-0000-0000-0000-000000000027"), "125", true, "Bank Kalteng" },
                    { new Guid("00000000-0000-0000-0000-000000000028"), "126", true, "Bank Sulselbar" },
                    { new Guid("00000000-0000-0000-0000-000000000029"), "127", true, "Bank Sulut Go" },
                    { new Guid("00000000-0000-0000-0000-00000000002a"), "128", true, "Bank NTB Syariah" },
                    { new Guid("00000000-0000-0000-0000-00000000002b"), "129", true, "Bank BPD Bali" },
                    { new Guid("00000000-0000-0000-0000-00000000002c"), "131", true, "Bank Maluku Malut" },
                    { new Guid("00000000-0000-0000-0000-00000000002d"), "132", true, "Bank Papua" },
                    { new Guid("00000000-0000-0000-0000-00000000002e"), "133", true, "Bank Bengkulu" },
                    { new Guid("00000000-0000-0000-0000-00000000002f"), "134", true, "Bank Sulteng" },
                    { new Guid("00000000-0000-0000-0000-000000000030"), "135", true, "Bank Sultra" },
                    { new Guid("00000000-0000-0000-0000-000000000031"), "137", true, "Bank Banten" },
                    { new Guid("00000000-0000-0000-0000-000000000032"), "147", true, "Bank Muamalat Indonesia" },
                    { new Guid("00000000-0000-0000-0000-000000000033"), "151", true, "Bank Mestika" },
                    { new Guid("00000000-0000-0000-0000-000000000034"), "152", true, "Bank Shinhan Indonesia" },
                    { new Guid("00000000-0000-0000-0000-000000000035"), "153", true, "Bank Sinarmas" },
                    { new Guid("00000000-0000-0000-0000-000000000036"), "157", true, "Bank Maspion Indonesia" },
                    { new Guid("00000000-0000-0000-0000-000000000037"), "161", true, "Bank Ganesha" },
                    { new Guid("00000000-0000-0000-0000-000000000038"), "167", true, "Bank QNB Indonesia" },
                    { new Guid("00000000-0000-0000-0000-000000000039"), "200", true, "Bank BTN" },
                    { new Guid("00000000-0000-0000-0000-00000000003a"), "212", true, "Bank Woori Saudara" },
                    { new Guid("00000000-0000-0000-0000-00000000003b"), "213", true, "Bank BTPN" },
                    { new Guid("00000000-0000-0000-0000-00000000003c"), "425", true, "Bank BJB Syariah" },
                    { new Guid("00000000-0000-0000-0000-00000000003d"), "426", true, "Bank Mega" },
                    { new Guid("00000000-0000-0000-0000-00000000003e"), "441", true, "Bank KB Bukopin" },
                    { new Guid("00000000-0000-0000-0000-00000000003f"), "451", true, "Bank Syariah Indonesia" },
                    { new Guid("00000000-0000-0000-0000-000000000040"), "472", true, "Bank Jasa Jakarta" },
                    { new Guid("00000000-0000-0000-0000-000000000041"), "484", true, "Bank KEB Hana" },
                    { new Guid("00000000-0000-0000-0000-000000000042"), "485", true, "MNC Bank" },
                    { new Guid("00000000-0000-0000-0000-000000000043"), "490", true, "Bank Neo Commerce" },
                    { new Guid("00000000-0000-0000-0000-000000000044"), "494", true, "Bank BRI Agroniaga" },
                    { new Guid("00000000-0000-0000-0000-000000000045"), "498", true, "Bank SBI" },
                    { new Guid("00000000-0000-0000-0000-000000000046"), "501", true, "Bank Digital BCA" },
                    { new Guid("00000000-0000-0000-0000-000000000047"), "503", true, "Bank Nobu" },
                    { new Guid("00000000-0000-0000-0000-000000000048"), "506", true, "Bank Mega Syariah" },
                    { new Guid("00000000-0000-0000-0000-000000000049"), "513", true, "Bank Ina Perdana" },
                    { new Guid("00000000-0000-0000-0000-00000000004a"), "517", true, "Bank Panin Dubai Syariah" },
                    { new Guid("00000000-0000-0000-0000-00000000004b"), "521", true, "Bank Syariah Bukopin" },
                    { new Guid("00000000-0000-0000-0000-00000000004c"), "523", true, "Bank Sahabat Sampoerna" },
                    { new Guid("00000000-0000-0000-0000-00000000004d"), "526", true, "Bank Oke Indonesia" },
                    { new Guid("00000000-0000-0000-0000-00000000004e"), "531", true, "AMAR BANK" },
                    { new Guid("00000000-0000-0000-0000-00000000004f"), "535", true, "SEA Bank" },
                    { new Guid("00000000-0000-0000-0000-000000000050"), "536", true, "Bank BCA Syariah" },
                    { new Guid("00000000-0000-0000-0000-000000000051"), "542", true, "Bank Jago" },
                    { new Guid("00000000-0000-0000-0000-000000000052"), "547", true, "Bank BTPN Syariah" },
                    { new Guid("00000000-0000-0000-0000-000000000053"), "548", true, "Bank Multiarta Sentosa" },
                    { new Guid("00000000-0000-0000-0000-000000000054"), "553", true, "Bank Mayora" },
                    { new Guid("00000000-0000-0000-0000-000000000055"), "555", true, "Bank Index Selindo" },
                    { new Guid("00000000-0000-0000-0000-000000000056"), "564", true, "Bank Mantap" },
                    { new Guid("00000000-0000-0000-0000-000000000057"), "566", true, "Bank Victoria International" },
                    { new Guid("00000000-0000-0000-0000-000000000058"), "567", true, "Allo Bank" },
                    { new Guid("00000000-0000-0000-0000-000000000059"), "947", true, "Bank Aladin Syariah" },
                    { new Guid("00000000-0000-0000-0000-00000000005a"), "949", true, "Bank CTBC" },
                    { new Guid("00000000-0000-0000-0000-00000000005b"), "950", true, "Bank Commonwealth" }
                });

            migrationBuilder.InsertData(
                table: "payment_gateway",
                columns: new[] { "id", "name" },
                values: new object[] { new Guid("10000000-0000-0000-0000-000000000000"), "Duitku" });

            migrationBuilder.InsertData(
                table: "payment_method",
                columns: new[] { "id", "admin_fee_bearer", "code", "flat_fee", "name", "payment_gateway_id", "percentage_fee", "type" },
                values: new object[,]
                {
                    { new Guid("20000000-0000-0000-0000-000000000001"), "Customer", "BC", 5000m, "BCA Virtual Account", new Guid("10000000-0000-0000-0000-000000000000"), 0m, "VirtualAccount" },
                    { new Guid("20000000-0000-0000-0000-000000000002"), "Customer", "M2", 3000m, "Mandiri Virtual Account", new Guid("10000000-0000-0000-0000-000000000000"), 0m, "VirtualAccount" },
                    { new Guid("20000000-0000-0000-0000-000000000003"), "Customer", "VA", 3000m, "Maybank Virtual Account", new Guid("10000000-0000-0000-0000-000000000000"), 0m, "VirtualAccount" },
                    { new Guid("20000000-0000-0000-0000-000000000004"), "Customer", "I1", 3000m, "BNI Virtual Account", new Guid("10000000-0000-0000-0000-000000000000"), 0m, "VirtualAccount" },
                    { new Guid("20000000-0000-0000-0000-000000000005"), "Customer", "B1", 3000m, "CIMB Niaga Virtual Account", new Guid("10000000-0000-0000-0000-000000000000"), 0m, "VirtualAccount" },
                    { new Guid("20000000-0000-0000-0000-000000000006"), "Customer", "BT", 0m, "Permata Bank Virtual Account", new Guid("10000000-0000-0000-0000-000000000000"), 0m, "VirtualAccount" },
                    { new Guid("20000000-0000-0000-0000-000000000007"), "Customer", "A1", 0m, "ATM Bersama", new Guid("10000000-0000-0000-0000-000000000000"), 0m, "VirtualAccount" },
                    { new Guid("20000000-0000-0000-0000-000000000008"), "Customer", "AG", 3000m, "Bank Artha Graha", new Guid("10000000-0000-0000-0000-000000000000"), 0m, "VirtualAccount" },
                    { new Guid("20000000-0000-0000-0000-000000000009"), "Customer", "NC", 3000m, "Bank Neo Commerce / BNC", new Guid("10000000-0000-0000-0000-000000000000"), 0m, "VirtualAccount" },
                    { new Guid("20000000-0000-0000-0000-00000000000a"), "Customer", "BR", 3000m, "BRIVA", new Guid("10000000-0000-0000-0000-000000000000"), 0m, "VirtualAccount" },
                    { new Guid("20000000-0000-0000-0000-00000000000b"), "Customer", "S1", 3000m, "Bank Sahabat Sampoerna", new Guid("10000000-0000-0000-0000-000000000000"), 0m, "VirtualAccount" },
                    { new Guid("20000000-0000-0000-0000-00000000000c"), "Customer", "BV", 3000m, "BSI Virtual Account", new Guid("10000000-0000-0000-0000-000000000000"), 0m, "VirtualAccount" },
                    { new Guid("20000000-0000-0000-0000-00000000000d"), "Merchant", "NQ", 0m, "Nobu", new Guid("10000000-0000-0000-0000-000000000000"), 0m, "Qris" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_role_claims_role_id",
                table: "AspNetRoleClaims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_claims_user_id",
                table: "AspNetUserClaims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_logins_id",
                table: "AspNetUserLogins",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_logins_user_id",
                table: "AspNetUserLogins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_roles_id",
                table: "AspNetUserRoles",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_roles_role_id",
                table: "AspNetUserRoles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "normalized_email");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "normalized_user_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_asp_net_user_tokens_id",
                table: "AspNetUserTokens",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_disbursement_detail_event_id",
                table: "disbursement_detail",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "ix_event_acara_cyou_user_id",
                table: "event",
                column: "acara_cyou_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_event_form_custom_url",
                table: "event",
                column: "form_custom_url",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_invoice_invoice_number",
                table: "invoice",
                column: "invoice_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_invoice_order_id",
                table: "invoice",
                column: "order_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_order_acara_cyou_user_id",
                table: "order",
                column: "acara_cyou_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_item_order_id",
                table: "order_item",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "ix_order_item_ticket_category_id",
                table: "order_item",
                column: "ticket_category_id");

            migrationBuilder.CreateIndex(
                name: "ix_payment_detail_invoice_id",
                table: "payment_detail",
                column: "invoice_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_payment_method_payment_gateway_id",
                table: "payment_method",
                column: "payment_gateway_id");

            migrationBuilder.CreateIndex(
                name: "ix_poster_event_id",
                table: "poster",
                column: "event_id");

            migrationBuilder.CreateIndex(
                name: "ix_ticket_category_event_id",
                table: "ticket_category",
                column: "event_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "bank_code");

            migrationBuilder.DropTable(
                name: "disbursement_detail");

            migrationBuilder.DropTable(
                name: "order_item");

            migrationBuilder.DropTable(
                name: "payment_detail");

            migrationBuilder.DropTable(
                name: "payment_method");

            migrationBuilder.DropTable(
                name: "poster");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "ticket_category");

            migrationBuilder.DropTable(
                name: "invoice");

            migrationBuilder.DropTable(
                name: "payment_gateway");

            migrationBuilder.DropTable(
                name: "event");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
