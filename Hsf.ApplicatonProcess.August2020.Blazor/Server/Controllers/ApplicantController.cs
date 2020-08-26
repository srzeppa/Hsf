using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hsf.ApplicatonProcess.August2020.Domain.Models;
using Hsf.ApplicatonProcess.August2020.Domain.Providers;
using Hsf.ApplicatonProcess.August2020.Domain.Services;
using Hsf.ApplicatonProcess.August2020.Domain.Storage;
using Hsf.ApplicatonProcess.August2020.Domain.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Hsf.ApplicatonProcess.August2020.Blazor.Server.Controllers
{
    [Route("[controller]")]
    public class ApplicantController : ControllerBase
    {
        private readonly IApplicantService _applicantService;
        private readonly ICountriesProvider _countriesProvider;
        private readonly ILogger<ApplicantController> _logger;

        public ApplicantController(IApplicantService applicantService,
            ICountriesProvider countriesProvider,
            ILogger<ApplicantController> logger)
        {
            _applicantService = applicantService;
            _countriesProvider = countriesProvider;
            _logger = logger;
        }

        [HttpPost]
        [Route("Insert")]
        public ActionResult Insert([FromBody] Applicant applicant)
        {
            _logger.LogInformation("Start inserting applicant");
            var validator = new ApplicantValidator(_countriesProvider);
            var result = validator.Validate(applicant);
            if (!result.IsValid)
            {
                return BadRequest(string.Join("\n", result.Errors));
            }

            ApplicantStorage.Add(applicant);
            return Ok();
        }

        [HttpPost]
        [Route("Confirm-Insert")]
        public async Task<IActionResult> ConfirmInsert([FromBody] Applicant applicant)
        {
            try
            {
                _logger.LogInformation("Start confirm inserting applicant");
                await _applicantService.Save(applicant);
                ApplicantStorage.Remove(applicant);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        [Route("Get")]
        public List<Applicant> Get()
        {
            _logger.LogInformation("Start getting applicant from database");
            return _applicantService.Get();
        }

        [HttpDelete]
        [Route("Delete")]
        public void Delete(int id)
        {
            _logger.LogInformation($"Start deleting applicant with id: {id}");
            _applicantService.Delete(id);
        }

        [HttpGet]
        [Route("GetById")]
        public Applicant GetById(int id)
        {
            _logger.LogInformation($"Start getting applicant with id {id}");
            return _applicantService.GetById(id);
        }

        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Edit([FromBody] Applicant applicant)
        {
            _logger.LogInformation($"Start edit applicant with id {applicant.Id}");
            await _applicantService.Edit(applicant);
            return Ok();
        }

        [HttpGet]
        [Route("GetFromStorage")]
        public Applicant GetFromStorage(int id)
        {
            return ApplicantStorage.Get(id);
        }
    }
}
