using Microsoft.AspNetCore.Identity;
using System;
using System.IO;

public class ApplicationUser : IdentityUser
{
    private static byte[] GetDefaultProfilePicture()
    {
        // Replace "default_profile_picture.jpg" with the path to your actual image file
        byte[] defaultPictureBytes = File.ReadAllBytes("wwwroot/default_profile_picture.jpg");
        return defaultPictureBytes;
    }
    public byte[] ProfilePicture { get; set; } = GetDefaultProfilePicture();
    public string Bio { get; set; } = "Default Bio";

}
