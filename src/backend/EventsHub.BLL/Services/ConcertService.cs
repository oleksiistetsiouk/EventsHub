using AutoMapper;
using EventsHub.BLL.DTO;
using EventsHub.BLL.Interfaces;
using EventsHub.Common.Helpers;
using EventsHub.DAL.Entities.Concert;
using EventsHub.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsHub.BLL.Services
{
    public class ConcertService : IConcertService
    {
        private readonly UnitOfWork unitOfWork;

        public ConcertService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<ConcertDto> GetConcert(int id)
        {
            var concert = await unitOfWork.Repository<Concert>().Get(t => t.ConcertId == id);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Concert, ConcertDto>()).CreateMapper();
            var concertDto = mapper.Map<Concert, ConcertDto>(concert);

            return concertDto;
        }

        public async Task<IEnumerable<ConcertDto>> GetAllConcerts(FilterParams filterParams)
        {
            var playsDto = await unitOfWork.ConcertRepository.GetAll(null, filterParams);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Concert, ConcertDto>()).CreateMapper();
            var plays = mapper.Map<IEnumerable<Concert>, IEnumerable<ConcertDto>>(playsDto);

            return plays;
        }
    }
}
