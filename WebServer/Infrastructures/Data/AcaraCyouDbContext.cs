using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AcaraCyouUser>(buildAction =>
            {
                buildAction.HasKey(acaraCyouUser => acaraCyouUser.Id);

                buildAction
                    .Property(acaraCyouUser => acaraCyouUser.Id)
                    .IsRequired(true)
                    .ValueGeneratedOnAdd();
            });

            builder.Entity<AcaraCyouRole>(buildAction =>
            {
                buildAction.HasKey(acaraCyouRole => acaraCyouRole.Id);

                buildAction
                    .Property(acaraCyouRole => acaraCyouRole.Id)
                    .IsRequired(true)
                    .ValueGeneratedOnAdd();
            });

            builder.Entity<AcaraCyouUserRole>(buildAction =>
            {
                buildAction
                    .Property(acaraCyouUserRole => acaraCyouUserRole.Id)
                    .IsRequired(true)
                    .ValueGeneratedOnAdd();

                buildAction.HasIndex(ur => ur.Id)
                      .IsUnique();
            });

            builder.Entity<AcaraCyouRoleClaim>(buildAction =>
            {
                buildAction.HasKey(acaraCyouRoleClaim => acaraCyouRoleClaim.Id);

                buildAction
                    .Property(acaraCyouRoleClaim => acaraCyouRoleClaim.Id)
                    .IsRequired(true)
                    .ValueGeneratedOnAdd();
            });

            builder.Entity<AcaraCyouUserClaim>(buildAction =>
            {
                buildAction.HasKey(acaraCyouUserClaim => acaraCyouUserClaim.Id);

                buildAction
                    .Property(acaraCyouUserClaim => acaraCyouUserClaim.Id)
                    .IsRequired(true)
                    .ValueGeneratedOnAdd();
            });

            builder.Entity<AcaraCyouUserLogin>(buildAction =>
            {
                buildAction
                    .Property(acaraCyouUserLogin => acaraCyouUserLogin.Id)
                    .IsRequired(true)
                    .ValueGeneratedOnAdd();

                buildAction.HasIndex(ur => ur.Id)
                      .IsUnique();
            });

            builder.Entity<AcaraCyouUserToken>(buildAction =>
            {
                buildAction
                    .Property(acaraCyouUserToken => acaraCyouUserToken.Id)
                    .IsRequired(true)
                    .ValueGeneratedOnAdd();

                buildAction.HasIndex(ur => ur.Id)
                      .IsUnique();
            });

            builder.Entity<Event>(buildAction =>
            {
                buildAction.HasKey(e => e.Id);

                buildAction
                    .Property(e => e.Id)
                    .IsRequired(true)
                    .ValueGeneratedOnAdd();

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
                    .ValueGeneratedOnAdd();

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
                    .ValueGeneratedOnAdd();

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
                    .ValueGeneratedOnAdd();

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
                    .ValueGeneratedOnAdd();

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
                    .ValueGeneratedOnAdd();

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
                    .ValueGeneratedOnAdd();

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
                    .ValueGeneratedOnAdd();

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
        }
    }
}
