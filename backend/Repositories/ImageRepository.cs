using backend.Core;
using System.Collections.Generic;
using backend.Models;
using System.Linq;

namespace backend.Repositories
{
    public class ImageRepository
    {
        private BrokerContext _Ctx;

        public ImageRepository()
        {
            _Ctx = new BrokerContext();
        }

        public List<Image> GetImages()
        {
            return _Ctx.Image.ToList();
        }

        public ResponseWrapper<Image> GetImageById(int id)
        {
            var result = _Ctx.Image.Find(id);
            if (result == null) return new ResponseWrapper<Image>
            {
                state = System.Net.HttpStatusCode.NotFound,
                error = "no image found for the given id"
            };
            return new ResponseWrapper<Image>
            {
                state = System.Net.HttpStatusCode.OK,
                content = result
            };
        }

        public ResponseWrapper<Image> PostImage(Image image)
        {
            _Ctx.Image.Add(image);
            _Ctx.SaveChanges();
            return new ResponseWrapper<Image>
            {
                state = System.Net.HttpStatusCode.Created,
                content = image
            };
        }

        public ResponseWrapper<Image> PutImage(Image image)
        {
            var result = _Ctx.Image.Find(image.Id);
            if (result == null) return new ResponseWrapper<Image>
            {
                state = System.Net.HttpStatusCode.BadRequest,
                error = "no image found for the given id for update"
            };

            result.ImageData = image.ImageData;
            result.ImageDescription = image.ImageDescription;

            _Ctx.SaveChanges();

            return new ResponseWrapper<Image>
            {
                state = System.Net.HttpStatusCode.OK,
                content = result
            };
        }

        public ResponseWrapper<Image> DeleteImage(int id)
        {
            var result = _Ctx.Image.Find(id);
            if (result == null) return new ResponseWrapper<Image>
            {
                state = System.Net.HttpStatusCode.BadRequest,
                error = "no image with the given id found while deleting"
            };

            _Ctx.Image.Remove(result);
            _Ctx.SaveChanges();

            return new ResponseWrapper<Image>
            {
                state = System.Net.HttpStatusCode.OK,
                content = result
            };
        }

        public Image PostImageFromFile(byte[] file, string mediaType)
        {
            var image = new Image { ImageData = file, MediaType = mediaType };
            _Ctx.Image.Add(image);
            _Ctx.SaveChanges();
            return image;
        }
    }
}