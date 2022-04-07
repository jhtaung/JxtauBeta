using Microsoft.EntityFrameworkCore;
using Server.Entities;

namespace Server.Data
{
    public partial class AppealContext : DbContext
    {
        public AppealContext()
        {
        }

        public AppealContext(DbContextOptions<AppealContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Appeal> Appeals { get; set; } = null!;
        public virtual DbSet<AppealClaim> AppealClaims { get; set; } = null!;
        public virtual DbSet<AppealClaimDetail> AppealClaimDetails { get; set; } = null!;
        public virtual DbSet<AppealContact> AppealContacts { get; set; } = null!;
        public virtual DbSet<AppealEvent> AppealEvents { get; set; } = null!;
        public virtual DbSet<AppealLog> AppealLogs { get; set; } = null!;
        public virtual DbSet<AppealMemo> AppealMemos { get; set; } = null!;
        public virtual DbSet<AppealStatusLog> AppealStatusLogs { get; set; } = null!;
        public virtual DbSet<AppealStatusType> AppealStatusTypes { get; set; } = null!;
        public virtual DbSet<AppellantType> AppellantTypes { get; set; } = null!;
        public virtual DbSet<ContactType> ContactTypes { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<EventType> EventTypes { get; set; } = null!;
        public virtual DbSet<MeetingSchedule> MeetingSchedules { get; set; } = null!;
        public virtual DbSet<PlanType> PlanTypes { get; set; } = null!;
        public virtual DbSet<Template> Templates { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appeal>(entity =>
            {
                entity.ToTable("Appeal", "dbo");

                entity.Property(e => e.AppealId)
                    .HasColumnName("AppealID")
                    .HasComment("Unique identifier for the appeal");

                entity.Property(e => e.AdditionalInfo)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasComment("Free form information to help the board with the appeal");

                entity.Property(e => e.Analysis)
                    .HasMaxLength(2048)
                    .IsUnicode(false)
                    .HasComment("HOlds additional analysis about the appeal");

                entity.Property(e => e.AppealInfo)
                    .HasMaxLength(2048)
                    .IsUnicode(false)
                    .HasComment("Holds additional information about the appeal");

                entity.Property(e => e.AppealReceivedDate)
                    .HasColumnType("datetime")
                    .HasComment("Date Appeal was recieved.");

                entity.Property(e => e.Comment)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("DepartmentID")
                    .HasComment("e.g. Employer Contracts, MPI Pension & Health Plans, Health Eligibility. Change name from DeptCode to DepartmentCode. Joins to DepartmentCode.DepartmentCode");

                entity.Property(e => e.ExecSummary)
                    .HasMaxLength(4096)
                    .IsUnicode(false)
                    .HasComment("High level descirption of what the appeal is concerning");

                entity.Property(e => e.ExpirationDate)
                    .HasColumnType("datetime")
                    .HasComment("Date Appeal will expire.");

                entity.Property(e => e.IsPrecedentEstablished).HasDefaultValueSql("((0))");

                entity.Property(e => e.Lock)
                    .HasDefaultValueSql("((0))")
                    .HasComment("Locks/Unlocis the Appeal, no additinoal changes can be added when locked");

                entity.Property(e => e.Mpid)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("MPID")
                    .HasComment("Participant ID - imported from OPUS");

                entity.Property(e => e.PlanReference)
                    .IsUnicode(false)
                    .HasComment("Contains reference material used to research the appeal");

                entity.Property(e => e.PlanTypeId)
                    .HasColumnName("PlanTypeID")
                    .HasComment("Joins to PlanType.PlanTypeID to get PlanType, e.g. Pension Plan, IAP, Active Health Plan. Joins to PlanType");

                entity.Property(e => e.Rap)
                    .HasColumnName("RAP")
                    .HasDefaultValueSql("((0))")
                    .HasComment("Same as VIP");

                entity.Property(e => e.Recommendations)
                    .HasMaxLength(2048)
                    .IsUnicode(false)
                    .HasComment("Recommendations on how to respond to the appeal");

                entity.Property(e => e.Subject)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasComment("This is the subject of the appeal");

                entity.Property(e => e.SupportingDocs)
                    .HasMaxLength(2048)
                    .IsUnicode(false)
                    .HasComment("Contains documents used to support the appeal and the pages that address the issue");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Appeals)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Appeal_Department");

                entity.HasOne(d => d.PlanType)
                    .WithMany(p => p.Appeals)
                    .HasForeignKey(d => d.PlanTypeId)
                    .HasConstraintName("FK_Appeal_PlanType");
            });

            modelBuilder.Entity<AppealClaim>(entity =>
            {
                entity.ToTable("AppealClaim", "dbo");

                entity.HasIndex(e => new { e.AppealId, e.ClaimId }, "IX_AppealClaim_AppealID_ClaimID")
                    .IsUnique();

                entity.Property(e => e.AppealClaimId)
                    .HasColumnName("AppealClaimID")
                    .HasComment("Unique ID for table");

                entity.Property(e => e.AppealId)
                    .HasColumnName("AppealID")
                    .HasComment("Appeal ID Claim is linked to");

                entity.Property(e => e.Billed)
                    .HasColumnType("money")
                    .HasComment("How much the Provider is billing");

                entity.Property(e => e.ClaimId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("ClaimID")
                    .HasComment("Claim ID associated with the appeal");

                entity.Property(e => e.ClaimStatus)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("Status of the claim in QNXT");

                entity.Property(e => e.Coballowed)
                    .HasColumnType("money")
                    .HasColumnName("COBAllowed")
                    .HasComment("Amount of Co ordination of Benefits allowed");

                entity.Property(e => e.Cobpaid)
                    .HasColumnType("money")
                    .HasColumnName("COBPaid")
                    .HasComment("Amount of Coordination of Beefits paid");

                entity.Property(e => e.Comment)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.PlanPaid).HasColumnType("money");

                entity.Property(e => e.SecondaryId)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("SecondaryID");

                entity.Property(e => e.StartDate)
                    .HasColumnType("datetime")
                    .HasComment("Start date of the claim");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");

                entity.HasOne(d => d.Appeal)
                    .WithMany(p => p.AppealClaims)
                    .HasForeignKey(d => d.AppealId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppealClaim_Appeal");
            });

            modelBuilder.Entity<AppealClaimDetail>(entity =>
            {
                entity.HasKey(e => e.AppealClaimDetailsId)
                    .HasName("PK_AppealClaimsDetail");

                entity.ToTable("AppealClaimDetails", "dbo");

                entity.Property(e => e.AppealClaimDetailsId)
                    .HasColumnName("AppealClaimDetailsID")
                    .HasComment("Unique ID for table");

                entity.Property(e => e.AppealClaimId)
                    .HasColumnName("AppealClaimID")
                    .HasComment("Claim ID associated with the Appeal");

                entity.Property(e => e.ClaimLine).HasComment("Claim Line associated with the Claim");

                entity.Property(e => e.CoPay)
                    .HasColumnType("money")
                    .HasComment("Amount of Co-Pay owed");

                entity.Property(e => e.Comment)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.PatientResp)
                    .HasColumnType("money")
                    .HasComment("Amount of patient responsibility");

                entity.Property(e => e.PlanAllowed).HasColumnType("money");

                entity.Property(e => e.PlanBenefit).HasColumnType("money");

                entity.Property(e => e.ProvDiscount).HasColumnType("money");

                entity.Property(e => e.ServiceCode)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Service code of the service provided for the claim");

                entity.Property(e => e.ServiceDescription)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasComment("Description of the service provided");

                entity.Property(e => e.ServiceEndDate).HasColumnType("datetime");

                entity.Property(e => e.ServiceStartDate)
                    .HasColumnType("datetime")
                    .HasComment("Date of service for the claim");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasComment("Status of the claim line");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");

                entity.HasOne(d => d.AppealClaim)
                    .WithMany(p => p.AppealClaimDetails)
                    .HasForeignKey(d => d.AppealClaimId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppealClaimDetails_AppealClaim");
            });

            modelBuilder.Entity<AppealContact>(entity =>
            {
                entity.ToTable("AppealContacts", "dbo");

                entity.Property(e => e.AppealContactId)
                    .HasColumnName("AppealContactID")
                    .HasComment("Unique ID for Appeal Contact");

                entity.Property(e => e.AddressLine1)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasComment("Address Line 1 of appeal contact");

                entity.Property(e => e.AddressLine2)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasComment("Address Line 2 of the appeal contact");

                entity.Property(e => e.AppealId)
                    .HasColumnName("AppealID")
                    .HasComment("Unique ID of the Appeal");

                entity.Property(e => e.AppellantTypeId)
                    .HasColumnName("AppellantTypeID")
                    .HasComment("Type of Appellant Spouse, Child, etc");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("City of the appeal contact");

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Company name for appeal contact");

                entity.Property(e => e.ContactTypeId)
                    .HasColumnName("ContactTypeID")
                    .HasComment("Contact Type ID indicating Provider, Appeallant, etc");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("First Name of the Contact");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Last name of the appeal contact");

                entity.Property(e => e.MiddleName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Middle Name of the appeal contact");

                entity.Property(e => e.ProviderId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ProviderID")
                    .HasComment("Provider ID associated with the appeal");

                entity.Property(e => e.State)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasComment("State code of the appeal contact");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Zip)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasComment("Zip code of the appeal contact");

                entity.HasOne(d => d.Appeal)
                    .WithMany(p => p.AppealContacts)
                    .HasForeignKey(d => d.AppealId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppealContacts_Appeal");

                entity.HasOne(d => d.AppellantType)
                    .WithMany(p => p.AppealContacts)
                    .HasForeignKey(d => d.AppellantTypeId)
                    .HasConstraintName("FK_AppealContacts_AppellantType");

                entity.HasOne(d => d.ContactType)
                    .WithMany(p => p.AppealContacts)
                    .HasForeignKey(d => d.ContactTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppealContacts_ContactType");
            });

            modelBuilder.Entity<AppealEvent>(entity =>
            {
                entity.ToTable("AppealEvent", "dbo");

                entity.Property(e => e.AppealEventId)
                    .HasColumnName("AppealEventID")
                    .HasComment("Unique identifier for Appeal Event");

                entity.Property(e => e.AppealId)
                    .HasColumnName("AppealID")
                    .HasComment("ID of the appeal");

                entity.Property(e => e.Comment)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.EventDate)
                    .HasColumnType("datetime")
                    .HasComment("Date of the event");

                entity.Property(e => e.EventText)
                    .HasMaxLength(4000)
                    .IsUnicode(false)
                    .HasComment("Information about the event");

                entity.Property(e => e.EventTypeId)
                    .HasColumnName("EventTypeID")
                    .HasComment("Type of event the data is for, Timeline, Note, etc");

                entity.Property(e => e.PageNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");

                entity.HasOne(d => d.Appeal)
                    .WithMany(p => p.AppealEvents)
                    .HasForeignKey(d => d.AppealId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppealEvent_Appeal");

                entity.HasOne(d => d.EventType)
                    .WithMany(p => p.AppealEvents)
                    .HasForeignKey(d => d.EventTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppealEvent_EventType");
            });

            modelBuilder.Entity<AppealLog>(entity =>
            {
                entity.ToTable("AppealLog", "dbo");

                entity.Property(e => e.AppealLogId).HasColumnName("AppealLogID");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ProcessName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");
            });

            modelBuilder.Entity<AppealMemo>(entity =>
            {
                entity.ToTable("AppealMemo", "dbo");

                entity.Property(e => e.AppealMemoId).HasColumnName("AppealMemoID");

                entity.Property(e => e.AppealId).HasColumnName("AppealID");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.MemoDate).HasColumnType("datetime");

                entity.Property(e => e.MemoFrom)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.MemoSubject)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.MemoText)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");

                entity.HasOne(d => d.Appeal)
                    .WithMany(p => p.AppealMemos)
                    .HasForeignKey(d => d.AppealId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppealMemo_Appeal");
            });

            modelBuilder.Entity<AppealStatusLog>(entity =>
            {
                entity.ToTable("AppealStatusLog", "dbo");

                entity.Property(e => e.AppealStatusLogId)
                    .HasColumnName("AppealStatusLogID")
                    .HasComment("Unique Identifier for the appeal status log");

                entity.Property(e => e.AppealId)
                    .HasColumnName("AppealID")
                    .HasComment("Identifier of the appeal");

                entity.Property(e => e.AppealStatusTypeId)
                    .HasColumnName("AppealStatusTypeID")
                    .HasComment("Current status of the appeal");

                entity.Property(e => e.Comment)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.MeetingScheduleId)
                    .HasColumnName("MeetingScheduleID")
                    .HasComment("Unique identifier for the meeting the appeal will be heard");

                entity.Property(e => e.Notes)
                    .HasMaxLength(2000)
                    .IsUnicode(false)
                    .HasComment("Notes about the status");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");

                entity.HasOne(d => d.Appeal)
                    .WithMany(p => p.AppealStatusLogs)
                    .HasForeignKey(d => d.AppealId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppealStatusLog_Appeals");

                entity.HasOne(d => d.AppealStatusType)
                    .WithMany(p => p.AppealStatusLogs)
                    .HasForeignKey(d => d.AppealStatusTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppealStatusLog_AppealStatusType");

                entity.HasOne(d => d.MeetingSchedule)
                    .WithMany(p => p.AppealStatusLogs)
                    .HasForeignKey(d => d.MeetingScheduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AppealStatus_MeetingSchedule");
            });

            modelBuilder.Entity<AppealStatusType>(entity =>
            {
                entity.ToTable("AppealStatusType", "dbo");

                entity.Property(e => e.AppealStatusTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("AppealStatusTypeID")
                    .HasComment("Unique identifier of the appeal status type");

                entity.Property(e => e.AppealStatusTypeDescription)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasComment("Descirption of the status e.g. Pending, Resolved, Tabled");

                entity.Property(e => e.Comment)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.IsActive).HasComment("Active/Inactive indicator of the status type");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");
            });

            modelBuilder.Entity<AppellantType>(entity =>
            {
                entity.ToTable("AppellantType", "dbo");

                entity.Property(e => e.AppellantTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("AppellantTypeID")
                    .HasComment("Unique identifier for the appellant type");

                entity.Property(e => e.AppellantTypeName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Appellant type name");

                entity.Property(e => e.Comment)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");
            });

            modelBuilder.Entity<ContactType>(entity =>
            {
                entity.ToTable("ContactType", "dbo");

                entity.Property(e => e.ContactTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("ContactTypeID")
                    .HasComment("Unique identifier for contact type");

                entity.Property(e => e.Comment)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ContactTypeName)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasComment("Contact type name: appellant, attorney, etc");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department", "dbo");

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("DepartmentID")
                    .HasComment("Unique Identifier for the Department");

                entity.Property(e => e.Comment)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.DepartmentCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength()
                    .HasComment("Unique Department Code, e.g. AUD, BRD, CLM");

                entity.Property(e => e.DepartmentName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Unique Department Name");

                entity.Property(e => e.PresenterName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Name of Department presenter");

                entity.Property(e => e.PresenterTitle)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Title of department presenter");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");
            });

            modelBuilder.Entity<EventType>(entity =>
            {
                entity.ToTable("EventType", "dbo");

                entity.Property(e => e.EventTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("EventTypeID")
                    .HasComment("Unique identifier for event type");

                entity.Property(e => e.Comment)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.EventTypeDescription)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Event type description: Note, Timeline, etc");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");
            });

            modelBuilder.Entity<MeetingSchedule>(entity =>
            {
                entity.ToTable("MeetingSchedule", "dbo");

                entity.Property(e => e.MeetingScheduleId)
                    .HasColumnName("MeetingScheduleID")
                    .HasComment("Unique identifier for meeting schedule");

                entity.Property(e => e.Comment)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.CutOffDate)
                    .HasColumnType("datetime")
                    .HasComment("Date the appeal memo is needed ");

                entity.Property(e => e.MeetingDate)
                    .HasColumnType("datetime")
                    .HasComment("Meeting Date");

                entity.Property(e => e.MeetingTime)
                    .HasColumnType("datetime")
                    .HasComment("meeting time");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");
            });

            modelBuilder.Entity<PlanType>(entity =>
            {
                entity.ToTable("PlanType", "dbo");

                entity.Property(e => e.PlanTypeId)
                    .ValueGeneratedNever()
                    .HasColumnName("PlanTypeID")
                    .HasComment("Unique Identifier for the Plan");

                entity.Property(e => e.Comment)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())")
                    .HasComment("");

                entity.Property(e => e.PlanTypeName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasComment("Plan type as it relates to Appeals, e.g. Active Health Plan, Retiree Health Plan, Pension and IAP Plans");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");
            });

            modelBuilder.Entity<Template>(entity =>
            {
                entity.ToTable("Template", "dbo");

                entity.Property(e => e.TemplateId)
                    .HasColumnName("TemplateID")
                    .HasComment("Unique identifier for template");

                entity.Property(e => e.AppealInfoTemplate)
                    .HasMaxLength(1000)
                    .HasComment("General information about the appeal");

                entity.Property(e => e.Comment)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");

                entity.Property(e => e.DepartmentId)
                    .HasColumnName("DepartmentID")
                    .HasComment("Dept ID the template is for");

                entity.Property(e => e.ExecSummaryTemplate)
                    .HasMaxLength(4096)
                    .IsUnicode(false);

                entity.Property(e => e.PlanReferenceTemplate).HasComment("References parts of the plan documents the appeal pertains to");

                entity.Property(e => e.RecommendationsTemplate)
                    .HasMaxLength(2048)
                    .IsUnicode(false);

                entity.Property(e => e.SubjectTemplate)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasComment("Subject the template addresses");

                entity.Property(e => e.UpdateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(suser_sname())");

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.Templates)
                    .HasForeignKey(d => d.DepartmentId)
                    .HasConstraintName("FK_Template_Department");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
