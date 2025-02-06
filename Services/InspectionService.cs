using Laboratory_2.Migrations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

public class InspectionService : IInspectionService
{
    private readonly Context _context;
    public InspectionService(Context context)
    {
        _context = context;
    }
    public async Task<CopyInspection> GetFullInspection(Guid id)
    {
        var inspection = _context.Inspections.FirstOrDefault(c => c.Id == id);
        if (inspection == null)
        {
            throw new Exception("Inspection Not Found");
        }

        /*var patient = inspection.Patient;

        if (patient == null)
        {
            throw new Exception("Patient Not Found");
        }

        var patientModel = new Patient
        {
            Id = patient.Id,
            CreateTime = patient.CreateTime,
            Name = patient.Name,
            Birthday = patient.Birthday,
            Gen = patient.Gen
        };

        var doctor = inspection.Doctor;
        if (doctor == null)
        {
            throw new Exception("Doctor Not Found");
        }

        var doctorModel = new Doctor
        {
            Id = doctor.Id,
            CreateTime = doctor.CreateTime,
            Name = doctor.Name,
            Birthday = doctor.Birthday,
            Gen = doctor.Gen,
            Email = doctor.Email,
            PhoneNumber = doctor.PhoneNumber,
            Speciality = doctor.Speciality
        };

        var diagnosisModel = new List<Diagnosis>();
        foreach (var diagnosis in inspection.Diagnoses)
        {
            var model = new Diagnosis
            {
                Id = diagnosis.Id,
                CreateTime = diagnosis.CreateTime,
                Code = diagnosis.Code,
                Name = diagnosis.Name,
                Description = diagnosis.Description,
                Type = diagnosis.Type
            };
            diagnosisModel.Add(model);
        }*/

        var consultationModel = new List<InspectionConsultationModel>();
        if (inspection.Consultation != null)
        {
            foreach (var consultation in inspection.Consultation)
            {
                var model = new InspectionConsultationModel
                {
                    Id = consultation.Id,
                    CreateTime = consultation.CreateTime,
                    InspectionId = consultation.InspectionId,
                    Speciality = new Speciality
                    {
                        Id = consultation.Specialty.Id,
                        CreateTime = consultation.Specialty.CreateTime,
                        Name = consultation.Specialty.Name,
                    },
                    RootComment = new InspectionCommentModel
                    {
                        Id = consultation.Comments.First(c => c.ParentId == Guid.Empty).Id,
                        CreateTime = consultation.Comments.First(c => c.ParentId == Guid.Empty).CreateTime,
                        ParentId = Guid.Empty,
                        Content = consultation.Comments.First(c => c.ParentId == Guid.Empty).Content,
                        Author = inspection.Doctor,
                        ModifyTime = consultation.Comments.First(c => c.ParentId == Guid.Empty).ModifiedDate
                    },
                    CommentsNumber = consultation.Comments.Count()
                };
                consultationModel.Add(model);
            }
        }

        var inspectionModel = new CopyInspection
        {
            Id = inspection.Id,
            CreateTime = inspection.CreateTime,
            Date = inspection.Date,
            Anamnesis = inspection.Anamnesis,
            Complaints = inspection.Complaints,
            Treatment = inspection.Treatment,
            Conclusions = inspection.Conclusions,
            NextVisitDate = inspection.NextVisitDate,
            DeathDate = inspection.DeathDate,
            BaseInspectionId = inspection.BaseInspectionId,
            PreviousInspectionId = inspection.PreviousInspectionId,
            Patient = inspection.Patient,
            Doctor = inspection.Doctor,
            Diagnoses = inspection.Diagnoses,
            Consultation = consultationModel
        };

        return inspectionModel;
    }
    public async Task EditInspection(Guid userId, Guid id, EditInspectionDTO editInspection)
    {
        var inspection = _context.Inspections.Include(i => i.Doctor).Include(i => i.Diagnoses).FirstOrDefault(c => c.Id == id);
        if (inspection == null)
        {
            throw new Exception("Not Found");
        }

        if (inspection.Doctor.Id != userId)
        {
            throw new Exception("User doesn't have editing rights (not the inspection author)");
        }


        foreach (var diagnoses in inspection.Diagnoses)
        {
            _context.Diagnosises.Remove(diagnoses);
        }
        

        var newDiagnoses = new List<Diagnosis>();

        if (editInspection.Diagnoses != null)
        {
            foreach (var diagnoses in editInspection.Diagnoses)
            {
                var newDiagnosis = new Diagnosis
                {
                    Id = diagnoses.DiagnosisId,
                    Description = diagnoses.Description,
                    Type = diagnoses.Type
                };

                newDiagnoses.Add(newDiagnosis);
                _context.Diagnosises.Add(newDiagnosis);
            }
        }

        inspection.Anamnesis = editInspection.Anamnesis;
        inspection.Complaints = editInspection.Complaints;
        inspection.Treatment = editInspection.Treatment;
        inspection.Conclusions = editInspection.Conclusion;
        inspection.NextVisitDate = editInspection.NextVisitDate;
        inspection.DeathDate = editInspection.DeathDate;
        inspection.Diagnoses = newDiagnoses;

        await _context.SaveChangesAsync();
    }
    public async Task<List<InspectionPreviewDTO>> GetInspectionChain(Guid id)
    {
        var rootInspection = _context.Inspections.Where(i => i.Id == id).Include(i => i.Diagnoses).FirstOrDefault(c => c.Id == id);
   
        if (rootInspection == null)
        {
            throw new Exception("Not Found");
        }

        if (rootInspection.BaseInspectionId != null)
        {
            throw new Exception("Это не рутовый осмотр");
        }

        var inspections = new List<InspectionPreviewDTO>();

        foreach (var inspection in _context.Inspections.Include(i => i.Patient).Include(i => i.Doctor).Include(i => i.Diagnoses))
        {
            if (inspection.PreviousInspectionId == rootInspection.Id)
            {
                foreach (var diagnoses in inspection.Diagnoses)
                {
                    if (diagnoses.Type == TypeDiagnosis.Main)
                    {
                        var inspectionPreviewDTO = new InspectionPreviewDTO
                        {
                            Id = inspection.Id,
                            CreateTime = inspection.CreateTime,
                            PreviousId = inspection.PreviousInspectionId,
                            Date = inspection.Date,
                            Conclusion = inspection.Conclusions,
                            DoctorId = inspection.Doctor.Id,
                            Doctor = inspection.Doctor.Name,
                            PatientId = inspection.Patient.Id,
                            Patient = inspection.Patient.Name,
                            Diagnosis = diagnoses,
                            HasChain = _context.Inspections.Any(i => i.BaseInspectionId == inspection.Id),
                            HasNested = _context.Inspections.Any(i => i.PreviousInspectionId == inspection.Id)
                        };
                        inspections.Add(inspectionPreviewDTO);
                    }
                }
                rootInspection = inspection;
            }
        }

        return inspections;
    }
}
