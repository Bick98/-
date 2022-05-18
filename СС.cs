using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CW_ThoughtsOutLoud
{
    public class CC : IEquatable<CC>// сравнение объектов
    {

        public string Category;
        public string Color;

        public CC(string ca, string co)
        {
            this.Category = ca;
            this.Color = co;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            CC objAsPart = obj as CC;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }

        public bool Equals(CC other)
        {
            if (other == null) return false;
            return (this.Category.Equals(other.Category) && this.Color.Equals(other.Color));
        }

        public override int GetHashCode()//сравнивать объекты классы
        {
            int Count = 0;
            for (int i = 0; i < this.Category.Length; i++) Count += (int)this.Category[i];
            for (int i = 0; i < this.Color.Length; i++) Count += (int)this.Color[i];
            return Count;
        }
    }
}
