using Microsoft.AspNetCore.Components.Forms;
using System.IO;

namespace BikeStore.UI.AppConstant
{
    public class ApplicationConstant
    {
        public const string Morning = "Good morning!\n Hope you have a great ride ahead! 🚴‍♂️";
        public const string Afternoon = "Good afternoon!\n Your bike is in great hands. Stay tuned for updates!";
        public const string Evening = "Good evening!\n We’re working on your bike to keep it in top shape!";
        public const string Night = "Good night!\n Rest well, and your bike will be ready for your next ride soon! 🌙";

     
    }


    public static class Extension {
        public static async Task<byte[]> ConvertToByteArrayAsync(this IBrowserFile file)
        {
            using MemoryStream ms = new MemoryStream();
            await file.OpenReadStream().CopyToAsync(ms);
            return ms.ToArray();
        }

    }

}
