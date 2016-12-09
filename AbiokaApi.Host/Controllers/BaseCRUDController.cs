using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.Infrastructure.Common.Domain;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AbiokaApi.Host.Controllers
{
    public abstract class BaseCrudController<T> : BaseApiController where T : IEntity
    {
        private readonly ICrudService<T> crudService;

        public BaseCrudController(ICrudService<T> crudService) {
            this.crudService = crudService;
        }

        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage Delete([FromUri]Guid id) {
            crudService.Delete(id);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [Route("")]
        [HttpGet]
        public virtual HttpResponseMessage Get([FromUri]int page, [FromUri]int limit, [FromUri]string order) {
            var result = crudService.GetWithPage(page, limit, order);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPut]
        [Route("")]
        public HttpResponseMessage Update([FromBody]T user) {
            crudService.Update(user);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
