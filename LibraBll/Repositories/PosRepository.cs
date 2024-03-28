using Libra.Dal.Entities;
using LibraBll.Abstractions.Repositories;
using LibraBll.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraBll.Repositories
{
    public class PosRepository : BaseRepository, IPosRepository
    {
        public async Task<List<PosDTO>> GetAllPosAsync()
        {
            List<PosDTO> PosList = null;
            try
            {
                PosList = await Context.Pos
                    .Include(p => p.City)
                    .Include(p => p.ConnectionType)
                    .Select(p => new PosDTO
                    {
                        Name = p.Name,
                        Telephone = p.Telephone,
                        Cellphone = p.Cellphone,
                        Address = p.Address,
                        City = p.City.CityName,
                        Model = p.Model,
                        Brand = p.Brand,
                        ConnectionType = p.ConnectionType.ConnectType,
                        MorningOpening = p.MorningOpening.ToString(),
                        MorningClosing = p.MorningClosing.ToString(),
                        AfternoonOpening = p.AfternoonOpening.ToString(),
                        AfternoonClosing = p.AfternoonClosing.ToString(),
                        InsertDate = p.InsertDate
                    }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return PosList;
        }

        public async Task<PosDTO> GetPosByIdAsync(int id)
        {
            Pos entity = await Context.Pos.FindAsync(id);
            string city = Context.Cities.Where(c => c.Id == entity.CityId).Select(c => c.CityName).FirstOrDefault();

            return new PosDTO()
            {
                Name = entity.Name,
                Telephone = entity.Telephone,
                Cellphone = entity.Cellphone,
                Address = entity.Address,
                City = city,
                Model = entity.Model,
                Brand = entity.Brand,
                ConnectionType = entity.ConnectionType.ConnectType,
                MorningOpening = entity.MorningOpening.ToString(),
                MorningClosing = entity.MorningClosing.ToString(),
                AfternoonOpening = entity.AfternoonOpening.ToString(),
                AfternoonClosing = entity.AfternoonClosing.ToString(), 
                InsertDate = entity.InsertDate
            };
        }

        public async Task<PosDTO> AddPosAsync(PosDTO pos)
        {
            string daysClosed = string.Join(",", pos.DaysClosed);
            int cityId = Context.Cities.Where(c => c.CityName == pos.City).Select(c => c.Id).FirstOrDefault();
            int connectionTypeId = Context.ConnectionType.Where(c => c.ConnectType == pos.ConnectionType).Select(c => c.Id).FirstOrDefault();
            Pos entity = new Pos
            {
                Name = pos.Name,
                Telephone = pos.Telephone,
                Cellphone = pos.Cellphone,
                Address = pos.Address,
                CityId = cityId,
                Model = pos.Model,
                Brand = pos.Brand,
                ConnectionTypeId = connectionTypeId,
                MorningOpening = Convert.ToInt32(pos.MorningOpening),
                MorningClosing = Convert.ToInt32(pos.MorningClosing),
                AfternoonOpening = Convert.ToInt32(pos.AfternoonOpening),
                AfternoonClosing = Convert.ToInt32(pos.AfternoonClosing),
                InsertDate = DateTime.Now
            };

            List<PosWeekDay> posWeekDays = pos.DaysClosed
                .Select(d => new PosWeekDay
                {
		           	PosId = entity.Id,
		           	WeekDayId = Context.WeekDays.Where(w => w.Day == d).Select(w => w.Id).FirstOrDefault()			
		        }).ToList();
			await Context.Pos.AddAsync(entity);
			await Context.PosWeekDay.AddRangeAsync(posWeekDays);

            await Context.SaveChangesAsync();

            return pos;
        }

        public async void UpdatePos(PosDTO pos)
        {
            string daysClosed = string.Join(",", pos.DaysClosed);
            int cityId = Context.Cities.Where(c => c.CityName == pos.City).Select(c => c.Id).FirstOrDefault();
            int connectionTypeId = Context.ConnectionType.Where(c => c.ConnectType == pos.ConnectionType).Select(c => c.Id).FirstOrDefault();
            Pos entity = new Pos
            {
                Name = pos.Name,
                Telephone = pos.Telephone,
                Cellphone = pos.Cellphone,
                Address = pos.Address,
                CityId = cityId,
                Model = pos.Model,
                Brand = pos.Brand,
                ConnectionTypeId = connectionTypeId,
                MorningOpening = Convert.ToInt32(pos.MorningOpening),
                MorningClosing = Convert.ToInt32(pos.MorningClosing),
                AfternoonOpening = Convert.ToInt32(pos.AfternoonOpening),
                AfternoonClosing = Convert.ToInt32(pos.AfternoonClosing),
                InsertDate = pos.InsertDate
            };

            await Context.Pos.AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public async void DeletePos(string name)
        {
            Pos entity = await Context.Pos.Where(p => p.Name == name).FirstOrDefaultAsync();

            Context.Pos.Remove(entity);
            await Context.SaveChangesAsync();
        }
    }
}