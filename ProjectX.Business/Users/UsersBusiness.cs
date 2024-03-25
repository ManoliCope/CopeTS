using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Users;
using ProjectX.Repository.UsersRepository;
using System;
using System.Collections.Generic;
using System.Text;
using ProjectX.Entities.Resources;
using ProjectX.Entities;
using ProjectX.Entities.bModels;

namespace ProjectX.Business.Users
{ 
    public class UsersBusiness : IUsersBusiness
    {
        private IUsersRepository _usersRepository;


        public UsersBusiness(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public int GetUserID(Guid id)
        {
            return _usersRepository.GetUserID(id);
        }

        public UsersResp ModifyUser(UsersReq req, string act, int userid)
        {
            UsersResp response = new UsersResp();
            response = _usersRepository.ModifyUser(req, act, userid);
            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success, req.Id == 0 ? SuccessCodeValues.Add : SuccessCodeValues.Update, "Users");
            return response;
           
        }

        public UsersSearchResp GetUsersList(UsersSearchReq req)
        {
            return _usersRepository.GetUsersList(req);
        }
        public LoginResp Login(LoginReq Req)
        {
            LoginResp response = new LoginResp();
            response.user = _usersRepository.Login(Req.username, Req.password);
            if (response.user != null)
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success);
            else
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.InvalidCredentials);
            return response;
        }
        public UsersResp GetUser(int userid)
        {
            if (userid <= 0)
                return null;
            TR_Users repores = _usersRepository.GetUser(userid);
            UsersResp resp = new UsersResp();
            resp.id = repores.U_Id;
            resp.first_Name = repores.U_First_Name;
            resp.middle_Name = repores.U_Middle_Name;
            resp.last_Name = repores.U_Last_Name;
            resp.user_Name = repores.U_User_Name;
            resp.category = repores.U_Category;
            resp.broker_Code = repores.U_Broker_Code;
            resp.country = repores.U_Country;
            resp.country_code = repores.U_Country_Code;
            resp.city = repores.U_City_Name;
            resp.email = repores.U_Email;
            resp.telephone = repores.U_Telephone;
            resp.super_Agent_Id = repores.U_Super_Agent_Id;
            resp.contact_Person = repores.U_Contact_Person;
            resp.insured_Number = repores.U_Insured_Number;
            resp.tax = repores.U_Tax;
            resp.tax_Type = repores.U_Tax_Type;
            resp.tax_Invoice = repores.U_Tax_Invoice;
            resp.currency = repores.U_Currency;
            resp.rounding_Rule = repores.U_Rounding_Rule;
            resp.unique_Tax = repores.U_Unique_Tax;
            resp.unique_Admin_Tax = repores.U_Unique_Admin_Tax;
            resp.commission = repores.U_Commission;
            resp.stamp = repores.U_Stamp;
            resp.retention = repores.U_Retention;
            resp.additional_Fees = repores.U_Additional_Fees;
            resp.vat = repores.U_VAT;
            resp.for_Syria = repores.U_For_Syria;
            resp.fixed_Additional_Fees = repores.U_Fixed_Additional_Fees;
            resp.apply_Rounding = repores.U_Apply_Rounding;
            resp.allow_Cancellation = repores.U_Allow_Cancellation;
            resp.show_Certificate = repores.U_Show_Certificate;
            resp.cancellation_SubAgent = repores.U_Cancellation_SubAgent;
            resp.preview_Total_Only = repores.U_Preview_Total_Only;
            resp.preview_Net = repores.U_Preview_Net;
            resp.agents_Creation = repores.U_Agents_Creation;
            resp.agents_View = repores.U_Agents_View;
            resp.agents_Creation_Approval = repores.U_Agents_Creation_Approval;
            resp.agents_Commission_ReportView = repores.U_Agents_Commission_ReportView;
            resp.subAgents_Commission_ReportView = repores.U_SubAgents_Commission_ReportView;
            resp.print_Client_Voucher = repores.U_Print_Client_Voucher;
            resp.multi_Lang_Policy = repores.U_Multi_Lang_Policy;
            resp.tax_Invoice = repores.U_Tax_Invoice;
            resp.hide_Premium_Info = repores.U_Hide_Premium_Info;
            resp.active = repores.U_Active;
            resp.max_Additional_Fees = repores.U_Max_Additional_Fees;
            resp.creation_Date = repores.U_Creation_Date;
            resp.have_Parents = repores.U_Have_Parents;
            resp.is_Admin = repores.U_Is_Admin;
            resp.logo = repores.U_Logo;
            resp.printlayout = repores.U_PrintLayout;
            resp.signature = repores.U_Signature;
            resp.manual_production = repores.U_Manual_Production;
            resp.prepaid_Account = repores.U_Prepaid_Account;
            
            resp.Guid = repores.U_Guid;

            resp.can_edit = repores.U_Can_Edit;
            resp.can_cancel = repores.U_Can_Cancel;


            return resp;
        }
        public ResetPass resetPass(ResetPass res) { 
        return _usersRepository.resetPass(res);
        }
        public GetUserResp GetUserAuth(GetUserReq Req)
        {
            GetUserResp response = new GetUserResp();
            response.user = _usersRepository.GetUser(Req.idUser);
            if (response.user != null)
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success);
            else
                response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.serverError);
            return response;
        }
        public UserRights GetUserRights(int userid)
        {
            var resp = new UserRights();
            var users= _usersRepository.GetUserRights(userid);
            if (users.U_Is_Admin == true)
            {
                resp = new UserRights
                {
                    Preview_Total_Only = false,
                    Hide_Premium_Info = false,
                    Fixed_Additional_Fees = true,
                    Allow_Cancellation = true,
                    Cancellation_SubAgent = true,
                    Preview_Net = true,
                    Agents_Creation = true,
                    Agents_View = true,
                    Agents_Commission_ReportView = true,
                    SubAgents_Commission_ReportView = true,
                    Multi_Lang_Policy = true,
                    Active = true,
                    Is_Admin = true,
                    Commission = users.U_Commission ?? 0,
                    VAT = users.U_VAT ?? 0, 
                    Stamp = users.U_Stamp,
                    Retention = users.U_Retention,
                    Addional_Fees = users.U_Additional_Fees,
                    Max_Additional_Fees = users.U_Max_Additional_Fees,
                    Tax = users.U_Tax,
                    Tax_Type = users.U_Tax_Type,
                    can_cancel=true,
                    can_edit=true,
                    Currency = users.U_Currency,
                    CurrencySymbol = users.U_CurrencySymbol,
                    CurrencyRate = users.U_CurrencyRate
                    
                };
            }
            else
            {
                resp = new UserRights
                {
                    Fixed_Additional_Fees = users.U_Fixed_Additional_Fees,
                    Allow_Cancellation = users.U_Allow_Cancellation,
                    Cancellation_SubAgent = users.U_Cancellation_SubAgent,
                    Preview_Total_Only = users.U_Preview_Total_Only,
                    Preview_Net = users.U_Preview_Net,
                    Agents_Creation = users.U_Agents_Creation,
                    Agents_View = users.U_Agents_View,
                    Agents_Commission_ReportView = users.U_Agents_Commission_ReportView,
                    SubAgents_Commission_ReportView = users.U_SubAgents_Commission_ReportView,
                    Multi_Lang_Policy = users.U_Multi_Lang_Policy,
                    Hide_Premium_Info = users.U_Hide_Premium_Info,
                    Active = users.U_Active,
                    Is_Admin = users.U_Is_Admin,
                    Commission = users.U_Commission ?? 0,
                    VAT = users.U_VAT ?? 0,
                    Stamp = users.U_Stamp,
                    Retention = users.U_Retention,
                    Addional_Fees = users.U_Additional_Fees,
                    Max_Additional_Fees = users.U_Max_Additional_Fees,
                    Tax = users.U_Tax,
                    Tax_Type = users.U_Tax_Type,
                    can_cancel = users.U_Can_Cancel,
                    can_edit = users.U_Can_Edit,
                    Currency = users.U_Currency,
                    CurrencySymbol = users.U_CurrencySymbol,
                    CurrencyRate = users.U_CurrencyRate,
                    prepaid_Account = users.U_Prepaid_Account

                };
            }
             
            
            return resp;
        }
        public string getUserPass(int userid)
        {
            return _usersRepository.getUserPass(userid);
        }
        public UserProductResp ModifyUsersProduct(UsProReq req)
        {
            return _usersRepository.ModifyUsersProduct(req);
        }
        public UserProductResp SaveUploadedLogo(UsProReq req)
        {
            return _usersRepository.SaveUploadedLogo(req);
        }
        public List<TR_UsersProduct> GetUsersProduct(int userid)
        {
            return _usersRepository.GetUsersProduct(userid);
        }
        public List<LookUpp> GetUsersChildren(int userid)
        {
            return _usersRepository.GetUsersChildren(userid);
        }
        public List<TR_Users> GetListedUserWithChildren(int userid)
        {
            return _usersRepository.GetListedUserWithChildren(userid);
        }
        public UserProductResp clearUploadedLogo(int userid)
        {
            return _usersRepository.clearUploadedLogo(userid);
        }
        public void CopyParentAttachments(int parentId, int childId,string uploadsDirectory)
        {
            //var parentPath = Path.Combine(uploadsDirectory, parentId.ToString(), "Conditions");
            //var childPath = Path.Combine(uploadsDirectory, childId.ToString(), "Conditions");
            var parentPath = Path.Combine(uploadsDirectory, parentId.ToString());
            var childPath = Path.Combine(uploadsDirectory, childId.ToString());
            if (Directory.Exists(parentPath))
            {
                // Create the destination directory if it doesn't exist
                if (!Directory.Exists(childPath))
                {
                    Directory.CreateDirectory(childPath);
                }
                CopyFolders(parentPath, childPath);
                // Get all files in the source directory
                //string[] files = Directory.GetFiles(parentPath);

                //// Copy each file to the destination directory
                //foreach (string filePath in files)
                //{
                //    string fileName = Path.GetFileName(filePath);
                //    string destinationFilePath = Path.Combine(childPath, fileName);

                //    // Copy the file
                //    File.Copy(filePath, destinationFilePath, true); // Set the third parameter to 'true' to overwrite existing files

            //}

                
            }
            //else
            //{
            //    Console.WriteLine("Source directory does not exist.");
            //}
        }
        static void CopyFolders(string sourceDir, string destinationDir)
        {
            // Check if the source directory exists
            if (!Directory.Exists(sourceDir))
            {
                Console.WriteLine("Source directory does not exist.");
                return;
            }

            // Create the destination directory if it doesn't exist
            if (!Directory.Exists(destinationDir))
            {
                Directory.CreateDirectory(destinationDir);
            }

            // Get DirectoryInfo object for source directory
            DirectoryInfo sourceDirInfo = new DirectoryInfo(sourceDir);

            // Copy subdirectories
            foreach (DirectoryInfo dir in sourceDirInfo.GetDirectories())
            {
                // Construct the destination subdirectory path
                string destSubDir = Path.Combine(destinationDir, dir.Name);

                // Recursively copy the subdirectory
                CopyFolders(dir.FullName, destSubDir);
            }

            // Copy files from source directory to destination directory
            foreach (FileInfo file in sourceDirInfo.GetFiles())
            {
                // Construct the destination file path
                string destFile = Path.Combine(destinationDir, file.Name);

                // Copy the file to the destination directory
                file.CopyTo(destFile, true);
            }
        }
    }
}
