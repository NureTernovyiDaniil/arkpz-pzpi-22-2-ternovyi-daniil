using ChefMate_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChefMate_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly ReportBuilderService _reportBuilderService;
        private readonly ReportService _reportService;
        public ReportController(ReportBuilderService reportBuilderService, ReportService reportService)
        {
            _reportBuilderService = reportBuilderService;
            _reportService = reportService;
        }

        [HttpGet("DetailedReport/period")]
        public async Task<IActionResult> GetPeriodReport(DateTime startDate, DateTime endDate)
        {
            var reportData = await _reportService.GenerateWeeklyReport(startDate, endDate);
            var reportBytes = await _reportBuilderService.GenerateWeekReport(reportData);

            return File(reportBytes, "application/pdf", $"PeriodReport_{startDate:dd-MM-yyyy}-{endDate:dd-MM-yyyy}");
        }

        [HttpGet("DetailedReport/date")]
        public async Task<IActionResult> GetPeriodReport(DateTime targetDate)
        {
            var reportData = await _reportService.GetDailyReportData(targetDate);
            var reportBytes = await _reportBuilderService.GenerateDailyReport(targetDate, reportData);

            return File(reportBytes, "application/pdf", $"PeriodReport_{targetDate:dd-MM-yyyy}");
        }

        [HttpGet("DetailedReport/financial")]
        public async Task<IActionResult> GetFinancialReport(DateTime targetDate)
        {
            var reportData = await _reportService.GetFinancialReport(targetDate);
            var reportBytes = await _reportBuilderService.GenerateFinancialReport(targetDate, reportData);

            return File(reportBytes, "application/pdf", $"FinancialReport_{targetDate:dd-MM-yyyy}");
        }

        [HttpGet("DetailedReport/accessability")]
        public async Task<IActionResult> GetAccessabilityReport(Guid menuId)
        {
            var reportData = await _reportService.GetAccessabilityReport(menuId);
            var reportBytes = await _reportBuilderService.GenerateAccessabilityReport(menuId, reportData);

            return File(reportBytes, "application/pdf", $"PeriodReport_{menuId:dd-MM-yyyy}");
        }
    }
}
