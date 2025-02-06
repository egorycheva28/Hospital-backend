using System;

public interface IReportService
{
    public Task<IcdRootReportModel> GetReport(IcdRootsReportFiltersModel data);
}
