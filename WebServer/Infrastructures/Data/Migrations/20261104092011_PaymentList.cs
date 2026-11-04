using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebServer.Infrastructures.Data.Migrations
{
    /// <inheritdoc />
    public partial class PaymentList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "bank_code",
                columns: new[] { "id", "code", "is_bifast_support", "issuer" },
                values: new object[,]
                {
                    { new Guid("02eeb09e-9e86-40ec-aaae-1b23b52c374d"), "046", true, "Bank DBS" },
                    { new Guid("07a5040b-7cdf-4ed3-952e-b334bf83298c"), "037", true, "Bank Artha Graha" },
                    { new Guid("09bc6f77-ec74-446f-8ae2-0c9c84af3e80"), "009", true, "Bank BNI" },
                    { new Guid("0a352567-ed59-4e37-bac1-6fed295ecb0c"), "490", true, "Bank Neo Commerce" },
                    { new Guid("0ba6f8ef-38e5-4e92-913b-9c3a23eace6d"), "135", true, "Bank Sultra" },
                    { new Guid("0de899cb-cfb6-44f3-acf9-e11bc6c82ed9"), "167", true, "Bank QNB Indonesia" },
                    { new Guid("0df0ece7-116d-4c11-8b8b-ef48ccdcf0ab"), "950", true, "Bank Commonwealth" },
                    { new Guid("0e4a706f-cdf3-4b44-a12a-e3d9561b4caa"), "111", true, "Bank DKI" },
                    { new Guid("124f75cb-0f15-4d03-88fe-0e6e2613b53d"), "132", true, "Bank Papua" },
                    { new Guid("12d6b585-7a04-4286-b25c-d27c9121643b"), "200", true, "Bank BTN" },
                    { new Guid("1313e34a-ecb7-435b-bbaa-a6e0dc4974f6"), "023", true, "Bank UOB Indonesia" },
                    { new Guid("14bca713-1927-4130-91c2-23c5cb280724"), "157", true, "Bank Maspion Indonesia" },
                    { new Guid("175cb9e4-5588-4a68-b4ea-b43a502824d2"), "120", true, "Bank Sumsel Babel" },
                    { new Guid("25bbe6df-9258-4758-b747-974367b7e828"), "126", true, "Bank Sulselbar" },
                    { new Guid("2bc8edaf-84ed-4bec-aaee-5fa579ef7a3c"), "531", true, "AMAR BANK" },
                    { new Guid("2c03808b-ec61-4fa6-9088-b272a035ef0f"), "564", true, "Bank Mantap" },
                    { new Guid("330e065b-c629-49ad-99be-3b8c82a6034e"), "426", true, "Bank Mega" },
                    { new Guid("33a5228c-cadf-40d8-bead-705b888dd94e"), "121", true, "Bank Lampung" },
                    { new Guid("37876e68-c265-482d-ae66-62187ee189a9"), "133", true, "Bank Bengkulu" },
                    { new Guid("37ce4dd6-9679-4867-998b-95da9986406a"), "212", true, "Bank Woori Saudara" },
                    { new Guid("39507920-5f60-4ee4-814b-d1c281a6172e"), "555", true, "Bank Index Selindo" },
                    { new Guid("3e5a2615-d410-4af5-adbf-af6bb89ff6a8"), "536", true, "Bank BCA Syariah" },
                    { new Guid("429acb96-ade0-459b-9977-863ecbe6c3a0"), "535", true, "SEA Bank" },
                    { new Guid("4412f239-9b1f-4440-840d-ec6561854cb3"), "147", true, "Bank Muamalat Indonesia" },
                    { new Guid("4b94a54d-d5ad-40ca-9de9-ece2ca7a59bf"), "134", true, "Bank Sulteng" },
                    { new Guid("4be4ff2f-527f-44d6-8908-d3f6dc5965c5"), "117", true, "Bank Sumut" },
                    { new Guid("4eda5e82-dd27-4ef1-a970-cf96370c3071"), "517", true, "Bank Panin Dubai Syariah" },
                    { new Guid("52d32091-da1e-4114-a631-5c29e095bfab"), "129", true, "Bank BPD Bali" },
                    { new Guid("540a117e-1744-4121-ba11-7f4cefbe036b"), "498", true, "Bank SBI" },
                    { new Guid("56bf2d5f-e11d-458c-81dc-e45d24eaff20"), "016", true, "Bank Maybank Indonesia" },
                    { new Guid("5c5f055c-89bf-4742-afb8-3bf2f4749503"), "014", true, "Bank Central Asia" },
                    { new Guid("5f7904a2-7920-4ea6-90d2-dd867e08c21d"), "124", true, "Bank Kaltimtara" },
                    { new Guid("5faab44f-f664-41e9-b117-855f42280695"), "137", true, "Bank Banten" },
                    { new Guid("634d07a3-0b93-4c17-a978-0f997bb31acf"), "526", true, "Bank Oke Indonesia" },
                    { new Guid("64bbc7cc-bb25-4579-8d49-b386b1f04171"), "213", true, "Bank BTPN" },
                    { new Guid("685045a7-cba8-42b5-8c92-215f72b8875b"), "451", true, "Bank Syariah Indonesia" },
                    { new Guid("6b835f48-cf3b-4b9c-86d1-5bc5100c3da2"), "011", true, "Bank Danamon" },
                    { new Guid("6bf594cf-f6e0-4d33-989b-34a7e9667251"), "548", true, "Bank Multiarta Sentosa" },
                    { new Guid("6dc1e8f5-e6d3-474d-b201-16f1e41a1677"), "506", true, "Bank Mega Syariah" },
                    { new Guid("7109b4ee-8ed8-4e3f-8177-d1335e2fc88f"), "484", true, "Bank KEB Hana" },
                    { new Guid("78a04854-04ff-43d0-8fc6-3b3e7b70b5b4"), "013", true, "Bank Permata" },
                    { new Guid("7e0c5519-a2e0-4da7-ba03-6aa5e97daff2"), "553", true, "Bank Mayora" },
                    { new Guid("82fcc786-7109-4173-b682-93bbd2f6bb91"), "127", true, "Bank Sulut Go" },
                    { new Guid("84c6e297-1906-42b4-aa90-dbfd2b65bfdc"), "521", true, "Bank Syariah Bukopin" },
                    { new Guid("8e8f1741-2bbf-42c3-9e74-e4e9f7537f84"), "087", true, "Bank HSBC Indonesia" },
                    { new Guid("8f73cf94-cd0c-4688-a978-0f7f2ed7229d"), "153", true, "Bank Sinarmas" },
                    { new Guid("90851c2a-9ede-4b47-8132-bc17fd004510"), "131", true, "Bank Maluku Malut" },
                    { new Guid("9376dd5f-e620-4326-b3a0-b77494396694"), "567", true, "Allo Bank" },
                    { new Guid("96603050-6950-431a-8063-240ca65b408f"), "425", true, "Bank BJB Syariah" },
                    { new Guid("9b3975ff-ee2c-4c61-a7da-ab2c5c8b87be"), "069", true, "Bank Of China Indonesia" },
                    { new Guid("9c6664b0-adae-4b7d-9b68-7cc14de87f5b"), "076", true, "Bank Bumi Arta" },
                    { new Guid("9cf2ba99-dbb8-419e-8442-045c054cc97a"), "054", true, "Bank Capital" },
                    { new Guid("9f9b9998-c4a3-4cf9-9295-9b0cebd876fe"), "097", true, "Bank Mayapada" },
                    { new Guid("a558355e-9552-4a0b-adbb-0422769d9a42"), "122", true, "Bank Kalsel" },
                    { new Guid("af99c855-6a90-48ea-910b-5dcd4088d1e1"), "112", true, "Bank BPD DIY" },
                    { new Guid("af9be80f-a893-4db5-9a79-517a4ec74c47"), "008", true, "Bank Mandiri" },
                    { new Guid("b273ee6a-661d-4477-a835-8dba5bced7e5"), "113", true, "Bank Jateng" },
                    { new Guid("b32dcb60-328d-4405-875b-658ea94f9348"), "125", true, "Bank Kalteng" },
                    { new Guid("b50cae28-e1f8-4540-90ab-4c34dca30ae9"), "002", true, "Bank BRI" },
                    { new Guid("bf1207b8-9d36-47e8-9844-b670ab2f18d4"), "441", true, "Bank KB Bukopin" },
                    { new Guid("c0fcb19f-8421-46fb-a60b-aa5f9cf6d6db"), "523", true, "Bank Sahabat Sampoerna" },
                    { new Guid("c5204104-8817-4a2c-9234-e068cf3cac7c"), "110", true, "Bank BJB" },
                    { new Guid("c7e567a7-5b9f-4a52-ae63-d343eab8c258"), "161", true, "Bank Ganesha" },
                    { new Guid("cdc28fb3-8b0a-4c23-870b-62d4370899a9"), "050", true, "Standard Chartered Bank" },
                    { new Guid("cdf7604f-d226-4ae6-b32c-e81a48c3ae9c"), "151", true, "Bank Mestika" },
                    { new Guid("d1c381c4-5938-4f15-ae9b-6197bf428149"), "036", true, "Bank CCB (Ex-Bank Windu Kentjana)" },
                    { new Guid("d3aa8f40-6d82-4171-85f8-448bcea7c38f"), "542", true, "Bank Jago" },
                    { new Guid("d53381e9-4ff9-4473-a77d-491c2129e1ab"), "566", true, "Bank Victoria International" },
                    { new Guid("d60b704b-6871-4b3f-9acb-ba05752c3145"), "947", true, "Bank Aladin Syariah" },
                    { new Guid("d7ebe307-2d98-495f-8626-1427a043e21b"), "031", true, "Citi Bank" },
                    { new Guid("d8c29d26-fc51-4471-8296-fec74a904b5c"), "472", true, "Bank Jasa Jakarta" },
                    { new Guid("dc2534db-ae5f-4401-ac76-713280ab4c48"), "485", true, "MNC Bank" },
                    { new Guid("dcb8c25e-56b2-4c33-867c-385b8f66e0af"), "494", true, "Bank BRI Agroniaga" },
                    { new Guid("dd9b6428-445a-40a5-8769-2ad1d6eef0ed"), "128", true, "Bank NTB Syariah" },
                    { new Guid("deb2b243-43d2-478c-8d94-658436d360f6"), "513", true, "Bank Ina Perdana" },
                    { new Guid("df71abeb-007f-41ab-9148-b0fc519df104"), "028", true, "Bank OCBC NISP" },
                    { new Guid("e2993be8-5350-4cdc-94b6-341a791f062a"), "118", true, "Bank Nagari" },
                    { new Guid("e30f8211-477b-4fe5-9285-5fabc50ef584"), "115", true, "Bank Jambi" },
                    { new Guid("e6154d7b-575c-45d6-9756-221c9c90d6eb"), "019", true, "Bank Panin" },
                    { new Guid("e6b1a3d8-5e4d-4489-ae41-dc07b11cd194"), "119", true, "Bank Riau Kepri" },
                    { new Guid("ea1c0315-995c-4528-9998-34d88a4a15c6"), "123", true, "Bank Kalbar" },
                    { new Guid("efa4388c-a7f0-4680-9a24-bc13d9f80904"), "503", true, "Bank Nobu" },
                    { new Guid("f30a0871-120f-4144-9dc4-7fef48072a08"), "547", true, "Bank BTPN Syariah" },
                    { new Guid("f392959d-b5d8-4c0c-9e5b-852ab2b264c1"), "949", true, "Bank CTBC" },
                    { new Guid("f65c789c-1a49-4613-b5fa-21d01750db6b"), "095", true, "Bank JTrust Indonesia" },
                    { new Guid("f87d8e72-65a7-40dd-ba1e-5c5b8593c6dd"), "061", true, "ANZ Indonesia" },
                    { new Guid("f90f4766-d214-4ef3-80d0-8528c2e0648f"), "022", true, "CIMB Niaga" },
                    { new Guid("f93deb71-3d0b-48d7-aab2-e9aa3d11f394"), "152", true, "Bank Shinhan Indonesia" },
                    { new Guid("ff24f85b-83d2-444d-9096-e17c13cbf1de"), "501", true, "Bank Digital BCA" },
                    { new Guid("ff3f98ea-fbda-4cce-8e75-1ddc5b4fd4c9"), "042", true, "MUFG Bank" },
                    { new Guid("ff42ebbc-743a-4275-9987-f55e51493355"), "116", true, "Bank Aceh" }
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
                    { new Guid("0c77b342-aba5-4aa5-a2b1-5adc15d7239f"), "Customer", "I1", 3000m, "BNI Virtual Account", new Guid("10000000-0000-0000-0000-000000000000"), 0m, "VirtualAccount" },
                    { new Guid("11bf5a23-3def-486b-aebd-dc8093e52f72"), "Customer", "BT", 0m, "Permata Bank Virtual Account", new Guid("10000000-0000-0000-0000-000000000000"), 0m, "VirtualAccount" },
                    { new Guid("256c3828-df50-4dbb-81c1-357b76dbb880"), "Customer", "A1", 0m, "ATM Bersama", new Guid("10000000-0000-0000-0000-000000000000"), 0m, "VirtualAccount" },
                    { new Guid("45d31c99-efa2-4ad9-a253-d0ffed3b1a67"), "Customer", "M2", 3000m, "Mandiri Virtual Account", new Guid("10000000-0000-0000-0000-000000000000"), 0m, "VirtualAccount" },
                    { new Guid("51e684e8-108a-412a-8212-ccbb888430ec"), "Customer", "S1", 3000m, "Bank Sahabat Sampoerna", new Guid("10000000-0000-0000-0000-000000000000"), 0m, "VirtualAccount" },
                    { new Guid("52d36286-76e0-4ad0-85c3-d66f68e1261d"), "Customer", "BV", 3000m, "BSI Virtual Account", new Guid("10000000-0000-0000-0000-000000000000"), 0m, "VirtualAccount" },
                    { new Guid("582f5cbb-858b-4c83-b1ca-209d6f2b886c"), "Customer", "VA", 3000m, "Maybank Virtual Account", new Guid("10000000-0000-0000-0000-000000000000"), 0m, "VirtualAccount" },
                    { new Guid("7e1b80d1-0411-4199-b781-00224c23e472"), "Customer", "BC", 5000m, "BCA Virtual Account", new Guid("10000000-0000-0000-0000-000000000000"), 0m, "VirtualAccount" },
                    { new Guid("ae4ea82d-5ae9-4abe-8b1e-fb54229b43b0"), "Customer", "BR", 3000m, "BRIVA", new Guid("10000000-0000-0000-0000-000000000000"), 0m, "VirtualAccount" },
                    { new Guid("af0902ca-2f24-4e5a-8d08-21e4838e37cf"), "Customer", "B1", 3000m, "CIMB Niaga Virtual Account", new Guid("10000000-0000-0000-0000-000000000000"), 0m, "VirtualAccount" },
                    { new Guid("bb412bac-6929-4da3-b010-dc665a85c439"), "Customer", "NC", 3000m, "Bank Neo Commerce / BNC", new Guid("10000000-0000-0000-0000-000000000000"), 0m, "VirtualAccount" },
                    { new Guid("f1108c01-ceea-4727-9126-47112f924d97"), "Customer", "AG", 3000m, "Bank Artha Graha", new Guid("10000000-0000-0000-0000-000000000000"), 0m, "VirtualAccount" },
                    { new Guid("ffca3042-05fc-4c1f-a4f5-8a874639e521"), "Merchant", "NQ", 0m, "Nobu", new Guid("10000000-0000-0000-0000-000000000000"), 0m, "Qris" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_payment_method_payment_gateway_id",
                table: "payment_method",
                column: "payment_gateway_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bank_code");

            migrationBuilder.DropTable(
                name: "payment_method");

            migrationBuilder.DropTable(
                name: "payment_gateway");
        }
    }
}
