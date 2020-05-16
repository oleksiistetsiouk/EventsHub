using AutoMapper;
using EventsHub.BLL.DTO;
using EventsHub.BLL.Interfaces;
using EventsHub.Common.Helpers;
using EventsHub.DAL.Entities.Theatre;
using EventsHub.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsHub.BLL.Services
{
    public class TheatreService : ITheatreService
    {
        private readonly UnitOfWork unitOfWork;

        public TheatreService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<TheatrePlayDto> GetTheatrePlay(int id)
        {
            var playDto = await unitOfWork.Repository<TheatrePlay>().Get(t => t.TheatrePlayId == id);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TheatrePlay, TheatrePlayDto>()).CreateMapper();
            var play = mapper.Map<TheatrePlay, TheatrePlayDto>(playDto);

            return play;
        }

        public async Task<IEnumerable<TheatrePlayDto>> GetAllTheatrePlays(FilterParams filterParams)
        {
            var playsDto = await unitOfWork.TheatreRepository.GetAll(null, filterParams);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<TheatrePlay, TheatrePlayDto>()).CreateMapper();
            var plays = mapper.Map<IEnumerable<TheatrePlay>, IEnumerable<TheatrePlayDto>>(playsDto);

            return plays;
        }

        public async Task<int> GetPlaysCount()
        {
            return (await unitOfWork.Repository<TheatrePlay>().GetAll()).Count();
        }
    }
}
