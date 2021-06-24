using Microsoft.Extensions.Logging;
using ProjectTest.Models;
using ProjectTest.Models.Request;
using ProjectTest.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectTest.WebApi.Services
{
    public class UserServices : IUserServices
    {
        private readonly DBtestContext _context;
        private readonly ILogger<UserServices> _logger;
        public UserServices(DBtestContext context, ILogger<UserServices> logger)
        {
            _context = context;
            _logger = logger;
        }

        public ResponseModel SaveUserAccount(UserRequestModel request)
        {
            ResponseModel response = new ResponseModel();
            using (var dbContextTransaction = _context.Database.BeginTransaction()) 
            {
                try
                {

                    UserEntity userid = _context.userEntities.Where(w => w.UserName == request.UserName.Trim()).FirstOrDefault();

                    if (userid == null)
                    {

                        userid  = new UserEntity()
                        {
                            UserName = request.UserName,
                            EmailAddress = request.EmailAddress,
                            RealName = request.RealName,
                            ModifiedDate = DateTime.Now
                        };

                        _context.userEntities.Add(userid);
                        _context.SaveChanges();

                        userid.UsersID = userid.UsersID;

                        WebpagesMembershipEntity members = _context.membershipEntities.Where(w => w.UserId == userid.UsersID).FirstOrDefault();

                        if (members == null)
                        {
                            var memData = new WebpagesMembershipEntity()
                            {
                                UserId = userid.UsersID,
                                CreateDate = DateTime.Now,
                                PasswordFailuresSinceLastSuccess = 0,
                                Password = request.Password,
                                PasswordSalt = "O"

                            };
                            _context.membershipEntities.Add(memData);
                            _context.SaveChanges();
                        }

                        dbContextTransaction.Commit();
                    }

                    response.data = userid;
                    response.success = true;
                    response.message = "OK";
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    dbContextTransaction.Rollback();
                    response.success = false;      
                    response.error = ex.Message;
                }
            }

                
            return response;
        }

        public ResponseModel UserCheck(UserRequestModel request)
        {
            ResponseModel response = new ResponseModel();
            try
            {
                UserEntity userid = _context.userEntities.Where(w => w.UserName == request.UserName.Trim()).FirstOrDefault();

                if (userid != null)
                {

                    WebpagesMembershipEntity members = _context.membershipEntities.Where(w => w.UserId == userid.UsersID && w.Password == request.Password).FirstOrDefault();

                    if (members != null)
                    {

                        var data = (from a in _context.userEntities
                                    from b in _context.membershipEntities.Where(w => w.UserId == a.UsersID).DefaultIfEmpty()
                                    where a.UsersID == userid.UsersID && b.UserId == userid.UsersID
                                    select new
                                    {
                                        UsersID = a.UsersID,
                                        RealName = a.RealName,
                                        UserName = a.UserName

                                    }).FirstOrDefault();
                        
                        
                        response.data = data;
                        response.success = true;
                        response.message = "ยินดีต้อนรับ" + userid.RealName;
                    }
                    else 
                    {
                        response.success = false;
                        response.message = "รหัสไม่ถูกต้อง";
                    }
                }
                else 
                {
                    response.message = "ไม่พบ ผู้ใช้งานนี้" + request.UserName;
                    response.success = false; 
                    
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                response.success = false;
                response.error = ex.Message;
            }

            return response;
        }
    }
}
