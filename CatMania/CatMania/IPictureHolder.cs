using System;

namespace CatMania
{
    public interface IPictureHolder
    {
        void AddPicture(PictureItem picture);

        void DeletePicture(Guid pictureId);

        void Save();
    }
}