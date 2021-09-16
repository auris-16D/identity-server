using System;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Api.Models
{
    public partial class BudgetContext : DbContext
    {
        public BudgetContext()
        {
        }

        public BudgetContext(DbContextOptions<BudgetContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Budget> Budgets { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<ContactType> ContactTypes { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupCategory> GroupCategories { get; set; }
        public virtual DbSet<PrincipleResourcePolicy> PrincipleResourcePolicies { get; set; }
        public virtual DbSet<Reconciled> Reconcileds { get; set; }
        public virtual DbSet<ResourcePolicy> ResourcePolicies { get; set; }
        public virtual DbSet<ResourceUser> ResourceUsers { get; set; }
        public virtual DbSet<TransactionHeader> TransactionHeaders { get; set; }
        public virtual DbSet<TransactionItem> TransactionItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var dataConnectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING");
                optionsBuilder.UseMySql(dataConnectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("5.7.35-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("latin1")
                .UseCollation("latin1_swedish_ci");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("accounts");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.BudgetId, "index_accounts_on_budget_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.AccountType)
                    .HasMaxLength(255)
                    .HasColumnName("account_type");

                entity.Property(e => e.BudgetId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("budget_id");

                entity.Property(e => e.CreatedAt)
                    .HasMaxLength(6)
                    .HasColumnName("created_at");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.UpdatedAt)
                    .HasMaxLength(6)
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Budget)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.BudgetId)
                    .HasConstraintName("fk_rails_c964342343");
            });

            modelBuilder.Entity<Budget>(entity =>
            {
                entity.ToTable("budgets");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.Property(e => e.BudgetId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("budget_id");

                entity.Property(e => e.CreatedAt)
                    .HasMaxLength(6)
                    .HasColumnName("created_at");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.UpdatedAt)
                    .HasMaxLength(6)
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("categories");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.BudgetId, "index_categories_on_budget_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.BudgetId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("budget_id");

                entity.Property(e => e.CreatedAt)
                    .HasMaxLength(6)
                    .HasColumnName("created_at");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.UpdatedAt)
                    .HasMaxLength(6)
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Budget)
                    .WithMany(p => p.Categories)
                    .HasForeignKey(d => d.BudgetId)
                    .HasConstraintName("fk_rails_c969876663");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("contacts");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.ContactTypeId, "index_contact_type_on_budget_id");

                entity.HasIndex(e => e.BudgetId, "index_contacts_on_budget_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.BudgetId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("budget_id");

                entity.Property(e => e.ContactTypeId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("contact_type_id");

                entity.Property(e => e.CreatedAt)
                    .HasMaxLength(6)
                    .HasColumnName("created_at");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.Location)
                    .HasMaxLength(255)
                    .HasColumnName("location");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.UpdatedAt)
                    .HasMaxLength(6)
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Budget)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.BudgetId)
                    .HasConstraintName("fk_rails_c432987659");

                entity.HasOne(d => d.ContactType)
                    .WithMany(p => p.Contacts)
                    .HasForeignKey(d => d.ContactTypeId)
                    .HasConstraintName("fk_rails_c432987563");
            });

            modelBuilder.Entity<ContactType>(entity =>
            {
                entity.ToTable("contact_types");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.BudgetId, "index_contact_types_on_budget_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.BudgetId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("budget_id");

                entity.Property(e => e.CreatedAt)
                    .HasMaxLength(6)
                    .HasColumnName("created_at");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.UpdatedAt)
                    .HasMaxLength(6)
                    .HasColumnName("updated_at");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.ToTable("groups");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.AccountId, "index_groups_on_account_id");

                entity.HasIndex(e => e.BudgetId, "index_groups_on_budget_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.AccountId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("account_id");

                entity.Property(e => e.BudgetId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("budget_id");

                entity.Property(e => e.CreatedAt)
                    .HasMaxLength(6)
                    .HasColumnName("created_at");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.UpdatedAt)
                    .HasMaxLength(6)
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("fk_rails_ed4ff9a299");

                entity.HasOne(d => d.Budget)
                    .WithMany(p => p.Groups)
                    .HasForeignKey(d => d.BudgetId)
                    .HasConstraintName("fk_rails_c967894322");
            });

            modelBuilder.Entity<GroupCategory>(entity =>
            {
                entity.ToTable("group_categories");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.BudgetId, "index_group_categories_on_budget_id");

                entity.HasIndex(e => e.CategoryId, "index_group_categories_on_category_id");

                entity.HasIndex(e => e.GroupId, "index_group_categories_on_group_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.BudgetId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("budget_id");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("category_id");

                entity.Property(e => e.CreatedAt)
                    .HasMaxLength(6)
                    .HasColumnName("created_at");

                entity.Property(e => e.GroupId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("group_id");

                entity.Property(e => e.UpdatedAt)
                    .HasMaxLength(6)
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Budget)
                    .WithMany(p => p.GroupCategories)
                    .HasForeignKey(d => d.BudgetId)
                    .HasConstraintName("fk_rails_c923987623");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.GroupCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_rails_9d65a21a8c");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupCategories)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_rails_ef10bdfffb");
            });

            modelBuilder.Entity<PrincipleResourcePolicy>(entity =>
            {
                entity.ToTable("principle_resource_policies");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.BudgetId, "index_principle_resource_policies_on_budget_id");

                entity.HasIndex(e => e.PrincipleGuid, "index_principle_resource_policies_on_principle_guid");

                entity.HasIndex(e => e.ResourcePolicyId, "index_principle_resource_policies_on_resource_policy_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.BudgetId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("budget_id");

                entity.Property(e => e.CreatedAt)
                    .HasMaxLength(6)
                    .HasColumnName("created_at");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.PrincipleGuid)
                    .IsRequired()
                    .HasMaxLength(36)
                    .HasColumnName("principle_guid");

                entity.Property(e => e.ResourcePolicyId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("resource_policy_id");

                entity.Property(e => e.UpdatedAt)
                    .HasMaxLength(6)
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Budget)
                    .WithMany(p => p.PrincipleResourcePolicies)
                    .HasForeignKey(d => d.BudgetId)
                    .HasConstraintName("fk_rails_c789988888");

                entity.HasOne(d => d.ResourcePolicy)
                    .WithMany(p => p.PrincipleResourcePolicies)
                    .HasForeignKey(d => d.ResourcePolicyId)
                    .HasConstraintName("fk_rails_c789989897");
            });

            modelBuilder.Entity<Reconciled>(entity =>
            {
                entity.ToTable("reconciled");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.BudgetId, "index_reconciled_on_budget_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.BudgetId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("budget_id");

                entity.Property(e => e.CreatedAt)
                    .HasMaxLength(6)
                    .HasColumnName("created_at");

                entity.Property(e => e.PrincipleGuid)
                    .IsRequired()
                    .HasMaxLength(36)
                    .HasColumnName("principle_guid");

                entity.Property(e => e.ReconciledBalance).HasColumnName("reconciled_balance");

                entity.Property(e => e.ReconciledDate)
                    .HasMaxLength(6)
                    .HasColumnName("reconciled_date");

                entity.Property(e => e.UpdatedAt)
                    .HasMaxLength(6)
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Budget)
                    .WithMany(p => p.Reconcileds)
                    .HasForeignKey(d => d.BudgetId)
                    .HasConstraintName("fk_rails_c456789098");
            });

            modelBuilder.Entity<ResourcePolicy>(entity =>
            {
                entity.ToTable("resource_policies");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.BudgetId, "index_resource_policies_on_budget_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.BudgetId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("budget_id");

                entity.Property(e => e.CreatedAt)
                    .HasMaxLength(6)
                    .HasColumnName("created_at");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.ResourceAction)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("resource_action");

                entity.Property(e => e.ResourceName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("resource_name");

                entity.Property(e => e.UpdatedAt)
                    .HasMaxLength(6)
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Budget)
                    .WithMany(p => p.ResourcePolicies)
                    .HasForeignKey(d => d.BudgetId)
                    .HasConstraintName("fk_rails_c789987790");
            });

            modelBuilder.Entity<ResourceUser>(entity =>
            {
                entity.ToTable("resource_users");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.BudgetId, "index_resource_users_on_budget_id");

                entity.HasIndex(e => e.PrincipleGuid, "index_resource_users_on_principle_guid");

                entity.HasIndex(e => e.ResourceId, "index_resource_users_on_resource_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.BudgetId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("budget_id");

                entity.Property(e => e.CreatedAt)
                    .HasMaxLength(6)
                    .HasColumnName("created_at");

                entity.Property(e => e.PrincipleGuid)
                    .IsRequired()
                    .HasMaxLength(36)
                    .HasColumnName("principle_guid");

                entity.Property(e => e.ResourceId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("resource_id");

                entity.Property(e => e.ResourceType)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("resource_type");

                entity.Property(e => e.UpdatedAt)
                    .HasMaxLength(6)
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Budget)
                    .WithMany(p => p.ResourceUsers)
                    .HasForeignKey(d => d.BudgetId)
                    .HasConstraintName("fk_rails_c964474634");
            });

            modelBuilder.Entity<TransactionHeader>(entity =>
            {
                entity.ToTable("transaction_headers");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.ReconciledId, "index_reconciled_on_budget_id");

                entity.HasIndex(e => e.AccountId, "index_transaction_headers_on_account_id");

                entity.HasIndex(e => e.BudgetId, "index_transaction_headers_on_budget_id");

                entity.HasIndex(e => e.ContactId, "index_transaction_headers_on_contact_id");

                entity.HasIndex(e => e.PrincipleGuid, "index_transaction_headers_on_principle_guid");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.AccountId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("account_id");

                entity.Property(e => e.BudgetId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("budget_id");

                entity.Property(e => e.ContactId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("contact_id");

                entity.Property(e => e.CreatedAt)
                    .HasMaxLength(6)
                    .HasColumnName("created_at");

                entity.Property(e => e.PrincipleGuid)
                    .IsRequired()
                    .HasMaxLength(36)
                    .HasColumnName("principle_guid");

                entity.Property(e => e.Reconciled)
                    .HasColumnType("date")
                    .HasColumnName("reconciled");

                entity.Property(e => e.ReconciledId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("reconciled_id");

                entity.Property(e => e.Sign)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("sign");

                entity.Property(e => e.Total).HasColumnName("total");

                entity.Property(e => e.TransactionDate)
                    .HasColumnType("date")
                    .HasColumnName("transaction_date");

                entity.Property(e => e.UpdatedAt)
                    .HasMaxLength(6)
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.TransactionHeaders)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("fk_rails_4898dd5bf8");

                entity.HasOne(d => d.Budget)
                    .WithMany(p => p.TransactionHeaders)
                    .HasForeignKey(d => d.BudgetId)
                    .HasConstraintName("fk_rails_c898765239");

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.TransactionHeaders)
                    .HasForeignKey(d => d.ContactId)
                    .HasConstraintName("fk_rails_263cfb3632");

                entity.HasOne(d => d.ReconciledNavigation)
                    .WithMany(p => p.TransactionHeaders)
                    .HasForeignKey(d => d.ReconciledId)
                    .HasConstraintName("fk_rails_c898765240");
            });

            modelBuilder.Entity<TransactionItem>(entity =>
            {
                entity.ToTable("transaction_items");

                entity.HasCharSet("utf8")
                    .UseCollation("utf8_general_ci");

                entity.HasIndex(e => e.BudgetId, "index_transaction_items_on_budget_id");

                entity.HasIndex(e => e.CategoryId, "index_transaction_items_on_category_id");

                entity.HasIndex(e => e.TransactionHeaderId, "index_transaction_items_on_transaction_header_id");

                entity.Property(e => e.Id)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("id");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.BudgetId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("budget_id");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("category_id");

                entity.Property(e => e.CreatedAt)
                    .HasMaxLength(6)
                    .HasColumnName("created_at");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.TransactionHeaderId)
                    .HasColumnType("bigint(20)")
                    .HasColumnName("transaction_header_id");

                entity.Property(e => e.UpdatedAt)
                    .HasMaxLength(6)
                    .HasColumnName("updated_at");

                entity.HasOne(d => d.Budget)
                    .WithMany(p => p.TransactionItems)
                    .HasForeignKey(d => d.BudgetId)
                    .HasConstraintName("fk_rails_c789987789");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.TransactionItems)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("fk_rails_73b0301634");

                entity.HasOne(d => d.TransactionHeader)
                    .WithMany(p => p.TransactionItems)
                    .HasForeignKey(d => d.TransactionHeaderId)
                    .HasConstraintName("fk_rails_6f7f94eb29");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
