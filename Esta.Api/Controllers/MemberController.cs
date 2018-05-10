using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Esta.Api.Models;
using Esta.Api.Repository;

namespace Esta.Api.Controllers
{
    [RoutePrefix("api/Member")]
    public class MemberController : ApiController
    {
        [HttpGet]
        [Route("GetMembers")]
        public OutputModels GetMembers()
        {
            OutputModels member = new OutputModels();
            try
            {
                ppobDataContextDataContext db = new ppobDataContextDataContext();
                var data = db.Members.Select(m => m);
                if (data == null)
                {
                    member.api_status = "Empty";
                    member.api_message =string.Empty;
                }
                else
                {
                    member.api_status = "Success";
                    member.api_message = string.Empty;
                    member.items = data;
                }


            }
            catch (Exception ex)
            {

                member.api_status = "Error";
                member.api_message = ex.Message;
                member.items = null;
            }
            return member;
        }

        [HttpGet]
        [Route("GetMemberById")]
        public OutputModels GetMemberById(int Id)
        {
            OutputModels member = new OutputModels();

            try
            {
                ppobDataContextDataContext db = new ppobDataContextDataContext();
                var data = from i in db.Members
                           where i.MemberId == Id
                           select new {
                               i.MemberId,
                               i.Nama,
                               i.NoTelpon,
                               i.Password,
                               i.Photo,
                               i.Token,
                               i.TypeMember,
                               i.TglBergabung,
                               i.Status
                           };

                if (data == null)
                {
                    member.api_status = "Empty";
                    member.api_message = string.Empty;
                }
                else
                {
                    member.api_status = "Success";
                    member.api_message = string.Empty;
                    member.items = data;
                }
            }
            catch (Exception ex)
            {

                member.api_status = "Error";
                member.api_message = ex.Message;
                member.items = null;
            }
            return member;
        }

        [HttpPost]
        [Route("CreateMember")]
        public OutputModels CreateMember(int MemberId, string Photo, 
                                string Nama, string Email, string NoTelpon,
                                string KodeReverall, string Password, string TypeMember, DateTime TglBergabung,
                                string PhotoKTP, string PhotoRekening, string PhotoKTPSelfie, int Status, string VoidRemark,
                                string Token, int Flag, string CreateBy, DateTime CreateDate)
        {
            OutputModels member = new OutputModels();
            try
            {
                ppobDataContextDataContext db = new ppobDataContextDataContext();
                Member M = new Member();
                M.MemberId = MemberId;
                M.Photo = Photo;
                M.Nama = Nama;
                M.Email = Email;
                M.NoTelpon = NoTelpon;
                M.KodeReverall = KodeReverall;
                M.Password = Password;
                M.TypeMember = TypeMember;
                M.TglBergabung = TglBergabung;
                M.PhotoKTP = PhotoKTP;
                M.PhotoRekening = PhotoRekening;
                M.PhotoKTPSelfie = PhotoKTPSelfie;
                M.Status = Status;
                M.VoidRemark = VoidRemark;
                M.Token = Token;
                M.Flag = Flag;
                M.CreateBy = CreateBy;
                M.CreateDate = CreateDate;

                db.Members.InsertOnSubmit(M);
                db.SubmitChanges();

                int IsSave = (from i in db.Members
                              where i.MemberId == MemberId
                              select i).Count();

                if (IsSave > 0)
                {
                    member.api_status = "Success";
                }
                else
                {
                    member.api_status = "Error";
                }
            }
            catch (Exception ex)
            {
                member.api_status = "Error";
                member.api_message = ex.Message;
                member.items = null;
            }
            return member;
        }
    }
}
