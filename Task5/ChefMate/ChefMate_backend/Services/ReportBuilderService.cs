using System.Reflection;
using System.Text;
using ChefMate_backend.Repositories;
using iText.Html2pdf;
using static ReportService;

namespace ChefMate_backend.Services
{
    public class ReportBuilderService
    {
        private readonly IMenuRepository _menuRepository;
        public ReportBuilderService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        /*public async Task<byte[]> GeneratePdfReport<T>(List<T> data, string reportTitle) where T : class
        {
            var htmlContent = $@"
            <html>
                <head>
                    <title>{reportTitle}</title>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            margin: 20px;
                        }}
                        h1 {{
                            text-align: center;
                            color: #333;
                        }}
                        table {{
                            width: 100%;
                            border-collapse: collapse;
                            margin-top: 20px;
                        }}
                        th, td {{
                            border: 1px solid #ddd;
                            padding: 8px;
                            text-align: left;
                        }}
                        th {{
                            background-color: #f4f4f4;
                            font-weight: bold;
                            color: #333;
                        }}
                        tr:nth-child(even) {{
                            background-color: #f9f9f9;
                        }}
                        tr:hover {{
                            background-color: #f1f1f1;
                        }}
                    </style>
                </head>
                <body>
                    <h1>{reportTitle}</h1>
                    <table>
                        <thead>
                            <tr>
                                {await GenerateTableHeaders<T>()}
                            </tr>
                        </thead>
                        <tbody>
                            {await GenerateTableRows(data)}
                        </tbody>
                    </table>
                </body>
            </html>";

            using var memoryStream = new MemoryStream();

            HtmlConverter.ConvertToPdf(htmlContent, memoryStream);

            return memoryStream.ToArray();
        }


        private async Task<string> GenerateTableHeaders<T>() where T : class
        {
            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            return string.Join("", properties.Select(p => $"<th>{p.Name}</th>"));
        }

        private async Task<string> GenerateTableRows<T>(List<T> data) where T : class
        {
            return string.Join("", data.Select(item =>
            {
                var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                return $"<tr>{string.Join("", properties.Select(p => $"<td>{p.GetValue(item)}</td>"))}</tr>";
            }));
        }*/

