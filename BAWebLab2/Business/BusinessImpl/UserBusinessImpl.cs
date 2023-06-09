﻿using Dapper;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Reflection.Metadata;
using System.Text.Json;
using BAWebLab2.LibCommon;

namespace BAWebLab2.Business
{
    public class UserBusinessImpl : IUserBusiness
    {
        private readonly UserRepository _userRepository;


        public UserBusinessImpl(UserRepository userRepository)
        {

            _userRepository = userRepository;
        }
        

        public Object getAllUser(JsonDocument json)
        {

            var t = new Object();

            var v = json.RootElement.GetProperty("user_id");

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("pUser_id", int.Parse(json.RootElement.GetProperty("user_id").ToString()));
            parameters.Add("pPage_number", int.Parse(json.RootElement.GetProperty("page_number").ToString()));
            parameters.Add("pPage_size", int.Parse(json.RootElement.GetProperty("page_size").ToString()));
            parameters.Add("pText_search", json.RootElement.GetProperty("text_search").ToString());
            parameters.Add("pBirthday_from", json.RootElement.GetProperty("birthday_from").ToString() == "" ? new DateTime(1900, 1, 1) :
                DateTime.Parse(json.RootElement.GetProperty("birthday_from").ToString()), DbType.DateTime2, ParameterDirection.Input);
            parameters.Add("pBirthday_to", json.RootElement.GetProperty("birthday_to").ToString() == "" ? new DateTime(1900, 1, 1) :
                DateTime.Parse(json.RootElement.GetProperty("birthday_to").ToString()), DbType.DateTime2, ParameterDirection.Input);
            parameters.Add("pGioi_tinh_search", int.Parse(json.RootElement.GetProperty("gioi_tinh_search").ToString()));


            t = _userRepository.GetAllProducts( parameters);

            return t;
        }



        public Object login(JsonDocument json)
        {

            var t = new Object();
 
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("pUsername", (json.RootElement.GetProperty("username").ToString())) ;
            parameters.Add("pPass", (LibCommon.LibCommon.HashMD5(json.RootElement.GetProperty("pass").ToString())));
            parameters.Add("pret", 0,DbType.Int64,ParameterDirection.Output);
             
            t = _userRepository.Login(parameters);

            return t;
        }

        public Object addUser(JsonDocument json)
        {

            var t = new Object();
 
            DynamicParameters parameters = new DynamicParameters();

            parameters.Add("pusername", (json.RootElement.GetProperty("username").ToString()));
            parameters.Add("ppassword", (LibCommon.LibCommon.HashMD5(json.RootElement.GetProperty("password").ToString())));
            parameters.Add("pho_ten", (json.RootElement.GetProperty("ho_ten").ToString()));
            parameters.Add("pgioi_tinh", (json.RootElement.GetProperty("gioi_tinh").ToString()));
            parameters.Add("psdt", (json.RootElement.GetProperty("sdt").ToString()));
            parameters.Add("pngay_sinh", (json.RootElement.GetProperty("ngay_sinh").ToString()));
            parameters.Add("pemail", (json.RootElement.GetProperty("email").ToString()));
            parameters.Add("pis_delete", (false));
            parameters.Add("pis_active", (json.RootElement.GetProperty("is_active").ToString()));
            parameters.Add("pis_admin", (json.RootElement.GetProperty("is_admin").ToString()));
            parameters.Add("puser_id", (json.RootElement.GetProperty("user_id").ToString()));
            
            parameters.Add("pret", 0, DbType.Int64, ParameterDirection.Output);
             
            t = _userRepository.AddUser(parameters);

            return t;
        }

        public Object editUser(JsonDocument json)
        {

            var t = new Object();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("pid", (json.RootElement.GetProperty("id").ToString().Trim()));
            parameters.Add("pusername", (json.RootElement.GetProperty("username").ToString().Trim()));
            parameters.Add("ppassword", (LibCommon.LibCommon.HashMD5(json.RootElement.GetProperty("password").ToString())));
            parameters.Add("pho_ten", (json.RootElement.GetProperty("ho_ten").ToString()));
            parameters.Add("pgioi_tinh", (json.RootElement.GetProperty("gioi_tinh").ToString()));
            parameters.Add("psdt", (json.RootElement.GetProperty("sdt").ToString()));
            parameters.Add("pngay_sinh", (json.RootElement.GetProperty("ngay_sinh").ToString()));
            parameters.Add("pemail", (json.RootElement.GetProperty("email").ToString()));
            parameters.Add("pis_delete", (false));
            parameters.Add("pis_active", (json.RootElement.GetProperty("is_active").ToString()));
            parameters.Add("pis_admin", (json.RootElement.GetProperty("is_admin").ToString()));
            parameters.Add("puser_id", (json.RootElement.GetProperty("user_id").ToString()));

            parameters.Add("pret", 0, DbType.Int64, ParameterDirection.Output);

            t = _userRepository.EditUser(parameters);

            return t;
        }

        public Object deleteUser(JsonDocument json)
        {

            var t = new Object();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("pid", (json.RootElement.GetProperty("id").ToString()));
            
            parameters.Add("puser_id", (json.RootElement.GetProperty("user_id").ToString()));

            parameters.Add("pret", 0, DbType.Int64, ParameterDirection.Output);

            t = _userRepository.AddUser(parameters);

            return t;
        }


    }
}
