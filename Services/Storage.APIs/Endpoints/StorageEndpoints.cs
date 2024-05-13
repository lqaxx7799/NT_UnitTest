using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;

namespace Storage.APIs;

public static class StorageEndpoints
{
    public static void UseStorageEndpoints(this WebApplication web)
    {
        var group = web.MapGroup("storage");

        group.MapGet("stream/{fileName}", StreamFile);
        group.MapPost("upload", UploadFile).DisableAntiforgery();
    }

    internal static async Task<IResult> StreamFile(
        [FromRoute(Name = "fileName")] string fileName,
        [FromServices] BlobContainerClient containerClient,
        HttpContext http)
    {
        var blobClient = containerClient.GetBlobClient(fileName);
        var existed = await blobClient.ExistsAsync();
        if (!existed)
        {
            return Results.NotFound();
        }

        var readStream = await blobClient.OpenReadAsync();
        http.Response.Headers.Append("Content-Disposition", "inline; filename=" + fileName);
        var contentType = MimeMapping.MimeUtility.GetMimeMapping(blobClient.Name);
        return Results.File(readStream, contentType);
    }

    internal static async Task<IResult> UploadFile(
        [FromForm] IFormFile file,
        [FromServices] BlobContainerClient containerClient)
    {
        var fileName = file.FileName;
        var blobClient = containerClient.GetBlobClient(fileName);
        await blobClient.UploadAsync(file.OpenReadStream(), true);
        return Results.Ok("ok");
    }
}
