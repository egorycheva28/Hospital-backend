using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System;

public class ReportService: IReportService
{
    private readonly Context _context;
    public ReportService(Context context)
    {
        _context = context;
    }
    public async Task<IcdRootReportModel> GetReport(IcdRootsReportFiltersModel data)
    {
        if(data.Start>data.End)
        {
            throw new Exception("Inspection Not Found");
        }

        var icdRoots = new List<string>();

        if (data.IcdRoots.Count != 0)
        {
            foreach (var item in data.IcdRoots)
            {
                var diagnoses = _context.Diagnosises.FirstOrDefault(d => d.Id == Guid.Parse(item));
                if (diagnoses == null)
                {
                    throw new Exception("Not Found");
                }
                icdRoots.Add(diagnoses.Code);
            }
        }
        else
        {
            icdRoots = _context.ICD.Where(d => d.ParentId == null).OrderBy(d => d.Code).Select(d => d.Code).ToList();
        }

        

        var records = new List<IcdRootsReportRecordModel>
        {
         //   PatientName=,
           // PatientBirthdate=,
            //Gender=,
            //VisitsByRoot=
        };
        var summaryByRoot = new Dictionary<string, int>();

        var result = new IcdRootReportModel
        {
            Filters = new IcdRootsReportFiltersModel
            {
                Start = data.Start,
                End = data.End,
                IcdRoots = data.IcdRoots
            },
            Records = records,
            SummaryByRoot = summaryByRoot
        };

        return result;
    }
}
