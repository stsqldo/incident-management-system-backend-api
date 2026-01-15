using Azure;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

namespace IncidentManagement.Infrastructure;

public class BlobStorageService : IBlobStorageService
{
    private readonly BlobContainerClient _container;

    public BlobStorageService(IConfiguration config)
    {
        var client = new BlobServiceClient(config["BlobConnection"]);
        _container = client.GetBlobContainerClient("incident-files");
        _container.CreateIfNotExists();
    }

    public async Task<string> UploadAsync(Stream stream, string fileName)
    {
        if (stream == null || stream.Length == 0)
            throw new ArgumentException("File stream is empty.", nameof(stream));

        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("File name is required.", nameof(fileName));

        try
        {
            var blobName = $"{Guid.NewGuid()}-{fileName}";
            var blobClient = _container.GetBlobClient(blobName);

            await blobClient.UploadAsync(stream, overwrite: true);

            return blobClient.Uri.ToString();
        }
        catch (RequestFailedException ex)
        {
            // Azure Blob specific failure (auth, quota, network, etc.)
            throw new InvalidOperationException(
                $"Azure Blob Storage upload failed. ErrorCode: {ex.ErrorCode}",
                ex);
        }
        catch (Exception ex)
        {
            // Any unexpected failure
            throw new InvalidOperationException(
                "Unexpected error occurred while uploading file to Blob Storage.",
                ex);
        }
    }
}