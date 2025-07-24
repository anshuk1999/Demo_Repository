using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DEMO.Dto.Dtoclear
{
    public static class DtoCleaner
    {
        public static void ClearFiles(AddVehicleDto dto)
        {
            dto.FileUploadPan = null;
            dto.FileUploadRc = null;
            dto.FileUploadAlt = null;
            //dto.Photo = null;
            //dto.Passbook = null;
        }
    }
}
