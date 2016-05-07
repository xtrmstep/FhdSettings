using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FhdSettings.Api.Models.Auth;
using FhdSettings.Data.Models;

namespace FhdSettings.Api.Types
{
    public class ViewModelMapper
    {
        static dynamic _instance = new Lazy<ViewModelMapper>(() => new ViewModelMapper());
        private static dynamic Instance { get { return _instance.Value; } }

        public static TToModel Map<TFromEntity, TToModel>(TFromEntity entity) where TToModel : new()
        {
            var model = new TToModel();
            Instance.MapFromTo(entity, model);
            return model;
        }

        private void MapFromTo(AuthToken from, AuthTokenViewModel to)
        {
            to.Token = from.Token;
            to.Expires = from.Expires;
        }
    }
}