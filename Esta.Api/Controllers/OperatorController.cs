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
    [RoutePrefix("api/Operator")]
    public class OperatorController : ApiController
    {
        [HttpGet]
        [Route("GetOperators")]
        public OutputModels GetMembers()
        {
            OutputModels Operator = new OutputModels();
            try
            {
                ppobDataContextDataContext db = new ppobDataContextDataContext();
                //var data = db.Operators.Select(m => m);
                var data = from tbl in db.Operators
                           select new
                           {
                               tbl.OperatorId,
                               tbl.Nama,
                               tbl.Flag
                           };

                if (data == null)
                {
                    Operator.api_status = "0";
                    Operator.api_message = string.Empty;
                }
                else
                {
                    Operator.api_status = "1";
                    Operator.api_message = "Success";
                    Operator.items = data;
                }


            }
            catch (Exception ex)
            {

                Operator.api_status = "0";
                Operator.api_message = "Error";
                Operator.items = null;
            }
            return Operator;
        }

        [HttpPost]
        [Route("CreateOperator")]
        public OutputModels CreateOperator(string Nama, int Flag, string CreatedBy, DateTime CreatedDate)
        {
            OutputModels Operators = new OutputModels();
            try
            {
                ppobDataContextDataContext db = new ppobDataContextDataContext();
                Operator OP = new Operator();
                OP.Nama = Nama;
                OP.Flag = Flag;
                OP.CreateBy = CreatedBy;
                OP.CreateDate = CreatedDate;

                db.Operators.InsertOnSubmit(OP);
                db.SubmitChanges();

                int IsSave = (from o in db.Operators
                              where o.Nama == Nama
                              select o).Count();

                if (IsSave > 0)
                {
                    Operators.api_status = "1";
                    Operators.api_message = "Success save data";
                }
                else
                {
                    Operators.api_status = "0";
                    Operators.api_message = "Failed save data";
                }
            }
            catch (Exception ex)
            {

                Operators.api_status = "0";
                Operators.api_message = "Failed save data";
                Operators.items = null;
            }
            return Operators;
        }
    }
}
