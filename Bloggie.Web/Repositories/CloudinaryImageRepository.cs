
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.ComponentModel;

namespace Bloggie.Web.Repositories
{
    public class CloudinaryImageRepository : IImageRepository
    {
        private readonly IConfiguration configuration;
        private readonly Account account; 
        public CloudinaryImageRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            account = new Account(
                configuration.GetSection("Cloudinary")["CloudName"],
                configuration.GetSection("Cloudinary")["ApiKey"],
                configuration.GetSection("Cloudinary")["ApiSecret"]

                ); 
        }

        public async Task<string> UploadAsync(IFormFile file)
        {
            //create client.
            var client = new Cloudinary(account);

            //upload params as per the docs.
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName = file.FileName
            };

            //Upload the file.
            var uploadResult = await client.UploadAsync(uploadParams);

            if(uploadResult != null && uploadResult.StatusCode==System.Net.HttpStatusCode.OK) {

                //return the URI.
                return uploadResult.SecureUri.ToString(); 
            }

            return null; 
        }
    }
}
