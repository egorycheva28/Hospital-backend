using Laboratory_2.Migrations;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Bcpg;
using System;

public class ConsultationService: IConsultationService
{
    private readonly Context _context;
    public ConsultationService(Context context)
    {
        _context = context;
    }
    public async Task<GetDictionaryDTO<InspectionPreviewDTO>> GetListInspectionConsultation(Guid userId, GetInspectionsDTO data)
    {
        var page = data.Page;
        var size = data.Size;

        var specialityId = _context.Doctors.FirstOrDefault(c => c.Id == userId).Speciality;

        var inspections = _context.Inspections.Include(i => i.Diagnoses).Include(i => i.Consultation).Where(i => i.Consultation.Any(c => c.Specialty.Id == specialityId)).AsQueryable();

        if (data.Grouped)
        {
            inspections = inspections.Where(i => i.PreviousInspectionId == null);
        }

        if (data.ICDRoots.Count != 0)
        {
            var icdRootCodes = new List<string>();
            foreach (var icd in data.ICDRoots)
            {
                var diagnosis = _context.ICD.FirstOrDefault(c => c.Id == Guid.Parse(icd));
                if (diagnosis == null)
                {
                    throw new Exception("Invalid argument for subsets");
                }

                var code = diagnosis.Code;
                var diagnoses = _context.Diagnosises.Where(d => d.Code.Contains(code));
                if (diagnoses == null)
                {
                    throw new Exception("Такого диагноза нет в базе");
                }

                if (diagnosis.ParentId != null)
                {
                    throw new Exception("Это не корневой диагноз");
                }

                icdRootCodes.Add(diagnosis.Code);
            }

            inspections = inspections.Where(i => i.Diagnoses.Any(d => d.Type == TypeDiagnosis.Main));
        }

        //пагинация
        var totalCount = await inspections.CountAsync();
        var count = (int)Math.Ceiling((double)totalCount / size); //всего страниц
        var list = await inspections.Skip((page - 1) * size).Take(size).ToListAsync();//список осмотров на данной странице
        if (totalCount > 0 && count < page)
        {
            throw new Exception("Invalid value for attribute page");
        }

        var pagination = new PaginationDTO
        {
            Size = size,
            Count = totalCount,
            Current = page
        };
        
        var result = new GetDictionaryDTO<InspectionPreviewDTO>
        {
            Dictionary  = inspections
                    .Select(inspection => new InspectionPreviewDTO
                    {
                        Id = inspection.Id,
                        CreateTime = inspection.CreateTime,
                        PreviousId = inspection.PreviousInspectionId,
                        Date = inspection.Date,
                        Conclusion = inspection.Conclusions,
                        DoctorId = inspection.Doctor.Id,
                        Doctor = _context.Doctors.First(d => d.Id == inspection.Doctor.Id).Name,
                        PatientId = inspection.Patient.Id,
                        Patient = _context.Patients.First(p => p.Id == inspection.Patient.Id).Name,
                        Diagnosis = new Diagnosis
                        {
                            Id = inspection.Diagnoses.First(d => d.Type == TypeDiagnosis.Main).Id,
                            CreateTime = inspection.Diagnoses.First(d => d.Type == TypeDiagnosis.Main).CreateTime,
                            Code = inspection.Diagnoses.First(d => d.Type == TypeDiagnosis.Main).Code,
                            Name = inspection.Diagnoses.First(d => d.Type == TypeDiagnosis.Main).Name,
                            Description = inspection.Diagnoses.First(d => d.Type == TypeDiagnosis.Main).Description,
                            Type = TypeDiagnosis.Main
                        },
                        HasChain = _context.Inspections.Any(i => i.BaseInspectionId == inspection.Id),
                        HasNested = _context.Inspections.Any(i => i.PreviousInspectionId == inspection.Id)
                    })
                    .ToList(),
            Pagination = pagination
        };

        return result;
    }
    public async Task<Consultation> GetConsultation(Guid id)
    {
        var consultation = _context.Consultations.Include(i => i.Comments).Include(i => i.Specialty).FirstOrDefault(c => c.Id == id);
        if (consultation == null)
        {
            throw new Exception("Not Found");
        }

        return consultation;
    }
    public async Task<Guid> AddComment(Guid userId, Guid id, AddCommentDTO comment)
    {
        var consultation = _context.Consultations.Include(i => i.Comments).FirstOrDefault(c => c.Id == id);
        if (consultation == null)
        {
            throw new Exception("Not Found");
        }

        var inspection = _context.Inspections.Include(i => i.Doctor).FirstOrDefault(c => c.Id == consultation.InspectionId);
        if (userId != inspection.Doctor.Id)
        {
            throw new Exception("User is not the author of the comment");
        }

        Comment newComment = new Comment()
        {
            Content = comment.Content,
            AuthorId = userId,
            Author =_context.Doctors.First(d => d.Id == userId).Name,
            ParentId = comment.ParentId,
            ConsultationId = consultation.Id
        };

        var parentsId = _context.Comments.FirstOrDefault(c => c.Id == newComment.ParentId);
        if (parentsId == null)
        {
            throw new Exception("Comment not found");
        }

        _context.Comments.Add(newComment);
        consultation.Comments.Add(newComment);
        await _context.SaveChangesAsync();

        return newComment.Id;
    }
    public async Task EditComment(Guid userId, Guid id, EditCommentDTO editComment)
    {
        var comment =  _context.Comments.FirstOrDefault(c => c.Id == id);
        if (comment == null)
        {
            throw new Exception("Not Found");
        }

        if (userId != comment.AuthorId)
        {
            throw new Exception("User is not the author of the comment");
        }

        comment.Content = editComment.Content;
        comment.ModifiedDate = DateTime.Now;

        await _context.SaveChangesAsync();
    }
}
