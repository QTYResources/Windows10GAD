using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace JsonDemo
{
    public class User
    {
        private const string idKey = "id";
        private const string nameKey = "name";
        private const string educationKey = "education";
        private const string ageKey = "age";
        private const string verifiedKey = "verified";

        public User()
        {
            Id = "";
            Name = "";
            Education = new ObservableCollection<School>();
        }

        public User(string jsonString)
            : this()
        {
            JsonObject jsonObject = JsonObject.Parse(jsonString);
            Id = jsonObject.GetNamedString(idKey, "");
            Name = jsonObject.GetNamedString(nameKey, "");
            Age = jsonObject.GetNamedNumber(ageKey, 0);
            Verified = jsonObject.GetNamedBoolean(verifiedKey, false);

            foreach (IJsonValue jsonValue in jsonObject.GetNamedArray(educationKey, new JsonArray()))
            {
                if (jsonValue.ValueType == JsonValueType.Object)
                {
                    Education.Add(new School(jsonValue.GetObject()));
                }
            }
        }

        public string Stringify()
        {
            JsonArray jsonArray = new JsonArray();
            foreach (School school in Education)
            {
                jsonArray.Add(school.ToJsonObject());
            }

            JsonObject jsonObject = new JsonObject();
            jsonObject[idKey] = JsonValue.CreateStringValue(Id);
            jsonObject[nameKey] = JsonValue.CreateStringValue(Name);
            jsonObject[educationKey] = jsonArray;
            jsonObject[ageKey] = JsonValue.CreateNumberValue(Age);
            jsonObject[verifiedKey] = JsonValue.CreateBooleanValue(Verified);

            return jsonObject.Stringify();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public ObservableCollection<School> Education { get; set; }
        public double Age { get; set; }
        public bool Verified { get; set; }
    }
}
