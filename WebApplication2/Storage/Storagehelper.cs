using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using WebApplication2.Models;

namespace WebApplication2.Storage
{
    public class StorageHelper
    {
        private CloudStorageAccount storageAccount;
        private CloudBlobClient blobClient;
        private CloudTableClient tableClient;
        private CloudQueueClient queueClient;

        public string ConnectionString
        {
            set
            {
                this.storageAccount = CloudStorageAccount.Parse(value);
                this.blobClient = storageAccount.CreateCloudBlobClient();
                this.tableClient = storageAccount.CreateCloudTableClient();
                this.queueClient = storageAccount.CreateCloudQueueClient();
            }
        }
        //Upload Image in the Azure 
        public async Task<string> UploadCustomerImageAsync(string containerName, string imagePath)
        {

            var container = blobClient.GetContainerReference(containerName);
            await container.CreateIfNotExistsAsync();
            var imageName = Path.GetFileName(imagePath);
            var blob = container.GetBlockBlobReference(imageName);
            await blob.DeleteIfExistsAsync();
            await blob.UploadFromFileAsync(imagePath);

            return blob.Uri.AbsoluteUri;
        }

        public async Task<Customer> InsertCustomerAsync(string tableName, Customer customer)
        {
            var table = tableClient.GetTableReference(tableName);
            await table.CreateIfNotExistsAsync();
            TableOperation insertOperation = TableOperation.Insert(customer);
            var result = await table.ExecuteAsync(insertOperation);
            return result.Result as Customer;

        }
    }
}
