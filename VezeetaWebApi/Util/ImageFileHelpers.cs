using Core.Repository;

namespace VezeetaWebApi.Util
{
    public static class ImageFileHelpers
    {
        // Helper method to save profile image to wwwroot/images
        public static void SaveProfileImage(this IWebHostEnvironment _hostingEnvironment, IFormFile profileImage, string imagePath)
        {
            var uploadsDirectory = Path.Combine(_hostingEnvironment.WebRootPath, "images");

            if (!Directory.Exists(uploadsDirectory))
            {
                Directory.CreateDirectory(uploadsDirectory);
            }

            var filePath = Path.Combine(uploadsDirectory, imagePath);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                profileImage.CopyTo(fileStream);
            }
        }

        // Helper method to delete profile image in wwwroot/images
        public static void DeleteProfileImage(this IWebHostEnvironment _hostingEnvironment, string imagePath)
        {
            var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images\\" + imagePath.TrimStart('/'));

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        // Helper method to edit profile image in wwwroot/images
        public static void EditProfileImage(this IWebHostEnvironment _hostingEnvironment,
            string oldImagePath, string newImagePath, IFormFile newProfileImage)
        {
            // Delete the existing image file
            DeleteProfileImage(_hostingEnvironment, oldImagePath);

            // Save the new profile image
            SaveProfileImage(_hostingEnvironment, newProfileImage, newImagePath);
        }

        // Helper method to get profile image path
        public static async Task<string> GetProfileImagePathAsync(IUnitOfWork _unitOfWork, int id)
        {
            var doctor = await _unitOfWork.DoctorRepository.GetEntityByIdAsync(id, "User");

            if (doctor != null)
                return doctor.User.ProfileImage;

            return null;
        }

        // Helper method to generate profile image path
        public static string GenerateProfileImagePath(IFormFile profileImage)
        {
            return (profileImage != null) ? Guid.NewGuid() + profileImage.FileName : null;
        }
    }
}
