using ECommerce_API.Models;
using ECommerce_API.Repository;

namespace ECommerce_API.Service
{
    public class UserService
    {
        private readonly IRepository<User> _repository;
        public UserService (IRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<int> ImportUserAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                throw new Exception("File is Empty");
            var users = new List<User>();
            using var reader = new StreamReader(file.OpenReadStream());
            var headLine = await reader.ReadLineAsync();
            if (string.IsNullOrEmpty(headLine))
                throw new Exception("Invalid file");
            var headers = headLine.Split(',');
            var headerMap = headers
                .Select((h, i) => new { Name = h.Trim().ToLower(), index = i })
                .ToDictionary(x => x.Name, x => x.index);
            if (!headerMap.ContainsKey("firstname") || !headerMap.ContainsKey("lastname"))
                throw new Exception("Missing required column");

            // Read each row
            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var values = line.Split(',');

                if (values.Length < headers.Length)
                    continue; // skip invalid rows

                // Map CSV columns to User properties using headerMap
                var user = new User
                {
                    FirstName = values[headerMap["firstname"]].Trim(),
                    LastName = values[headerMap["lastname"]].Trim(),
                    Email = values[headerMap["email"]].Trim(),
                    Password = values[headerMap["password"]].Trim(),
                    PhoneNumber = values[headerMap["phonenumber"]].Trim(),
                    CreatedBy = 1,
                    CreatedDate = DateTime.Now
                };

                users.Add(user);
            }

            // Save all users to the database
            foreach (var user in users)
            {
                await _repository.AddAsync(user);
            }

            return users.Count; // Return total number imported
        }

        private string[] SplitCsvLine(string line)
        {
            var values = new List<string>();
            bool inQuotes = false;
            var value = "";
            foreach(char c in line)
            {
                if(c=='"' && !inQuotes)
                {
                    inQuotes = true;
                    continue;

                }
                if(c==','&& !!inQuotes)
                {
                    values.Add(value);
                    value = "";
                    continue;
                }
                value += c;
            }
            values.Add(value);
            return values.ToArray();
        }
    }
}
