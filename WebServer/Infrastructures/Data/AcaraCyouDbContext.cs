using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using WebServer.Infrastructures.Data.Entities;

namespace WebServer.Infrastructures.Data
{
    public class AcaraCyouDbContext : IdentityDbContext<AcaraCyouUser, AcaraCyouRole, Guid, AcaraCyouUserClaim, AcaraCyouUserRole, AcaraCyouUserLogin, AcaraCyouRoleClaim, AcaraCyouUserToken>
    {
        public AcaraCyouDbContext(DbContextOptions<AcaraCyouDbContext> options) : base(options) { }

        // DbSet Here
        public DbSet<AcaraCyouUser> AcaraCyouUser { get; set; }
        public DbSet<AcaraCyouRole> AcaraCyouRole { get; set; }
        public DbSet<AcaraCyouUserRole> AcaraCyouUserRole { get; set; }
        public DbSet<AcaraCyouRoleClaim> AcaraCyouRoleClaim { get; set; }
        public DbSet<AcaraCyouUserClaim> AcaraCyouUserClaim { get; set; }
        public DbSet<AcaraCyouUserLogin> AcaraCyouUserLogin { get; set; }
        public DbSet<AcaraCyouUserToken> AcaraCyouUserToken { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<DisbursementDetail> DisbursementDetail { get; set; }
        public DbSet<Poster> Poster { get; set; }
        public DbSet<TicketCategory> TicketCategory { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<PaymentDetail> PaymentDetail { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<BankCode> BankCode { get; set; }
        public DbSet<PaymentGateway> PaymentGateway { get; set; }
        public DbSet<PaymentMethod> PaymentMethod { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AcaraCyouUser>(buildAction =>
            {
                buildAction.HasKey(acaraCyouUser => acaraCyouUser.Id);

                buildAction
                    .Property(acaraCyouUser => acaraCyouUser.Id)
                    .IsRequired(true)
                    .ValueGeneratedOnAdd()
                    .HasValueGenerator<SequentialGuidValueGenerator>();
            });

            builder.Entity<AcaraCyouRole>(buildAction =>
            {
                buildAction.HasKey(acaraCyouRole => acaraCyouRole.Id);

                buildAction
                    .Property(acaraCyouRole => acaraCyouRole.Id)
                    .IsRequired(true)
                    .ValueGeneratedOnAdd()
                    .HasValueGenerator<SequentialGuidValueGenerator>();
            });

            builder.Entity<AcaraCyouUserRole>(buildAction =>
            {
                buildAction
                    .Property(acaraCyouUserRole => acaraCyouUserRole.Id)
                    .IsRequired(true)
                    .ValueGeneratedOnAdd()
                    .HasValueGenerator<SequentialGuidValueGenerator>();

                buildAction.HasIndex(ur => ur.Id)
                      .IsUnique();
            });

            builder.Entity<AcaraCyouRoleClaim>(buildAction =>
            {
                buildAction.HasKey(acaraCyouRoleClaim => acaraCyouRoleClaim.Id);

                buildAction
                    .Property(acaraCyouRoleClaim => acaraCyouRoleClaim.Id)
                    .IsRequired(true)
                    .ValueGeneratedOnAdd()
                    .HasValueGenerator<SequentialGuidValueGenerator>();
            });

            builder.Entity<AcaraCyouUserClaim>(buildAction =>
            {
                buildAction.HasKey(acaraCyouUserClaim => acaraCyouUserClaim.Id);

                buildAction
                    .Property(acaraCyouUserClaim => acaraCyouUserClaim.Id)
                    .IsRequired(true)
                    .ValueGeneratedOnAdd()
                    .HasValueGenerator<SequentialGuidValueGenerator>();
            });

            builder.Entity<AcaraCyouUserLogin>(buildAction =>
            {
                buildAction
                    .Property(acaraCyouUserLogin => acaraCyouUserLogin.Id)
                    .IsRequired(true)
                    .ValueGeneratedOnAdd()
                    .HasValueGenerator<SequentialGuidValueGenerator>();

                buildAction.HasIndex(ur => ur.Id)
                      .IsUnique();
            });

            builder.Entity<AcaraCyouUserToken>(buildAction =>
            {
                buildAction
                    .Property(acaraCyouUserToken => acaraCyouUserToken.Id)
                    .IsRequired(true)
                    .ValueGeneratedOnAdd()
                    .HasValueGenerator<SequentialGuidValueGenerator>();

                buildAction.HasIndex(ur => ur.Id)
                      .IsUnique();
            });

            builder.Entity<Event>(buildAction =>
            {
                buildAction.HasKey(e => e.Id);

                buildAction
                    .Property(e => e.Id)
                    .IsRequired(true)
                    .ValueGeneratedOnAdd()
                    .HasValueGenerator<SequentialGuidValueGenerator>();

                buildAction
                    .HasOne(e => e.AcaraCyouUser)
                    .WithMany(e => e.Events)
                    .HasForeignKey(e => e.AcaraCyouUserId)
                    .IsRequired(true);
            });

            builder.Entity<DisbursementDetail>(buildAction =>
            {
                buildAction.HasKey(disbursementDetail => disbursementDetail.Id);

                buildAction
                    .Property(disbursementDetail => disbursementDetail.Id)
                    .IsRequired(true)
                    .ValueGeneratedOnAdd()
                    .HasValueGenerator<SequentialGuidValueGenerator>();

                buildAction
                    .HasOne(e => e.Event)
                    .WithMany(e => e.DisbursementDetails)
                    .HasForeignKey(e => e.EventId)
                    .IsRequired(true);
            });

            builder.Entity<Poster>(buildAction =>
            {
                buildAction.HasKey(poster => poster.Id);

                buildAction
                    .Property(poster => poster.Id)
                    .IsRequired(true)
                    .ValueGeneratedOnAdd()
                    .HasValueGenerator<SequentialGuidValueGenerator>();

                buildAction
                    .HasOne(e => e.Event)
                    .WithMany(e => e.Posters)
                    .HasForeignKey(e => e.EventId)
                    .IsRequired(true);
            });

            builder.Entity<TicketCategory>(buildAction =>
            {
                buildAction.HasKey(ticketCategory => ticketCategory.Id);

                buildAction
                    .Property(ticketCategory => ticketCategory.Id)
                    .IsRequired(true)
                    .ValueGeneratedOnAdd()
                    .HasValueGenerator<SequentialGuidValueGenerator>();

                buildAction
                    .HasOne(e => e.Event)
                    .WithMany(e => e.TicketCategorys)
                    .HasForeignKey(e => e.EventId)
                    .IsRequired(true);
            });

            builder.Entity<Order>(buildAction =>
            {
                buildAction.HasKey(order => order.Id);

                buildAction
                    .Property(order => order.Id)
                    .IsRequired(true)
                    .ValueGeneratedOnAdd()
                    .HasValueGenerator<SequentialGuidValueGenerator>();

                buildAction
                    .HasOne(e => e.AcaraCyouUser)
                    .WithMany(e => e.Orders)
                    .HasForeignKey(e => e.AcaraCyouUserId)
                    .IsRequired(true);

                buildAction
                    .Property(order => order.Status)
                    .HasConversion<string>();
            });

            builder.Entity<Invoice>(buildAction =>
            {
                buildAction.HasKey(invoice => invoice.Id);

                buildAction
                    .Property(invoice => invoice.Id)
                    .IsRequired(true)
                    .ValueGeneratedOnAdd()
                    .HasValueGenerator<SequentialGuidValueGenerator>();

                buildAction
                    .HasOne(e => e.Order)
                    .WithOne(e => e.Invoice)
                    .HasForeignKey<Invoice>(e => e.OrderId)
                    .IsRequired(true);

                buildAction
                    .Property(invoice => invoice.Status)
                    .HasConversion<string>();

                buildAction.HasIndex(ur => ur.InvoiceNumber)
                      .IsUnique();
            });

            builder.Entity<PaymentDetail>(buildAction =>
            {
                buildAction.HasKey(paymentDetail => paymentDetail.Id);

                buildAction
                    .Property(paymentDetail => paymentDetail.Id)
                    .IsRequired(true)
                    .ValueGeneratedOnAdd()
                    .HasValueGenerator<SequentialGuidValueGenerator>();

                buildAction
                    .HasOne(e => e.Invoice)
                    .WithOne(e => e.PaymentDetail)
                    .HasForeignKey<PaymentDetail>(e => e.InvoiceId)
                    .IsRequired(true);

                buildAction
                    .Property(paymentDetail => paymentDetail.Status)
                    .HasConversion<string>();
            });

            builder.Entity<OrderItem>(buildAction =>
            {
                buildAction.HasKey(orderItem => orderItem.Id);

                buildAction
                    .Property(orderItem => orderItem.Id)
                    .IsRequired(true)
                    .ValueGeneratedOnAdd()
                    .HasValueGenerator<SequentialGuidValueGenerator>();

                buildAction
                    .HasOne(e => e.TicketCategory)
                    .WithMany(e => e.OrderItems)
                    .HasForeignKey(e => e.TicketCategoryId)
                    .IsRequired(true);

                buildAction
                    .HasOne(e => e.Order)
                    .WithMany(e => e.OrderItems)
                    .HasForeignKey(e => e.OrderId)
                    .IsRequired(true);
            });

            builder.Entity<BankCode>(buildAction =>
            {
                buildAction.HasKey(p => p.Id);

                buildAction
                    .Property(p => p.Id)
                    .IsRequired(true)
                    .ValueGeneratedOnAdd()
                    .HasValueGenerator<SequentialGuidValueGenerator>();

                buildAction.HasData(
                [
                    new BankCode() { Id = Guid.NewGuid(), Code = "002", Issuer = "Bank BRI", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "008", Issuer = "Bank Mandiri", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "009", Issuer = "Bank BNI", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "011", Issuer = "Bank Danamon", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "013", Issuer = "Bank Permata", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "014", Issuer = "Bank Central Asia", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "016", Issuer = "Bank Maybank Indonesia", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "019", Issuer = "Bank Panin", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "022", Issuer = "CIMB Niaga", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "023", Issuer = "Bank UOB Indonesia", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "028", Issuer = "Bank OCBC NISP", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "031", Issuer = "Citi Bank", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "036", Issuer = "Bank CCB (Ex-Bank Windu Kentjana)", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "037", Issuer = "Bank Artha Graha", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "042", Issuer = "MUFG Bank", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "046", Issuer = "Bank DBS", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "050", Issuer = "Standard Chartered Bank", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "054", Issuer = "Bank Capital", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "061", Issuer = "ANZ Indonesia", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "069", Issuer = "Bank Of China Indonesia", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "076", Issuer = "Bank Bumi Arta", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "087", Issuer = "Bank HSBC Indonesia", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "095", Issuer = "Bank JTrust Indonesia", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "097", Issuer = "Bank Mayapada", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "110", Issuer = "Bank BJB", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "111", Issuer = "Bank DKI", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "112", Issuer = "Bank BPD DIY", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "113", Issuer = "Bank Jateng", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "115", Issuer = "Bank Jambi", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "116", Issuer = "Bank Aceh", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "117", Issuer = "Bank Sumut", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "118", Issuer = "Bank Nagari", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "119", Issuer = "Bank Riau Kepri", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "120", Issuer = "Bank Sumsel Babel", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "121", Issuer = "Bank Lampung", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "122", Issuer = "Bank Kalsel", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "123", Issuer = "Bank Kalbar", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "124", Issuer = "Bank Kaltimtara", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "125", Issuer = "Bank Kalteng", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "126", Issuer = "Bank Sulselbar", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "127", Issuer = "Bank Sulut Go", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "128", Issuer = "Bank NTB Syariah", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "129", Issuer = "Bank BPD Bali", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "131", Issuer = "Bank Maluku Malut", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "132", Issuer = "Bank Papua", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "133", Issuer = "Bank Bengkulu", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "134", Issuer = "Bank Sulteng", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "135", Issuer = "Bank Sultra", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "137", Issuer = "Bank Banten", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "147", Issuer = "Bank Muamalat Indonesia", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "151", Issuer = "Bank Mestika", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "152", Issuer = "Bank Shinhan Indonesia", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "153", Issuer = "Bank Sinarmas", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "157", Issuer = "Bank Maspion Indonesia", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "161", Issuer = "Bank Ganesha", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "167", Issuer = "Bank QNB Indonesia", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "200", Issuer = "Bank BTN", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "212", Issuer = "Bank Woori Saudara", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "213", Issuer = "Bank BTPN", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "425", Issuer = "Bank BJB Syariah", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "426", Issuer = "Bank Mega", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "441", Issuer = "Bank KB Bukopin", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "451", Issuer = "Bank Syariah Indonesia", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "472", Issuer = "Bank Jasa Jakarta", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "484", Issuer = "Bank KEB Hana", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "485", Issuer = "MNC Bank", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "490", Issuer = "Bank Neo Commerce", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "494", Issuer = "Bank BRI Agroniaga", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "498", Issuer = "Bank SBI", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "501", Issuer = "Bank Digital BCA", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "503", Issuer = "Bank Nobu", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "506", Issuer = "Bank Mega Syariah", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "513", Issuer = "Bank Ina Perdana", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "517", Issuer = "Bank Panin Dubai Syariah", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "521", Issuer = "Bank Syariah Bukopin", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "523", Issuer = "Bank Sahabat Sampoerna", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "526", Issuer = "Bank Oke Indonesia", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "531", Issuer = "AMAR BANK", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "535", Issuer = "SEA Bank", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "536", Issuer = "Bank BCA Syariah", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "542", Issuer = "Bank Jago", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "547", Issuer = "Bank BTPN Syariah", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "548", Issuer = "Bank Multiarta Sentosa", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "553", Issuer = "Bank Mayora", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "555", Issuer = "Bank Index Selindo", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "564", Issuer = "Bank Mantap", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "566", Issuer = "Bank Victoria International", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "567", Issuer = "Allo Bank", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "947", Issuer = "Bank Aladin Syariah", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "949", Issuer = "Bank CTBC", IsBifastSupport = true },
                    new BankCode() { Id = Guid.NewGuid(), Code = "950", Issuer = "Bank Commonwealth", IsBifastSupport = true }
                ]);
            });

            builder.Entity<PaymentGateway>(buildAction =>
            {
                buildAction.HasKey(p => p.Id);

                buildAction
                    .Property(p => p.Id)
                    .IsRequired(true)
                    .ValueGeneratedOnAdd()
                    .HasValueGenerator<SequentialGuidValueGenerator>();

                buildAction
                    .Property(pg => pg.Name)
                    .HasConversion<string>();

                buildAction.HasData(new PaymentGateway() { Id = Guid.Parse("10000000-0000-0000-0000-000000000000"), Name = PaymentGatewayName.Duitku });
            });

            builder.Entity<PaymentMethod>(buildAction =>
            {
                buildAction.HasKey(p => p.Id);

                buildAction
                    .Property(p => p.Id)
                    .IsRequired(true)
                    .ValueGeneratedOnAdd()
                    .HasValueGenerator<SequentialGuidValueGenerator>();

                buildAction
                    .HasOne(pm => pm.PaymentGateway)
                    .WithMany(pg => pg.PaymentMethods)
                    .HasForeignKey(pm => pm.PaymentGatewayId)
                    .IsRequired(true);

                buildAction
                    .Property(pm => pm.Type)
                    .HasConversion<string>();

                buildAction
                    .Property(pm => pm.AdminFeeBearer)
                    .HasConversion<string>();

                buildAction.HasData(
                [
                    new PaymentMethod() { Id = Guid.NewGuid(), Type = PaymentMethodType.VirtualAccount, Code = "BC", Name = "BCA Virtual Account", FlatFee = 5000m, PercentageFee = 0m, AdminFeeBearer = PaymentMethodAdminFeeBearer.Customer, PaymentGatewayId = Guid.Parse("10000000-0000-0000-0000-000000000000") },
                    new PaymentMethod() { Id = Guid.NewGuid(), Type = PaymentMethodType.VirtualAccount, Code = "M2", Name = "Mandiri Virtual Account", FlatFee = 3000m, PercentageFee = 0m, AdminFeeBearer = PaymentMethodAdminFeeBearer.Customer, PaymentGatewayId = Guid.Parse("10000000-0000-0000-0000-000000000000") },
                    new PaymentMethod() { Id = Guid.NewGuid(), Type = PaymentMethodType.VirtualAccount, Code = "VA", Name = "Maybank Virtual Account", FlatFee = 3000m, PercentageFee = 0m, AdminFeeBearer = PaymentMethodAdminFeeBearer.Customer, PaymentGatewayId = Guid.Parse("10000000-0000-0000-0000-000000000000") },
                    new PaymentMethod() { Id = Guid.NewGuid(), Type = PaymentMethodType.VirtualAccount, Code = "I1", Name = "BNI Virtual Account", FlatFee = 3000m, PercentageFee = 0m, AdminFeeBearer = PaymentMethodAdminFeeBearer.Customer, PaymentGatewayId = Guid.Parse("10000000-0000-0000-0000-000000000000") },
                    new PaymentMethod() { Id = Guid.NewGuid(), Type = PaymentMethodType.VirtualAccount, Code = "B1", Name = "CIMB Niaga Virtual Account", FlatFee = 3000m, PercentageFee = 0m, AdminFeeBearer = PaymentMethodAdminFeeBearer.Customer, PaymentGatewayId = Guid.Parse("10000000-0000-0000-0000-000000000000") },
                    new PaymentMethod() { Id = Guid.NewGuid(), Type = PaymentMethodType.VirtualAccount, Code = "BT", Name = "Permata Bank Virtual Account", FlatFee = 0m, PercentageFee = 0m, AdminFeeBearer = PaymentMethodAdminFeeBearer.Customer, PaymentGatewayId = Guid.Parse("10000000-0000-0000-0000-000000000000") },
                    new PaymentMethod() { Id = Guid.NewGuid(), Type = PaymentMethodType.VirtualAccount, Code = "A1", Name = "ATM Bersama", FlatFee = 0m, PercentageFee = 0m, AdminFeeBearer = PaymentMethodAdminFeeBearer.Customer, PaymentGatewayId = Guid.Parse("10000000-0000-0000-0000-000000000000") },
                    new PaymentMethod() { Id = Guid.NewGuid(), Type = PaymentMethodType.VirtualAccount, Code = "AG", Name = "Bank Artha Graha", FlatFee = 3000m, PercentageFee = 0m, AdminFeeBearer = PaymentMethodAdminFeeBearer.Customer, PaymentGatewayId = Guid.Parse("10000000-0000-0000-0000-000000000000") },
                    new PaymentMethod() { Id = Guid.NewGuid(), Type = PaymentMethodType.VirtualAccount, Code = "NC", Name = "Bank Neo Commerce / BNC", FlatFee = 3000m, PercentageFee = 0m, AdminFeeBearer = PaymentMethodAdminFeeBearer.Customer, PaymentGatewayId = Guid.Parse("10000000-0000-0000-0000-000000000000") },
                    new PaymentMethod() { Id = Guid.NewGuid(), Type = PaymentMethodType.VirtualAccount, Code = "BR", Name = "BRIVA", FlatFee = 3000m, PercentageFee = 0m, AdminFeeBearer = PaymentMethodAdminFeeBearer.Customer, PaymentGatewayId = Guid.Parse("10000000-0000-0000-0000-000000000000") },
                    new PaymentMethod() { Id = Guid.NewGuid(), Type = PaymentMethodType.VirtualAccount, Code = "S1", Name = "Bank Sahabat Sampoerna", FlatFee = 3000m, PercentageFee = 0m, AdminFeeBearer = PaymentMethodAdminFeeBearer.Customer, PaymentGatewayId = Guid.Parse("10000000-0000-0000-0000-000000000000") },
                    new PaymentMethod() { Id = Guid.NewGuid(), Type = PaymentMethodType.VirtualAccount, Code = "BV", Name = "BSI Virtual Account", FlatFee = 3000m, PercentageFee = 0m, AdminFeeBearer = PaymentMethodAdminFeeBearer.Customer, PaymentGatewayId = Guid.Parse("10000000-0000-0000-0000-000000000000") },
                    new PaymentMethod() { Id = Guid.NewGuid(), Type = PaymentMethodType.Qris, Code = "NQ", Name = "Nobu", FlatFee = 0m, PercentageFee = 0m, AdminFeeBearer = PaymentMethodAdminFeeBearer.Merchant, PaymentGatewayId = Guid.Parse("10000000-0000-0000-0000-000000000000") },
                ]);
            });
        }
    }
}