using Core.Repository;

namespace VezeetaWebApi.Util
{
    public static class ImageFileHelpers
    {
        // Helper method to save profile image to wwwroot/images
        public static void SaveProfileImage(this IWebHostEnvironment _hostingEnvironment, IFormFile profileImage)
        {
            var uploadsDirectory = Path.Combine(_hostingEnvironment.WebRootPath, "images");

            if (!Directory.Exists(uploadsDirectory))
            {
                Directory.CreateDirectory(uploadsDirectory);
            }

            var uniqueFileName = Guid.NewGuid() + profileImage.FileName;
            var filePath = Path.Combine(uploadsDirectory, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                profileImage.CopyTo(fileStream);
            }
        }

        // Helper method to delete profile image in wwwroot/images
        public static void DeleteProfileImage(this IWebHostEnvironment _hostingEnvironment, string imagePath)
        {
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, imagePath.TrimStart('/'));

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        // Helper method to edit profile image in wwwroot/images
        public static void EditProfileImage(this IWebHostEnvironment _hostingEnvironment, string existingImagePath, IFormFile newProfileImage)
        {
            // Delete the existing image file
            DeleteProfileImage(_hostingEnvironment, existingImagePath);

            // Save the new profile image
            SaveProfileImage(_hostingEnvironment, newProfileImage);
        }

        // Helper method to get profile image path
        public static async Task<string> GetProfileImagePathAsync(this IUnitOfWork _unitOfWork, int id)
        {
            return (await _unitOfWork.DoctorRepository.GetEntityByIdAsync(id, "User")).User.ProfileImage;
        }
    }
}
