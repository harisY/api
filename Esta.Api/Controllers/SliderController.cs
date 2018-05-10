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
    [RoutePrefix("api/Slider")]
    public class SliderController : ApiController
    {
        [HttpGet]
        [Route("GetSliders")]
        public OutputModels GetSliders()
        {
            OutputModels Slider = new OutputModels();
            try
            {
                ppobDataContextDataContext db = new ppobDataContextDataContext();
               
                var data = from tbl in db.Sliders
                           select new
                           {
                               tbl.Id,
                               tbl.Judul,
                               tbl.DesSingkat,
                               tbl.DesPanjang,
                               tbl.Photo
                           };


                Slider.api_status = "1";
                Slider.api_message = "Success";
                Slider.items = data;


            }
            catch (Exception ex)
            {

                Slider.api_status = "0";
                Slider.api_message = "Error - " + ex.Message;
                Slider.items = null;
            }
            return Slider;
        }

    }
}
