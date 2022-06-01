using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTest.Database
{
    public partial class Product
    {
        private string _image;

        public string Image
        {
            get
            {
                if(_image == null || _image == string.Empty)
                {
                    return @"\Resources\picture.png";
                }
                return _image;
            }
            set
            {
                _image = value;
            }
        }
        public string MaterialList
        {
            get
            {
                return String.Join(", ", ProductMaterials.Select(pm => pm.Material.Title));
            }
        }

        public decimal MaterialCost
        {
            get
            {
                return ProductMaterials.Sum(pm => pm.Material.Cost);
            }
        }
    }
}
