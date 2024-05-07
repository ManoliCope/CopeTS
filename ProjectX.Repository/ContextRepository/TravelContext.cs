using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using ProjectX.Entities.AppSettings;

namespace ProjectX.Repository.ContextRepository
{
    public partial class TravelContext : DbContext
    {
        private readonly TrAppSettings _appSettings;

        public TravelContext(IOptions<TrAppSettings> appIdentitySettingsAccessor)
        {
            _appSettings = appIdentitySettingsAccessor.Value;
        }

        public TravelContext(DbContextOptions<TravelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DemoQuote> DemoQuotes { get; set; } = null!;
        public virtual DbSet<InsuredQuotation> InsuredQuotations { get; set; } = null!;
        public virtual DbSet<TrAppConfig> TrAppConfigs { get; set; } = null!;
        public virtual DbSet<TrAttachement> TrAttachements { get; set; } = null!;
        public virtual DbSet<TrBeneficiary> TrBeneficiaries { get; set; } = null!;
        public virtual DbSet<TrBenefit> TrBenefits { get; set; } = null!;
        public virtual DbSet<TrBenefitTitle> TrBenefitTitles { get; set; } = null!;
        public virtual DbSet<TrContact> TrContacts { get; set; } = null!;
        public virtual DbSet<TrCountry> TrCountries { get; set; } = null!;
        public virtual DbSet<TrCountryIso> TrCountryIsos { get; set; } = null!;
        public virtual DbSet<TrCurrency> TrCurrencies { get; set; } = null!;
        public virtual DbSet<TrCurrency1> TrCurrency1s { get; set; } = null!;
        public virtual DbSet<TrCurrencyRate> TrCurrencyRates { get; set; } = null!;
        public virtual DbSet<TrDestination> TrDestinations { get; set; } = null!;
        public virtual DbSet<TrDestionations1> TrDestionations1s { get; set; } = null!;
        public virtual DbSet<TrDiagnosis> TrDiagnoses { get; set; } = null!;
        public virtual DbSet<TrEmailLog> TrEmailLogs { get; set; } = null!;
        public virtual DbSet<TrEmailTemplate> TrEmailTemplates { get; set; } = null!;
        public virtual DbSet<TrErrorLog> TrErrorLogs { get; set; } = null!;
        public virtual DbSet<TrFeedbackConfig> TrFeedbackConfigs { get; set; } = null!;
        public virtual DbSet<TrFileDirectory> TrFileDirectories { get; set; } = null!;
        public virtual DbSet<TrGroup> TrGroups { get; set; } = null!;
        public virtual DbSet<TrGroupPage> TrGroupPages { get; set; } = null!;
        public virtual DbSet<TrGroupUser> TrGroupUsers { get; set; } = null!;
        public virtual DbSet<TrLine> TrLines { get; set; } = null!;
        public virtual DbSet<TrLog> TrLogs { get; set; } = null!;
        public virtual DbSet<TrLookup> TrLookups { get; set; } = null!;
        public virtual DbSet<TrNotification> TrNotifications { get; set; } = null!;
        public virtual DbSet<TrNotificationsLog> TrNotificationsLogs { get; set; } = null!;
        public virtual DbSet<TrPackage> TrPackages { get; set; } = null!;
        public virtual DbSet<TrPage> TrPages { get; set; } = null!;
        public virtual DbSet<TrPageBackup> TrPageBackups { get; set; } = null!;
        public virtual DbSet<TrPlan> TrPlans { get; set; } = null!;
        public virtual DbSet<TrPolicyAdditionalBenefit> TrPolicyAdditionalBenefits { get; set; } = null!;
        public virtual DbSet<TrPolicyDestination> TrPolicyDestinations { get; set; } = null!;
        public virtual DbSet<TrPolicyDetail> TrPolicyDetails { get; set; } = null!;
        public virtual DbSet<TrPolicyHeader> TrPolicyHeaders { get; set; } = null!;
        public virtual DbSet<TrPolicyInfo> TrPolicyInfos { get; set; } = null!;
        public virtual DbSet<TrPrepaidAccount> TrPrepaidAccounts { get; set; } = null!;
        public virtual DbSet<TrPrepaidAccountsTransaction> TrPrepaidAccountsTransactions { get; set; } = null!;
        public virtual DbSet<TrProduct> TrProducts { get; set; } = null!;
        public virtual DbSet<TrProductionBatch> TrProductionBatches { get; set; } = null!;
        public virtual DbSet<TrProductionBatchDetail> TrProductionBatchDetails { get; set; } = null!;
        public virtual DbSet<TrProfile> TrProfiles { get; set; } = null!;
        public virtual DbSet<TrProfileService> TrProfileServices { get; set; } = null!;
        public virtual DbSet<TrRegisterEmailTo> TrRegisterEmailTos { get; set; } = null!;
        public virtual DbSet<TrRequiredDocument> TrRequiredDocuments { get; set; } = null!;
        public virtual DbSet<TrTariff> TrTariffs { get; set; } = null!;
        public virtual DbSet<TrTextReplacement> TrTextReplacements { get; set; } = null!;
        public virtual DbSet<TrUser> TrUsers { get; set; } = null!;
        public virtual DbSet<TrUsersProduct> TrUsersProducts { get; set; } = null!;
        public virtual DbSet<TrUserstest> TrUserstests { get; set; } = null!;
        public virtual DbSet<TrZone> TrZones { get; set; } = null!;
        public virtual DbSet<TrZoneDestination> TrZoneDestinations { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer( _appSettings.connectionStrings.ccContext );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<DemoQuote>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Demo_quote");
            });

            modelBuilder.Entity<InsuredQuotation>(entity =>
            {
                entity.ToTable("InsuredQuotation");
            });

            modelBuilder.Entity<TrAppConfig>(entity =>
            {
                entity.HasKey(e => e.AcId);

                entity.ToTable("TR_AppConfig");

                entity.Property(e => e.AcId).HasColumnName("AC_ID");

                entity.Property(e => e.AcCode)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("AC_Code");

                entity.Property(e => e.AcIsActive).HasColumnName("AC_IsActive");

                entity.Property(e => e.AcName)
                    .HasMaxLength(100)
                    .HasColumnName("AC_Name");

                entity.Property(e => e.AcValue)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("AC_Value");
            });

            modelBuilder.Entity<TrAttachement>(entity =>
            {
                entity.HasKey(e => e.AtId)
                    .HasName("PK_tr_Attachement");

                entity.ToTable("TR_Attachement");

                entity.Property(e => e.AtId).HasColumnName("AT_ID");

                entity.Property(e => e.AtCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("AT_Created")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.AtCreatedBy).HasColumnName("AT_CreatedBy");

                entity.Property(e => e.AtDescription)
                    .HasMaxLength(500)
                    .HasColumnName("AT_Description");

                entity.Property(e => e.AtExtension)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("AT_Extension");

                entity.Property(e => e.AtFileName)
                    .HasMaxLength(500)
                    .HasColumnName("AT_FileName");

                entity.Property(e => e.AtIdDocumentType).HasColumnName("AT_IdDocumentType");

                entity.Property(e => e.AtIdFileType).HasColumnName("AT_IdFileType");

                entity.Property(e => e.AtIdReference).HasColumnName("AT_IdReference");

                entity.Property(e => e.AtIdReferenceObject).HasColumnName("AT_IdReferenceObject");

                entity.Property(e => e.AtIsDeleted).HasColumnName("AT_IsDeleted");

                entity.Property(e => e.AtNameMd5)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("AT_NameMd5");

                entity.Property(e => e.FdId).HasColumnName("FD_ID");
            });

