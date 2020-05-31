using AutoMapper;
using Common.Exceptions;
using EventsHub.BLL.DTO;
using EventsHub.BLL.Interfaces;
using EventsHub.Common.Helpers;
using EventsHub.DAL.Entities.Concert;
using EventsHub.DAL.UnitOfWork;
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
            var concert = await unitOfWork.Repository<Concert>().Get(t => t.ConcertId == id) ??
                throw new NotFoundException(nameof(Concert)); ;

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Concert, ConcertDto>()).CreateMapper();
            var concertDto = mapper.Map<Concert, ConcertDto>(concert);

            return concertDto;
        }

        public async Task<IEnumerable<ConcertDto>> GetAllConcerts(FilterParams filterParams)
        {
            var concertsDto = await unitOfWork.ConcertRepository.GetAll(null, filterParams);

            if (!concertsDto.Any())
                throw new NotFoundException("Concerts");

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Concert, ConcertDto>()).CreateMapper();
            var concerts = mapper.Map<IEnumerable<Concert>, IEnumerable<ConcertDto>>(concertsDto);

            return concerts;
        }

        public async Task<int> GetConcertsCount()
        {
            return (await unitOfWork.Repository<Concert>().GetAll()).Count();
        }
    }
}
