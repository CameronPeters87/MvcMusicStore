using MvcMovieStore.DataAccessLayer;
using MvcMovieStore.Models;
using System.Collections.Generic;

namespace MvcMovieStore.Interfaces
{
    public interface IPayment
    {
        string ToUrlEncodedString(Dictionary<string, string> request);
        Dictionary<string, string> ToDictionary(string response);
        bool AddTransaction(Dictionary<string, string> request, string payRequestId);
        bool UpdateTransaction(Dictionary<string, string> request, string PayrequestId);
        Transaction GetTransaction(string payRequestId);
        string GetMd5Hash(Dictionary<string, string> data, string encryptionKey);
        bool VerifyMd5Hash(Dictionary<string, string> data, string encryptionKey, string hash);
        ApplicationUser GetAuthenticatedUser();
    }
}