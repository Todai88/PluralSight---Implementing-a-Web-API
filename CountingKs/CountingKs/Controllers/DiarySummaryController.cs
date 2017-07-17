using CountingKs.Data;
using CountingKs.Models;
using CountingKs.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace CountingKs.Controllers
{
    public class DiarySummaryController : BaseApiController
    {
        private ICountingKsIdentityService _identityService;

        public DiarySummaryController(ICountingKsIdentityService identityService,
                                      ICountingKsRepository repo) : base (repo)
        {
            _identityService = identityService;
        }

        public HttpResponseMessage Get(DateTime diaryId)
        {
            try
            {
                var diary = TheRepository.GetDiary(_identityService.CurrentUser, diaryId);

                if (diary == null)
                    return Request.CreateResponse(HttpStatusCode.NotFound, "Could not find diary");

                return Request.CreateResponse(HttpStatusCode.OK, TheModelFactory.CreateSummary(diary));
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
