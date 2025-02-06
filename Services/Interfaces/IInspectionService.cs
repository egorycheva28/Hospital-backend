using System;

public interface IInspectionService
{
    public Task<CopyInspection> GetFullInspection(Guid id);
    public Task EditInspection(Guid userId, Guid id, EditInspectionDTO editInspection);
    public Task<List<InspectionPreviewDTO>> GetInspectionChain (Guid id);

}
