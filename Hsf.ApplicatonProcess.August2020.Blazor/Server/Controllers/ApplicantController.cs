using System.Collections.Generic;
using System.Threading.Tasks;
using Hsf.ApplicatonProcess.August2020.Domain.Models;
using Hsf.ApplicatonProcess.August2020.Domain.Services;
using Hsf.ApplicatonProcess.August2020.Domain.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Hsf.ApplicatonProcess.August2020.Blazor.Server.Controllers
{
    [Route("[controller]")]
    public class ApplicantController : ControllerBase
    {
        private readonly IApplicantService _applicantService;

        public ApplicantController(IApplicantService applicantService)
        {
            _applicantService = applicantService;
        }

        [HttpPost]
        [Route("Insert")]
        public async Task<IActionResult> Insert([FromBody] Applicant applicant)
        {
            var validator = new ApplicantValidator();
            var result = validator.Validate(applicant);
            if (!result.IsValid)
            {
                return BadRequest(string.Join("\n", result.Errors));
            }
            await _applicantService.Save(applicant);
            return Ok();
        }

        [HttpGet]
        [Route("Get")]
        public List<Applicant> Get()
        {
            return _applicantService.Get();
        }

        [HttpDelete]
        [Route("Delete")]
        public void Delete(int id)
        {
            _applicantService.Delete(id);
        }

        [HttpGet]
        [Route("GetById")]
        public Applicant GetById(int id)
        {
            return _applicantService.GetById(id);
        }

        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Edit([FromBody] Applicant applicant)
        {
            await _applicantService.Edit(applicant);
            return Ok();
        }
    }
}
