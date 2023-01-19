using API2.Context;
using API2.Models;
using API2.Repositories.Data;
using API2.Repository.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;




namespace API2.Base
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]

    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;
        private MyContext myContext;

        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        public BaseController(MyContext myContext)
        {
            this.myContext = myContext;
        }

        [HttpGet]
        [EnableCors("AllowOrigin")]
        public ActionResult<Entity> Get()
        {
            var result = repository.Get();
            return StatusCode(200,
            new
            {
                status = HttpStatusCode.OK,
                message = "Data Ditemukan",
                Data = result
            });
        }
        [HttpPost]
        [EnableCors("AllowOrigin")]
        public ActionResult Insert(Entity entity)
        {
            var insert = repository.Insert(entity);
            if (insert >= 1)
            {
                return StatusCode(200, new
                {
                    status = HttpStatusCode.OK,
                    message = "Data Berhasil Dimasukkan",
                    Data = insert
                });
            }
            else
            {
                return StatusCode(500, new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Gagal Memasukkan Data"
                });
            }
        }

        [HttpGet]
        [Route("{Key}")]
        [EnableCors("AllowOrigin")]
        public ActionResult Get(Key key)
        {
            var data = repository.Get(key);
            if (data != null)
            {
                return StatusCode(200, new
                {
                    status = HttpStatusCode.OK,
                    message = "Data Ditemukkan",
                    Data = data
                });
            }
            else
            {
                return StatusCode(500, new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Data Gagal Ditemukkan"
                });
            }
        }

        [HttpDelete]
        [Route("{Key}")]
        [EnableCors("AllowOrigin")]
        public ActionResult Delete(Key key)
        {
            var del= repository.Delete(key);
            if (del >=1 )
            {
                return StatusCode(200, new
                {
                    status = HttpStatusCode.OK,
                    message = "Data Employee {key} Deleted",
                    Data = del
                });
            }
            else
            {
                return StatusCode(500, new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Data Tidak Ditemukan"
                });
            }
            
        }
        [HttpPut]
        public ActionResult Update(Entity entity, Key key)
        {
            var update = repository.Update(entity, key);
            if (update >= 1)
            {
                return StatusCode(200, new
                {
                    status = HttpStatusCode.OK,
                    message = "Data Berhasil Di Ubah",
                    Data = update
                });
            }
            else
            {
                return StatusCode(500, new
                {
                    status = HttpStatusCode.BadRequest,
                    message = "Data Tidak Ditemukan"
                });
            }
        }
        
        
    }
}