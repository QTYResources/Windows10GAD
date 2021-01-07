using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace JsonDemo
{
    public class School
    {
        private const string idKey = "id";
        private const string schoolKey = "school";
        private const string nameKey = "name";

        public School()
        {
            Id = "";
            Name = "";
        }

        public School(JsonObject jsonObject)
        {
            JsonObject schoolObject = jsonObject.GetNamedObject(schoolKey, null);
            if (schoolObject != null)
            {
                Id = schoolObject.GetNamedString(idKey, "");
                Name = schoolObject.GetNamedString(nameKey, "");
            }
        }

        public JsonObject ToJsonObject()
        {
            JsonObject schoolObject = new JsonObject();
            schoolObject.SetNamedValue(idKey, JsonValue.CreateStringValue(Id));
            schoolObject.SetNamedValue(nameKey, JsonValue.CreateStringValue(Name));

            JsonObject jsonObject = new JsonObject();
            jsonObject.SetNamedValue(schoolKey, schoolObject);
            return jsonObject;
        }

        public string Id { get; set; }
        public string Name { get; set; }
    }
}
