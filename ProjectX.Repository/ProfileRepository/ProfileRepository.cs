using ProjectX.Entities.AppSettings;
using ProjectX.Entities.dbModels;
//using ProjectX.Entities.Models.Case;
using ProjectX.Entities.Models.Profile;
//using ProjectX.Entities.Models.Travel;
using ProjectX.Entities.TableTypes;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Transactions;
using Utilities;

namespace ProjectX.Repository.ProfileRepository
{
    public class ProfileRepository : IProfileRepository
    {
        private SqlConnection _db;
        private readonly TrAppSettings _appSettings;
        public ProfileRepository(IOptions<TrAppSettings> appIdentitySettingsAccessor)
        {
            _appSettings = appIdentitySettingsAccessor.Value;
        }
        public List<ProfileContract> getContracts(SearchProfilesReq req)
        {
            List<ProfileContract> profiles = new List<ProfileContract>();

            var param = new DynamicParameters();
            param.Add("@IdProfile", req.idProfileType);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("tr_profilecontract_select_all", param, commandType: CommandType.StoredProcedure))
                {
                    profiles = result.Read<ProfileContract>().ToList();
                }
            }
            return profiles;
        }
        public List<Profile> getProfiles(SearchProfilesReq req)
        {
            List<Profile> profiles = new List<Profile>();
            //List<Profile> profilesToReturn = new List<Profile>();

            var param = new DynamicParameters();
            param.Add("@Name", req.profileName);
            param.Add("@IdProfileType", req.idProfileType);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("tr_profile_select_all", param, commandType: CommandType.StoredProcedure))
                {
                    //try
                    //{
                    profiles = result.Read<Profile>().ToList();
                    //if (profiles != null && profiles.Count > 0)
                    //{
                    //    foreach (Profile profile in profiles)
                    //    {
                    //        Profile profileToAdd = new Profile();

                    //        var param1 = new DynamicParameters();
                    //        param1.Add("@IdProfile", profile.IdProfile);

                    //        using (SqlMapper.GridReader result1 = _db.QueryMultiple("tr_profile_select", param1, commandType: CommandType.StoredProcedure))
                    //        {
                    //            profileToAdd = result1.ReadFirstOrDefault<Profile>();
                    //            if (profileToAdd != null)
                    //            {
                    //                profileToAdd.contacts = result1.Read<Contact>().ToList();
                    //                profileToAdd.countries = result1.Read<LookUpp>().ToList();
                    //                profileToAdd.profileTypes = result1.Read<ProfileService>().ToList();
                    //            }
                    //        }
                    //        profilesToReturn.Add(profileToAdd);
                    //    }
                    //}
                    //}
                    //catch (Exception ex)
                    //{
                    //    throw ex;
                    //}
                }
            }
            return profiles;
        }

        public Profile getProfile(int IdProfile, bool withGOP)
        {
            Profile profile = new Profile();
            var param = new DynamicParameters();
            param.Add("@IdProfile", IdProfile);
            param.Add("@WithGOP", withGOP);

            using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
            {
                using (SqlMapper.GridReader result = _db.QueryMultiple("tr_profile_select", param, commandType: CommandType.StoredProcedure))
                {
                    profile = result.ReadFirstOrDefault<Profile>();
                    if (profile != null)
                    {
                        //profile.contacts = result.Read<Contact>().ToList();
                        profile.countries = result.Read<LookUpp>().ToList();
                        profile.profileTypes = result.Read<ProfileService>().ToList();
                        //profile.caseSetups = result.Read<ProfileCaseSetup>().ToList();
                        //profile.additionalCoverage= result.Read<AdditionalCoverage>().ToList();
                    }
                }
            }
            return profile;
        }

        public Profile saveProfile(SaveProfileReq req, int IdUser, out int status)
        {
            Profile profile = new Profile();

            DataTable dtContacts = new DataTable();
            DataTable dtCountries = new DataTable();
            DataTable dtProfileTypes = new DataTable();
            DataTable dtAdditionalCoverage = new DataTable();
            DataTable dtCaseSetup = new DataTable();

            //List<tr_Contact> contacts = new List<tr_Contact>();
            List<TR_IntegerID> countries = new List<TR_IntegerID>();
            List<TR_IntegerID> profileTypes = new List<TR_IntegerID>();
            List<TR_IntegerID> additionalCoverage = new List<TR_IntegerID>();
            List<TR_ProfileCaseSetup> caseSetup = new List<TR_ProfileCaseSetup>();

          
            if (req.countries != null && req.countries.Count > 0)
            {
                foreach (int country in req.countries)
                {
                    countries.Add(new TR_IntegerID
                    {
                        ID = country
                    });
                }
            }
            if (req.profileTypes != null && req.profileTypes.Count > 0)
                        {
                            foreach (int profileType in req.profileTypes)
                            {
                                profileTypes.Add(new TR_IntegerID
                                {
                                    ID = profileType
                                });
                            }
                        }
            if (req.additionalCoverage != null && req.additionalCoverage.Count > 0)
            {
                foreach (int addCoverage in req.additionalCoverage)
                {
                    additionalCoverage.Add(new TR_IntegerID
                    {
                        ID = addCoverage
                    });
                }
            }


            dtCountries = ObjectConvertor.ListToDataTable<TR_IntegerID>(countries);
            dtProfileTypes = ObjectConvertor.ListToDataTable<TR_IntegerID>(profileTypes);
            dtAdditionalCoverage = ObjectConvertor.ListToDataTable<TR_IntegerID>(additionalCoverage);
            dtCaseSetup = ObjectConvertor.ListToDataTable<TR_ProfileCaseSetup>(caseSetup);

            var param = new DynamicParameters();
            param.Add("@IdProfile", req.IdProfile);
            param.Add("@Name", req.Name);
            param.Add("@IntCode", req.IntCode);
            param.Add("@Phone", req.Phone);
            param.Add("@Email", req.Email);
            param.Add("@IdCurrency", req.IdCurrency);
            //param.Add("@IdFeesType", req.IdFeesType);
            //param.Add("@SimpleCaseFees", req.SimpleCaseAmount);
            //param.Add("@ComplexCaseFees", req.ComplexCaseAmount);
            param.Add("@EmailNotificationEnabled", req.EmailNotificationEnabled);
            param.Add("@IdUser", IdUser);
            param.Add("@ApprovalRequired", req.ApprovalRequired);
            param.Add("@AccountNo", req.AccountNo);
            param.Add("@Contacts", dtContacts.AsTableValuedParameter("tr_Contact"));
            param.Add("@Countries", dtCountries.AsTableValuedParameter("tr_IntegerID"));
            param.Add("@ProfileTypes", dtProfileTypes.AsTableValuedParameter("tr_IntegerID"));
            param.Add("@AddCoverage", dtAdditionalCoverage.AsTableValuedParameter("tr_IntegerID"));
            param.Add("@ProfileCaseSetup", dtCaseSetup.AsTableValuedParameter("tr_ProfileCaseSetup"));
            param.Add("@Status", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);

            using (TransactionScope scope = new TransactionScope())
            {
                using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
                {
                    using (SqlMapper.GridReader result = _db.QueryMultiple("tr_profile_save", param, commandType: CommandType.StoredProcedure))
                    {
                        profile = result.ReadFirstOrDefault<Profile>();
                        if (profile != null)
                        {
                            //profile.contacts = result.Read<Contact>().ToList();
                            profile.countries = result.Read<LookUpp>().ToList();
                            profile.profileTypes = result.Read<ProfileService>().ToList();
                            //profile.caseSetups = result.Read<ProfileCaseSetup>().ToList();
                        }
                    }
                    status = param.Get<int>("@Status");
                }
                scope.Complete();
            }
            return profile;
        }

        public void deleteProfile(int IdProfile, int IdUser)
        {
            Profile profile = new Profile();
            var param = new DynamicParameters();
            param.Add("@IdProfile", IdProfile);
            param.Add("@IdUser", IdUser);

            using (TransactionScope scope = new TransactionScope())
            {
                using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
                {
                    _db.Execute("tr_profile_delete", param, commandType: CommandType.StoredProcedure);
                }
                scope.Complete();
            }
        }

        public void SaveGop(int IdProfile, int IdDocumentType, string htmlText)
        {
            var param = new DynamicParameters();
            param.Add("@IdProfile", IdProfile);
            param.Add("@IdDocumentType", IdDocumentType);
            param.Add("@htmlText", htmlText);

            using (TransactionScope scope = new TransactionScope())
            {
                using (_db = new SqlConnection(_appSettings.connectionStrings.ccContext))
                {
                    _db.Execute("tr_profile_save_gop", param, commandType: CommandType.StoredProcedure);
                }
                scope.Complete();
            }
        }

   

    }
}
