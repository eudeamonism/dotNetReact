using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Photos;
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces
{
    public interface IPhotoAccessor
    {
        Task<PhotoUploadResults> AddPhoto(IFormFile file);
        Task<string> DeletePhoto(string publicId);
    }
}

//This doesn't touch database, but it used to access Cloudinary