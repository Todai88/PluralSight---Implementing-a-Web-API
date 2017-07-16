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

namespace CountingKs.Controllers
{
    public class DiariesController : BaseApiController
    {
        private ICountingKsIdentityService _identityService;

        public DiariesController(ICountingKsRepository repo,
                                 ICountingKsIdentityService identityService)
            : base(repo)
        {
            _identityService = identityService;
        }

        public IEnumerable<DiaryModel> Get()
        {
            var username = _identityService.CurrentUser;
            var results = TheRepository.GetDiaries(username)
                                        .OrderBy(d => d.CurrentDate)
                                        .Take(10)
                                        .ToList()
                                        .Select(d => 
                                            TheModelFactory.Create(d));
            return results;
        }

        public HttpResponseMessage Get(DateTime diaryid)
        {
            var username = _identityService.CurrentUser;
            var result = TheRepository.GetDiary(username, diaryid);

            if (result == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            var response = Request.CreateResponse(HttpStatusCode.OK, 
                                                  TheModelFactory.Create(result));
            // silly
            return response;
        }
    }
}
