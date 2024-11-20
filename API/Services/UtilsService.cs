using dotnet_anime_list.API.Models;

namespace dotnet_anime_list.API.Services
{
    public class UtilsService
    {

        public async Task<string> SaveImg(IFormFile img, string path, CancellationToken ct)
        {
            if (img == null || img.Length == 0) throw new Exception("Image is required");

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(img.FileName).ToLower();
            if (!allowedExtensions.Contains(extension)) throw new Exception("Invalid image extension");

            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(path, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await img.CopyToAsync(stream, ct);
            }
            return fileName;
        }


        //Tenho que fazer a função pra remover uma imagem, para as funções de atualização.
        //também tenho que fazer a função para pegar uma imagem e retornar um FileContentResult?, para as funções de leitura.
    }
}
