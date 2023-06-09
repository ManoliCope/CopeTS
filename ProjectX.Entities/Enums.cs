using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectX.Entities
{
    public enum Genders
    {
        male = 1,
        female = 2
    }

    public enum Languages
    {
        english = 1,
        arabic = 2
    }

    public enum StatusCodeValues
    {
        //Basic
        serverError = 0,
        success = 1,
        SessionExpiry = 2,
        SqlTimeout = 3,
        CheckAttactment = 1504,

        //System
        InvalidCredentials = 101,

        //General
        WrongDateFormat = 500,
        InvalidPhoneNo = 501,
        InvalidEmail = 502,

        //Policy
        InvalidPolicyNo = 1000,

        //Profile
        InvalidProfileName = 1100,
        InvalidProfileType = 1101,
        InvalidContactName = 1102,
        InvalidCountry = 1103,
        LinkedToFees = 1104,
        LinkedToAdherent = 1105,
        LinkedToCase = 1106,
        LinkedToGop = 1107,

        //Case
        NoCallDetected = 1200,
        CaseClosed = 1201,
        CannotProceedNotAcceptedCase = 1202,
        CannotReOpenNotClosedCase = 1203,
        AuditNotAllowed = 1204,
        NoApprovalNeeded = 1205,
        MedicalReportRequired = 1206,
        NonMedicalCase = 1207,
        CountryRequired = 1208,
        AlreadyApproved = 1209,
        InvalidReason = 1210,
        InvalidPartner = 1211,
        InvalidPartnerEmail = 1212,
        InvalidBranch = 1213,
        //Call
        CallAlreadyConsumed = 1300,

        //File
        FileSizeTooLarge = 1400,
        NoFileDetected = 1401,
        InvalidFile = 1402,

        //Email
        SendingEmailFailed = 1500,
        SubjectRequired = 1501,
        BodyRequired = 1502,
        RecipientRequired = 1503,

            //testing
        test = 7000

    }

    public enum SuccessCodeValues
    {
        Add = 1,
        Update = 2,
        Delete = 3,
        ReOpened = 4,
        Cancelled= 5
    }

    public enum FileTypes
    {
        Any = 1,
        Image = 2,
        Audio = 3,
        Video = 4,
        Document = 5,
        YouTube = 6
    }

    public enum ExecutableFileExtensions
    {
        EX0 = 1,
        K37 = 2,
        K98 = 3,
        KC8 = 4,
        A6P = 5,
        A7R = 6,
        AC = 7,
        ACC = 8,
        ACR = 9,
        ACTC = 10,
        ACTION = 11,
        ACTM = 12,
        AHK = 13,
        AIR = 14,
        APK = 15,
        APP = 16,
        APPLESCRIPT = 19,
        ARSCRIPT = 20,
        ASB = 21,
        AZW2 = 22,
        BA_ = 23,
        BAT = 24,
        BEAM = 25,
        BIN = 26,
        BTM = 28,
        CACTION = 29,
        CEL = 30,
        CELX = 31,
        CGI = 32,
        CMD = 33,
        COF = 34,
        COFFEE = 35,
        COM = 36,
        COMMAND = 37,
        CSH = 38,
        CYW = 39,
        DEK = 40,
        DLD = 41,
        DMC = 42,
        DS = 43,
        DXL = 44,
        E_E = 45,
        EAR = 46,
        EBM = 47,
        EBS = 48,
        EBS2 = 49,
        ECF = 50,
        EHAM = 51,
        ELF = 52,
        EPK = 53,
        ES = 54,
        ESH = 55,
        EX4 = 56,
        EX5 = 57,
        EX_ = 58,
        EXE = 60,
        EXE1 = 62,
        EXOPC = 63,
        EZS = 64,
        EZT = 65,
        FAS = 66,
        FKY = 68,
        FPI = 69,
        FRS = 70,
        FXP = 71,
        GADGET = 72,
        GPE = 73,
        GPU = 74,
        GS = 75,
        HAM = 76,
        HMS = 77,
        HPF = 78,
        HTA = 79,
        ICD = 80,
        IIM = 81,
        IPA = 82,
        IPF = 83,
        ISU = 84,
        ITA = 85,
        JAR = 86,
        JS = 87,
        JSE = 88,
        JSF = 89,
        JSX = 90,
        KIX = 91,
        KSH = 92,
        KX = 93,
        LO = 94,
        LS = 95,
        M3G = 96,
        MAC = 97,
        MAM = 98,
        MCR = 99,
        MEL = 101,
        MEM = 102,
        MIO = 103,
        MLX = 104,
        MM = 105,
        MPX = 106,
        MRC = 107,
        MRP = 108,
        MS = 109,
        MSL = 111,
        MXE = 112,
        N = 113,
        NCL = 114,
        NEXE = 115,
        ORE = 116,
        OSX = 117,
        OTM = 118,
        OUT = 119,
        PAF = 120,
        PAFEXE = 121,
        PEX = 122,
        PHAR = 123,
        PIF = 124,
        PLSC = 125,
        PLX = 126,
        PRC = 127,
        PRG = 128,
        PS1 = 130,
        PVD = 131,
        PWC = 132,
        PYC = 133,
        PYO = 134,
        QIT = 135,
        QPX = 136,
        RBF = 137,
        RBX = 138,
        RFU = 139,
        RGS = 140,
        ROX = 141,
        RPJ = 142,
        RUN = 143,
        RXE = 144,
        S2A = 145,
        SBS = 146,
        SCA = 147,
        SCAR = 148,
        SCB = 149,
        SCPT = 150,
        SCPTD = 151,
        SCR = 152,
        SCRIPT = 153,
        SCT = 154,
        SEED = 155,
        SERVER = 156,
        SHB = 157,
        SMM = 158,
        SPR = 159,
        TCP = 160,
        THM = 161,
        TIAPP = 162,
        TMS = 163,
        U3P = 164,
        UDF = 165,
        UPX = 166,
        VBE = 167,
        VBS = 168,
        VBSCRIPT = 169,
        VDO = 170,
        VEXE = 171,
        VLX = 172,
        VPM = 173,
        VXP = 174,
        WCM = 175,
        WIDGET = 176,
        WIZ = 177,
        WORKFLOW = 178,
        WPK = 179,
        WPM = 180,
        WS = 181,
        WSF = 182,
        WSH = 183,
        X86 = 184,
        XAP = 185,
        XBAP = 186,
        XLM = 187,
        XQT = 188,
        XYS = 189,
        ZL9 = 190
    }

    public enum ImageFormats
    {
        memorybmp = 1,
        bmp = 2,
        emf = 3,
        wmf = 4,
        gif = 5,
        jpeg = 6,
        png = 7,
        tiff = 8,
        exif = 9,
        ico = 10,
        jfif = 11,
        jpg = 12,
        tif = 13,
        icon = 14,
    }

    public enum AudioFormats
    {
        mp3 = 1,
        caf = 2,
        wav = 3,
        m4a = 4,
        flac = 5,
        wma = 6,
        aac = 7,
        amr = 8
    }

    public enum VideoFormats
    {
        mp4 = 1,
        mov = 2
    }

    public enum DocumentFormats
    {
        pdf = 1,
        csv = 2,
        xlsx = 3,
        xls = 4,
        doc = 5,
        docx = 6
    }

    public enum FileDirectories
    {
        Default_GOP_Template = 1,
        Profile_Documents = 2,
        Case_Documents = 3,
        Case_Audit_Documents = 4,
        Profile_GOP = 5,
        Profile_Excel = 6,
        Motor_Declaration = 7,
        Registery_Documents = 8,
        Case_Finance_Documents=9
    }

    public enum ObjectReferences
    {
        System = 0,
        Profile = 1,
        Case = 2,
        MotorDeclaration = 3,
        CaseAudit = 4,
        FinanceComplete=5
    }

    public enum DocumentTypes
    {
        All = -1,
        Others = 0,
        GOP = 1,
        ID = 2,
        MedicalReport = 3,
        Contract = 4,
        DrivingLicense = 5,
        VehicleImages = 6,
        DeclarationReport = 7,
        Invoice = 8
    }

    public enum ProfileTypes
    {
        Payer = 1,
        TPA = 2,
        Assistance_Company = 3
    }

    public enum CaseUpdateActions
    {
        Save = 1,
        Proceed = 2,
        Close = 3,
        ReOpen = 4,
        Cancel = 5,
        FinanceComplete = 6
    }

    public enum CaseStatus
    {
        Opened = 1,
        Inprogress = 2,
        Outstanding = 3,
        ReOpened = 4,
        Closed = 5,
        AwaitingInvoice = 6,
        Completed=7
    }

    public enum CaseApproval
    {
        Pending = 0,
        Accepted = 1,
        Rejected = 2,
        Reimbursement = 3,
        Cancelled = 5
    }

    public enum ProfileFeesTypes
    {
        PerCase = 1,
        PerCapita = 2,
        PerCountry = 3
    }

    public enum CaseComplexity
    {
        Simple = 1,
        Moderate = 2,
        Complex = 3
    }

    public enum CaseEmailOptions
    {
        GetApprovalFromPayer = 1,
        GetMedicalReportFromPartner = 2,
        AnyEmail = 3,
        MotorDeclaration = 4,
    }

    public enum CaseTypes
    {
        Expert = 1,
        TowingAndExpert = 2,
        Others = 3,
        Property = 4,
        BadReview = 5,
        Towing = 6,
        Assistance = 7,
        Medical = 8
    }

    public enum Groups
    {
        Administrator = 1,
        Operator = 2,
        Auditor = 3,
        Payer = 4,
        Accountant = 6,
        Operator_Supervisor = 7
    }

    public enum TowingStatus
    {
        Pending = 1,
        Arrived = 2,
        Cancelled = 3,
    }
    public enum ExpertStatus
    {
        Pending = 1,
        Arrived = 2,
        Cancelled = 3,
    }

    public enum LOB
    {
        TR,
        MO,
        MD,
        NM,
        OT
    }
}
