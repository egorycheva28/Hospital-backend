using System;
using Microsoft.EntityFrameworkCore;
//using Newtonsoft.Json;
using System.Text.Json;
using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using NetTopologySuite.Index.HPRtree;
public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new Context(serviceProvider.GetRequiredService<DbContextOptions<Context>>()))
        {
            if (context.Specialties.Any())
            {
                return;
            }

            var speciality1 = new Speciality
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.UtcNow,
                Name = "Акушер-гинеколог"
            };
            context.Specialties.Add(speciality1);

            var speciality2 = new Speciality
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.UtcNow,
                Name = "Анастезиолог-реаниматолог"
            };
            context.Specialties.Add(speciality2);

            var speciality3 = new Speciality
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.UtcNow,
                Name = "Дерматовенеролог"
            };
            context.Specialties.Add(speciality3);

            var speciality4 = new Speciality
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.UtcNow,
                Name = "Инфекционист"
            };
            context.Specialties.Add(speciality4);

            var speciality5 = new Speciality
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.UtcNow,
                Name = "Кардиолог"
            };
            context.Specialties.Add(speciality5);

            var speciality6 = new Speciality
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.UtcNow,
                Name = "Невролог"
            };
            context.Specialties.Add(speciality6);

            var speciality7 = new Speciality
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.UtcNow,
                Name = "Онколог"
            };
            context.Specialties.Add(speciality7);

            var speciality8 = new Speciality
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.UtcNow,
                Name = "Отоларинголог"
            };
            context.Specialties.Add(speciality8);

            var speciality9 = new Speciality
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.UtcNow,
                Name = "Офтальмолог"
            };
            context.Specialties.Add(speciality9);

            var speciality10 = new Speciality
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.UtcNow,
                Name = "Психиатр"
            };
            context.Specialties.Add(speciality10);

            var speciality11 = new Speciality
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.UtcNow,
                Name = "Психолог"
            };
            context.Specialties.Add(speciality11);

            var speciality12 = new Speciality
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.UtcNow,
                Name = "Рентгенолог"
            };
            context.Specialties.Add(speciality12);

            var speciality13 = new Speciality
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.UtcNow,
                Name = "Стоматолог"
            };
            context.Specialties.Add(speciality13);

            var speciality14 = new Speciality
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.UtcNow,
                Name = "Терапевт"
            };
            context.Specialties.Add(speciality14);

            var speciality15 = new Speciality
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.UtcNow,
                Name = "УЗИ-специалист"
            };
            context.Specialties.Add(speciality15);

            var speciality16 = new Speciality
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.UtcNow,
                Name = "Уролог"
            };
            context.Specialties.Add(speciality16);

            var speciality17 = new Speciality
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.UtcNow,
                Name = "Хирург"
            };
            context.Specialties.Add(speciality17);

            var speciality18 = new Speciality
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.UtcNow,
                Name = "Эндокринолог"
            };
            context.Specialties.Add(speciality18);

            context.SaveChanges();
        }
    }

    public static void ICD(IServiceProvider serviceProvider,string path)
    {
        using (var context = new Context(serviceProvider.GetRequiredService<DbContextOptions<Context>>()))
        {
            if (context.ICD.Any())
            {
                return;
            }

            string json = File.ReadAllText(path);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var data = JsonSerializer.Deserialize<Record>(json, options);

            var idmap = new Dictionary<int, Guid>();
            var copyICD = new List<ICD10>();

            foreach (var icd in data.Records)
            {
                var icd10 = new ICD10
                {
                    Id = Guid.NewGuid(),
                    CreateTime = DateTime.UtcNow,
                    Code = icd.MKB_CODE,
                    Name = icd.MKB_NAME,
                };

                copyICD.Add(icd10);
                idmap[icd.ID] = icd10.Id;
            }
            
            foreach (var icd in copyICD)
            {
                var icd10 = data.Records.FirstOrDefault(c => c.MKB_CODE == icd.Code);

                if(icd10 != null)
                {
                    if(!string.IsNullOrEmpty(icd10.ID_PARENT) && int.TryParse(icd10.ID_PARENT, out int parentId) && idmap.ContainsKey(parentId))
                    {
                        icd.ParentId = idmap[parentId];
                    }
                    else
                    {
                        icd.ParentId = null;
                    }
                }
            }

            context.ICD.AddRange(copyICD);
            context.SaveChanges();
        }
    }
}