            modelBuilder.Entity<TrBeneficiary>(entity =>
            {
                entity.HasKey(e => e.BeId);

                entity.ToTable("TR_Beneficiary");

                entity.Property(e => e.BeId).HasColumnName("BE_Id");

                entity.Property(e => e.BeCountryResidenceid).HasColumnName("BE_CountryResidenceid");

                entity.Property(e => e.BeCreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("BE_CreationDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.BeDob)
                    .HasColumnType("datetime")
                    .HasColumnName("BE_DOB");

                entity.Property(e => e.BeFirstName)
                    .HasMaxLength(100)
                    .HasColumnName("BE_FirstName");

                entity.Property(e => e.BeLastName)
                    .HasMaxLength(100)
                    .HasColumnName("BE_LastName");

                entity.Property(e => e.BeMaidenName)
                    .HasMaxLength(100)
                    .HasColumnName("BE_MaidenName");

                entity.Property(e => e.BeMiddleName)
                    .HasMaxLength(100)
                    .HasColumnName("BE_MiddleName");

                entity.Property(e => e.BeNationalityid).HasColumnName("BE_Nationalityid");

                entity.Property(e => e.BePassportNumber)
                    .HasMaxLength(100)
                    .HasColumnName("BE_PassportNumber");

                entity.Property(e => e.BeSex).HasColumnName("BE_Sex");

                entity.Property(e => e.UId)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("U_Id");
            });

            modelBuilder.Entity<TrBenefit>(entity =>
            {
                entity.HasKey(e => e.BId)
                    .HasName("PK_tr_BenefitsTravel");

                entity.ToTable("TR_Benefit");

                entity.HasIndex(e => e.BtId, "Benefit_Title");

                entity.Property(e => e.BId).HasColumnName("B_ID");

                entity.Property(e => e.BAdditionalBenefits).HasColumnName("B_Additional_Benefits");

                entity.Property(e => e.BAdditionalBenefitsFormat).HasColumnName("B_Additional_Benefits_Format");

                entity.Property(e => e.BCreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("B_Creation_Date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.BIsPlus).HasColumnName("B_Is_Plus");

                entity.Property(e => e.BLimit)
                    .HasMaxLength(200)
                    .HasColumnName("B_Limit");

                entity.Property(e => e.BTitle)
                    .HasMaxLength(200)
                    .HasColumnName("B_Title");

                entity.Property(e => e.BtId).HasColumnName("BT_Id");

                entity.Property(e => e.PId).HasColumnName("P_Id");

                entity.HasOne(d => d.Bt)
                    .WithMany(p => p.TrBenefits)
                    .HasForeignKey(d => d.BtId)
                    .HasConstraintName("FK_TR_Benefit_TR_BenefitTitle");

                entity.HasOne(d => d.PIdNavigation)
                    .WithMany(p => p.TrBenefits)
                    .HasForeignKey(d => d.PId)
                    .HasConstraintName("FK_TR_Benefit_TR_Package");
            });

            modelBuilder.Entity<TrBenefitTitle>(entity =>
            {
                entity.HasKey(e => e.BtId);

                entity.ToTable("TR_BenefitTitle");

                entity.HasIndex(e => e.BtTitle, "unique_Benefit_Title")
                    .IsUnique();

                entity.Property(e => e.BtId).HasColumnName("BT_Id");

                entity.Property(e => e.BtTitle)
                    .HasMaxLength(200)
                    .HasColumnName("BT_Title");
            });

            modelBuilder.Entity<TrContact>(entity =>
            {
                entity.HasKey(e => e.CtId)
                    .HasName("PK_tr_Contacts");

                entity.ToTable("TR_Contact");

                entity.Property(e => e.CtId).HasColumnName("CT_ID");

                entity.Property(e => e.CtDialCode)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CT_DialCode");

                entity.Property(e => e.CtEmail)
                    .HasMaxLength(50)
                    .HasColumnName("CT_Email");

                entity.Property(e => e.CtExtension)
                    .HasMaxLength(50)
                    .HasColumnName("CT_Extension");

                entity.Property(e => e.CtIdCountry).HasColumnName("CT_IdCountry");

                entity.Property(e => e.CtIdDepartment).HasColumnName("CT_IdDepartment");

                entity.Property(e => e.CtName)
                    .HasMaxLength(250)
                    .HasColumnName("CT_Name");

                entity.Property(e => e.CtPhone)
                    .HasMaxLength(50)
                    .HasColumnName("CT_Phone");

                entity.Property(e => e.PrId).HasColumnName("PR_ID");
            });

            modelBuilder.Entity<TrCountry>(entity =>
            {
                entity.HasKey(e => e.CtId)
                    .HasName("PK_tr_Country");

                entity.ToTable("TR_Country");

                entity.Property(e => e.CtId).HasColumnName("CT_ID");

                entity.Property(e => e.CtCode2)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CT_Code_2");

                entity.Property(e => e.CtCode3)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CT_Code_3");

                entity.Property(e => e.CtContinent)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CT_Continent")
                    .IsFixedLength();

                entity.Property(e => e.CtFifa)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CT_Fifa");

                entity.Property(e => e.CtIntDialCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CT_IntDialCode");

                entity.Property(e => e.CtIsActive)
                    .IsRequired()
                    .HasColumnName("CT_IsActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CtM49)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CT_M49");

                entity.Property(e => e.CtName)
                    .HasMaxLength(500)
                    .HasColumnName("CT_Name");
            });

            modelBuilder.Entity<TrCountryIso>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TR_Country_ISO");

                entity.Property(e => e.Capital).HasMaxLength(255);

                entity.Property(e => e.Continent).HasMaxLength(255);

                entity.Property(e => e.CountryName)
                    .HasMaxLength(255)
                    .HasColumnName("Country_Name");

                entity.Property(e => e.Ds)
                    .HasMaxLength(255)
                    .HasColumnName("DS");

                entity.Property(e => e.Edgar)
                    .HasMaxLength(255)
                    .HasColumnName("EDGAR");

                entity.Property(e => e.Fifa)
                    .HasMaxLength(255)
                    .HasColumnName("FIFA");

                entity.Property(e => e.Fips)
                    .HasMaxLength(255)
                    .HasColumnName("FIPS");

                entity.Property(e => e.Gaul).HasColumnName("GAUL");

                entity.Property(e => e.GeoNameId).HasColumnName("Geo_Name_ID");

                entity.Property(e => e.Ioc)
                    .HasMaxLength(255)
                    .HasColumnName("IOC");

                entity.Property(e => e.IsIndependent)
                    .HasMaxLength(255)
                    .HasColumnName("Is_Independent");

                entity.Property(e => e.Iso31661Alpha2)
                    .HasMaxLength(255)
                    .HasColumnName("ISO3166_1_Alpha_2");

                entity.Property(e => e.Iso31661Alpha3)
                    .HasMaxLength(255)
                    .HasColumnName("ISO3166_1_Alpha_3");

                entity.Property(e => e.Iso4217CurrencyAlphabeticCode)
                    .HasMaxLength(255)
                    .HasColumnName("ISO4217_Currency_Alphabetic_Code");

                entity.Property(e => e.Iso4217CurrencyCountryName)
                    .HasMaxLength(255)
                    .HasColumnName("ISO4217_Currency_Country_Name");

                entity.Property(e => e.Iso4217CurrencyMinorUnit).HasColumnName("ISO4217_Currency_Minor_Unit");

                entity.Property(e => e.Iso4217CurrencyName)
                    .HasMaxLength(255)
                    .HasColumnName("ISO4217_Currency_Name");

                entity.Property(e => e.Iso4217CurrencyNumericCode).HasColumnName("ISO4217_Currency_Numeric_Code");

                entity.Property(e => e.Itu)
                    .HasMaxLength(255)
                    .HasColumnName("ITU");

                entity.Property(e => e.Languages).HasMaxLength(255);

                entity.Property(e => e.Marc)
                    .HasMaxLength(255)
                    .HasColumnName("MARC");

                entity.Property(e => e.OfficialNameEnglish)
                    .HasMaxLength(255)
                    .HasColumnName("Official_Name_English");

                entity.Property(e => e.Tld)
                    .HasMaxLength(255)
                    .HasColumnName("TLD");

                entity.Property(e => e.Wmo)
                    .HasMaxLength(255)
                    .HasColumnName("WMO");
            });

            modelBuilder.Entity<TrCurrency>(entity =>
            {
                entity.HasKey(e => e.CuId)
                    .HasName("PK_tr_Currency");

                entity.ToTable("TR_Currency");

                entity.Property(e => e.CuId)
                    .ValueGeneratedNever()
                    .HasColumnName("CU_ID");

                entity.Property(e => e.CuCode)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CU_Code");

                entity.Property(e => e.CuFractionNo).HasColumnName("CU_FractionNo");

                entity.Property(e => e.CuIsActive)
                    .IsRequired()
                    .HasColumnName("CU_IsActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CuName)
                    .HasMaxLength(50)
                    .HasColumnName("CU_Name");

                entity.Property(e => e.CuSymbol)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CU_Symbol");
            });

            modelBuilder.Entity<TrCurrency1>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TR_Currency1");

                entity.Property(e => e.Code)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Symbol)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TrCurrencyRate>(entity =>
            {
                entity.HasKey(e => e.CrId);

                entity.ToTable("TR_CurrencyRate");

                entity.HasIndex(e => e.CrCurrencyId, "FCurrency_id");

                entity.Property(e => e.CrId).HasColumnName("CR_Id");

                entity.Property(e => e.CrCreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CR_Creation_Date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CrCurrencyId).HasColumnName("CR_Currency_Id");

                entity.Property(e => e.CrRate).HasColumnName("CR_Rate");

                entity.HasOne(d => d.CrCurrency)
                    .WithMany(p => p.TrCurrencyRates)
                    .HasForeignKey(d => d.CrCurrencyId)
                    .HasConstraintName("FK_TR_CurrencyRate_TR_Currency");
            });

            modelBuilder.Entity<TrDestination>(entity =>
            {
                entity.HasKey(e => e.DId);

                entity.ToTable("TR_Destinations");

                entity.Property(e => e.DId).HasColumnName("D_Id");

                entity.Property(e => e.DContinent)
                    .HasMaxLength(100)
                    .HasColumnName("D_Continent");

                entity.Property(e => e.DDestination)
                    .HasMaxLength(100)
                    .HasColumnName("D_Destination");

                entity.Property(e => e.DIdContinent).HasColumnName("D_IdContinent");
            });

            modelBuilder.Entity<TrDestionations1>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TR_Destionations1'");

                entity.Property(e => e.Continent)
                    .HasMaxLength(255)
                    .HasColumnName("continent");

                entity.Property(e => e.CountryName)
                    .HasMaxLength(255)
                    .HasColumnName("country name");

                entity.Property(e => e._).HasColumnName("#");
            });

            modelBuilder.Entity<TrDiagnosis>(entity =>
            {
                entity.HasKey(e => e.DiId)
                    .HasName("PK_tr_Diagnosis");

                entity.ToTable("TR_Diagnosis");

                entity.Property(e => e.DiId).HasColumnName("DI_ID");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Created)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(500);
            });

