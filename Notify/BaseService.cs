using Notify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationBackService
{
    public interface BaseService
    {
        void Start(localNote Note);
        void Stop();
    }
}
