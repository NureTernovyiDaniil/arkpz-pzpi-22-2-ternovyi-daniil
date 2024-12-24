using ChefMate_backend.Repositories;

public class ReportService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMenuItemRepository _menuItemRepository;

    public ReportService(IOrderRepository orderRepository, IMenuItemRepository menuItemRepository)
    {
        _orderRepository = orderRepository;
        _menuItemRepository = menuItemRepository;
    }

    public async Task<WeeklyReport> GeneratePeriodReport(DateTime startDate, DateTime endDate, Guid organizationId)
    {
        var orders = await _orderRepository.RetrieveByPeriod(startDate, endDate, organizationId);
        
        if (!orders.Any())
        {
            return new WeeklyReport
            {
                StartDate = startDate,
                EndDate = endDate,
                TotalIncome = 0,
                AverageCheck = 0
            };
        }

        var totalIncome = orders.Sum(o => o.TotalAmount ?? 0);

        var averageCheck = totalIncome / orders.Count;

        var topMenuItems = orders
            .SelectMany(o => o.OrderItems)
            .GroupBy(oi => oi.MenuItem.Name)
            .Select(g => new TopMenuItem
            {
                Name = g.Key,
                TotalIncome = g.Sum(oi => oi.Price * oi.Quantity)
            })
            .OrderByDescending(item => item.TotalIncome)
            .Take(3)
            .ToList();

        var dailySales = orders
            .GroupBy(o => o.OrderDate.Date)
            .ToDictionary(
                g => g.Key.ToString("yyyy-MM-dd"),
                g => g.Sum(o => o.TotalAmount ?? 0)
            );

        return new WeeklyReport
        {
            StartDate = startDate,
            EndDate = endDate,
            TotalIncome = totalIncome,
            AverageCheck = averageCheck,
            TopMenuItems = topMenuItems,
            DailySales = dailySales
        };
    }

    public async Task<DailyReport> GetDailyReportData(DateTime reportDate, Guid organizationId)
    {
        var ordersForDate = await _orderRepository.RetrieveByDate(reportDate, organizationId);

        var totalOrders = ordersForDate.Count;

        var totalIncome = ordersForDate
            .Where(o => o.TotalAmount.HasValue)
            .Sum(o => o.TotalAmount.Value);

        var topMenuItems = ordersForDate
            .SelectMany(o => o.OrderItems)
            .GroupBy(oi => oi.MenuItem.Name)
            .Select(g => new TopMenuItem
            {
                Name = g.Key,
                TotalIncome = g.Sum(oi => oi.Price * oi.Quantity)
            })
            .OrderByDescending(d => d.TotalIncome)
            .Take(5)
            .ToList();

        return new DailyReport()
        {
            TotalOrders = totalOrders,
            TotalIncome = totalIncome,
            TopMenuItems = topMenuItems
        };

    }
    public async Task<FinancialReport> GetFinancialReport(DateTime reportDate, Guid organizationId)
    {
        var ordersForDate = await _orderRepository.RetrieveByDate(reportDate, organizationId);

        var totalOrders = ordersForDate.Count;

        var totalIncome = ordersForDate
            .Where(o => o.TotalAmount.HasValue)
            .Sum(o => o.TotalAmount.Value);

        var averageOrderValue = totalIncome / totalOrders;

        var minOrderValue = ordersForDate
            .Where(o => o.TotalAmount.HasValue)
            .Min(o => o.TotalAmount.Value);

        var maxOrderValue = ordersForDate
            .Where(o => o.TotalAmount.HasValue)
            .Max(o => o.TotalAmount.Value);

        return new FinancialReport
        {
            ReportDate = reportDate,
            TotalOrders = totalOrders,
            TotalIncome = totalIncome,
            AverageOrderValue = averageOrderValue,
            MinOrderValue = minOrderValue,
            MaxOrderValue = maxOrderValue,
        };
    }

    public async Task<List<AccessabilityReport>> GetAccessabilityReport(Guid menuId)
    {
        var menuItems = await _menuItemRepository.RetriveByMenuId(menuId);

        var reportData = menuItems.Select(x => new AccessabilityReport
        {
            IsAvaivable = x.IsAvailable,
            Name = x.Name,
            Price = x.Price
        }).ToList(); 

        return reportData;
    }

    public class WeeklyReport
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal AverageCheck { get; set; }
        public List<TopMenuItem> TopMenuItems { get; set; } = new();
        public Dictionary<string, decimal> DailySales { get; set; } = new();
    }

    public class DailyReport
    {
        public DateTime ReportDate { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalIncome { get; set; }
        public List<TopMenuItem> TopMenuItems { get; set; }
    }

    public class TopMenuItem
    {
        public string Name { get; set; }
        public decimal TotalIncome { get; set; }
    }

    public class FinancialReport
    {
        public DateTime ReportDate { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal AverageOrderValue { get; set; }
        public decimal MinOrderValue { get; set; }
        public decimal MaxOrderValue { get; set; }
    }

    public class AccessabilityReport
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsAvaivable { get; set; }
    }
}