            modelBuilder.Entity<TrEmailLog>(entity =>
            {
                entity.HasKey(e => e.ElId)
                    .HasName("PK_tr_EmailLog");

                entity.ToTable("TR_EmailLog");

                entity.Property(e => e.ElId)
                    .HasColumnName("EL_ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ElBody).HasColumnName("EL_Body");

                entity.Property(e => e.ElCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("EL_Created")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ElCreatedBy).HasColumnName("EL_CreatedBy");

                entity.Property(e => e.ElDisplayName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("EL_DisplayName");

                entity.Property(e => e.ElErrorMessage)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("EL_ErrorMessage");

                entity.Property(e => e.ElIsManual).HasColumnName("EL_IsManual");

                entity.Property(e => e.ElIsSent).HasColumnName("EL_IsSent");

                entity.Property(e => e.ElRecipient)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("EL_Recipient");

                entity.Property(e => e.ElSender)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EL_Sender");

                entity.Property(e => e.ElSubject)
                    .HasMaxLength(500)
                    .HasColumnName("EL_Subject");

                entity.Property(e => e.PolicyId).HasColumnName("PolicyID");
            });

            modelBuilder.Entity<TrEmailTemplate>(entity =>
            {
                entity.HasKey(e => e.EtCode)
                    .HasName("PK_tr_EmailTemplate");

                entity.ToTable("TR_EmailTemplate");

                entity.Property(e => e.EtCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ET_Code");

                entity.Property(e => e.EtBody).HasColumnName("ET_Body");

                entity.Property(e => e.EtCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("ET_Created")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EtLob)
                    .HasMaxLength(2)
                    .HasColumnName("ET_LOB");

                entity.Property(e => e.EtRecepients)
                    .HasMaxLength(250)
                    .HasColumnName("ET_Recepients");

                entity.Property(e => e.EtSubject).HasColumnName("ET_Subject");

                entity.Property(e => e.EtTitle)
                    .HasMaxLength(50)
                    .HasColumnName("ET_Title");

                entity.Property(e => e.EtUpdated)
                    .HasColumnType("datetime")
                    .HasColumnName("ET_Updated");

                entity.Property(e => e.EtWithAttachment).HasColumnName("ET_WithAttachment");
            });

            modelBuilder.Entity<TrErrorLog>(entity =>
            {
                entity.ToTable("TR_ErrorLog");

                entity.Property(e => e.Action).HasMaxLength(255);

                entity.Property(e => e.Controller).HasMaxLength(255);

                entity.Property(e => e.RequestPath).HasMaxLength(255);

                entity.Property(e => e.Type).HasMaxLength(50);

                entity.Property(e => e.UserId)
                    .HasMaxLength(36)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TrFeedbackConfig>(entity =>
            {
                entity.HasKey(e => e.FeId)
                    .HasName("PK_tr_FeebackConfig");

                entity.ToTable("TR_FeedbackConfig");

                entity.Property(e => e.FeId)
                    .ValueGeneratedNever()
                    .HasColumnName("FE_ID");

                entity.Property(e => e.FeCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("FE_Created")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FeDescription)
                    .HasMaxLength(500)
                    .HasColumnName("FE_Description");

                entity.Property(e => e.FeIdService).HasColumnName("FE_IdService");

                entity.Property(e => e.FeIsActive)
                    .IsRequired()
                    .HasColumnName("FE_IsActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.FeIsNegative).HasColumnName("FE_IsNegative");
            });

            modelBuilder.Entity<TrFileDirectory>(entity =>
            {
                entity.HasKey(e => e.FdId)
                    .HasName("PK_tr_FileDirectory");

                entity.ToTable("TR_FileDirectory");

                entity.Property(e => e.FdId)
                    .ValueGeneratedNever()
                    .HasColumnName("FD_ID");

                entity.Property(e => e.FdAllowDowload).HasColumnName("FD_AllowDowload");

                entity.Property(e => e.FdDescription)
                    .HasMaxLength(500)
                    .HasColumnName("FD_Description");

                entity.Property(e => e.FdIdFileType).HasColumnName("FD_IdFileType");

                entity.Property(e => e.FdIdObjectReference).HasColumnName("FD_IdObjectReference");

                entity.Property(e => e.FdName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FD_Name");

                entity.Property(e => e.FdNameMd5)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("FD_NameMd5");

                entity.Property(e => e.FdPath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("FD_Path");
            });

            modelBuilder.Entity<TrGroup>(entity =>
            {
                entity.HasKey(e => e.GrId)
                    .HasName("PK_ccGroup");

                entity.ToTable("TR_Group");

                entity.Property(e => e.GrId).HasColumnName("GR_ID");

                entity.Property(e => e.GrCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("GR_Created")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.GrIsActive)
                    .IsRequired()
                    .HasColumnName("GR_IsActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.GrIsAdmin).HasColumnName("GR_IsAdmin");

                entity.Property(e => e.GrName)
                    .HasMaxLength(100)
                    .HasColumnName("GR_Name");
            });

            modelBuilder.Entity<TrGroupPage>(entity =>
            {
                entity.HasKey(e => e.GpId)
                    .HasName("PK_ccGroupRight");

                entity.ToTable("TR_GroupPage");

                entity.Property(e => e.GpId).HasColumnName("GP_ID");

                entity.Property(e => e.GpAllowAudit).HasColumnName("GP_AllowAudit");

                entity.Property(e => e.GpAllowCopy).HasColumnName("GP_AllowCopy");

                entity.Property(e => e.GpAllowDelete).HasColumnName("GP_AllowDelete");

                entity.Property(e => e.GpAllowExecute).HasColumnName("GP_AllowExecute");

                entity.Property(e => e.GpAllowInMenu)
                    .IsRequired()
                    .HasColumnName("GP_AllowInMenu")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.GpAllowInsert).HasColumnName("GP_AllowInsert");

                entity.Property(e => e.GpAllowPrint).HasColumnName("GP_AllowPrint");

                entity.Property(e => e.GpAllowRenew).HasColumnName("GP_AllowRenew");

                entity.Property(e => e.GpAllowUpdate).HasColumnName("GP_AllowUpdate");

                entity.Property(e => e.GpAllowView)
                    .IsRequired()
                    .HasColumnName("GP_AllowView")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.GpIsDefault).HasColumnName("GP_IsDefault");

                entity.Property(e => e.GrId).HasColumnName("GR_ID");

                entity.Property(e => e.PgId).HasColumnName("PG_ID");
            });

            modelBuilder.Entity<TrGroupUser>(entity =>
            {
                entity.HasKey(e => e.GuId)
                    .HasName("PK_ccGroupUser");

                entity.ToTable("TR_GroupUser");

                entity.Property(e => e.GuId).HasColumnName("GU_ID");

                entity.Property(e => e.GrId).HasColumnName("GR_ID");

                entity.Property(e => e.GuCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("GU_Created")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.GuIsActive)
                    .IsRequired()
                    .HasColumnName("GU_IsActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UsId)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("US_ID");
            });

            modelBuilder.Entity<TrLine>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TR_Line");

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.DiId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("DI_ID");

                entity.Property(e => e.Name).HasMaxLength(500);
            });

            modelBuilder.Entity<TrLog>(entity =>
            {
                entity.HasKey(e => e.LoId)
                    .HasName("PK_tr_Log");

                entity.ToTable("TR_Log");

                entity.Property(e => e.LoId).HasColumnName("LO_ID");

                entity.Property(e => e.LoBaseLink)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("LO_BaseLink");

                entity.Property(e => e.LoContentType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LO_ContentType");

                entity.Property(e => e.LoCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("LO_Created")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LoDecHeaders)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LO_DecHeaders");

                entity.Property(e => e.LoDecRequestBody)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LO_DecRequestBody");

                entity.Property(e => e.LoDecResponse)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LO_DecResponse");

                entity.Property(e => e.LoEncKeys)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LO_EncKeys");

                entity.Property(e => e.LoException)
                    .IsUnicode(false)
                    .HasColumnName("LO_Exception");

                entity.Property(e => e.LoExecutionTime).HasColumnName("LO_ExecutionTime");

                entity.Property(e => e.LoHeaders)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LO_Headers");

                entity.Property(e => e.LoIp)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("LO_IP");

                entity.Property(e => e.LoLevel)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LO_Level");

                entity.Property(e => e.LoLogger)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LO_Logger");

                entity.Property(e => e.LoMachine)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("LO_Machine");

                entity.Property(e => e.LoMessage)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("LO_Message");

                entity.Property(e => e.LoRequestBody)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LO_RequestBody");

                entity.Property(e => e.LoRequestMethod)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LO_RequestMethod");

                entity.Property(e => e.LoRequestPath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("LO_RequestPath");

                entity.Property(e => e.LoResponse)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("LO_Response");

                entity.Property(e => e.LoStacktrace)
                    .IsUnicode(false)
                    .HasColumnName("LO_Stacktrace");

                entity.Property(e => e.LoUsername)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("LO_Username");
            });

            modelBuilder.Entity<TrLookup>(entity =>
            {
                entity.HasKey(e => new { e.LkTableName, e.LkId })
                    .HasName("PK_tr_Lookup");

                entity.ToTable("TR_Lookup");

                entity.Property(e => e.LkTableName)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("LK_TableName");

                entity.Property(e => e.LkId).HasColumnName("LK_ID");

                entity.Property(e => e.LkCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("LK_Created")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.LkIcon)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("LK_Icon");

                entity.Property(e => e.LkIdLookup)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("LK_IdLookup");

                entity.Property(e => e.LkIsActive)
                    .IsRequired()
                    .HasColumnName("LK_IsActive")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LkLob)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("LK_Lob")
                    .IsFixedLength();

                entity.Property(e => e.LkSort).HasColumnName("LK_Sort");

                entity.Property(e => e.LkTableField)
                    .HasMaxLength(500)
                    .HasColumnName("LK_TableField");
            });

            modelBuilder.Entity<TrNotification>(entity =>
            {
                entity.HasKey(e => e.NId)
                    .HasName("PK_tr_Notifications");

                entity.ToTable("TR_Notifications");

                entity.Property(e => e.NId).HasColumnName("N_Id");

                entity.Property(e => e.NCreatedBy)
                    .HasMaxLength(500)
                    .HasColumnName("N_CreatedBy");

                entity.Property(e => e.NCreationDate)
                    .HasColumnType("date")
                    .HasColumnName("N_CreationDate");

                entity.Property(e => e.NExpiryDate)
                    .HasColumnType("date")
                    .HasColumnName("N_ExpiryDate");

                entity.Property(e => e.NExpiryTime).HasColumnName("N_ExpiryTime");

                entity.Property(e => e.NIsActive).HasColumnName("N_isActive");

                entity.Property(e => e.NIsDeleted).HasColumnName("N_isDeleted");

                entity.Property(e => e.NIsImportant).HasColumnName("N_isImportant");

                entity.Property(e => e.NText).HasColumnName("N_Text");

                entity.Property(e => e.NTitle)
                    .HasMaxLength(1000)
                    .HasColumnName("N_Title");
            });

            modelBuilder.Entity<TrNotificationsLog>(entity =>
            {
                entity.HasKey(e => e.NlId)
                    .HasName("PK_tr_NotificationsLog");

                entity.ToTable("TR_NotificationsLog");

                entity.Property(e => e.NlId).HasColumnName("NL_Id");

                entity.Property(e => e.NId).HasColumnName("N_Id");

                entity.Property(e => e.NlActionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("NL_ActionDate");

                entity.Property(e => e.NlActionType)
                    .HasMaxLength(50)
                    .HasColumnName("NL_ActionType");

                entity.Property(e => e.NlName)
                    .HasMaxLength(200)
                    .HasColumnName("NL_Name");
            });

            modelBuilder.Entity<TrPackage>(entity =>
            {
                entity.HasKey(e => e.PId);

                entity.ToTable("TR_Package");

                entity.HasIndex(e => new { e.PrId, e.PZoneId }, "Unique_PR_Zone_Combination")
                    .IsUnique();

                entity.HasIndex(e => e.PName, "Unique_Package")
                    .IsUnique();

                entity.HasIndex(e => e.PrId, "fk_ind_PR_Id");

                entity.HasIndex(e => e.PZoneId, "fk_ind_PZone_Id");

                entity.Property(e => e.PId).HasColumnName("P_Id");

                entity.Property(e => e.PAdultMaxAge).HasColumnName("P_Adult_Max_Age");

                entity.Property(e => e.PAdultNo).HasColumnName("P_Adult_No");

                entity.Property(e => e.PChildMaxAge).HasColumnName("P_Child_Max_Age");

                entity.Property(e => e.PChildrenNo).HasColumnName("P_Children_No");

                entity.Property(e => e.PName)
                    .HasMaxLength(100)
                    .HasColumnName("P_Name");

                entity.Property(e => e.PPaIncluded).HasColumnName("P_PA_Included");

                entity.Property(e => e.PRemoveDeductable).HasColumnName("P_Remove_deductable");

                entity.Property(e => e.PRemoveSportsActivities).HasColumnName("P_Remove_Sports_Activities");

                entity.Property(e => e.PSpecialCase).HasColumnName("P_Special_Case");

                entity.Property(e => e.PZoneId).HasColumnName("P_ZoneID");

                entity.Property(e => e.PlId).HasColumnName("PL_Id");

                entity.Property(e => e.PrId).HasColumnName("PR_Id");

                entity.HasOne(d => d.PZone)
                    .WithMany(p => p.TrPackages)
                    .HasForeignKey(d => d.PZoneId)
                    .HasConstraintName("FK_TR_Package_TR_Zone");

                entity.HasOne(d => d.Pr)
                    .WithMany(p => p.TrPackages)
                    .HasForeignKey(d => d.PrId)
                    .HasConstraintName("FK_TR_Package_TR_Product");
            });

            modelBuilder.Entity<TrPage>(entity =>
            {
                entity.HasKey(e => e.PgId)
                    .HasName("PK_ccWebPage");

                entity.ToTable("TR_Page");

                entity.Property(e => e.PgId).HasColumnName("PG_ID");

                entity.Property(e => e.PgAllowInMenu)
                    .IsRequired()
                    .HasColumnName("PG_AllowInMenu")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PgAllowView)
                    .IsRequired()
                    .HasColumnName("PG_AllowView")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PgCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("PG_Created")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PgDesc)
                    .HasMaxLength(500)
                    .HasColumnName("PG_Desc");

                entity.Property(e => e.PgIsAspx).HasColumnName("PG_IsAspx");

                entity.Property(e => e.PgIsDefault).HasColumnName("PG_IsDefault");

                entity.Property(e => e.PgLogo)
                    .HasMaxLength(500)
                    .HasColumnName("PG_Logo");

                entity.Property(e => e.PgName)
                    .HasMaxLength(500)
                    .HasColumnName("PG_Name");

                entity.Property(e => e.PgParent).HasColumnName("PG_Parent");

                entity.Property(e => e.PgSort).HasColumnName("PG_Sort");

                entity.Property(e => e.PgUrl)
                    .HasMaxLength(1000)
                    .HasColumnName("PG_Url");

                entity.Property(e => e.PgUrlParam)
                    .HasMaxLength(1000)
                    .HasColumnName("PG_UrlParam");
            });

            modelBuilder.Entity<TrPageBackup>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TR_Page_backup");

                entity.Property(e => e.PgAllowInMenu).HasColumnName("PG_AllowInMenu");

                entity.Property(e => e.PgAllowView).HasColumnName("PG_AllowView");

                entity.Property(e => e.PgCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("PG_Created");

                entity.Property(e => e.PgDesc)
                    .HasMaxLength(500)
                    .HasColumnName("PG_Desc");

                entity.Property(e => e.PgId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PG_ID");

                entity.Property(e => e.PgIsAspx).HasColumnName("PG_IsAspx");

                entity.Property(e => e.PgIsDefault).HasColumnName("PG_IsDefault");

                entity.Property(e => e.PgLogo)
                    .HasMaxLength(500)
                    .HasColumnName("PG_Logo");

                entity.Property(e => e.PgName)
                    .HasMaxLength(500)
                    .HasColumnName("PG_Name");

                entity.Property(e => e.PgParent).HasColumnName("PG_Parent");

                entity.Property(e => e.PgSort).HasColumnName("PG_Sort");

                entity.Property(e => e.PgUrl)
                    .HasMaxLength(1000)
                    .HasColumnName("PG_Url");

                entity.Property(e => e.PgUrlParam)
                    .HasMaxLength(1000)
                    .HasColumnName("PG_UrlParam");
            });

            modelBuilder.Entity<TrPlan>(entity =>
            {
                entity.HasKey(e => e.PlId);

                entity.ToTable("TR_Plan");

                entity.HasIndex(e => e.PlTitle, "Unique_PlTitle")
                    .IsUnique();

                entity.Property(e => e.PlId).HasColumnName("PL_Id");

                entity.Property(e => e.PlPaIncluded)
                    .HasColumnName("PL_PA_Included")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PlTitle)
                    .HasMaxLength(100)
                    .HasColumnName("PL_Title");
            });

            modelBuilder.Entity<TrPolicyAdditionalBenefit>(entity =>
            {
                entity.ToTable("TR_PolicyAdditionalBenefits");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AbId).HasColumnName("AB_ID");

                entity.Property(e => e.PolicyId).HasColumnName("PolicyID");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Policy)
                    .WithMany(p => p.TrPolicyAdditionalBenefits)
                    .HasForeignKey(d => d.PolicyId)
                    .HasConstraintName("FK_TR_PolicyAdditionalBenefits_TR_PolicyHeader");
            });

            modelBuilder.Entity<TrPolicyDestination>(entity =>
            {
                entity.ToTable("TR_PolicyDestination");

                entity.HasIndex(e => e.PolicyId, "PDes_PolicyID");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DestinationId).HasColumnName("DestinationID");

                entity.Property(e => e.PolicyId).HasColumnName("PolicyID");

                entity.HasOne(d => d.Policy)
                    .WithMany(p => p.TrPolicyDestinations)
                    .HasForeignKey(d => d.PolicyId)
                    .HasConstraintName("FK_TR_PolicyDestination_TR_PolicyHeader");
            });

            modelBuilder.Entity<TrPolicyDetail>(entity =>
            {
                entity.HasKey(e => e.PolicyDetailId)
                    .HasName("PK__PolicyDe__C67223014A6DDBF3");

                entity.ToTable("TR_PolicyDetails");

                entity.HasIndex(e => e.InsuredId, "PD_InsuredId");

                entity.HasIndex(e => e.PolicyId, "PD_PolicyID");

                entity.HasIndex(e => e.Tariff, "PD_Tariff");

                entity.Property(e => e.PolicyDetailId).HasColumnName("PolicyDetailID");

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.DeductiblePrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Discount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FinalPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.Property(e => e.Gender).HasMaxLength(10);

                entity.Property(e => e.Insured).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.NetPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PassportNo).HasMaxLength(20);

                entity.Property(e => e.Plan).HasMaxLength(50);

                entity.Property(e => e.PlanPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PolicyId).HasColumnName("PolicyID");

                entity.Property(e => e.SportsActivitiesPrice).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.InsuredNavigation)
                    .WithMany(p => p.TrPolicyDetails)
                    .HasForeignKey(d => d.InsuredId)
                    .HasConstraintName("FK_TR_PolicyDetails_TR_Beneficiary");

                entity.HasOne(d => d.Policy)
                    .WithMany(p => p.TrPolicyDetails)
                    .HasForeignKey(d => d.PolicyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TR_PolicyDetails_TR_PolicyHeader");

                entity.HasOne(d => d.TariffNavigation)
                    .WithMany(p => p.TrPolicyDetails)
                    .HasForeignKey(d => d.Tariff)
                    .HasConstraintName("FK_TR_PolicyDetails_TR_Tariff");
            });

            modelBuilder.Entity<TrPolicyHeader>(entity =>
            {
                entity.HasKey(e => e.PolicyId)
                    .HasName("PK__PolicyHe__2E1339440B99860D");

                entity.ToTable("TR_PolicyHeader");

                entity.HasIndex(e => e.Reference, "UQ_TR_PolicyHeader_Reference")
                    .IsUnique();

                entity.Property(e => e.PolicyId).HasColumnName("PolicyID");

                entity.Property(e => e.AdditionalValue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CanceledOn).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(36)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedOn).HasColumnType("datetime");

                entity.Property(e => e.Fromdate).HasColumnType("date");

                entity.Property(e => e.GrandTotal).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.InitialPremium).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.IsEditable).HasDefaultValueSql("((0))");

