using Libra.Dal.Entities;
using LibraBll.Abstractions.Repositories;
using LibraBll.Common;
using LibraBll.DTOs.Dropdown;
using LibraBll.DTOs.Pos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraBll.Repositories
{
    public class PosRepository : BaseRepository, IPosRepository
    {
        public async Task<List<PosGetDTO>> GetAllPosAsync()
        {
            List<PosGetDTO> posList = null;
            try
            {
                posList = await Context.Pos
                    .Include(p => p.City)
                    .Include(p => p.ConnectionType)
                    .Include(p => p.Issues)
                    .Select(p => new PosGetDTO
                    {
                        PosId = p.Id,
                        Name = p.Name,
                        Telephone = p.Telephone,
                        Cellphone = p.Cellphone,
                        FullAddress = string.Join(", ", p.City.CityName, p.Address),
                        City = p.City.CityName,
                        Address = p.Address,
                        Model = p.Model,
                        Brand = p.Brand,
                        Status = p.Issues.Count() > 0 ? p.Issues.Count().ToString() + " active issues" : "No active issues",
                        ConnectionType = p.ConnectionType.ConnectType,
                        MorningProgram = p.MorningOpening.ToString() + " - " + p.MorningClosing.ToString(),
                        AfternoonProgram = p.AfternoonOpening.ToString() + " - " + p.AfternoonClosing.ToString(),
                        InsertDate = p.InsertDate.ToString("dd/MM/yyyy")
                    }).ToListAsync();
            }
            catch (Exception ex)
            {
            }
            return posList;
        }

        public async Task<PosGetDTO> GetPosByIdAsync(int id)
        {
            Pos entity = await Context.Pos.Include(x => x.City).Include(x => x.ConnectionType).FirstOrDefaultAsync(x => x.Id == id);
            //string city = Context.Cities.Where(c => c.Id == entity.CityId).Select(c => c.CityName).FirstOrDefault();

            try
            {
                return new PosGetDTO()
                {
                    Name = entity.Name,
                    Telephone = entity.Telephone,
                    Cellphone = entity.Cellphone,
                    FullAddress = entity?.City.CityName + ", " + entity?.Address,
                    Address = entity?.Address,
                    City = entity?.City.CityName,
                    Model = entity?.Model,
                    Brand = entity?.Brand,
                    ConnectionType = entity?.ConnectionType.ConnectType,
                    MorningProgram = entity?.MorningOpening + " - " + entity?.MorningClosing,
                    AfternoonProgram = entity?.AfternoonOpening + " - " + entity?.AfternoonClosing,
                    InsertDate = entity?.InsertDate.ToString("dd/MM/yyyy")
                };
            }
            catch (Exception)
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

            await Context.Pos.AddAsync(entity);
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

            await Context.PosWeekDay.AddRangeAsync(posWeekDays);
            await Context.SaveChangesAsync();

            return pos;
        }

        public async Task UpdatePos(PosEditDTO pos)
        {
            var entity = await Context.Pos.Where(p => p.Id == pos.Id).FirstOrDefaultAsync();

            if (entity == null)
            {
                // Handle entity not found scenario
                // For example, throw an exception or return an error response
                throw new ArgumentException($"POS with ID {pos.Id} not found.");
            }

            string daysClosed = string.Join(",", pos.DaysClosed);
            int cityId = Context.Cities
                .Where(c => c.CityName == pos.City)
                .Select(c => c.Id)
                .FirstOrDefault();
            int connectionTypeId = Context.ConnectionType
                .Where(c => c.ConnectType == pos.ConnectionType)
                .Select(c => c.Id)
                .FirstOrDefault();

            entity.Name = pos.Name;
            entity.Telephone = pos.Telephone;
            entity.Cellphone = pos.Cellphone;
            entity.Address = pos.Address;
            entity.CityId = cityId;
            entity.Model = pos.Model;
            entity.Brand = pos.Brand;
            entity.ConnectionTypeId = connectionTypeId;
            entity.MorningOpening = pos.MorningOpening;
            entity.MorningClosing = pos.MorningClosing;
            entity.AfternoonOpening = pos.AfternoonOpening;
            entity.AfternoonClosing = pos.AfternoonClosing;
            entity.InsertDate = DateTime.Now;

            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public async void DeletePos(int id)
        {
            Pos entity = await Context.Pos.Where(p => p.Id == id).FirstOrDefaultAsync();

            if (entity != null)
                entity.IsDeleted = true;

            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync();
        }

        public List<CityDTO> GetCityList()
        {
            List<CityDTO> cityList = Context.Cities
                .Select(c => new CityDTO
                {
                    Id = c.Id,
                    CityName = c.CityName
                }).ToList();

            return cityList;
        }

        public List<ConnectionTypeDTO> GetConnectionTypeList()
        {
            List<ConnectionTypeDTO> connectionTypeList = Context.ConnectionType
                .Select(c => new ConnectionTypeDTO
                {
                    Id = c.Id,
                    ConnectionType = c.ConnectType
                }).ToList();

            return connectionTypeList;
        }
    }
}