using DAOLibrary.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HOVStoryUtils
{
    public static class Utils
    {

        #region Public Members
        public static HOVStoryLog Logger { get => new HOVStoryLog(); }

        public static HOVStoryDatetime Datetime { get => new HOVStoryDatetime(); }
        #endregion

    }
}