                entity.Property(e => e.PolicyGuid).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Reference).HasMaxLength(50);

                entity.Property(e => e.Source)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.StampsValue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TaxVatvalue)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("TaxVATValue");

                entity.Property(e => e.Todate).HasColumnType("date");

                entity.Property(e => e.ZoneId).HasColumnName("ZoneID");
            });

            modelBuilder.Entity<TrPolicyInfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TR_PolicyInfo");

                entity.Property(e => e.PiId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("PI_ID");

                entity.Property(e => e.PolicyId).HasColumnName("PolicyID");

                entity.Property(e => e.UCommission).HasColumnName("U_Commission");

                entity.Property(e => e.UId).HasColumnName("U_Id");

                entity.Property(e => e.UMaxAdditionalFees).HasColumnName("U_Max_Additional_Fees");

                entity.Property(e => e.URetention).HasColumnName("U_Retention");

                entity.Property(e => e.URoundingRule).HasColumnName("U_Rounding_Rule");

                entity.Property(e => e.UStamp).HasColumnName("U_Stamp");

                entity.Property(e => e.UTax).HasColumnName("U_Tax");

                entity.Property(e => e.UTaxType).HasColumnName("U_Tax_Type");

                entity.Property(e => e.UVat).HasColumnName("U_VAT");

                entity.Property(e => e.UpId)
                    .HasColumnName("UP_Id")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<TrPrepaidAccount>(entity =>
            {
                entity.HasKey(e => e.PaId);

                entity.ToTable("TR_PrepaidAccounts");

                entity.Property(e => e.PaId).HasColumnName("PA_Id");

                entity.Property(e => e.PaActive).HasColumnName("PA_Active");

                entity.Property(e => e.PaAmount).HasColumnName("PA_Amount");

                entity.Property(e => e.PaCreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("PA_CreationDate");

                entity.Property(e => e.PaModifiactionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("PA_ModifiactionDate");

                entity.Property(e => e.UId).HasColumnName("U_Id");

                entity.HasOne(d => d.UIdNavigation)
                    .WithMany(p => p.TrPrepaidAccounts)
                    .HasForeignKey(d => d.UId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TR_PrepaidAccounts_TR_Users");
            });

            modelBuilder.Entity<TrPrepaidAccountsTransaction>(entity =>
            {
                entity.HasKey(e => e.PatId);

                entity.ToTable("TR_PrepaidAccountsTransactions");

                entity.Property(e => e.PatId).HasColumnName("PAT_Id");

                entity.Property(e => e.PaId).HasColumnName("PA_Id");

                entity.Property(e => e.PatAction).HasColumnName("PAT_Action");

                entity.Property(e => e.PatAmount).HasColumnName("PAT_Amount");

                entity.Property(e => e.PatCreatedBy).HasColumnName("PAT_CreatedBy");

                entity.Property(e => e.PatCreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("PAT_CreationDate");

                entity.Property(e => e.PatDetails)
                    .HasMaxLength(1000)
                    .HasColumnName("PAT_Details");

                entity.Property(e => e.PolicyId).HasColumnName("PolicyID");

                entity.HasOne(d => d.Pa)
                    .WithMany(p => p.TrPrepaidAccountsTransactions)
                    .HasForeignKey(d => d.PaId)
                    .HasConstraintName("FK_TR_PrepaidAccountsTransactions_TR_PrepaidAccounts");
            });

            modelBuilder.Entity<TrProduct>(entity =>
            {
                entity.HasKey(e => e.PrId)
                    .HasName("PK_TR_COB");

                entity.ToTable("TR_Product");

                entity.HasIndex(e => e.PrTitle, "Unique_PrTitle")
                    .IsUnique();

                entity.Property(e => e.PrId).HasColumnName("PR_Id");

                entity.Property(e => e.PrActivationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("PR_Activation_Date");

                entity.Property(e => e.PrAdditionalBenefits).HasColumnName("PR_Additional_Benefits");

                entity.Property(e => e.PrAdditionalBenefitsFormat).HasColumnName("PR_Additional_Benefits_Format");

                entity.Property(e => e.PrDeductibleFormat).HasColumnName("PR_Deductible_Format");

                entity.Property(e => e.PrDescription)
                    .HasMaxLength(2000)
                    .HasColumnName("PR_Description");

                entity.Property(e => e.PrIsActive).HasColumnName("PR_Is_Active");

                entity.Property(e => e.PrIsDeductible).HasColumnName("PR_Is_Deductible");

                entity.Property(e => e.PrIsFamily).HasColumnName("PR_Is_Family");

                entity.Property(e => e.PrIsGroup).HasColumnName("PR_Is_Group");

                entity.Property(e => e.PrIsIndividual).HasColumnName("PR_Is_Individual");

                entity.Property(e => e.PrSportsActivities).HasColumnName("PR_Sports_Activities");

                entity.Property(e => e.PrSportsActivityFormat).HasColumnName("PR_Sports_Activity_Format");

                entity.Property(e => e.PrTitle)
                    .HasMaxLength(100)
                    .HasColumnName("PR_Title");
            });

            modelBuilder.Entity<TrProductionBatch>(entity =>
            {
                entity.HasKey(e => e.PbId);

                entity.ToTable("TR_ProductionBatch");

                entity.Property(e => e.PbId).HasColumnName("PB_Id");

                entity.Property(e => e.PbCreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("PB_CreationDate")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PbFileName)
                    .HasMaxLength(1000)
                    .HasColumnName("PB_FileName");

                entity.Property(e => e.PbTitle)
                    .HasMaxLength(100)
                    .HasColumnName("PB_Title");

                entity.Property(e => e.UId)
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .HasColumnName("U_Id");
            });

            modelBuilder.Entity<TrProductionBatchDetail>(entity =>
            {
                entity.ToTable("TR_ProductionBatchDetails");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BatchId).HasColumnName("BatchID");

                entity.Property(e => e.BenId).HasColumnName("BenID");

                entity.Property(e => e.Country).HasMaxLength(255);

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Deductible).HasMaxLength(255);

                entity.Property(e => e.FirstName).HasMaxLength(255);

                entity.Property(e => e.Gender).HasMaxLength(1);

                entity.Property(e => e.LastName).HasMaxLength(255);

                entity.Property(e => e.MiddleName).HasMaxLength(255);

                entity.Property(e => e.Nationality).HasMaxLength(255);

                entity.Property(e => e.NetInUsd)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("NetInUSD");

                entity.Property(e => e.PassportNumber).HasMaxLength(255);

                entity.Property(e => e.Plan).HasMaxLength(255);

                entity.Property(e => e.PremiumInUsd)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("PremiumInUSD");

                entity.Property(e => e.Product).HasMaxLength(255);

                entity.Property(e => e.Reason).HasMaxLength(50);

                entity.Property(e => e.ReferenceNumber).HasMaxLength(255);

                entity.Property(e => e.SportsActivities).HasMaxLength(255);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Type).HasMaxLength(255);

                entity.Property(e => e.Zone).HasMaxLength(255);
            });

            modelBuilder.Entity<TrProfile>(entity =>
            {
                entity.HasKey(e => e.PrId)
                    .HasName("PK_tr_Profiles");

                entity.ToTable("TR_Profile");

                entity.Property(e => e.PrId).HasColumnName("PR_ID");

                entity.Property(e => e.PrAccountNo)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PR_AccountNo");

                entity.Property(e => e.PrApprovalRequired).HasColumnName("PR_ApprovalRequired");

                entity.Property(e => e.PrComplexCaseAmount)
                    .HasColumnType("numeric(18, 3)")
                    .HasColumnName("PR_ComplexCaseAmount");

                entity.Property(e => e.PrCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("PR_Created")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PrCreatedBy).HasColumnName("PR_CreatedBy");

                entity.Property(e => e.PrDeletedBy).HasColumnName("PR_DeletedBy");

                entity.Property(e => e.PrDeletionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("PR_DeletionDate");

                entity.Property(e => e.PrDialCode)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("PR_DialCode");

                entity.Property(e => e.PrEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("PR_Email");

                entity.Property(e => e.PrEmailNotification).HasColumnName("PR_EmailNotification");

                entity.Property(e => e.PrFeesType).HasColumnName("PR_FeesType");

                entity.Property(e => e.PrIdCurrency).HasColumnName("PR_IdCurrency");

                entity.Property(e => e.PrIsDeleted).HasColumnName("PR_IsDeleted");

                entity.Property(e => e.PrName)
                    .HasMaxLength(500)
                    .HasColumnName("PR_Name");

                entity.Property(e => e.PrPhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("PR_PhoneNumber");

                entity.Property(e => e.PrSimpleCaseAmount)
                    .HasColumnType("numeric(18, 3)")
                    .HasColumnName("PR_SimpleCaseAmount");
            });

            modelBuilder.Entity<TrProfileService>(entity =>
            {
                entity.HasKey(e => new { e.PrId, e.PsIdProfileType })
                    .HasName("PK_tr_ProfileService");

                entity.ToTable("TR_ProfileService");

                entity.Property(e => e.PrId).HasColumnName("PR_ID");

                entity.Property(e => e.PsIdProfileType).HasColumnName("PS_IdProfileType");

                entity.Property(e => e.PsCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("PS_Created")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PsIdCaseType).HasColumnName("PS_IdCaseType");

                entity.Property(e => e.PsIsActive)
                    .IsRequired()
                    .HasColumnName("PS_IsActive")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<TrRegisterEmailTo>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TR_RegisterEmailTo");

                entity.Property(e => e.EtCode)
                    .HasMaxLength(50)
                    .HasColumnName("ET_Code");

                entity.Property(e => e.EtEmail)
                    .HasMaxLength(150)
                    .HasColumnName("ET_Email");

                entity.Property(e => e.EtId).HasColumnName("ET_ID");

                entity.Property(e => e.EtMngemail)
                    .HasMaxLength(150)
                    .HasColumnName("ET_MNGEmail");

                entity.Property(e => e.EtName)
                    .HasMaxLength(150)
                    .HasColumnName("ET_Name");
            });

            modelBuilder.Entity<TrRequiredDocument>(entity =>
            {
                entity.HasKey(e => new { e.RdIdReferenceObject, e.FdId, e.RdIdDocumentType })
                    .HasName("PK_tr_RequiredDocument_1");

                entity.ToTable("TR_RequiredDocument");

                entity.HasIndex(e => e.RdId, "IX_tr_RequiredDocument")
                    .IsUnique();

                entity.Property(e => e.RdIdReferenceObject).HasColumnName("RD_IdReferenceObject");

                entity.Property(e => e.FdId).HasColumnName("FD_ID");

                entity.Property(e => e.RdIdDocumentType).HasColumnName("RD_IdDocumentType");

                entity.Property(e => e.RdId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("RD_ID");

                entity.Property(e => e.RdIsHtml).HasColumnName("RD_IsHtml");

                entity.Property(e => e.RdIsMandatory).HasColumnName("RD_IsMandatory");

                entity.Property(e => e.RdIsUnique).HasColumnName("RD_IsUnique");
            });

            modelBuilder.Entity<TrTariff>(entity =>
            {
                entity.HasKey(e => e.TId);

                entity.ToTable("TR_Tariff");

                entity.HasIndex(e => new { e.PId, e.TStartAge, e.TEndAge, e.TNumberOfDays, e.TTariffStartingDate, e.PlId }, "Unique_TariffCombination")
                    .IsUnique();

                entity.HasIndex(e => e.PlId, "fk_ind_PL_Id");

                entity.HasIndex(e => e.PId, "fk_ind_P_Id");

                entity.Property(e => e.TId).HasColumnName("T_Id");

                entity.Property(e => e.PId).HasColumnName("P_Id");

                entity.Property(e => e.PlId).HasColumnName("PL_Id");

                entity.Property(e => e.TEndAge).HasColumnName("T_End_Age");

                entity.Property(e => e.TNetPremiumAmount)
                    .HasColumnType("decimal(18, 3)")
                    .HasColumnName("T_Net_Premium_Amount");

                entity.Property(e => e.TNumberOfDays).HasColumnName("T_Number_Of_Days");

                entity.Property(e => e.TOverrideAmount)
                    .HasColumnType("decimal(18, 3)")
                    .HasColumnName("T_Override_Amount");

                entity.Property(e => e.TPaAmount)
                    .HasColumnType("decimal(18, 3)")
                    .HasColumnName("T_PA_Amount");

                entity.Property(e => e.TPriceAmount)
                    .HasColumnType("decimal(18, 3)")
                    .HasColumnName("T_Price_Amount");

                entity.Property(e => e.TStartAge).HasColumnName("T_Start_Age");

                entity.Property(e => e.TTariffStartingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("T_Tariff_Starting_Date");

                entity.HasOne(d => d.PIdNavigation)
                    .WithMany(p => p.TrTariffs)
                    .HasForeignKey(d => d.PId)
                    .HasConstraintName("FK_TR_Tariff_TR_Package");

                entity.HasOne(d => d.Pl)
                    .WithMany(p => p.TrTariffs)
                    .HasForeignKey(d => d.PlId)
                    .HasConstraintName("FK_TR_Tariff_TR_Plan");
            });

            modelBuilder.Entity<TrTextReplacement>(entity =>
            {
                entity.HasKey(e => e.TrCode)
                    .HasName("PK_tr_TextReplacement");

                entity.ToTable("TR_TextReplacement");

                entity.Property(e => e.TrCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TR_Code");

                entity.Property(e => e.TrDesc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TR_Desc");

                entity.Property(e => e.TrIsActive)
                    .IsRequired()
                    .HasColumnName("TR_IsActive")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<TrUser>(entity =>
            {
                entity.HasKey(e => e.UId);

                entity.ToTable("TR_Users");

                entity.HasIndex(e => e.UUserName, "Unique_Username")
                    .IsUnique();

                entity.Property(e => e.UId).HasColumnName("U_Id");

                entity.Property(e => e.UActive).HasColumnName("U_Active");

                entity.Property(e => e.UAdditionalFees).HasColumnName("U_Additional_Fees");

                entity.Property(e => e.UAgentsCommissionReportView).HasColumnName("U_Agents_Commission_ReportView");

                entity.Property(e => e.UAgentsCreation).HasColumnName("U_Agents_Creation");

                entity.Property(e => e.UAgentsCreationApproval).HasColumnName("U_Agents_Creation_Approval");

                entity.Property(e => e.UAgentsView).HasColumnName("U_Agents_View");

                entity.Property(e => e.UAllowCancellation).HasColumnName("U_Allow_Cancellation");

                entity.Property(e => e.UApplyRounding).HasColumnName("U_Apply_Rounding");

                entity.Property(e => e.UBrokerCode).HasColumnName("U_Broker_Code");

                entity.Property(e => e.UCanCancel)
                    .HasColumnName("U_Can_Cancel")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UCanEdit)
                    .HasColumnName("U_Can_Edit")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UCancellationSubAgent).HasColumnName("U_Cancellation_SubAgent");

                entity.Property(e => e.UCategory)
                    .HasMaxLength(50)
                    .HasColumnName("U_Category");

                entity.Property(e => e.UCity).HasColumnName("U_City");

                entity.Property(e => e.UCityName)
                    .HasMaxLength(100)
                    .HasColumnName("U_City_Name");

                entity.Property(e => e.UCommission).HasColumnName("U_Commission");

                entity.Property(e => e.UContactPerson)
                    .HasMaxLength(50)
                    .HasColumnName("U_Contact_Person");

                entity.Property(e => e.UCountry).HasColumnName("U_Country");

                entity.Property(e => e.UCountryCode).HasColumnName("U_Country_Code");

                entity.Property(e => e.UCreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("U_Creation_Date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UCreatorId).HasColumnName("U_Creator_Id");

                entity.Property(e => e.UCurrency).HasColumnName("U_Currency");

                entity.Property(e => e.UEmail)
                    .HasMaxLength(100)
                    .HasColumnName("U_Email");

                entity.Property(e => e.UFirstName)
                    .HasMaxLength(50)
                    .HasColumnName("U_First_Name");

                entity.Property(e => e.UFixedAdditionalFees).HasColumnName("U_Fixed_Additional_Fees");

                entity.Property(e => e.UForSyria).HasColumnName("U_For_Syria");

                entity.Property(e => e.UGuid)
                    .HasColumnName("U_Guid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.UHidePremiumInfo).HasColumnName("U_Hide_Premium_Info");

                entity.Property(e => e.UInsuredNumber).HasColumnName("U_Insured_Number");

                entity.Property(e => e.UIsAdmin)
                    .HasColumnName("U_Is_Admin")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ULastName)
                    .HasMaxLength(50)
                    .HasColumnName("U_Last_Name");

                entity.Property(e => e.ULogo)
                    .HasMaxLength(1000)
                    .HasColumnName("U_Logo");

                entity.Property(e => e.UManualProduction)
                    .HasColumnName("U_Manual_Production")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UMaxAdditionalFees).HasColumnName("U_Max_Additional_Fees");

                entity.Property(e => e.UMiddleName)
                    .HasMaxLength(50)
                    .HasColumnName("U_Middle_Name");

                entity.Property(e => e.UMultiLangPolicy).HasColumnName("U_Multi_Lang_Policy");

                entity.Property(e => e.UPassword)
                    .HasMaxLength(100)
                    .HasColumnName("U_Password");

                entity.Property(e => e.UPrepaidAccount).HasColumnName("U_Prepaid_Account");

                entity.Property(e => e.UPreviewNet).HasColumnName("U_Preview_Net");

                entity.Property(e => e.UPreviewTotalOnly).HasColumnName("U_Preview_Total_Only");

                entity.Property(e => e.UPrintClientVoucher).HasColumnName("U_Print_Client_Voucher");

                entity.Property(e => e.UPrintLayout)
                    .HasMaxLength(250)
                    .HasColumnName("U_PrintLayout");

                entity.Property(e => e.URetention).HasColumnName("U_Retention");

                entity.Property(e => e.URoundingRule).HasColumnName("U_Rounding_Rule");

                entity.Property(e => e.UShowCertificate).HasColumnName("U_Show_Certificate");

                entity.Property(e => e.UShowCommission).HasColumnName("U_Show_Commission");

                entity.Property(e => e.USignature)
                    .HasMaxLength(250)
                    .HasColumnName("U_Signature");

                entity.Property(e => e.UStamp).HasColumnName("U_Stamp");

                entity.Property(e => e.USubAgentsCommissionReportView).HasColumnName("U_SubAgents_Commission_ReportView");

                entity.Property(e => e.USuperAgentId).HasColumnName("U_Super_Agent_Id");

                entity.Property(e => e.UTax).HasColumnName("U_Tax");

                entity.Property(e => e.UTaxInvoice).HasColumnName("U_Tax_Invoice");

                entity.Property(e => e.UTaxType).HasColumnName("U_Tax_Type");

                entity.Property(e => e.UTelephone)
                    .HasMaxLength(50)
                    .HasColumnName("U_Telephone");

                entity.Property(e => e.UUniqueAdminTax).HasColumnName("U_Unique_Admin_Tax");

                entity.Property(e => e.UUniqueTax).HasColumnName("U_Unique_Tax");

                entity.Property(e => e.UUserName)
                    .HasMaxLength(50)
                    .HasColumnName("U_User_Name");

                entity.Property(e => e.UVat).HasColumnName("U_VAT");

                entity.HasOne(d => d.UCurrencyNavigation)
                    .WithMany(p => p.TrUsers)
                    .HasForeignKey(d => d.UCurrency)
                    .HasConstraintName("FK_TR_Users_TR_CurrencyRate");
            });

            modelBuilder.Entity<TrUsersProduct>(entity =>
            {
                entity.HasKey(e => e.UpId)
                    .HasName("PK_TR_UsersProduct_1");

                entity.ToTable("TR_UsersProduct");

                entity.Property(e => e.UpId).HasColumnName("UP_Id");

                entity.Property(e => e.PrId).HasColumnName("PR_Id");

                entity.Property(e => e.UId).HasColumnName("U_Id");

                entity.Property(e => e.UpActive).HasColumnName("UP_Active");

                entity.Property(e => e.UpCreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("UP_CreationDate");

                entity.Property(e => e.UpIssuingFees).HasColumnName("UP_IssuingFees");

                entity.Property(e => e.UpUploadedFile).HasColumnName("UP_UploadedFile");
            });

            modelBuilder.Entity<TrUserstest>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TR_Userstest");

                entity.Property(e => e.UActive).HasColumnName("U_Active");

                entity.Property(e => e.UAdditionalFees).HasColumnName("U_Additional_Fees");

                entity.Property(e => e.UAgentsCommissionReportView).HasColumnName("U_Agents_Commission_ReportView");

                entity.Property(e => e.UAgentsCreation).HasColumnName("U_Agents_Creation");

                entity.Property(e => e.UAgentsCreationApproval).HasColumnName("U_Agents_Creation_Approval");

                entity.Property(e => e.UAllowCancellation).HasColumnName("U_Allow_Cancellation");

                entity.Property(e => e.UApplyRounding).HasColumnName("U_Apply_Rounding");

                entity.Property(e => e.UBrokerCode).HasColumnName("U_Broker_Code");

                entity.Property(e => e.UCanCancel).HasColumnName("U_Can_Cancel");

                entity.Property(e => e.UCanEdit).HasColumnName("U_Can_Edit");

                entity.Property(e => e.UCancellationSubAgent).HasColumnName("U_Cancellation_SubAgent");

                entity.Property(e => e.UCategory)
                    .HasMaxLength(50)
                    .HasColumnName("U_Category");

                entity.Property(e => e.UCity).HasColumnName("U_City");

                entity.Property(e => e.UCityName)
                    .HasMaxLength(100)
                    .HasColumnName("U_City_Name");

                entity.Property(e => e.UCommission).HasColumnName("U_Commission");

                entity.Property(e => e.UContactPerson)
                    .HasMaxLength(50)
                    .HasColumnName("U_Contact_Person");

                entity.Property(e => e.UCountry).HasColumnName("U_Country");

                entity.Property(e => e.UCountryCode).HasColumnName("U_Country_Code");

                entity.Property(e => e.UCreationDate)
                    .HasColumnType("datetime")
                    .HasColumnName("U_Creation_Date");

                entity.Property(e => e.UCreatorId).HasColumnName("U_Creator_Id");

                entity.Property(e => e.UCurrency).HasColumnName("U_Currency");

                entity.Property(e => e.UEmail)
                    .HasMaxLength(100)
                    .HasColumnName("U_Email");

                entity.Property(e => e.UFirstName)
                    .HasMaxLength(50)
                    .HasColumnName("U_First_Name");

                entity.Property(e => e.UFixedAdditionalFees).HasColumnName("U_Fixed_Additional_Fees");

                entity.Property(e => e.UForSyria).HasColumnName("U_For_Syria");

                entity.Property(e => e.UGuid).HasColumnName("U_Guid");

                entity.Property(e => e.UHidePremiumInfo).HasColumnName("U_Hide_Premium_Info");

                entity.Property(e => e.UId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("U_Id");

                entity.Property(e => e.UInsuredNumber).HasColumnName("U_Insured_Number");

                entity.Property(e => e.UIsAdmin).HasColumnName("U_Is_Admin");

                entity.Property(e => e.ULastName)
                    .HasMaxLength(50)
                    .HasColumnName("U_Last_Name");

                entity.Property(e => e.ULogo)
                    .HasMaxLength(1000)
                    .HasColumnName("U_Logo");

                entity.Property(e => e.UManualProduction).HasColumnName("U_Manual_Production");

                entity.Property(e => e.UMaxAdditionalFees).HasColumnName("U_Max_Additional_Fees");

                entity.Property(e => e.UMiddleName)
                    .HasMaxLength(50)
                    .HasColumnName("U_Middle_Name");

                entity.Property(e => e.UMultiLangPolicy).HasColumnName("U_Multi_Lang_Policy");

                entity.Property(e => e.UPassword)
                    .HasMaxLength(100)
                    .HasColumnName("U_Password");

                entity.Property(e => e.UPreviewNet).HasColumnName("U_Preview_Net");

                entity.Property(e => e.UPreviewTotalOnly).HasColumnName("U_Preview_Total_Only");

                entity.Property(e => e.UPrintClientVoucher).HasColumnName("U_Print_Client_Voucher");

                entity.Property(e => e.UPrintLayout)
                    .HasMaxLength(250)
                    .HasColumnName("U_PrintLayout");

                entity.Property(e => e.URetention).HasColumnName("U_Retention");

                entity.Property(e => e.URoundingRule).HasColumnName("U_Rounding_Rule");

                entity.Property(e => e.UShowCertificate).HasColumnName("U_Show_Certificate");

                entity.Property(e => e.UShowCommission).HasColumnName("U_Show_Commission");

                entity.Property(e => e.USignature)
                    .HasMaxLength(250)
                    .HasColumnName("U_Signature");

                entity.Property(e => e.UStamp).HasColumnName("U_Stamp");

                entity.Property(e => e.USubAgentsCommissionReportView).HasColumnName("U_SubAgents_Commission_ReportView");

                entity.Property(e => e.USuperAgentId).HasColumnName("U_Super_Agent_Id");

                entity.Property(e => e.UTax).HasColumnName("U_Tax");

                entity.Property(e => e.UTaxInvoice).HasColumnName("U_Tax_Invoice");

                entity.Property(e => e.UTaxType).HasColumnName("U_Tax_Type");

                entity.Property(e => e.UTelephone)
                    .HasMaxLength(50)
                    .HasColumnName("U_Telephone");

                entity.Property(e => e.UUniqueAdminTax).HasColumnName("U_Unique_Admin_Tax");

                entity.Property(e => e.UUniqueTax).HasColumnName("U_Unique_Tax");

                entity.Property(e => e.UUserName)
                    .HasMaxLength(50)
                    .HasColumnName("U_User_Name");

                entity.Property(e => e.UVat).HasColumnName("U_VAT");
            });

            modelBuilder.Entity<TrZone>(entity =>
            {
                entity.HasKey(e => e.ZId)
                    .HasName("PK_TR_Product");

                entity.ToTable("TR_Zone");

                entity.Property(e => e.ZId).HasColumnName("Z_Id");

                entity.Property(e => e.ZTitle)
                    .HasMaxLength(100)
                    .HasColumnName("Z_Title");
            });

            modelBuilder.Entity<TrZoneDestination>(entity =>
            {
                entity.HasKey(e => e.ZdId);

                entity.ToTable("TR_ZoneDestination");

                entity.Property(e => e.ZdId).HasColumnName("ZD_ID");

                entity.Property(e => e.DId).HasColumnName("D_Id");

                entity.Property(e => e.ZId).HasColumnName("Z_Id");

                entity.HasOne(d => d.DIdNavigation)
                    .WithMany(p => p.TrZoneDestinations)
                    .HasForeignKey(d => d.DId)
                    .HasConstraintName("FK_TR_ZoneDestination_TR_Destinations");

                entity.HasOne(d => d.ZIdNavigation)
                    .WithMany(p => p.TrZoneDestinations)
                    .HasForeignKey(d => d.ZId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_TR_ZoneDestination_TR_Zone");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
