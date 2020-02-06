using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StokOto
{
    public delegate void UrunSecildiHandle();
    public interface IUrunSecme
    {
        event UrunSecildiHandle UrunSecildi;
    }
}
