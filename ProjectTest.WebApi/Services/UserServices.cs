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
        public UserServices(DBtestContext context)
        {
            _context = context;
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
                            RealName = null,
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

                        response.data = userid;
                        response.success = true;
                        response.message = "OK";
                    }

                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    response.success = false;      
                    response.error = ex.Message;
                }
            }

                
            return response;
        }
    }
}
