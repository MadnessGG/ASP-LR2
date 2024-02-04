namespace WebApplication4.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using System;

    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly CompanyService _companyService;
        private readonly IConfiguration _configuration;

        public CompanyController(CompanyService companyService, IConfiguration configuration)
        {
            _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpGet("combinedInfo")]
        public ActionResult<object> GetCombinedInfo()
        {
            try
            {
                var maxEmployeesCompany = _companyService.GetCompanyWithMostEmployees();

                if (maxEmployeesCompany == null)
                {
                    return NotFound("No company with the most employees found.");
                }

                var personalInfo = _configuration.GetSection("PersonalInfo").Get<PersonalInfo>();

                if (personalInfo == null)
                {
                    return BadRequest("PersonalInfo is missing or invalid in configuration.");
                }

                return new
                {
                    MaxEmployeesCompany = maxEmployeesCompany,
                    PersonalInfo = personalInfo
                };
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
