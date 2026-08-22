// using Microsoft.EntityFrameworkCore;
// using WMS.Domain.Entities.Attachments;
// using WMS.Domain.Enums;
// using WMS.Persistence.Context;

// namespace WMS.Persistence.Seeders;

// public static class EntityAttachmentSeeder
// {
//     public static async Task SeedAsync(AppDbContext context)
//     {
//         if (await context.EntityAttachments.AnyAsync())
//             return;

//         var files = await context.Files.AsNoTracking().ToListAsync();
//         var contractors = await context.Contractors.AsNoTracking().ToListAsync();
//         var contracts = await context.Contract.AsNoTracking().ToListAsync();

//         // اگر داده پایه کافی نبود، نرم رد شو
//         if (files.Count == 0 || (contractors.Count == 0 && contracts.Count == 0))
//         {
//             Console.WriteLine("[EntityAttachmentSeeder] داده کافی برای seed پیوست وجود ندارد. Skip شد.");
//             return;
//         }

//         var attachments = new List<EntityAttachment>();
//         var fileIndex = 0;

//         Guid NextFileId()
//         {
//             var id = files[fileIndex % files.Count].Id;
//             fileIndex++;
//             return id;
//         }

//         // اتصال فایل به پیمانکارها
//         for (int i = 0; i < contractors.Count; i++)
//         {
//             attachments.Add(new EntityAttachment
//             {
//                 Id = Guid.NewGuid(),
//                 FileAssetId = NextFileId(),
//                 EntityId = contractors[i].Id,
//                 EntityType = EntityType.Contractor,
//                 Title = $"پیوست پیمانکار {i + 1}",

//                 IsActive = true,
//                 CreatedAt = DateTime.UtcNow,
//                 IsDeleted = false
//             });
//         }

//         // اتصال فایل به قراردادها
//         for (int i = 0; i < contracts.Count; i++)
//         {
//             attachments.Add(new EntityAttachment
//             {
//                 Id = Guid.NewGuid(),
//                 FileAssetId = NextFileId(),
//                 EntityId = contracts[i].Id,
//                 EntityType = EntityType.Contract,
//                 Title = $"پیوست قرارداد {i + 1}",
//                 IsActive = true,
//                 CreatedAt = DateTime.UtcNow,
//                 IsDeleted = false
//             });
//         }

//         if (attachments.Count == 0)
//             return;

//         await context.EntityAttachments.AddRangeAsync(attachments);
//         await context.SaveChangesAsync();

//         Console.WriteLine($"[EntityAttachmentSeeder] {attachments.Count} پیوست seed شد.");
//     }
// }