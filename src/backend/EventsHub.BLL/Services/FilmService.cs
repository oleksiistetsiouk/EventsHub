using AutoMapper;
using EventsHub.BLL.DTO;
using EventsHub.BLL.Interfaces;
using EventsHub.Common.Helpers;
using EventsHub.DAL.Entities.Film;
using EventsHub.DAL.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsHub.BLL.Services
{
    public class FilmService : IFilmService
    {
        private readonly UnitOfWork unitOfWork;

        public FilmService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<FilmDto> GetFilm(int id)
        {
            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Film, FilmDto>();
                cfg.CreateMap<Cinema, CinemaDto>();
                cfg.CreateMap<Session, SessionDto>();
            }).CreateMapper();

            var film = await unitOfWork.FilmRepository.Get(f => f.FilmId == id);
            var filmDto = mapper.Map<Film, FilmDto>(film);

            return filmDto;
        }

        public async Task<IEnumerable<FilmDto>> GetAllFilms(FilterParams filterParams)
        {
            var films = await unitOfWork.FilmRepository.GetAll(null, filterParams);

            var mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Film, FilmDto>();
                cfg.CreateMap<Cinema, CinemaDto>();
                cfg.CreateMap<Session, SessionDto>();
            }).CreateMapper();
            var filmsDto = mapper.Map<IEnumerable<Film>, IEnumerable<FilmDto>>(films);

            return filmsDto;
        }

        public async Task<int> GetFilmsCount()
        {
            return (await unitOfWork.Repository<Film>().GetAll()).Count();
        }
    }
}
