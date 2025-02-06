using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class DictionaryService : IDictionaryService
{
    private readonly Context _context;
    public DictionaryService(Context context)
    {
        _context = context;
    }

    public async Task<GetDictionaryDTO<Speciality>> GetSpeciality(GetDictionaryListDTO data)
    {
        var size = data.Size;
        var page = data.Page;
        /*if (page == null)
        {
        }
        else
        {
            page = 1;
        }

        if (size == null)
        {
        }
        else
        {
            size = 5;
        }*/

        var specialties = _context.Specialties.Where(s => s.Name.Contains(data.Name));

        if (string.IsNullOrEmpty(data.Name))
        {
            specialties = _context.Specialties;
        }

        var totalCount = await specialties.CountAsync();//всего специальностей

        //делим общее количество специальностей на размер данной страницы и округляем в большую сторону
        var count = (int)Math.Ceiling((double)totalCount / size); //всего страниц

        if (totalCount > 0 && count < page)
        {
            throw new Exception("Invalid value for attribute page");
        }

        var listSpecialties = await specialties.Skip((page - 1) * size).Take(size).ToListAsync();//список специальностей на данной странице

        var pagination = new PaginationDTO
        {
            Size = size,
            Count = count,
            Current = page
        };

        var answer = new GetDictionaryDTO<Speciality>
        {
            Dictionary = listSpecialties,
            Pagination = pagination
        };

        return answer;
    }

    public async Task<GetDictionaryDTO<Icd10DTO>> GetDiagnosis(GetDictionaryListDTO data)
    {
        var size = data.Size;
        var page = data.Page;
        /*if (page == null)
       {
       }
       else
       {
           page = 1;
       }

       if (size == null)
       {
       }
       else
       {
           size = 5;
       }*/

        var records = _context.ICD.Where(s => s.Name.Contains(data.Name) || s.Code.Contains(data.Name));

        if (string.IsNullOrEmpty(data.Name))
        {
            records = _context.ICD;
        }

        var totalCount = await records.CountAsync();//всего диагнозов

        //делим общее количество диагнозов на размер данной страницы и округляем в большую сторону
        var count = (int)Math.Ceiling((double)totalCount / size); //всего страниц

        if (totalCount > 0 && count < page)
        {
            throw new Exception("Invalid value for attribute page");
        }

        var listICD = await records.Skip((page - 1) * size).Take(size).ToListAsync();//список диагнозов на данной странице

        var list = new List<Icd10DTO>();
        foreach (var item in listICD)
        {
            var icd = new Icd10DTO
            {
                Id = item.Id,
                CreateTime = DateTime.Now,
                Code = item.Code,
                Name = item.Name
            };
            list.Add(icd);
        }

        var pagination = new PaginationDTO
        {
            Size = size,
            Count = count,
            Current = page
        };

        var answer = new GetDictionaryDTO<Icd10DTO>
        {
            Dictionary = list,
            Pagination = pagination
        };

        return answer;
    }
    public async Task<List<Icd10DTO>> GetRootDiagnosis()
    {
        var list = new List<Icd10DTO>();

        foreach (var item in _context.ICD)
        {
            if (item.ParentId == null)
            {
                var icd = new Icd10DTO
                {
                    Id = item.Id,
                    CreateTime = item.CreateTime,
                    Code = item.Code,
                    Name = item.Name
                };
                list.Add(icd);
            }
        }

        return list;
    }
}
