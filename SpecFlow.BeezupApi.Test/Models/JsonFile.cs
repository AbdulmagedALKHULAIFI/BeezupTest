using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlow.BeezupApi.Test.Models
{
    public class JsonFile
    {
        public List<JsonRow> Rows { get; set; }

        public override bool Equals(Object obj)
        {
            if (obj is JsonFile)
            {
                var that = obj as JsonFile;

                for (int i = 0; i < Rows.Count; i++)
                {
                    if (!this.Rows[i].Equals(that.Rows[i]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
