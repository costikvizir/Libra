using Libra.Dal.Entities;
using LibraBll.Abstractions.Repositories;
using LibraBll.Common;
using LibraBll.Common.DataTableModels;
using LibraBll.Common.Extensions;
using LibraBll.DTOs.Dropdown;
using LibraBll.DTOs.Pos;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraBll.Repositories
{
    public class PosRepository : BaseRepository, IPosRepository
    {
        //public PosRepository(LibraContext context) : base(context)
        //{
        //}

        public async Task<List<PosGetDTO>> GetAllPosAsync(DataTablesParameters parameters, CancellationToken cancellationToken)
        {
            List<PosGetDTO> mappedPosList = new List<PosGetDTO>();
            try
            {
                var posList = await Context.Pos
                 .Include(p => p.City)
                 .Include(p => p.ConnectionType)
                 .Include(p => p.PosWeekDays.Select(d => d.DayOfWeek))
                 .Select(p => new
                 {
                     p,
                     Issues = p.Issues.Where(i => !i.IsDeleted).ToList() 
                 })
                 .Where(x => !x.p.IsDeleted)
                 .ToListAsync();

                mappedPosList = posList
                    .Select(x => new PosGetDTO
                    {
                        PosId = x.p?.Id ?? 0, // Assuming default value of 0 if x.p is null
                        Name = x.p?.Name,
                        Telephone = x.p?.Telephone,
                        Cellphone = x.p?.Cellphone,
                        FullAddress = x.p?.City != null ? string.Join(", ", x.p.City.CityName, x.p.Address) : x.p?.Address,
                        City = x.p?.City?.CityName,
                        Address = x.p?.Address,
                        Model = x.p?.Model,
                        Brand = x.p?.Brand,
                        DaysClosed = x.p?.PosWeekDays?.Select(d => d.DayOfWeek.Day).ToList() ?? new List<string>(),
                        Status = x.Issues != null ? (x.Issues.Count > 1 ? x.Issues.Count + " Active issues" : x.Issues.Count == 1 ? x.Issues.Count + " Active issue" : "No active issues") : "No active issues",
                        ConnectionType = x.p?.ConnectionType?.ConnectType,
                        MorningProgram = x.p?.MorningOpening != null && x.p?.MorningClosing != null ? x.p.MorningOpening.ToString() + " - " + x.p.MorningClosing.ToString() : null,
                        AfternoonProgram = x.p?.AfternoonOpening != null && x.p?.AfternoonClosing != null ? x.p.AfternoonOpening.ToString() + " - " + x.p.AfternoonClosing.ToString() : null,
                        InsertDate = x.p.InsertDate.ToString("dd/MM/yyyy")
                    })
                    .AsQueryable()
                    .Search(parameters) 
                    .OrderBy(parameters) 
                    .Page(parameters)
                    .ToList();
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw;
            }

            return mappedPosList;
        }

        //public async Task<List<PosGetDTO>> GetAllPosAsync(DataTablesParameters parameters, CancellationToken cancellationToken)
        //{

        //    //.Select(p => new PosGetDTOtry
        //    List<Pos> rawPosList = null;
        //    List<PosGetDTO> mappedPosList = null;
        //    try
        //    {
        //        rawPosList = await Context.Pos
        //           .Include(p => p.City)
        //           .Include(p => p.ConnectionType)
        //           .Include(p => p.Issues.Select(i => i).ToList())
        //           .Include(p => p.PosWeekDays.Select(d => d.DayOfWeek))
        //           .ToListAsync();

        //        mappedPosList = rawPosList
        //            .Select(p => new PosGetDTO
        //            {
        //                PosId = p.Id,
        //                Name = p.Name,
        //                Telephone = p.Telephone,
        //                Cellphone = p.Cellphone,
        //                FullAddress = string.Join(", ", p.City.CityName, p.Address),
        //                City = p.City.CityName,
        //                Address = p.Address,
        //                Model = p.Model,
        //                Brand = p.Brand,
        //                DaysClosed = p.PosWeekDays.Select(d => d.DayOfWeek.Day).ToList(),
        //                Status = p.Issues.Count() > 1 ? p.Issues.Count().ToString() + " Active issues" : p.Issues.Count() == 1 ? p.Issues.Count().ToString() + " Active issue" : "No active issues",
        //                ConnectionType = p.ConnectionType.ConnectType,
        //                MorningProgram = p.MorningOpening.ToString() + " - " + p.MorningClosing.ToString(),
        //                AfternoonProgram = p.AfternoonOpening.ToString() + " - " + p.AfternoonClosing.ToString(),
        //                InsertDate = p.InsertDate.ToString("dd/MM/yyyy")
        //            })
        //            .AsQueryable()
        //            .Search(parameters)
        //            .OrderBy(parameters)
        //            .Page(parameters)
        //            .ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        await Console.Out.WriteLineAsync(ex.Message);
        //        throw;
        //    }

        //    return mappedPosList;
        //}

        public async Task<List<PosGetDTO>> GetAllPosAsync(DataTablesParameters parameters, string name, string brand, string fullAddress, CancellationToken cancellationToken)
        {
            //var rawPosList = await Context.Pos
            //    .Include(p => p.City)
            //    .Include(p => p.ConnectionType)
            //    .Include(p => p.Issues.Where(i => !i.IsDeleted))
            //    .Include(p => p.PosWeekDays.Select(d => d.DayOfWeek))
            //    .ToListAsync();


            List<PosGetDTO> mappedPosList = new List<PosGetDTO>();
            try
            {
                var posList = await Context.Pos
                 .Include(p => p.City)
                 .Include(p => p.ConnectionType)
                 .Include(p => p.PosWeekDays.Select(d => d.DayOfWeek))
                 .Select(p => new
                 {
                     p,
                     Issues = p.Issues.Where(i => !i.IsDeleted).ToList()
                 })
                 .Where(x => !x.p.IsDeleted)
                 .ToListAsync();

                if (!string.IsNullOrEmpty(name))
                {
                    posList = posList.Where(o => o.p.Name.Contains(name)).ToList();
                }
                if (!string.IsNullOrEmpty(brand))
                {
                    posList = posList.Where(o => o.p.Brand.Contains(brand)).ToList();
                }
                if (!string.IsNullOrEmpty(fullAddress))
                {
                    string lowerFullAddress = fullAddress.ToLower();
                    posList = posList.Where(o => (o.p.City.CityName + ", " + o.p.Address).ToLower().Contains(lowerFullAddress)).ToList();
                }

                mappedPosList = posList
                    .Select(x => new PosGetDTO
                    {
                        PosId = x.p?.Id ?? 0, // Assuming default value of 0 if x.p is null
                        Name = x.p?.Name,
                        Telephone = x.p?.Telephone,
                        Cellphone = x.p?.Cellphone,
                        FullAddress = x.p?.City != null ? string.Join(", ", x.p.City.CityName, x.p.Address) : x.p?.Address,
                        City = x.p?.City?.CityName,
                        Address = x.p?.Address,
                        Model = x.p?.Model,
                        Brand = x.p?.Brand,
                        DaysClosed = x.p?.PosWeekDays?.Select(d => d.DayOfWeek.Day).ToList() ?? new List<string>(),
                        Status = x.Issues != null ? (x.Issues.Count > 1 ? x.Issues.Count + " Active issues" : x.Issues.Count == 1 ? x.Issues.Count + " Active issue" : "No active issues") : "No active issues",
                        ConnectionType = x.p?.ConnectionType?.ConnectType,
                        MorningProgram = x.p?.MorningOpening != null && x.p?.MorningClosing != null ? x.p.MorningOpening.ToString() + " - " + x.p.MorningClosing.ToString() : null,
                        AfternoonProgram = x.p?.AfternoonOpening != null && x.p?.AfternoonClosing != null ? x.p.AfternoonOpening.ToString() + " - " + x.p.AfternoonClosing.ToString() : null,
                        InsertDate = x.p.InsertDate.ToString("dd/MM/yyyy")
                    })
                    .AsQueryable()
                    .Search(parameters)
                    .OrderBy(parameters)
                    .Page(parameters)
                    .ToList();
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.Message);
                throw;
            }

            return mappedPosList;
        }

        public async Task<PosGetDTO> GetPosByIdAsync(int id)
        {
            Pos entity = await Context.Pos
                .Include(x => x.City)
                .Include(x => x.ConnectionType)
                .Include(x => x.PosWeekDays.Select(d => d.DayOfWeek))
                .Include(x => x.Issues)
                .FirstOrDefaultAsync(x => x.Id == id);

            string status;

            if (entity.Issues.Count() > 1)
                status = entity.Issues.Count().ToString() + " Active issues";
            else if (entity.Issues.Count() == 1)
                status = entity.Issues.Count().ToString() + " Active issue";
            else
                status = "No active issues";

            try
            {
                PosGetDTO posGet = new PosGetDTO();

                posGet.PosId = entity.Id;
                posGet.Name = entity.Name;
                posGet.Telephone = entity.Telephone;
                posGet.Cellphone = entity.Cellphone;
                posGet.FullAddress = entity?.City.CityName + ", " + entity?.Address;
                posGet.Address = entity?.Address;
                posGet.City = entity?.City.CityName;
                posGet.Model = entity?.Model;
                posGet.Brand = entity?.Brand;
                posGet.Status = status;
                posGet.DaysClosed = entity?.PosWeekDays.Select(d => d.DayOfWeek.Day).ToList();
                posGet.ConnectionType = entity?.ConnectionType.ConnectType;
                posGet.MorningProgram = entity?.MorningOpening + " - " + entity?.MorningClosing;
                posGet.AfternoonProgram = entity?.AfternoonOpening + " - " + entity?.AfternoonClosing;
                posGet.InsertDate = entity?.InsertDate.ToString("dd/MM/yyyy");

                return posGet;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<PosPostDTO> AddPosAsync(PosPostDTO pos)
        {
            string daysClosed = string.Join(",", pos.DaysClosed);
            // int cityId = Context.Cities.Where(c => c.CityName == pos.City).Select(c => c.Id).FirstOrDefault();
            //int connectionTypeId = Context.ConnectionType.Where(c => c.ConnectType == pos.ConnectionType).Select(c => c.Id).FirstOrDefault();
            Pos entity = new Pos
            {
                Name = pos.Name,
                Telephone = pos.Telephone,
                Cellphone = pos.Cellphone,
                Address = pos.Address,
                CityId = pos.CityId,
                Model = pos.Model,
                Brand = pos.Brand,
                ConnectionTypeId = pos.ConnectionType,
                MorningOpening = pos.MorningOpening,
                MorningClosing = pos.MorningClosing,
                AfternoonOpening = pos.AfternoonOpening,
                AfternoonClosing = pos.AfternoonClosing,
                InsertDate = DateTime.Now
            };

            Context.Pos.Add(entity);
            await Context.SaveChangesAsync();

            List<PosWeekDay> posWeekDays = pos.DaysClosed
                .Select(d => new PosWeekDay
                {
                    PosId = entity.Id,
                    WeekDayId = Context.WeekDays
                        .Where(w => w.Day == d)
                        .Select(w => w.Id)
                        .FirstOrDefault()
                }).ToList();

            Context.PosWeekDay.AddRange(posWeekDays);
            await Context.SaveChangesAsync();

            return pos;
        }

        public async Task UpdatePos(PosEditDTO pos)
        {
            var entity = await Context.Pos.Where(p => p.Id == pos.Id).FirstOrDefaultAsync();

            if (entity == null)
            {
                throw new ArgumentException($"POS with ID {pos.Id} not found.");
            }

            List<PosWeekDay> posWeekDays = pos.DaysClosed
                .Select(d => new PosWeekDay
                {
                    PosId = entity.Id,
                    WeekDayId = Context.WeekDays
                        .Where(w => w.Day == d)
                        .Select(w => w.Id)
                        .FirstOrDefault()
                }).ToList();

            Context.PosWeekDay.RemoveRange(Context.PosWeekDay.Where(p => p.PosId == entity.Id));

            entity.Name = pos.Name;
            entity.Telephone = pos.Telephone;
            entity.Cellphone = pos.Cellphone;
            entity.Address = pos.Address;
            entity.CityId = pos.CityId;
            entity.Model = pos.Model;
            entity.Brand = pos.Brand;
            entity.ConnectionTypeId = pos.ConnectionType;
            entity.MorningOpening = pos.MorningOpening;
            entity.MorningClosing = pos.MorningClosing;
            entity.AfternoonOpening = pos.AfternoonOpening;
            entity.AfternoonClosing = pos.AfternoonClosing;
            //entity.InsertDate = DateTime.Now;

            Context.PosWeekDay.AddRange(posWeekDays);
            //Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        //public async void DeletePos(int id)
        //{
        //    Pos entity = await Context.Pos.Where(p => p.Id == id).FirstOrDefaultAsync();

        //    if (entity != null)
        //        entity.IsDeleted = true;

        //    //Context.Entry(entity).State = EntityState.Modified;
        //    await Context.SaveChangesAsync();
        //}

        public void DeletePos(int id)
        {
            Pos entity = Context.Pos.Where(p => p.Id == id).FirstOrDefault();
            List<Issue> issues = Context.Issues.Where(i => i.PosId == id && i.IsDeleted == false).ToList();

            foreach (Issue issue in issues)
                issue.IsDeleted = true;

            Context.SaveChanges();

            if (entity != null)
                entity.IsDeleted = true;

            Context.SaveChanges();
        }

        public async Task<IEnumerable<CityDTO>> GetCityList()
        {
            List<CityDTO> cityList = await Context.Cities
                .Select(c => new CityDTO
                {
                    Id = c.Id,
                    CityName = c.CityName
                }).ToListAsync();

            return cityList;
        }

        public async Task<List<ConnectionTypeDTO>> GetConnectionTypeList()
        {
            List<ConnectionTypeDTO> connectionTypeList = await Context.ConnectionType
                .Select(c => new ConnectionTypeDTO
                {
                    Id = c.Id,
                    ConnectionType = c.ConnectType
                }).ToListAsync();

            return connectionTypeList;
        }

        public async Task<List<PosWeekDayDTO>> GetPosClosingDays(int posId)
        {
            List<PosWeekDayDTO> posWeekDays = await Context.PosWeekDay
                .Include(p => p.DayOfWeek)
                .Where(p => p.PosId == posId)
                .Select(p => new PosWeekDayDTO
                {
                    PosId = p.PosId,
                    Day = p.DayOfWeek.Day
                }).ToListAsync();

            return posWeekDays;
        }
    }
}