        public async Task<byte[]> GenerateWeekReport(WeeklyReport report)
        {
            var html = $@"
            <!DOCTYPE html>
            <html lang='en'>
            <head>
                <meta charset='UTF-8'>
                <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                <title>Weekly Report</title>
                <style>
                    body {{ font-family: Arial, sans-serif; line-height: 1.6; }}
                    h1, h2, h3 {{ color: #333; }}
                    table {{ width: 100%; border-collapse: collapse; margin: 20px 0; }}
                    th, td {{ border: 1px solid #ddd; padding: 8px; text-align: left; }}
                    th {{ background-color: #f4f4f4; }}
                    .summary {{ margin-bottom: 20px; }}
                </style>
            </head>
            <body>
                <h1>Weekly Report</h1>
                <h2>{report.StartDate:dd.MM.yyyy} - {report.EndDate:dd.MM.yyyy}</h2>
    
                <div class='summary'>
                    <p><strong>Total Income:</strong> {report.TotalIncome:C}</p>
                    <p><strong>Average Check:</strong> {report.AverageCheck:C}</p>
                </div>
    
                <h3>Top-3 Profitable Dishes</h3>
                <table>
                    <thead>
                        <tr>
                            <th>Dish</th>
                            <th>Total Income</th>
                        </tr>
                    </thead>
                    <tbody>
                        {string.Join("", report.TopMenuItems.Select(item => $@"
                        <tr>
                            <td>{item.Name}</td>
                            <td>{item.TotalIncome:C}</td>
                        </tr>"))}
                    </tbody>
                </table>

                <h3>Sales by Day</h3>
                <table>
                    <thead>
                        <tr>
                            <th>Day</th>
                            <th>Sales</th>
                        </tr>
                    </thead>
                    <tbody>
                        {string.Join("", report.DailySales.Select(day => $@"
                        <tr>
                            <td>{day.Key}</td>
                            <td>{day.Value:C}</td>
                        </tr>"))}
                    </tbody>
                </table>
            </body>
            </html>";

            using var memoryStream = new MemoryStream();

            HtmlConverter.ConvertToPdf(html, memoryStream);

            return memoryStream.ToArray();
        }

        public async Task<byte[]> GenerateDailyReport(DateTime reportDate, DailyReport reportData)
        {
            var htmlContent = new StringBuilder($@"
            <html>
                <head>
                    <title>Звіт за {reportDate:dd.MM.yyyy}</title>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            margin: 20px;
                            line-height: 1.6;
                        }}
                        h1 {{
                            text-align: center;
                            color: #333;
                        }}
                        .summary {{
                            margin-top: 20px;
                            font-size: 18px;
                        }}
                        table {{
                            width: 100%;
                            border-collapse: collapse;
                            margin-top: 20px;
                        }}
                        th, td {{
                            border: 1px solid #ddd;
                            padding: 10px;
                            text-align: left;
                        }}
                        th {{
                            background-color: #f4f4f4;
                            font-weight: bold;
                            color: #333;
                        }}
                        tr:nth-child(even) {{
                            background-color: #f9f9f9;
                        }}
                        tr:hover {{
                            background-color: #f1f1f1;
                        }}
                    </style>
                </head>
                <body>
                    <h1>Звіт за {reportDate:dd.MM.yyyy}</h1>
                    <div class='summary'>
                        <p><strong>Загальна кількість замовлень:</strong> {reportData.TotalOrders}</p>
                        <p><strong>Сума доходу:</strong> {reportData.TotalIncome:C}</p>
                    </div>
                    <h2>Топ-5 страв</h2>
                    <table>
                        <thead>
                            <tr>
                                <th>№</th>
                                <th>Назва страви</th>
                                <th>Кількість замовлень</th>
                            </tr>
                        </thead>
                        <tbody>");

            for (int i = 0; i < reportData.TopMenuItems.Count; i++)
            {
                var dish = reportData.TopMenuItems[i];
                htmlContent.Append($@"
                <tr>
                    <td>{i + 1}</td>
                    <td>{dish.Name}</td>
                    <td>{dish.TotalIncome}</td>
                </tr>");
            }

            htmlContent.Append(@"
                        </tbody>
                    </table>
                </body>
            </html>");

            using var memoryStream = new MemoryStream();

            HtmlConverter.ConvertToPdf(htmlContent.ToString(), memoryStream);

            return memoryStream.ToArray();
        }

        public async Task<byte[]> GenerateFinancialReport(DateTime reportDate, FinancialReport reportData)
        {
            var htmlContent = new StringBuilder($@"
            <!DOCTYPE html>
            <html lang=""uk"">
                <head>
                    <meta charset=""UTF-8"">
                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                    <title>Фінансовий Звіт - {reportDate:dd.MM.yyyy}</title>
                    <style>
                        body {{
                            font-family: Arial, sans-serif;
                            margin: 20px;
                            line-height: 1.6;
                        }}
                        h1 {{
                            text-align: center;
                            color: #333;
                        }}
                        table {{
                            width: 100%;
                            border-collapse: collapse;
                            margin-top: 20px;
                        }}
                        th, td {{
                            border: 1px solid #ddd;
                            padding: 10px;
                            text-align: left;
                        }}
                        th {{
                            background-color: #007bff;
                            color: white;
                            font-weight: bold;
                        }}
                        tr:nth-child(even) {{
                            background-color: #f9f9f9;
                        }}
                        tr:hover {{
                            background-color: #f1f1f1;
                        }}
                    </style>
                </head>
                <body>
                    <h1>Фінансовий Звіт - {reportDate:dd.MM.yyyy}</h1>
                    <table>
                        <thead>
                            <tr>
                                <th>Параметр</th>
                                <th>Значення</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>Загальна кількість замовлень</td>
                                <td>{reportData.TotalOrders}</td>
                            </tr>
                            <tr>
                                <td>Загальний дохід</td>
                                <td>{reportData.TotalIncome:C}</td>
                            </tr>
                            <tr>
                                <td>Середня вартість замовлення</td>
                                <td>{reportData.AverageOrderValue:C}</td>
                            </tr>
                            <tr>
                                <td>Мінімальна вартість замовлення</td>
                                <td>{reportData.MinOrderValue:C}</td>
                            </tr>
                            <tr>
                                <td>Максимальна вартість замовлення</td>
                                <td>{reportData.MaxOrderValue:C}</td>
                            </tr>
                        </tbody>
                    </table>
                </body>
            </html>");

            using var memoryStream = new MemoryStream();

            HtmlConverter.ConvertToPdf(htmlContent.ToString(), memoryStream);

            return memoryStream.ToArray();
        }

        public async Task<byte[]> GenerateAccessabilityReport(Guid menuId, List<AccessabilityReport> reportData)
        {
            var menuNameVal = string.Empty;
            var menu = await _menuRepository.Retrieve(menuId);
            if(menu != null)
            {
                menuNameVal = menu.Name;
            }

            var htmlContent = new StringBuilder($@"
            <!DOCTYPE html>
            <html lang=""uk"">
            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                <title>Звіт доступності позицій меню</title>
                <style>
                    body {{
                        font-family: Arial, sans-serif;
                        margin: 20px;
                        line-height: 1.6;
                    }}
                    h1 {{
                        text-align: center;
                        color: #333;
                    }}
                    table {{
                        width: 100%;
                        border-collapse: collapse;
                        margin-top: 20px;
                    }}
                    th, td {{
                        border: 1px solid #ddd;
                        padding: 10px;
                        text-align: left;
                    }}
                    th {{
                        background-color: #007bff;
                        color: white;
                        font-weight: bold;
                    }}
                    tr:nth-child(even) {{
                        background-color: #f9f9f9;
                    }}
                    tr:hover {{
                        background-color: #f1f1f1;
                    }}
                </style>
            </head>
            <body>
                <h1>{menuNameVal}</h1>
        
                <table>
                    <thead>
                        <tr>
                            <th>№</th>
                            <th>Назва страви</th>
                            <th>Ціна</th>
                            <th>Доступність</th>
                        </tr>
                    </thead>
                    <tbody>");

                    for (int i = 0; i < reportData.Count; i++)
                    {
                        var item = reportData[i];
                        htmlContent.Append($@"
                    <tr>
                        <td>{i + 1}</td>
                        <td>{item.Name}</td>
                        <td>{item.Price:C}</td>
                        <td>{(item.IsAvaivable ? "Доступна" : "Недоступна")}</td>
                    </tr>");
                    }

                    htmlContent.Append(@"
                    </tbody>
                </table>
            </body>
            </html>");

            using var memoryStream = new MemoryStream();

            HtmlConverter.ConvertToPdf(htmlContent.ToString(), memoryStream);

            return memoryStream.ToArray();
        }

    }
}
