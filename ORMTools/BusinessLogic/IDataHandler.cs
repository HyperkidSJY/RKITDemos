using ORMTools.Models;
using ORMTools.Models.ENUMS;

namespace ORMTools.BusinessLogic
{
    public interface IDataHandler<T> where T : class
    {
        EnmType Type { get; set; }

        void PreSave(T objDto);

        Response Validate();

        Response Save();
    }
}
