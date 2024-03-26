using AutoMapper;
using OnlineCasino.DatabaseContext.Entities;
using OnlineCasino.Models;

namespace OnlineCasino.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GameCollectionsModel, GameCollections>()
                .ForMember(dest => dest.Games, opt => opt.Ignore()) // We will ignore this for now
                .ForMember(dest => dest.SubCollections, opt => opt.MapFrom(src => src.SubCollections));

            CreateMap<GameCollections, GameCollectionsModel>()
                .ForMember(dest => dest.GameIds, opt => opt.MapFrom(src => src.Games.Select(g => g.Id)))
                .ForMember(dest => dest.SubCollections, opt => opt.MapFrom(src => src.SubCollections));

            CreateMap<GameCollectionsModel, GameCollections>()
                .ForMember(dest => dest.Games, opt => opt.Ignore())
                .ForMember(dest => dest.SubCollections, opt => opt.MapFrom(src => src.SubCollections));

            CreateMap<GameCollections, GameCollectionsModel>()
                .ForMember(dest => dest.GameIds, opt => opt.MapFrom(src => src.Games.Select(g => g.Id)))
                .ForMember(dest => dest.SubCollections, opt => opt.MapFrom(src => src.SubCollections));


            CreateMap<Game, GameModel>()
                .ForMember(dest => dest.AvailableDevices, opt => opt.MapFrom(src => ParseIntoList(src.AvailableDevices)))
                .ForMember(dest => dest.CollectionIds, opt => opt.MapFrom(src => src.Collections.Select(c => c.Id)));

            CreateMap<GameModel, Game>();
        }


        private List<string> ParseIntoList(string data)
        {
            if (string.IsNullOrEmpty(data))
                return new List<string>();

            return data.Split(',').ToList();
        }
    }
}
