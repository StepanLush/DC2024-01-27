﻿using Publisher.Entity.Db;
using Publisher.Entity.DTO.RequestTO;
using Publisher.Entity.DTO.ResponseTO;
using Publisher.Service.Interface.Common;

namespace Publisher.Service.Interface
{
    public interface INewsService : ICrudService<News, NewsRequestTO, NewsResponseTO>
    {
        Task<NewsResponseTO> GetNewsByParam(IList<string> markerNames, IList<int> markerIds, string creatorLogin,
            string title, string content);
    }
}
