using System;
using AutoMapper;
using FhdSettings.Api.Models.Auth;
using SettingsService.Core.Data.Models;

namespace FhdSettings.Api.Types
{
    public class ViewModelMapper
    {
        private static readonly dynamic _instance = new Lazy<ViewModelMapper>(() => new ViewModelMapper());
        private readonly IMapper _mapper;

        public ViewModelMapper()
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<AuthToken, AuthTokenViewModel>(); });
            _mapper = config.CreateMapper();
        }

        private static dynamic Instance
        {
            get { return _instance.Value; }
        }

        public static TToModel Map<TFromEntity, TToModel>(TFromEntity entity) where TToModel : new()
        {
            var model = new TToModel();
            Instance.MapFromTo(entity, model);
            return model;
        }

        private void MapFromTo(AuthToken from, AuthTokenViewModel to)
        {
            _mapper.Map(from, to);
        }
    }
}