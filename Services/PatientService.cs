using Humanizer;
using Laboratory_2.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using NuGet.Common;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;
public class PatientService : IPatientService
{
    private readonly Context _context;
    public PatientService(Context context)
    {
        _context = context;
    }

    public async Task<Guid> CreateNewPatient(CreatePatientDTO patient)
    {
        var newPatient = new Patient
        {
            Name = patient.Name,
            Birthday = patient.Birthday,
            Gen = patient.Gen,
            Inspections=new List<Inspection>()
        };

        newPatient.Validate();
        await _context.Patients.AddAsync(newPatient);
        await _context.SaveChangesAsync();

        return newPatient.Id;
    }
    public async Task<PatientsListDTO> GetPatients(Guid userId, GetPatientsListDTO data)
    {
        var page = data.Page;
        var size = data.Size;

        var patients = _context.Patients.AsQueryable();

        //фильтрация по имени
        if (!string.IsNullOrEmpty(data.Name))
        {
            patients = patients.Where(p => p.Name.Contains(data.Name));
        }

        //фильтрация по заключениям
        if (data.Conclusions.Count != 0)
        {
            patients = patients.Where(p => p.Inspections.Any(i => data.Conclusions.Contains(i.Conclusions)));
        }

        //фильтрация по запланированным визитам
        if (data.ScheduledVisits == true)
        {
            patients = patients.Where(p => p.Inspections.Any(i => i.NextVisitDate != null && i.NextVisitDate > DateTime.UtcNow));

        }

        //фильтрация по моим пациентам 
        if (data.OnlyMine == true)
        {
            patients = patients.Where(p => p.Inspections.Any(i => i.Doctor.Id == userId));
        }

        //сортировка
        if (data.Sort == Sorting.NameAsc)
        {
            patients = patients.OrderBy(p => p.Name);
        }
        else if (data.Sort == Sorting.NameDesc)
        {
            patients = patients.OrderByDescending(p => p.Name);
        }
        else if (data.Sort == Sorting.CreateAsc)
        {
            patients = patients.OrderBy(p => p.CreateTime);
        }
        else if (data.Sort == Sorting.CreateDesc)
        {
            patients = patients.OrderByDescending(p => p.CreateTime);
        }
        else if (data.Sort == Sorting.InspectionAsc)
        {
            patients = patients.OrderBy(p => p.Inspections.Min(i => i.Date));
        }
        else if (data.Sort == Sorting.InspectionDesc)
        {
            patients = patients.OrderByDescending(p => p.Inspections.Max(i => i.Date));
        }
        else
        {
            patients = patients.OrderBy(p => p.Name);
        }

        //пагинация
        var totalCount = await patients.CountAsync();
        var count = (int)Math.Ceiling((double)totalCount / size); //всего страниц
        var list = await patients.Skip((page - 1) * size).Take(size).ToListAsync();//список пациентов на данной странице
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

        var result = new PatientsListDTO
        {
            Patients = list,
            Pagination = pagination
        };

        return result;
    }
    public async Task<Guid> CreateNewInspection(Guid idDoctor, Guid id, CreatInspectionDTO inspection)
    {
        var patient = _context.Patients.FirstOrDefault(c => c.Id == id);
        if (patient == null)
        {
            throw new Exception("Not Found patient");
        }

        var doctor = _context.Doctors.FirstOrDefault(c => c.Id == idDoctor);
        if (doctor == null)
        {
            throw new Exception("Not Found doctor");
        }

        var baseInspectionId = new Guid?();
        if (inspection.PreviousInspectionId == Guid.Empty)
        {
            baseInspectionId = null;
        }
        else
        {
            var previousInspection = _context.Inspections.FirstOrDefault(c => c.Id == inspection.PreviousInspectionId);
            if (previousInspection!=null && previousInspection.BaseInspectionId == null)
            {
                baseInspectionId = previousInspection.Id;
            }
            else
            {
                baseInspectionId = previousInspection.BaseInspectionId;
            }
        }

        var diagnosis = new List<Diagnosis>();

        foreach (var diagnos in inspection.Diagnoses)
        {
            var ICDid = _context.ICD.FirstOrDefault(c => c.Id == diagnos.DiagnosisId);
            if (ICDid == null)
            {
                throw new Exception("Такого диагноза нет в базе");
            }

            var newDiagnoses = new Diagnosis
            {
                Id = diagnos.DiagnosisId,
                Code = ICDid.Code,
                Name = ICDid.Name,
                Description = diagnos.Description,
                Type = diagnos.Type
            };
            diagnosis.Add(newDiagnoses);
        }

        var consultations=new List<Consultation>();
        var newInspectionId= Guid.NewGuid();
        foreach (var consultation in inspection.Consultation)
        {
            var speciality = _context.Specialties.FirstOrDefault(c => c.Id == consultation.SpecialityId);
            if (speciality == null)
            {
                throw new Exception("Такой специальности нет в базе");
            }

            var newConsultationId = Guid.NewGuid();

            var newComment = new Comment
            {
                Content = consultation.Comment.Content,
                AuthorId = doctor.Id,
                Author = doctor.Name,
                ConsultationId = newConsultationId
            };

            var newConsultation = new Consultation
            {
                Id = newConsultationId,
                InspectionId = newInspectionId,
                Specialty = speciality,
                Comments = new List<Comment>
                {
                    newComment
                }
            };
            consultations.Add(newConsultation);
        }

        var newInspection = new Inspection
        {
            Id = newInspectionId,
            Date = inspection.Date,
            Anamnesis = inspection.Anamnesis,
            Complaints = inspection.Complaints,
            NextVisitDate = inspection.NextVisitDate,
            DeathDate = inspection.DeathDate,
            Treatment = inspection.Treatment,
            Conclusions = inspection.Conclusion,
            PreviousInspectionId = inspection.PreviousInspectionId,
            BaseInspectionId = baseInspectionId,
            Patient = patient,
            Doctor = doctor,
            Diagnoses = diagnosis,
            Consultation = consultations
        };

        newInspection.Validate();
        await _context.Inspections.AddAsync(newInspection);
        patient.Inspections.Add(newInspection);
        await _context.SaveChangesAsync();

        return newInspection.Id;
    }
    public async Task<GetDictionaryDTO<InspectionPreviewDTO>> GetListInspections(Guid id, GetInspectionsDTO data)
    {
        var page = data.Page;
        var size = data.Size;

        var patient = _context.Patients.FirstOrDefault(c => c.Id == id);

        if (patient == null)
        {
            throw new Exception("Not Found");
        }

        var inspections = _context.Inspections.Include(i => i.Diagnoses).Where(i => i.Patient == patient).AsQueryable();

        if (data.Grouped)
        {
            inspections = inspections.Where(i => i.BaseInspectionId == null);
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
                var diagnoses = _context.Diagnosises.Where(d => d.Code == code);
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
            Dictionary = inspections
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
    public async Task<PatientCardDTO> GetPatientCard(Guid id)
    {
        var patient = _context.Patients.FirstOrDefault(c => c.Id == id);

        if (patient == null)
        {
            throw new Exception("Not Found");
        }

        var patientCard = new PatientCardDTO
        {
            Id = patient.Id,
            CreateTime = patient.CreateTime,
            Name = patient.Name,
            Birthday = patient.Birthday,
            Gen = patient.Gen,
        };

        return patientCard;
    }
    public async Task<List<InspectionShortDTO>> GetBaseInspections(Guid id, RequestDTO request)
    {
        var patient = _context.Patients.FirstOrDefault(c => c.Id == id);

        if (patient == null)
        {
            throw new Exception("Patient not found");
        }

        var answer = new List<InspectionShortDTO>();

        var inspections = _context.Inspections.Where(i => i.Patient == patient);
        foreach (var inspection in inspections)
        {
            if (inspection.BaseInspectionId == Guid.Empty && inspection.Diagnoses != null)
            {
                foreach (var diagnoses in inspection.Diagnoses)
                {
                    if ((diagnoses.Name.Contains(request.Request) || diagnoses.Code.Contains(request.Request)) && diagnoses.Type == TypeDiagnosis.Main)
                    {
                        var inspectionShortDTO = new InspectionShortDTO
                        {
                            Id = inspection.Id,
                            CreateTime = inspection.CreateTime,
                            Date = inspection.Date,
                            Diagnosis = diagnoses
                        };
                        answer.Add(inspectionShortDTO);
                    }
                }
            }
        }

        return answer;
    }
}

