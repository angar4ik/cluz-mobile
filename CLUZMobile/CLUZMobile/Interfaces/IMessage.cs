using System;
using System.Collections.Generic;
using System.Text;

namespace CLUZMobile.Interfaces
{
    public interface IMessage
    {
        void LongAlert(string message);
        void ShortAlert(string message);
        //how long in secongs
        void CustomAlert(string message, int howLong);
    }
}
