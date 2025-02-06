using System;

public interface IConsultationService
{
    public Task<GetDictionaryDTO<InspectionPreviewDTO>> GetListInspectionConsultation(Guid userId, GetInspectionsDTO data);
    public Task<Consultation> GetConsultation(Guid id);
    public Task<Guid> AddComment(Guid userId, Guid id, AddCommentDTO comment);
    public Task EditComment(Guid userId, Guid id, EditCommentDTO editComment);
}
