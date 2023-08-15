﻿using ProjectX.Entities.dbModels;
using ProjectX.Entities.Models.Users;
using ProjectX.Repository.UsersRepository;
using System;
using System.Collections.Generic;
using System.Text;
using ProjectX.Entities.Resources;
using ProjectX.Entities;
namespace ProjectX.Business.Users
{ 
    public class UsersBusiness : IUsersBusiness
    {
        IUsersRepository _usersRepository;


        public UsersBusiness(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public UsersResp ModifyUser(UsersReq req, string act, int userid)
        {
            UsersResp response = new UsersResp();
            response = _usersRepository.ModifyUser(req, act, userid);
            response.statusCode = ResourcesManager.getStatusCode(Languages.english, StatusCodeValues.success, req.Id == 0 ? SuccessCodeValues.Add : SuccessCodeValues.Update, "Users");
            return response;
           
        }
        public List<TR_Users> GetUsersList(UsersSearchReq req)
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
            resp.agents_Creation_Approval = repores.U_Agents_Creation_Approval;
            resp.agents_Commission_ReportView = repores.U_Agents_Commission_ReportView;
            resp.subAgents_Commission_ReportView = repores.U_SubAgents_Commission_ReportView;
            resp.print_Client_Voucher = repores.U_Print_Client_Voucher;
            resp.multi_Lang_Policy = repores.U_Multi_Lang_Policy;
            resp.tax_Invoice = repores.U_Tax_Invoice;
            resp.hide_Premium_Info = repores.U_Hide_Premium_Info;
            resp.active = repores.U_Active;
            resp.parent_Id = repores.U_Parent_Id;
            resp.max_Additional_Fees = repores.U_Max_Additional_Fees;
            resp.creation_Date = repores.U_Creation_Date;
            resp.have_Parents = repores.U_Have_Parents;


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
                    Fixed_Additional_Fees = true,
                    Allow_Cancellation = true,
                    Cancellation_SubAgent = true,
                    Preview_Total_Only = true,
                    Preview_Net = true,
                    Agents_Creation = true,
                    Agents_Commission_ReportView = true,
                    SubAgents_Commission_ReportView = true,
                    Multi_Lang_Policy = true,
                    Hide_Premium_Info = true,
                    Active = true,
                    Is_Admin = true
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
                    Agents_Commission_ReportView = users.U_Agents_Commission_ReportView,
                    SubAgents_Commission_ReportView = users.U_SubAgents_Commission_ReportView,
                    Multi_Lang_Policy = users.U_Multi_Lang_Policy,
                    Hide_Premium_Info = users.U_Hide_Premium_Info,
                    Active = users.U_Active,
                    Is_Admin = users.U_Is_Admin,
                    Commission = users.U_Commission,
                    Stamp = users.U_Stamp,
                    Addional_Fees = users.U_Additional_Fees,
                    Max_Additional_Fees = users.U_Max_Additional_Fees,
                    VAT = users.U_VAT,
                    Tax = users.U_Tax,
                    Tax_Type = users.U_Tax_Format

                };
            }
             
            
            return resp;
        }
        public string getUserPass(int userid)
        {
            return _usersRepository.getUserPass(userid);
        }

    }
}
