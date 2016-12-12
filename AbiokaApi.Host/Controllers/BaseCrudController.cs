using AbiokaApi.ApplicationService.Abstractions;
using AbiokaApi.Infrastructure.Common.Domain;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AbiokaApi.Host.Controllers
{
    public abstract class BaseCrudController<T> : BaseReadController<T> where T : IIdEntity<Guid>
    {
        private readonly ICrudService<T> crudService;

        public BaseCrudController(ICrudService<T> crudService)
            : base(crudService) {
            this.crudService = crudService;
        }

        [HttpDelete]
        [Route("{id}")]
        public HttpResponseMessage Delete([FromUri]Guid id) {
            crudService.Delete(id);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPut]
        [Route("")]
        public HttpResponseMessage Update([FromBody]T user) {
            crudService.Update(user);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
