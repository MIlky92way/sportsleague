using CinemaStore.Entities.PosterImage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaStore.Blogic.Poster
{
    public interface IPoster
    {
        List<PosterImageEntity> this[int filmId] { get; }
        void Add(PosterImageEntity image);
       
        void Delete(PosterImageEntity image);
        void Delete(List<PosterImageEntity> images);

    }
}
