namespace WebApplication4
{
    using Microsoft.Extensions.Configuration;

    public class CompanyService
    {
        private readonly IConfiguration _configuration;

        public CompanyService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetCompanyWithMostEmployees()
        {
            var companies = _configuration.GetSection("Companies").GetChildren();

            var maxEmployees = 0;
            var maxCompany = "";

            foreach (var company in companies)
            {
                var employees = int.Parse(company["employees"]);

                if (employees > maxEmployees)
                {
                    maxEmployees = employees;
                    maxCompany = company["name"];
                }
            }

            return maxCompany;
        }
    }

}
