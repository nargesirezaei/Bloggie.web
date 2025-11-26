
using Azure.Core;
using Bloggie.web.Models.Domain;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.SqlServer.Server;
using Microsoft.VisualBasic;
using System.IO;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using static System.Net.WebRequestMethods;
//SDK-en inneholder ferdige klasser (som Account, Cloudinary, ImageUploadParams osv.)
//som gjør at du kan kommunisere direkte med Cloudinarys servere uten å skrive rå HTTP-kall.

namespace Bloggie.web.Repository
{
    public class CloudinaryImageRepository : IImageRepository
    {
        //gir tilgang til innholdet i appsetting.json for å hente ut cloudname, apikey, apisecret
        private readonly IConfiguration configuration;
        //klasse som representerer konto i cloudinary, med nøkkel blir man authentisert
        private readonly Account account;
        public CloudinaryImageRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            //
            account = new Account(
                configuration.GetSection("Cloudinary")["CloudName"],
                configuration.GetSection("Cloudinary")["ApiKey"],
                configuration.GetSection("Cloudinary")["ApiSecret"]);
        }

        //Metoden tar inn et bilde (IFormFile) fra for eksempel et skjema i frontend og returnerer en URL til det opplastede bildet
        public async Task<string> UploadAsync(IFormFile file)
        {
            // incorporate/register cloudinary sdk and api så we can call it to upload our img
            //Dette client-objektet vet nå hvilken konto det skal laste opp til
            var client = new Cloudinary(account);
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName,file.OpenReadStream()), 
            //inneholder filnavn + filstrøm – altså selve bildet.
                DisplayName =file.FileName //er valgfritt og kan brukes som en etikett i Cloudinary.

            };
            //uploadParams(filnavn, strøm, metadata) sendes til Cloudinary som en HTTP-forespørsel.

            //Cloudinary sjekker API - nøklene dine og lagrer bildet i skyen.

            //Du får et svar(JSON) tilbake med informasjon som PublicId, Format, og SecureUrl – adressen
            //du kan bruke for å vise bildet.
            var uploadResult = await client.UploadAsync(uploadParams);
            if(uploadResult != null && uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUri.ToString();
            }
            return null;
           

            }
            
        }
    }

