using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.ApplicationService.DTOs;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AbiokaApi.Host.Controllers
{
    public abstract class BaseCrudController<T> : BaseReadController<T> where T : DTO
    {
        private readonly ICrudService<T> crudService;

        public BaseCrudController(ICrudService<T> crudService)
            : base(crudService) {
            this.crudService = crudService;
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Add([FromBody]T entity) {
            crudService.Add(entity);

            var response = Request.CreateResponse(HttpStatusCode.Created, entity);
            return response;
        }

        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage Delete([FromUri]Guid id) {
            crudService.Delete(id);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
        [Route("")]
        public HttpResponseMessage Update([FromBody]T entity) {
            crudService.Update(entity);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
